using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class UIElementMain : MonoBehaviour
{
    [Header("BackGround Movement Element")]
    [SerializeField] private RectTransform backGround;
    [SerializeField] private float moveRange = 70f;

    [SerializeField] private PlayableDirector MainLogoAnim;


    void Start()
    {
        //SoundManager.Instance.TurnOnGameBackGround();
    }

    private void OnEnable()
    {
        Invoke($"{nameof(LogoPlay)}", 1f);
    }

    void LogoPlay()
    {
        MainLogoAnim.Play();
    }

    // Update is called once per frame
    void Update()
    {
        InputExcute();
        BackGroundMove();
    }

    public void OnClickStartButton()
    {
        //isKeyStop = true;
        UIManager.Instance.GoPanelAlbumSelect();
    }

    void InputExcute()
    {
        if (DataManager.dataBackgroundProcActive)
        {
            if (!GlobalState.Instance.IsTweening)
            {
                if (GlobalState.Instance.DevMode)
                {
                    InputReturn();
                    InputEscape();
                }
            }
        }
    }

    void InputReturn()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            OnClickStartButton();
        }
    }

    void InputEscape()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            AppManager.Instance.Quit();
        }
    }

    void BackGroundMove()
    {
        backGround.anchoredPosition = Vector3.right * Mathf.Sin(Time.time) * moveRange;
    }
}
