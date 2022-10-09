using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIObjectShop : MonoBehaviour
{
    // 쇼핑 화면 오브젝트
    public GameObject PanelShop;
    // 각 Tab 별 화면 오브젝트
    public GameObject TabSkinGroup;
    public GameObject TabSkillGroup;
    public GameObject TabVideoGroup;
    // 버튼 제어
    public Button ButtonClose;
    public Button ButtonSkinTabOn;
    public Button ButtonSkinTabOff;
    public Button ButtonSkillTabOn;
    public Button ButtonSkillTabOff;
    public Button ButtonVideoTabOn;
    public Button ButtonVideoTabOff;

    // 버튼 제어 이벤트 모음
    public void ButtonEvent(string Division)
    {
        // Division : 버튼 이벤트 구분자
        if(Division.Equals("SkinOn") || Division.Equals("SkinOff"))
        {
            // 스킨 상점 On 클릭 시
            ButtonSkinTabOn.gameObject.SetActive(true);
            ButtonSkinTabOff.gameObject.SetActive(false);
            ButtonSkillTabOn.gameObject.SetActive(false);
            ButtonSkillTabOff.gameObject.SetActive(true);
            ButtonVideoTabOn.gameObject.SetActive(false);
            ButtonVideoTabOff.gameObject.SetActive(true);
            TabSkinGroup.SetActive(true);
            TabSkillGroup.SetActive(false);
            TabVideoGroup.SetActive(false);
        }
        else if (Division.Equals("SkillOn") || Division.Equals("SkillOff"))
        {
            ButtonSkinTabOn.gameObject.SetActive(false);
            ButtonSkinTabOff.gameObject.SetActive(true);
            ButtonSkillTabOn.gameObject.SetActive(true);
            ButtonSkillTabOff.gameObject.SetActive(false);
            ButtonVideoTabOn.gameObject.SetActive(false);
            ButtonVideoTabOff.gameObject.SetActive(true);
            TabSkinGroup.SetActive(false);
            TabSkillGroup.SetActive(true);
            TabVideoGroup.SetActive(false);
        }
        else if (Division.Equals("VideoOn") || Division.Equals("VideoOff"))
        {
            ButtonSkinTabOn.gameObject.SetActive(false);
            ButtonSkinTabOff.gameObject.SetActive(true);
            ButtonSkillTabOn.gameObject.SetActive(false);
            ButtonSkillTabOff.gameObject.SetActive(true);
            ButtonVideoTabOn.gameObject.SetActive(true);
            ButtonVideoTabOff.gameObject.SetActive(false);
            TabSkinGroup.SetActive(false);
            TabSkillGroup.SetActive(false);
            TabVideoGroup.SetActive(true);
        }
    }

    // 창 닫기 버튼 클릭 시
    public void buttonCloseClick()
    {
        // 글로벌 변수에 세팅에서 설정 한 값들 세팅
        SetGlobalValueSetting("CLOSE");
        // 팝업 닫기
        PanelShop.SetActive(false);
    }

    public void SetEvent()
    {
        // 설정 화면 버튼 이벤트 추가
        ButtonClose.onClick.AddListener(buttonCloseClick);
        ButtonSkinTabOn.onClick.AddListener(() => ButtonEvent("SkinOn"));
        ButtonSkinTabOff.onClick.AddListener(() => ButtonEvent("SkinOff"));
        ButtonSkillTabOn.onClick.AddListener(() => ButtonEvent("SkillOn"));
        ButtonSkillTabOff.onClick.AddListener(() => ButtonEvent("SkillOff"));
        ButtonVideoTabOn.onClick.AddListener(() => ButtonEvent("VideoOn"));
        ButtonVideoTabOff.onClick.AddListener(() => ButtonEvent("VideoOff"));

        // 화면 최초 Tab 활성화 제어
        ButtonEvent("SkinOn");
    }

    // 글로벌변수 가져오기
    public void GetGlobalValueSetting()
    {

    }

    // 글로벌변수 세팅
    public void SetGlobalValueSetting(string Division)
    {

    }

    void Start()
    {
        SetEvent();
        GetGlobalValueSetting();
    }

    void Update()
    {
        
    }
}
