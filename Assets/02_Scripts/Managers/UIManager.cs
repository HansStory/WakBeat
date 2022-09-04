using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [Header("UI Manager")]
    [SerializeField]
    private Image[] m_boundaryArr;

    [Header("Anim rect(Pause)")]
    [SerializeField]
    private AnimRectParams m_pauseAnimRect;

    [SerializeField]
    private Image m_fadeImage;

    #region Common member
    private Stack<RectTransform> m_FullRectStack = new Stack<RectTransform>();
    private Stack<RectTransform> m_popupRectStack = new Stack<RectTransform>();
    private Stack<Image> m_boundaryImageStack = new Stack<Image>();

    private Canvas m_canvas;
    private GraphicRaycaster m_raycaster;
    #endregion

    public Canvas Canvas => m_canvas;
    public GraphicRaycaster Raycaster => m_raycaster;
    public Camera CanvasCam => m_canvas.worldCamera;

    //Game
    private GameProgress m_gameProgress;

    private UIMODE m_uiMode = UIMODE.MAIN;

    private AlbumSelectCtrl m_albumSelectCtrl;
    private MusicSelectCtrl m_musicSelectCtrl;
    private SettingCtrl m_settingCtrl;
    private ShopCtrl m_shopCtrl;


    protected override void Init()
    {
        //멤버 캐싱
        m_canvas = GetComponent<Canvas>();
        m_raycaster = GetComponent<GraphicRaycaster>();

        //컨트롤러 캐싱
        m_albumSelectCtrl = GetComponentInChildren<AlbumSelectCtrl>();
        m_musicSelectCtrl = GetComponentInChildren<MusicSelectCtrl>();
        m_settingCtrl = GetComponentInChildren<SettingCtrl>();
        m_shopCtrl = GetComponentInChildren<ShopCtrl>();

        m_gameProgress = GetComponentInChildren<GameProgress>();

        //메인 페이지 Push
        m_FullRectStack.Push(transform.GetChild(0).GetComponent<RectTransform>());
    }

    private void Start()
    {
        HidePages();

        m_settingCtrl.Init();
        m_shopCtrl.Init();
    }

    private void HidePages()
    {
        for(int i = 1; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    protected virtual void Update()
    {
        if(!m_raycaster.enabled)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Escape) && m_popupRectStack.Count > 0)
        {
            ClosePopupRect();
        }
        else
        {
            InputKeyExecute();
        }
    }

    private void InputKeyExecute()
    {
        switch (m_uiMode)
        {
            case UIMODE.MAIN:
                break;
            case UIMODE.SELECT_ALBUM:
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    CloseFullRect();
                    ChangeUIMode(UIMODE.MAIN);
                    SoundManager.Instance.TurnOffGameBackground();
                }
                else if (Input.GetKeyDown(KeyCode.Return))
                {
                    m_raycaster.enabled = false;
                    SoundManager.Instance.TurnOffGameBackground();

                    m_musicSelectCtrl.SetMusics(m_albumSelectCtrl.Album);
                    m_albumSelectCtrl.StartChangeWindow(() =>
                    {
                        OpenFullRect(m_musicSelectCtrl.Rect);
                        ChangeUIMode(UIMODE.SELECT_MUSIC);
                    });
                }
                else
                {
                    m_albumSelectCtrl.InputExecute();
                }
                break;
            case UIMODE.SELECT_MUSIC:
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    ReturnToAlbumSelect();
                }
                else if (Input.GetKeyDown(KeyCode.Return))
                {
                    ChangeUIMode(UIMODE.GAME);
                }
                else
                {
                    m_musicSelectCtrl.InputExecute();
                }
                break;
            case UIMODE.GAME:
                break;
        }
    }

    public void ChangeUIMode(UIMODE mode)
    {
        m_uiMode = mode;
    }

    private void CloseFullRect()
    {
        m_FullRectStack.Pop().gameObject.SetActive(false);
        m_FullRectStack.Peek().gameObject.SetActive(true);
    }

    private void OpenPopupRect(RectTransform rect)
    {
        Image boundary = GetBoundary();
        if (boundary != null)
        {
            m_boundaryImageStack.Push(boundary);
            m_popupRectStack.Push(rect);

            StartCoroutine(WindowAnim.OpenPopup(m_raycaster, m_boundaryImageStack.Peek().gameObject, m_popupRectStack.Peek(), this));
        }
    }

    private void OpenPopupRectWithAnim(AnimRectParams arp)
    {
        Image boundary = GetBoundary();
        if (boundary != null)
        {
            //Temp : 게임모드일때 Pause창 오픈시 게임 정지
            if (!CheckPauseOn())
            {
                Time.timeScale = 0;
            }

            m_boundaryImageStack.Push(boundary);
            m_popupRectStack.Push(arp.Arg2);

            StartCoroutine(WindowAnim.OpenPopup(m_raycaster, m_boundaryImageStack.Peek().gameObject, m_popupRectStack.Peek(), this, arp.Arg1.OpenPauseAnim()));
        }
    }

    private bool CheckPauseOn()
    {
        return m_uiMode == UIMODE.GAME && m_popupRectStack.Count == 0 && Time.timeScale == 0;
    }

    private Image GetBoundary()
    {
        for(int i = 0; i < m_boundaryArr.Length; i++)
        {
            if(!m_boundaryArr[i].gameObject.activeSelf)
            {
                return m_boundaryArr[i];
            }
        }

        return null;
    }

    private void ClosePopupRect()
    {
        StartCoroutine(WindowAnim.ClosePopup(m_raycaster, m_boundaryImageStack.Pop().gameObject, m_popupRectStack.Pop(),
            ()=>
            {
                if(CheckPauseOn())
                {
                    Time.timeScale = 1;
                }
            }));
    }

    //private bool CheckAnimEnd()
    //{
    //    for(int i = 0; i < m_changerArr.Length; i++)
    //    {
    //        if(m_changerArr[i].IsPlaying)
    //        {
    //            return false;
    //        }
    //    }

    //    return true;
    //}

    #region BUTTON EVENT
    public void BUTTON_OpenFullPage(RectTransform rect) => OpenFullRect(rect);
    public void BUTTON_CloseFullPage() => CloseFullRect();
    public void BUTTON_OpenPopupPage(RectTransform rect) => OpenPopupRect(rect);
    public void BUTTON_OpenPopupPageWithAnim(AnimRectParams arp) => OpenPopupRectWithAnim(arp);
    public void BUTTON_ClosePopupPage() => ClosePopupRect();
    public void BUTTON_EnterMain(RectTransform rect) => EnterMain(rect);
    public void BUTTON_ReturnToAlbumSelect() => ReturnToAlbumSelect();
    #endregion

    private void OpenFullRect(RectTransform rect)
    {
        m_FullRectStack.Peek().gameObject.SetActive(false);
        rect.gameObject.SetActive(true);
        m_FullRectStack.Push(rect);
    }

    public void EnterMain(RectTransform rect)
    {
        var mainRect = m_FullRectStack.Peek();
        m_FullRectStack.Push(rect);
        m_raycaster.enabled = false;
        StartCoroutine(FadeTransition(mainRect));
        ChangeUIMode(UIMODE.SELECT_ALBUM);
    }

    private void ReturnToAlbumSelect()
    {
        CloseFullRect();
        ChangeUIMode(UIMODE.SELECT_ALBUM);
        SoundManager.Instance.TurnOnGameBackGround();
    }

    IEnumerator FadeTransition(RectTransform rect)
    {
        float elapsed = 0f;
        m_fadeImage.gameObject.SetActive(true);

        while(true)
        {
            elapsed = MyUtil.CalcLerpTime(elapsed, 2f);
            m_fadeImage.color = Color.Lerp(Color.clear, Color.black, elapsed);

            if(elapsed >= 1f)
            {
                break;
            }

            yield return null;
        }

        rect.gameObject.SetActive(false);
        m_albumSelectCtrl.gameObject.SetActive(true);
        m_albumSelectCtrl.StartOpenAnim();
        SoundManager.Instance.TurnOnGameBackGround();

        while (true)
        {
            elapsed = MyUtil.CalcLerpTime(elapsed, -2f);
            m_fadeImage.color = Color.Lerp(Color.clear, Color.black, elapsed);

            if (elapsed <= 0f)
            {
                m_fadeImage.gameObject.SetActive(false);
                break;
            }

            yield return null;
        }
    }
}