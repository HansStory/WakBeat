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
    [SerializeField] private AnimCurve curveAlbumTitle;

    private Vector2 upPos = new Vector2(-333f, 415f);
    private Vector2 centerPos = new Vector2(12f, -16f);
    private Vector2 downPos = new Vector2(-333f, -430f);

    private Vector3 centerSize = new Vector3(1f, 1f, 1f);
    private Vector3 smallSize = new Vector3(0.6f, 0.6f, 0.6f);

    [SerializeField] private float _slideDuration = 2f;
    [SerializeField] private RectTransform myRectTransform;


    public Tween TitleTween;
    void Start()
    {
        Init();
    }

    private void OnEnable()
    {
        ShowMyTitle(1f);
    }

    void Update()
    {
        InputExecute();
        InputTest();
    }

    public void InputExecute()
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

    //void SetMyAlbumProcedure()
    //{
    //    albumTitle.gameObject.SetActive(GlobalState.Instance.AlbumIndex == AlbumIndex);
    //}

    private Vector3 startTitleVector = new Vector3(-680f, 0f, 0f);

    void ShowMyTitle(float delay)
    {
        albumTitle.gameObject.SetActive(GlobalState.Instance.AlbumIndex == AlbumIndex);
        TitleTween.Pause();

        if (GlobalState.Instance.AlbumIndex == AlbumIndex)
        {            
            albumTitle.rectTransform.localPosition = startTitleVector;
            TitleTween = albumTitle.rectTransform.DOLocalMove(Vector3.zero, 1f).SetEase(curveAlbumTitle.Curve);
            TitleTween.SetDelay(delay);
        }
    }

    int SFX_Move_02 = 3;
    void SelectAlbum()
    {
        if (GlobalState.Instance.AlbumIndex == AlbumIndex)
        {
            DOTween.PauseAll();
            albumTitle.rectTransform.localPosition = Vector3.zero;
            Tween TitleClose = albumTitle.rectTransform.DOLocalMove(startTitleVector, 1f).SetEase(curveAlbumTitle.Curve);
            TitleClose.OnComplete(() => { UIManager.Instance.GoPanelMusicSelect(); });

            SoundManager.Instance.PlaySoundFX(SFX_Move_02);
        }
    }

    void ExitAlbumSelect()
    {
        UIManager.Instance.GoPanelMain();
    }


    void Init()
    {
        albumTitle.gameObject.SetActive(GlobalState.Instance.AlbumIndex == AlbumIndex);
    }

    float _duration = 2f;
    public void InitMyIndexPos()
    {
        //for (int i = 0; i < length; i++)
        //{

        //}

        switch (AlbumIndex)
        {
            case (int)GlobalData.ALBUM.ISEDOL:
                myRectTransform.anchoredPosition = centerPos;
                myRectTransform.localScale = centerSize;
                albumCircle.rectTransform.DOScale(Vector3.one, _duration).SetEase(curveAlbumCircle.Curve);
                break;
            case (int)GlobalData.ALBUM.CONTEST:
                myRectTransform.anchoredPosition = downPos;
                myRectTransform.localScale = smallSize;
                albumCircle.rectTransform.DOScale(Vector3.one, _duration).SetEase(curveAlbumCircle.Curve).SetDelay(0.1f);
                break;
            case (int)GlobalData.ALBUM.GOMIX:
                myRectTransform.anchoredPosition = downPos + downPos;
                myRectTransform.localScale = smallSize;
                albumCircle.rectTransform.DOScale(Vector3.one, _duration).SetEase(curveAlbumCircle.Curve);
                break;
            case (int)GlobalData.ALBUM.WAKALOID:
                myRectTransform.anchoredPosition = upPos;
                myRectTransform.localScale = smallSize;
                albumCircle.rectTransform.DOScale(Vector3.one, _duration).SetEase(curveAlbumCircle.Curve).SetDelay(0.2f);
                break;
        }
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
