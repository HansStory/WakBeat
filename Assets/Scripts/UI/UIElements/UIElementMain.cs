using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIElementMain : MonoBehaviour
{
    [Header("BackGround Movement Element")]
    [SerializeField] private RectTransform backGround;
    [SerializeField] private float moveRange = 70f;

    [Space(10)]
    public Image fadeImage = null;
    private float fadeTime = 0.5f;
    private float fadeHoldTime = 0.1f;

    private bool isStart = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartMainProcedure());
        SoundManager.Instance.TurnOnGameBackGround();
        isStart = true;
    }

    private void OnEnable()
    {
        if (isStart)
        {
            StartCoroutine(BackToMain());
        }
    }

    // Update is called once per frame
    void Update()
    {
        GameStart();
        OnClickEsc();
        BackGroundMove();
    }

    void BackGroundMove()
    {
        backGround.anchoredPosition = Vector3.right * Mathf.Sin(Time.time) * moveRange;
    }

    void GameStart()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            StartCoroutine(GoToAlbumSelectPanel());
        }
    }

    public void OnClickStartButton()
    {
        StartCoroutine(GoToAlbumSelectPanel());
    }

    public void OnClickEsc()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            AppManager.Instance.Quit();
        }
    }

    IEnumerator StartMainProcedure()
    {
        fadeImage.color = Color.white;
        fadeImage.gameObject.SetActive(true);
        UIManager.Instance.FadeToWhite(fadeImage, fadeTime);
        yield return new WaitForSeconds(fadeTime);

        fadeImage.gameObject.SetActive(false);
    }

    IEnumerator GoToAlbumSelectPanel()
    {
        fadeImage.color = Color.black;
        fadeImage.gameObject.SetActive(true);
        UIManager.Instance.FadeToBlack(fadeImage, fadeTime);
        yield return new WaitForSeconds(fadeTime + fadeHoldTime);

        fadeImage.gameObject.SetActive(false);
        UIManager.Instance.GoPanelAlbumSelect();
    }
   
    IEnumerator BackToMain()
    {
        fadeImage.color = Color.black;
        fadeImage.gameObject.SetActive(true);
        UIManager.Instance.FadeToWhite(fadeImage, fadeTime);
        yield return new WaitForSeconds(fadeTime);

        fadeImage.gameObject.SetActive(false);
    }
}
