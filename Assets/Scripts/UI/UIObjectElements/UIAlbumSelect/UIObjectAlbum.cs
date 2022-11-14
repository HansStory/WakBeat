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
    private bool isTitleMove = false;

    void Start()
    {
        Init();
    }

    private void OnEnable()
    {
        ShowMyTitle(1f,1f);
    }

    void Update()
    {
        InputExecute();
    }

    public void InputExecute()
    {
        if (GlobalState.Instance.UserData.data.BackgroundProcActive)
        {
            if (Input.GetKey(KeyCode.DownArrow))
            {
                ShowMyTitle(titleTweenDuration, 0.1f);
            }

            if (Input.GetKey(KeyCode.UpArrow))
            {
                ShowMyTitle(titleTweenDuration, 0.1f);
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
    void ShowMyTitle(float duration, float delay)
    {
        albumTitle.gameObject.SetActive(GlobalState.Instance.AlbumIndex == AlbumIndex);

        if (GlobalState.Instance.AlbumIndex == AlbumIndex)
        {                              
            if (!isTitleMove)
            {
                albumTitle.rectTransform.localPosition = startTitleVector;
                TitleTween = albumTitle.rectTransform.DOLocalMove(Vector3.zero, duration).SetEase(curveAlbumTitle.Curve);
                TitleTween.SetDelay(delay);
                TitleTween.OnComplete(() => { CheckEndTween(); });
                isTitleMove = true;
            }         
        }
    }

    void CheckEndTween()
    {
        isTitleMove = false;
        UIElementAlbumSelect.isAlbumMove = false;
    }

    public void SelectAlbum()
    {
        if (GlobalState.Instance.AlbumIndex == AlbumIndex)
        {
            if (!isTitleMove)
            {
                albumTitle.rectTransform.localPosition = Vector3.zero;
                TitleTween = albumTitle.rectTransform.DOLocalMove(startTitleVector, titleTweenDuration).SetEase(curveAlbumTitle.Curve);
                TitleTween.OnComplete(() => { AlbumToMusic(); });

                isTitleMove = true;

                SoundManager.Instance.FadeOutMusicVolume(1f);
                SoundManager.Instance.PlaySoundFX((int)GlobalData.SFX.AlbumSelect);
            }

        }
    }

    void AlbumToMusic()
    {
        isTitleMove = false;
        UIElementAlbumSelect.isAlbumMove = false;
        UIElementAlbumSelect.ShowHideAlbumList(0f);
    }

    public void ExitAlbumSelect()
    {
        UIManager.Instance.GoPanelMain();
    }

    void Init()
    {
        albumTitle.gameObject.SetActive(GlobalState.Instance.AlbumIndex == AlbumIndex);
    }

    // ¾Ù¹ü Á¤º¸ ÆË¾÷ Ãâ·Â
    public void OnClickInfoButton()
    {
        UIElementAlbumSelect.ShowAlbumInfo();
    }
}
