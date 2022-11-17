using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UIElementSetting : MonoBehaviourSingleton<UIElementSetting>
{
    public GameObject PanelButton;
    public GameObject Background;
    public GameObject PanelSetting;
    public GameObject PanelShop;
    public GameObject PanelTrophy;
    public GameObject PanelPause;

    // 버튼 표기 설정
    public void PanelViewController(int index)
    {
        // Function Parameter
        // 1. true/false : 버튼 영역 자체를 보여 줄 것인지
        // 2. Index : 각 버튼의 보여줄지 여부 제어
        //     Index Detail : 0 > 메인 화면 버튼 제어
        //                            1 > 앨범 선택 및 곡 선택 버튼 제어
        //                            2 > 인게임 화면 버튼 제어
        //                            3 > 결과 화면 버튼 제어
        //                            9 > 전체 버튼 미출력 (ex. 인트로 화면 등)
        switch (index)
        {
            case (int)GlobalData.UIMODE.INTRO:
                ShowSettingPanel(false, 9);
                break;
            case (int)GlobalData.UIMODE.MAIN:
                ShowSettingPanel(true, 0);
                break;
            case (int)GlobalData.UIMODE.SELECT_ALBUM:
                ShowSettingPanel(true, 1);
                break;
            case (int)GlobalData.UIMODE.SELECT_MUSIC:
                ShowSettingPanel(true, 2);
                break;
            case (int)GlobalData.UIMODE.GAME:
                ShowSettingPanel(true, 3);
                break;
            case (int)GlobalData.UIMODE.RESULT:
                ShowSettingPanel(true, 4);
                break;
        }
    }

    // 화면 전환 시 버튼 영역 제어 Function 호출
    public void ShowSettingPanel(bool isShow, int index)
    {
        // 버튼 영역 View 여부
        PanelButton.SetActive(isShow);
        // 각 버튼 별 View 여부 제어 Function
        //PanelButton.GetComponent<UIObjectButton>().buttonViewController(index);
        PanelButton.transform.Find("UIObjectButton").GetComponent<UIObjectButton>().ButtonViewController(index);
    }

    // 버튼 클릭 시 화면 제어 이벤트
    public void ButtonClickControll(string Division, string OpenYN)
    {
        if(Division.Equals("Setting"))
        {
            if(OpenYN.Equals("Open"))
            {
                Background.SetActive(true);
                PanelSetting.SetActive(true);
            }
            else
            {
                Background.SetActive(false);
                PanelSetting.SetActive(false);
            }
        }
        else if (Division.Equals("Shop"))
        {
            if(OpenYN.Equals("Open"))
            {
                Background.SetActive(true);
                PanelShop.SetActive(true);
            }
            else
            {
                Background.SetActive(false);
                PanelShop.SetActive(false);
            }
        }
        else if (Division.Equals("Trophy"))
        {
            if (OpenYN.Equals("Open"))
            {
                Background.SetActive(true);
                PanelTrophy.SetActive(true);
            }
            else
            {
                Background.SetActive(false);
                PanelTrophy.SetActive(false);
            }
        }
        else if (Division.Equals("Pause"))
        {
            if (OpenYN.Equals("Open"))
            {
                Background.SetActive(false);
                PanelPause.SetActive(true);
            }
            else
            {
                Background.SetActive(false);
                PanelPause.SetActive(false);
            }
        }
    }

    void Start()
    {
        
    }

    void Update()
    {

    }
}