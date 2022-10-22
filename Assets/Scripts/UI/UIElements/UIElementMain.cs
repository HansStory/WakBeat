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

    private bool isStart = false;

    // Start is called before the first frame update
    void Start()
    {
        //SoundManager.Instance.TurnOnGameBackGround();
        isStart = true;
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
        UIManager.Instance.GoPanelAlbumSelect();
    }

    void InputExcute()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            OnClickStartButton();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            AppManager.Instance.Quit();
        }

        // For Test
        //if (Input.GetKeyDown(KeyCode.Alpha1))
        //{
        //    LogoPlay();
        //}
    }

    void BackGroundMove()
    {
        backGround.anchoredPosition = Vector3.right * Mathf.Sin(Time.time) * moveRange;
    }
}
