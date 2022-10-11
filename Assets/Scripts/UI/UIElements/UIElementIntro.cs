using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIElementIntro : MonoBehaviour
{
    public Image fadeImage = null;
    private float fadeTime = 2f;
    private float fadeHoldTime = 2f;
    bool wantFade = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(IntroProcedure());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator IntroProcedure()
    {
        // 주석 해제 > Yesoa : 이 함수 미 호출 시 Intro에서 버튼 출력
        UIManager.Instance.GoPanelIntro();

        UIManager.Instance.FadeInOut(fadeImage, fadeTime, wantFade);
        yield return new WaitForSeconds(fadeTime + fadeHoldTime);

        wantFade = !wantFade;
        UIManager.Instance.FadeInOut(fadeImage, fadeTime, wantFade);

        yield return new WaitForSeconds(fadeTime);
        UIManager.Instance.GoPanelMain();
    }
}
