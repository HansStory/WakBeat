using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIElementFadePanel : MonoBehaviour
{
    // For Main To Album Transition 
    [SerializeField] private GameObject transitionPanel;

    [SerializeField] private GameObject fadeClear;
    [SerializeField] private AnimCurve curveClear;

    [SerializeField] private Image fadeRed;
    [SerializeField] private AnimCurve curveRed;

    [SerializeField] private Image fadeYellow;
    [SerializeField] private AnimCurve curveYellow;

    [SerializeField] private Image fadeOrange;
    [SerializeField] private AnimCurve curveOrange;

    [SerializeField] private Image fadeIvory;
    [SerializeField] private AnimCurve curveIvory;

    public float TransitionTime = 2f;

    public void MainToAlbumTransition()
    {
        fadeClear.transform.DOScale(Vector3.zero, TransitionTime).SetEase(curveClear.Curve);
        fadeRed.transform.DOScale(Vector3.zero, TransitionTime).SetEase(curveRed.Curve);
        fadeYellow.transform.DOScale(Vector3.zero, TransitionTime).SetEase(curveYellow.Curve);
        fadeOrange.transform.DOScale(Vector3.zero, TransitionTime).SetEase(curveOrange.Curve);
        fadeIvory.transform.DOScale(Vector3.zero, TransitionTime).SetEase(curveIvory.Curve);

        SoundManager.Instance.PlaySoundFX(0);
    }
    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(MainToAlbumTransition), 2f); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
