using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIElementSettings : MonoBehaviour
{
    public Button btnHome;
    public Button btnStore;
    public Button btnSetting;
    public Button btnTrophy;
    public Button btnPause;

    //  버튼 onClick 제어
    public void buttonHomeClick()
    {
        Debug.Log(">>>>>>>>>>>> Home Button Click.");
    }

    public void buttonStoreClick()
    {
        Debug.Log(">>>>>>>>>>>> Home Store Click.");
    }

    public void buttonSettingClick()
    {
        Debug.Log(">>>>>>>>>>>> Home Setting Click.");
    }

    public void buttonTrophyClick()
    {
        Debug.Log(">>>>>>>>>>>> Home Trophy Click.");
    }

    public void buttonPauseClick()
    {
        Debug.Log(">>>>>>>>>>>> Home Pause Click.");
    }

    void Start()
    {
        btnHome.onClick.AddListener(buttonHomeClick);
        btnStore.onClick.AddListener(buttonStoreClick);
        btnSetting.onClick.AddListener(buttonSettingClick);
        btnTrophy.onClick.AddListener(buttonTrophyClick);
        btnPause.onClick.AddListener(buttonPauseClick);
    }

    // Update is called once per frame
    void Update()
    {

    }
}