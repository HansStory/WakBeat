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
    private bool isKeyStop = false;

    private float sTime = 3f;
    private float rTime;

    // Start is called before the first frame update
    void Start()
    {
        //SoundManager.Instance.TurnOnGameBackGround();
        isStart = true;
        isKeyStop = true;
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
        rTime += Time.deltaTime;

        if(rTime >= sTime)
        {
            isKeyStop = false;
        }

        InputExcute();
        BackGroundMove();
    }

    public void OnClickStartButton()
    {
        rTime = new float();
        isKeyStop = true;
        UIManager.Instance.GoPanelAlbumSelect();
    }

    void InputExcute()
    {
        if (DataManager.dataBackgroundProcActive)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                if(!isKeyStop)
                {
                    OnClickStartButton();
                }
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
    }

    void BackGroundMove()
    {
        backGround.anchoredPosition = Vector3.right * Mathf.Sin(Time.time) * moveRange;
    }
}
