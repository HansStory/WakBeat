using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIObjectAlbum : MonoBehaviour
{
    public UIElementAlbumSelect UIElementAlbumSelect { get; set; }

    [SerializeField] private Image albumTitle; 
    public Image AlbumTitle
    {
        get { return albumTitle; }
        set
        {
            albumTitle = value;
        }
    }

    [SerializeField] private Image albumCircle;
    public Image AlbumCircle
    {
        get { return albumCircle; }
        set
        {
            albumCircle = value;
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

    [SerializeField] private RectTransform myRectTransform;
    public RectTransform MyRectTransform
    {
        get { return myRectTransform; }
        set
        {
            myRectTransform = value;
        }
    }

    public AnimCurve CurveAlbumCircle;

    public Tween CircleTween;
    public Tween TitleTween;

    [SerializeField] private AnimCurve curveAlbumTitle;
    private float titleTweenDuration = 0.4f;
    public bool IsTitleMove = false;

    void Start()
    {
        Init();
    }

    void Init()
    {
        albumTitle.gameObject.SetActive(GlobalState.Instance.AlbumIndex == AlbumIndex);
    }

    private void OnEnable()
    {
        ShowMyTitle(1f,1f);
    }

    private Vector3 startTitleVector = new Vector3(-700f, 0f, 0f);
    public void ShowMyTitle(float duration, float delay)
    {
        //albumTitle.gameObject.SetActive(GlobalState.Instance.AlbumIndex == AlbumIndex);

        if (GlobalState.Instance.AlbumIndex == AlbumIndex)
        {                              
            if (!IsTitleMove)
            {
                ShowTitle(duration, delay);
                TitleTween.OnComplete(() => { CheckTitleTween(); });
            }         
        }
    }

    void ShowTitle(float duration, float delay)
    {
        albumTitle.rectTransform.localPosition = startTitleVector;
        TitleTween = albumTitle.rectTransform.DOLocalMove(Vector3.zero, duration)
        .SetEase(curveAlbumTitle.Curve).SetDelay(delay);

        IsTitleMove = true;
    }

    void CheckTitleTween()
    {
        IsTitleMove = false;
        //UIElementAlbumSelect.isAlbumMove = false;
    }

    public void OnClickAlbum()
    {
        GlobalState.Instance.IsTweening = true;

        if (!IsTitleMove)
        {
            HideTitle();
            //TitleTween.OnComplete(() => { AlbumToMusic(); });

            SoundManager.Instance.FadeOutMusicVolume(1f);
            SoundManager.Instance.PlaySoundFX((int)GlobalData.SFX.AlbumSelect);
        }
    }

    void HideTitle()
    {
        albumTitle.rectTransform.localPosition = Vector3.zero;
        albumTitle.rectTransform.DOLocalMove(startTitleVector, titleTweenDuration).SetEase(curveAlbumTitle.Curve).OnComplete(() => { AlbumToMusic(); }); ;

        IsTitleMove = true;
    }

    void AlbumToMusic()
    {
        //GlobalState.Instance.IsTweening = false;
        IsTitleMove = false;
        //UIElementAlbumSelect.isAlbumMove = false;
        UIElementAlbumSelect.ShowHideAlbumList(0f);
    }

    // ¾Ù¹ü Á¤º¸ ÆË¾÷ Ãâ·Â
    public void OnClickInfoButton()
    {
        UIManager.Instance.UIElementPopUp.SetPopUpAlbumInfo();
        SoundManager.Instance.PlaySoundFX((int)GlobalData.SFX.SettingIn);
    }

    public void OnClickAlbumCircle()
    {
        if (AlbumIndex == UIElementAlbumSelect.UpIndex)
        {
            UIElementAlbumSelect.InputUpFunction();
        }
        else if (AlbumIndex == UIElementAlbumSelect.DownIndex)
        {
            UIElementAlbumSelect.InputDownFunction();
        }
        else if (AlbumIndex == GlobalState.Instance.AlbumIndex)
        {
            OnClickAlbum();
        }
    }
}
