using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIObjectButton : MonoBehaviour
{
    public Button ButtonHome;
    public Button ButtonSetting;
    public Button ButtonShop;
    public Button ButtonTrophy;
    public Button ButtonPause;
    const int SFX_Home = 1;
    const int SFX_Setting = 4;

    // Index Detail : 0 > 메인 화면 버튼 제어
    //                        1 > 앨범 선택 및 곡 선택 버튼 제어
    //                        2 > 인게임 화면 버튼 제어
    //                        3 > 결과 화면 버튼 제어
    //                        9 > 전체 버튼 미출력 (ex. 인트로 화면 등)
    public void ButtonViewController(int index)
    {
        switch (index)
        {
            case 0:
                ButtonHome.gameObject.SetActive(false);
                ButtonSetting.gameObject.SetActive(true);
                ButtonShop.gameObject.SetActive(false);
                ButtonTrophy.gameObject.SetActive(false);
                ButtonPause.gameObject.SetActive(false);
                break;
            case 1:
                ButtonHome.gameObject.SetActive(false);
                ButtonSetting.gameObject.SetActive(true);
                ButtonShop.gameObject.SetActive(true);
                ButtonTrophy.gameObject.SetActive(false);
                ButtonPause.gameObject.SetActive(false);
                break;
            case 2:
                ButtonHome.gameObject.SetActive(true);
                ButtonSetting.gameObject.SetActive(true);
                ButtonShop.gameObject.SetActive(true);
                ButtonTrophy.gameObject.SetActive(false);
                ButtonPause.gameObject.SetActive(false);
                break;
            case 3:
                ButtonHome.gameObject.SetActive(false);
                ButtonSetting.gameObject.SetActive(false);
                ButtonShop.gameObject.SetActive(false);
                ButtonTrophy.gameObject.SetActive(false);
                ButtonPause.gameObject.SetActive(true);
                break;
            case 4:
                ButtonHome.gameObject.SetActive(false);
                ButtonSetting.gameObject.SetActive(true);
                ButtonShop.gameObject.SetActive(false);
                ButtonTrophy.gameObject.SetActive(false);
                ButtonPause.gameObject.SetActive(false);
                break;
            case 9:
                ButtonHome.gameObject.SetActive(false);
                ButtonSetting.gameObject.SetActive(false);
                ButtonShop.gameObject.SetActive(false);
                ButtonTrophy.gameObject.SetActive(false);
                ButtonPause.gameObject.SetActive(false);
                break;
        }
    }

    // 각 버튼 별 이벤트 정의
    public void SetButtonEvent()
    {
        ButtonHome.onClick.AddListener(() => SetButtonClickEvent("goHome"));
        ButtonSetting.onClick.AddListener(() => SetButtonClickEvent("goSetting"));
        ButtonShop.onClick.AddListener(() => SetButtonClickEvent("goShop"));
        ButtonTrophy.onClick.AddListener(() => SetButtonClickEvent("goTrophy"));
        ButtonPause.onClick.AddListener(() => SetButtonClickEvent("goPause"));
    }

    // 각 버튼 별 클릭 이벤트 정의
    public void SetButtonClickEvent(string Division) 
    {
        if(Division.Equals("goHome"))
        {
            //  홈 버튼 클릭 시 화면 제어
            // 메인 화면으로 이동
            UIManager.Instance.GoPanelMain();
            // BGM 멈춤
            SoundManager.Instance.PlaySoundFX(SFX_Home);
            SoundManager.Instance.ForceAudioStop();
        }
        else if (Division.Equals("goSetting"))
        {
            // 설정 버튼 클릭 시 화면 제어
            SoundManager.Instance.PlaySoundFX(SFX_Setting);
            UIElementSetting.Instance.ButtonClickControll("Setting", "Open");
        }
        else if (Division.Equals("goShop"))
        {
            //  상점 버튼 클릭 시 화면 제어
            SoundManager.Instance.PlaySoundFX(SFX_Home);
            UIElementSetting.Instance.ButtonClickControll("Shop", "Open");
        }
        else if (Division.Equals("goTrophy"))
        {
            // 트로피 버튼 클릭 시 화면 제어
            UIElementSetting.Instance.ButtonClickControll("Trophy", "Open");
        }
        else if (Division.Equals("goPause"))
        {
            // 일시정지 버튼 클릭 시 화면 제어
        }
    }

    void Start()
    {
        // 각 버튼 별 클릭 이벤트 생성
        SetButtonEvent();
    }

    void Update()
    {
        
    }
}
