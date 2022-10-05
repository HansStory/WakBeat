using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DOTweenTest : MonoBehaviour
{
    private float myFloat;
    private Vector3 myVector;
    private string myString;

    // How To Use DOTween.TO
    void UsingDoTweenTo()
    {
        // float
        DOTween.To(() => myFloat, x => myFloat = x, 100f, 1f);

        // Vector
        DOTween.To(() => myVector, x => myVector = x, new Vector3(3f, 4f, 5f), 1f);

        // String
        DOTween.To(() => myString, str => myString = str, "Hello, world!", 1f);
    }

    [SerializeField] private Image circle;


    public Vector2 StartScale = new Vector2(1f, 1f);
    public Vector2 TargetScale = new Vector2(0.6f, 0.6f);

    [Header("그래프 타입")]
    public Ease EaseType;

    [Header("몇초간 Tweening 할것인가")]
    public float Duration = 1f;

    private bool isLoop = false;

    public void OnClickOriginButton()
    {
        DOTween.PauseAll(); 
        circle.rectTransform.localScale = StartScale;
    }

    public void OnClickLoopTween()
    {
        isLoop = !isLoop;

        if (isLoop)
        {
            circle.rectTransform.localScale = StartScale;
            circle.rectTransform.DOScale(TargetScale, Duration).SetEase(EaseType).SetLoops(-1,LoopType.Restart);
        }
        else
        {
            DOTween.PauseAll();
        }

    }

    public void OnclickOneShot()
    {
        DOTween.PauseAll();
        circle.rectTransform.localScale = StartScale;
        circle.rectTransform.DOScale(TargetScale, Duration).SetEase(EaseType);
    }

    void Start()
    {

    }
}
