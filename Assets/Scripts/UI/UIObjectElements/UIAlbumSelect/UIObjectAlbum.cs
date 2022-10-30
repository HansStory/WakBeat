using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIObjectAlbum : MonoBehaviour
{
    public UIElementAlbumSelect UIElementAlbumSelect { get; set; }

    [SerializeField] private Image albumTitle; 
    private Sprite _albumTitle;
    public Sprite AlbumTitle
    {
        get { return _albumTitle; }
        set
        {
            _albumTitle = value;
            albumTitle.sprite = _albumTitle;
        }
    }

    [SerializeField] private Image albumCircle;
    private Sprite _albumCircle;
    public Sprite AlbumCircle
    {
        get { return _albumCircle; }
        set
        {
            _albumCircle = value;
            albumCircle.sprite = _albumCircle;
        }
    }

    private int _albumIndex = 0;
    public int AlbumIndex
    {
        get { return _albumIndex; }
        set
        {
            _albumIndex = value;
        }
    }

    [SerializeField] private AnimCurve curveAlbumCircle;
    [SerializeField] private float circleTweenDuration;

    [SerializeField] private AnimCurve curveAlbumTitle;
    [SerializeField] private float titleTweenDuration;

    private Vector2 upPos = new Vector2(-333f, 415f);
    private Vector2 centerPos = new Vector2(12f, -16f);
    private Vector2 downPos = new Vector2(-333f, -430f);

    private Vector3 centerSize = new Vector3(1f, 1f, 1f);
    private Vector3 smallSize = new Vector3(0.6f, 0.6f, 0.6f);

    private Vector3 startAlbumCircleSize = new Vector3(0.9f, 0.9f, 0.9f);

    private float _slideDuration = 2f;
    [SerializeField] private RectTransform myRectTransform;

    public Tween CircleTween;
    public Tween TitleTween;

    void Start()
    {
        Init();
    }

    private void OnEnable()
    {
        InitMyIndexPos();
        ShowMyTitle(1f);
    }

    void Update()
    {
        InputExecute();
        InputTest();
    }

    public void InputExecute()
    {
        if (GlobalState.Instance.UserData.data.BackgroundProcActive)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                ShowMyTitle(0f);
            }

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                ShowMyTitle(0f);
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                SelectAlbum();
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ExitAlbumSelect();
            }
        }
    }

    private Vector3 startTitleVector = new Vector3(-700f, 0f, 0f);

    void ShowMyTitle(float delay)
    {
        albumTitle.gameObject.SetActive(GlobalState.Instance.AlbumIndex == AlbumIndex);
        TitleTween.Pause();

        if (GlobalState.Instance.AlbumIndex == AlbumIndex)
        {            
            albumTitle.rectTransform.localPosition = startTitleVector;
            TitleTween = albumTitle.rectTransform.DOLocalMove(Vector3.zero, titleTweenDuration).SetEase(curveAlbumTitle.Curve);
            TitleTween.SetDelay(delay);
        }
    }

    int SFX_Move_02 = 3;
    public void SelectAlbum()
    {
        TitleTween.Pause();

        if (GlobalState.Instance.AlbumIndex == AlbumIndex)
        {
            albumTitle.rectTransform.localPosition = Vector3.zero;
            TitleTween = albumTitle.rectTransform.DOLocalMove(startTitleVector, titleTweenDuration).SetEase(curveAlbumTitle.Curve);
            TitleTween.OnComplete(() => { UIElementAlbumSelect.ShowHideAlbumList(0f); });

            SoundManager.Instance.PlaySoundFX(SFX_Move_02);
        }
    }


    public void ExitAlbumSelect()
    {
        UIManager.Instance.GoPanelMain();
    }

    void Init()
    {
        albumTitle.gameObject.SetActive(GlobalState.Instance.AlbumIndex == AlbumIndex);
    }

    public void InitMyIndexPos()
    {

        switch (AlbumIndex)
        {
            case (int)GlobalData.ALBUM.ISEDOL:
                InitMyCircle(centerPos, centerSize, 0.5f);
                break;
            case (int)GlobalData.ALBUM.CONTEST:
                InitMyCircle(downPos, smallSize, 0.6f);
                break;
            case (int)GlobalData.ALBUM.GOMIX:
                InitMyCircle(downPos + downPos, smallSize, 0.5f);
                break;
            case (int)GlobalData.ALBUM.WAKALOID:
                InitMyCircle(upPos, smallSize, 0.7f);
                break;
        }
    }

    void InitMyCircle(Vector3 initPos, Vector3 initSize, float delay)
    {
        myRectTransform.anchoredPosition = initPos;
        myRectTransform.localScale = initSize;
        AlbumCircleTween(delay);
    }

    void AlbumCircleTween(float delay)
    {
        CircleTween.Pause();

        albumCircle.rectTransform.localScale = startAlbumCircleSize;
        CircleTween = albumCircle.rectTransform.DOScale(Vector3.one, circleTweenDuration).SetEase(curveAlbumCircle.Curve).SetDelay(delay);
    }

    public void OnClickInfoButton()
    {
        UIElementAlbumSelect.ShowAlbumInfo();
        Debug.Log($"Click Info Button. My Album Index = {_albumIndex}");
    }


    void InputTest()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            myRectTransform.DOAnchorPos(upPos, _slideDuration, false);
            myRectTransform.DOScale(smallSize, _slideDuration);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            myRectTransform.DOAnchorPos(centerPos, _slideDuration, false);
            myRectTransform.DOScale(centerSize, _slideDuration);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            myRectTransform.DOAnchorPos(downPos, _slideDuration, false);
            myRectTransform.DOScale(smallSize, _slideDuration);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {

        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {

        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {

        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {

        }
    }
}
