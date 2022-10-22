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

    public void IntroToMain(float blackToClear, float toWhiteTime, float holdTime, float toClearTime)
    {
        imageWhite.gameObject.SetActive(true);

        TransitionSequence = DOTween.Sequence().SetAutoKill(false).OnStart(() =>
        {
            imageWhite.color = Color.black;
            Tween blackToClearTween = imageWhite.DOColor(Color.clear, blackToClear);
            blackToClearTween.OnComplete(() => { imageWhite.color = whiteAlpha0; });

            imageWhite.DOColor(Color.white, toWhiteTime).SetDelay(blackToClear + holdTime);

            TransitionTween = imageWhite.DOColor(Color.clear, toClearTime).SetDelay(blackToClear + holdTime + toWhiteTime).SetEase(Ease.InExpo);
            TransitionTween.OnComplete(() => { imageWhite.gameObject.SetActive(false); });
        });
    }

    public void BetweenMainToAlbumTransition()
    {
        InitBetweenMainToTransitionTweening();
        transitionPanel.SetActive(true);

        TransitionSequence = DOTween.Sequence().SetAutoKill(false).OnStart(() =>
        {
            fadeClear.transform.DOScale(Vector3.zero, TransitionTime).SetEase(curveClear.Curve);
            fadeRed.transform.DOScale(Vector3.zero, TransitionTime).SetEase(curveRed.Curve);
            fadeYellow.transform.DOScale(Vector3.zero, TransitionTime).SetEase(curveYellow.Curve);
            fadeOrange.transform.DOScale(Vector3.zero, TransitionTime).SetEase(curveOrange.Curve);

            TransitionTween = fadeIvory.transform.DOScale(Vector3.zero, TransitionTime).SetEase(curveIvory.Curve);
            TransitionTween.OnComplete(() => { transitionPanel.SetActive(false); });
        });

        SoundManager.Instance.PlaySoundFX(0);
    }

    private void InitBetweenMainToTransitionTweening()
    {
        DOTween.PauseAll();
        fadeClear.transform.localScale = fadeClearOriginScale;
        fadeRed.transform.localScale = Vector3.one;
        fadeYellow.transform.localScale = Vector3.one;
        fadeOrange.transform.localScale = Vector3.one;
    }
}
