using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIElementFadePanel : MonoBehaviourSingleton<UIElementFadePanel>
{
    [Header("For Fade In Out Effect")]
    // For Fade White Transition
    [SerializeField] private Image imageWhite;
    private Color whiteAlpha0 = new Vector4(1f, 1f, 1f, 0f);

    // For Fade Black Transition
    [SerializeField] private Image imageBlack;

    [Space(10)]
    [Header("For Transition Effect")]
    // For Main To Album Transition 
    [SerializeField] private GameObject transitionPanel;

    public Sequence TransitionSequence;
    public Tween TransitionTween = null;

    [SerializeField] private GameObject fadeClear;
    [SerializeField] private AnimCurve curveClear;
    private Vector3 fadeClearOriginScale;

    [SerializeField] private Image fadeRed;
    [SerializeField] private AnimCurve curveRed;

    [SerializeField] private Image fadeYellow;
    [SerializeField] private AnimCurve curveYellow;

    [SerializeField] private Image fadeOrange;
    [SerializeField] private AnimCurve curveOrange;

    [SerializeField] private Image fadeIvory;
    [SerializeField] private AnimCurve curveIvory;


    [SerializeField] private Image fadeBackGround;
    public float TransitionTime = 2f;

    void Start()
    {
        Init();
    }

    private void Init()
    {
        if (fadeClear != null)
        {
            fadeClearOriginScale = fadeClear.transform.localScale;
        }
    }

    #region Fade Basic
    public void DeepToWhite(float duration)
    {
        imageWhite.gameObject.SetActive(true);
        imageWhite.color = whiteAlpha0;
        Tween tween = imageWhite.DOColor(Color.white, duration);

        tween.OnComplete(() => { imageWhite.gameObject.SetActive(false); });
    }

    public void WhiteToClear(float duration)
    {
        imageWhite.gameObject.SetActive(true);
        imageWhite.color = Color.white;
        Tween tween = imageWhite.DOColor(whiteAlpha0, duration);

        tween.OnComplete(() => { imageWhite.gameObject.SetActive(false); });
    }

    public void WhiteFadeInOut()
    {
        imageWhite.gameObject.SetActive(true);
    }

    public void DeepToBlack(float duration)
    {
        imageBlack.gameObject.SetActive(true);
        imageBlack.color = Color.clear;
        Tween tween = imageBlack.DOColor(Color.black, duration);

        tween.OnComplete(() => { imageBlack.gameObject.SetActive(false); });
    }

    public void BlackToClear(float duration)
    {
        imageBlack.gameObject.SetActive(true);
        imageBlack.color = Color.black;
        Tween tween = imageBlack.DOColor(Color.clear, duration);

        tween.OnComplete(() => { imageBlack.gameObject.SetActive(false); });
    }
    #endregion

    #region Transition Intro To Main
    public void IntroToMain(float blackToClear, float toWhiteTime, float holdTime, float toClearTime)
    {
        imageWhite.gameObject.SetActive(true);

        // Check Tweening
        GlobalState.Instance.IsTweening = true;

        TransitionSequence = DOTween.Sequence().SetAutoKill(false).OnStart(() =>
        {
            imageWhite.color = Color.black;
            Tween blackToClearTween = imageWhite.DOColor(Color.clear, blackToClear);
            blackToClearTween.OnComplete(() => { imageWhite.color = whiteAlpha0; });

            imageWhite.DOColor(Color.white, toWhiteTime).SetDelay(blackToClear + holdTime);

            TransitionTween = imageWhite.DOColor(Color.clear, toClearTime).SetDelay(blackToClear + holdTime + toWhiteTime).SetEase(Ease.InExpo);
            TransitionTween.OnComplete(() => { OnCompleteIntroToMain(); });
        });
    }

    private void OnCompleteIntroToMain()
    {
        GlobalState.Instance.IsTweening = false;
        imageWhite.gameObject.SetActive(false);
    }

    #endregion

    #region Transition Between Main To Album
    public void MainToAlbum()
    {
        InitMainToAlbum();

        TransitionSequence = DOTween.Sequence().SetAutoKill(false).OnStart(() =>
        {
            fadeClear.transform.DOScale(Vector3.zero, TransitionTime).SetEase(curveClear.Curve);
            fadeRed.transform.DOScale(Vector3.zero, TransitionTime).SetEase(curveRed.Curve);
            fadeYellow.transform.DOScale(Vector3.zero, TransitionTime).SetEase(curveYellow.Curve);
            fadeOrange.transform.DOScale(Vector3.zero, TransitionTime).SetEase(curveOrange.Curve);

            TransitionTween = fadeIvory.transform.DOScale(Vector3.zero, TransitionTime).SetEase(curveIvory.Curve).SetAutoKill();
            TransitionTween.OnComplete(() => { OnCompleteMainToAlbum(); });
        });

        SoundManager.Instance.PlaySoundFX((int)GlobalData.SFX.MainSelect);
    }

    private void InitMainToAlbum()
    {
        DOTween.PauseAll();
        transitionPanel.SetActive(true);

        // Check Tweening
        GlobalState.Instance.IsTweening = true;

        fadeClear.transform.localScale = fadeClearOriginScale;
        fadeRed.transform.localScale = Vector3.one;
        fadeYellow.transform.localScale = Vector3.one;
        fadeOrange.transform.localScale = Vector3.one;
    }

    private void OnCompleteMainToAlbum()
    {
        GlobalState.Instance.IsTweening = false;
        transitionPanel.SetActive(false);
    }
    #endregion

    #region Album To Music Select
    Tween fadeInTween;
    Tween fadeOutTween;
    public void AlbumToMusic(float transitionTime, float delay)
    {
        InitAlbumToMusic();

        TransitionSequence = DOTween.Sequence().SetAutoKill().OnStart(() =>
        {
            fadeInTween = fadeBackGround.DOColor(Color.white, transitionTime);
            fadeInTween.SetDelay(delay).SetAutoKill().OnComplete(() => 
            { fadeOutTween = fadeBackGround.DOColor(whiteAlpha0, transitionTime + transitionTime)
                .SetDelay(0.5f).SetAutoKill().OnComplete(() => { OnCompleteAlbumToMusic(); }); 
            });
        });

    }

    void InitAlbumToMusic()
    {
        // Check Tweening
        GlobalState.Instance.IsTweening = true;

        if (fadeBackGround)
        {
            fadeBackGround.gameObject.SetActive(true);
            fadeBackGround.color = whiteAlpha0;
            SetFadeBackGroundImages();
        }
    }

    private void OnCompleteAlbumToMusic()
    {
        GlobalState.Instance.IsTweening = false;
        fadeBackGround.gameObject.SetActive(false);
    }

    void SetFadeBackGroundImages()
    {
        var state = GlobalState.Instance;
        var albumInfo = GlobalData.Instance.Album;

        switch (GlobalState.Instance.AlbumIndex)
        {
            case (int)GlobalData.ALBUM.ISEDOL:
                fadeBackGround.sprite = albumInfo.FirstAlbumMusicBackground[state.StageIndex];
                break;
            case (int)GlobalData.ALBUM.CONTEST:
                fadeBackGround.sprite = albumInfo.SecondAlbumMusicBackground[state.StageIndex];
                break;
            case (int)GlobalData.ALBUM.GOMIX:
                fadeBackGround.sprite = albumInfo.ThirdAlbumMusicBackground[state.StageIndex];
                break;
            case (int)GlobalData.ALBUM.WAKALOID:
                fadeBackGround.sprite = albumInfo.ForthAlbumMusicBackground[state.StageIndex];
                break;
        }
    }
    #endregion

    #region Transition Between MusicSelect To Stage
    #region Music To Stage
    public void MusicToStage()
    {
        InitMusicToStage();
       
        imageBlack.DOColor(Color.black, 0.3f).SetAutoKill()
        .SetEase(Ease.OutQuad).OnComplete(() => OnBlackMusicToStage());
    }


    private void OnBlackMusicToStage()
    {
        UIManager.Instance.GoPanelGamePlay();

        imageBlack.DOColor(Color.clear, 0.3f).SetAutoKill().SetDelay(0.2f)
        .SetEase(Ease.InQuad).OnComplete(() => OnCompleteTweening());
    }
    #endregion

    #region Stage To Music
    public void StageToMusic()
    {
        InitMusicToStage();

        imageBlack.DOColor(Color.black, 0.3f).SetAutoKill()
        .SetEase(Ease.OutQuad).OnComplete(() => OnBlackStageToMusic());
    }

    private void OnBlackStageToMusic()
    {
        UIManager.Instance.GoPanelMusicSelect();       

        imageBlack.DOColor(Color.clear, 0.3f).SetAutoKill().SetDelay(0.2f)
        .SetEase(Ease.InQuad).OnComplete(() => OnCompleteStageToMusic());
    }


    private void OnCompleteStageToMusic()
    {
        SoundManager.Instance.TurnOnSelectedMusic();
        SoundManager.Instance.FadeInMusicVolume(0.5f);

        OnCompleteTweening();
    }
    #endregion

    private void InitMusicToStage()
    {
        GlobalState.Instance.IsTweening = true;
        SoundManager.Instance.FadeOutMusicVolume(0.3f);

        imageBlack.gameObject.SetActive(true);
        imageBlack.color = Color.clear;
    }
    #endregion 

    #region Stage To Result
    public void StageToResult()
    {
        InitTweening();

        imageBlack.DOColor(Color.black, 0.3f).SetAutoKill()
        .SetEase(Ease.OutQuad).OnComplete(() => OnBlackStageToResult());
    }

    private void OnBlackStageToResult()
    {
        UIManager.Instance.GoPanelResult();

        imageBlack.DOColor(Color.clear, 0.3f).SetAutoKill().SetDelay(0.2f)
        .SetEase(Ease.InQuad).OnComplete(() => OnCompleteTweening());
    }
    #endregion

    #region Result To Stage
    public void ResultToStage()
    {
        InitTweening();

        imageBlack.DOColor(Color.black, 0.3f).SetAutoKill()
        .SetEase(Ease.OutQuad).OnComplete(() => OnBlackResultToStage());
    }

    private void OnBlackResultToStage()
    {
        UIManager.Instance.GoPanelGamePlay();

        imageBlack.DOColor(Color.clear, 0.3f).SetAutoKill().SetDelay(0.2f)
        .SetEase(Ease.InQuad).OnComplete(() => OnCompleteTweening());
    }

    #endregion

    #region Result To MusicSelect
    public void ResultToMusicSelect()
    {
        InitTweening();

        imageBlack.DOColor(Color.black, 0.3f).SetAutoKill()
        .SetEase(Ease.OutQuad).OnComplete(() => OnBlackResultToMusicSelect());
    }

    private void OnBlackResultToMusicSelect()
    {
        UIManager.Instance.GoPanelMusicSelect();

        imageBlack.DOColor(Color.clear, 0.3f).SetAutoKill().SetDelay(0.2f)
        .SetEase(Ease.InQuad).OnComplete(() => OnCompleteTweening());
    }
    #endregion

    private void InitTweening()
    {
        GlobalState.Instance.IsTweening = true;

        imageBlack.gameObject.SetActive(true);
        imageBlack.color = Color.clear;
    }

    private void OnCompleteTweening()
    {
        GlobalState.Instance.IsTweening = false;
        imageBlack.gameObject.SetActive(false);
    }
}
