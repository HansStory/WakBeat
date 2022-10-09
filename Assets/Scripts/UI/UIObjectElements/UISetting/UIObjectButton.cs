using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIObjectButton : MonoBehaviour
{
    // 버튼 제어
    public Button btnHome;
    public Button btnStore;
    public Button btnSetting;
    public Button btnTrophy;
    public Button btnPause;
    // 세팅 화면 오브젝트
    public GameObject PanelSetting;
    public GameObject PanelShop;

    // Index Detail : 0 > 메인 화면 버튼 제어
    //                        1 > 앨범 선택 및 곡 선택 버튼 제어
    //                        2 > 인게임 화면 버튼 제어
    //                        3 > 결과 화면 버튼 제어
    //                        9 > 전체 버튼 미출력 (ex. 인트로 화면 등)
    public void buttonViewController(int index)
    {
        switch (index)
        {
            case 0:
                btnHome.gameObject.SetActive(false);
                btnStore.gameObject.SetActive(false);
                btnSetting.gameObject.SetActive(true);
                btnTrophy.gameObject.SetActive(true);
                btnPause.gameObject.SetActive(false);
                break;
            case 1:
                btnHome.gameObject.SetActive(true);
                btnStore.gameObject.SetActive(true);
                btnSetting.gameObject.SetActive(true);
                btnTrophy.gameObject.SetActive(false);
                btnPause.gameObject.SetActive(false);
                break;
            case 2:
                btnHome.gameObject.SetActive(false);
                btnStore.gameObject.SetActive(false);
                btnSetting.gameObject.SetActive(false);
                btnTrophy.gameObject.SetActive(false);
                btnPause.gameObject.SetActive(true);
                break;
            case 3:
                btnHome.gameObject.SetActive(false);
                btnStore.gameObject.SetActive(false);
                btnSetting.gameObject.SetActive(true);
                btnTrophy.gameObject.SetActive(false);
                btnPause.gameObject.SetActive(false);
                break;
            case 9:
                btnHome.gameObject.SetActive(false);
                btnStore.gameObject.SetActive(false);
                btnSetting.gameObject.SetActive(false);
                btnTrophy.gameObject.SetActive(false);
                btnPause.gameObject.SetActive(false);
                break;
        }
    }

    //  홈 버튼 클릭 시 화면 제어
    public void buttonHomeClick()
    {
        // 메인 화면으로 이동
        UIManager.Instance.GoPanelMain();
        // BGM 제거
        SoundManager.Instance.ForceAudioStop();
    }

    //  상점 버튼 클릭 시 화면 제어
    public void buttonStoreClick()
    {
        // 팝업 오픈
        PanelShop.SetActive(true);
    }

    // 설정 버튼 클릭 시 화면 제어
    public void buttonSettingClick()
    {
        // 팝업 오픈
        PanelSetting.SetActive(true);
    }

    // 트로피 버튼 클릭 시 화면 제어
    public void buttonTrophyClick()
    {
        Debug.Log(">>>>>>>>>>>> Home Trophy Click.");
    }

    // 일시정지 버튼 클릭 시 화면 제어
    public void buttonPauseClick()
    {
        Debug.Log(">>>>>>>>>>>> Home Pause Click.");
    }

    void Start()
    {
        // 각 버튼 별 클릭 이벤트 생성
        btnHome.onClick.AddListener(buttonHomeClick);
        btnStore.onClick.AddListener(buttonStoreClick);
        btnSetting.onClick.AddListener(buttonSettingClick);
        btnTrophy.onClick.AddListener(buttonTrophyClick);
        btnPause.onClick.AddListener(buttonPauseClick);
    }

    void Update()
    {
        
    }
}
