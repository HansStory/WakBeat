using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIObjectSetting : MonoBehaviour
{
    // 버튼 오브젝트 선언
    public Button ButtonClose;
    public Button ButtonExit;
    public Button ButtonIntegrationOn;
    public Button ButtonIntegrationOff;
    public Button ButtonSeparationOn;
    public Button ButtonSeparationOff;
    // 효과/배경음 슬라이더 선언
    public Slider EffectSlider;
    public Slider BackgroundSlider;
    public float EffectGauge;
    public float BackgroundGauge;
    // 분리 팝업 오브젝트 선언
    public GameObject SeparationGroup;
    public Button SeparationClose;
    public GameObject SeparationInBoxs;
    public GameObject SeparationOutBoxs;

    // 각 버튼 별 이벤트 정의
    public void SetButtonEvent()
    {
        // 설정 화면 버튼 이벤트
        ButtonClose.onClick.AddListener(() => SetButtonClickEvent("Close"));
        ButtonExit.onClick.AddListener(() => SetButtonClickEvent("Exit"));
        ButtonIntegrationOn.onClick.AddListener(() => SetButtonClickEvent("IntegrationOn"));
        ButtonIntegrationOff.onClick.AddListener(() => SetButtonClickEvent("IntegrationOff"));
        ButtonSeparationOn.onClick.AddListener(() => SetButtonClickEvent("SeparationOn"));
        ButtonSeparationOff.onClick.AddListener(() => SetButtonClickEvent("SeparationOff"));
        // 효과/배경음 변경 이벤트
        EffectSlider.onValueChanged.AddListener(delegate { setSoundChange("Effect"); });
        BackgroundSlider.onValueChanged.AddListener(delegate { setSoundChange("Background"); });
        // 설정 > 키 설정 > 분리 팝업 버튼 이벤트
        SeparationClose.onClick.AddListener(() => SetButtonClickEvent("SeparationClose"));
        SeparationInBoxs.transform.Find("SeparationInBox1").Find("BoxOn").GetComponent<Button>().onClick.AddListener(() => SetSeparationInputClick(1, "In", "On"));
        SeparationInBoxs.transform.Find("SeparationInBox1").Find("BoxOff").GetComponent<Button>().onClick.AddListener(() => SetSeparationInputClick(1, "In", "Off"));
        SeparationInBoxs.transform.Find("SeparationInBox2").Find("BoxOn").GetComponent<Button>().onClick.AddListener(() => SetSeparationInputClick(2, "In", "On"));
        SeparationInBoxs.transform.Find("SeparationInBox2").Find("BoxOff").GetComponent<Button>().onClick.AddListener(() => SetSeparationInputClick(2, "In", "Off"));
        SeparationInBoxs.transform.Find("SeparationInBox3").Find("BoxOn").GetComponent<Button>().onClick.AddListener(() => SetSeparationInputClick(3, "In", "On"));
        SeparationInBoxs.transform.Find("SeparationInBox3").Find("BoxOff").GetComponent<Button>().onClick.AddListener(() => SetSeparationInputClick(3, "In", "Off"));
        SeparationInBoxs.transform.Find("SeparationInBox4").Find("BoxOn").GetComponent<Button>().onClick.AddListener(() => SetSeparationInputClick(4, "In", "On"));
        SeparationInBoxs.transform.Find("SeparationInBox4").Find("BoxOff").GetComponent<Button>().onClick.AddListener(() => SetSeparationInputClick(4, "In", "Off"));
        SeparationOutBoxs.transform.Find("SeparationOutBox1").Find("BoxOn").GetComponent<Button>().onClick.AddListener(() => SetSeparationInputClick(1, "Out", "On"));
        SeparationOutBoxs.transform.Find("SeparationOutBox1").Find("BoxOff").GetComponent<Button>().onClick.AddListener(() => SetSeparationInputClick(1, "Out", "Off"));
        SeparationOutBoxs.transform.Find("SeparationOutBox2").Find("BoxOn").GetComponent<Button>().onClick.AddListener(() => SetSeparationInputClick(2, "Out", "On"));
        SeparationOutBoxs.transform.Find("SeparationOutBox2").Find("BoxOff").GetComponent<Button>().onClick.AddListener(() => SetSeparationInputClick(2, "Out", "Off"));
        SeparationOutBoxs.transform.Find("SeparationOutBox3").Find("BoxOn").GetComponent<Button>().onClick.AddListener(() => SetSeparationInputClick(3, "Out", "On"));
        SeparationOutBoxs.transform.Find("SeparationOutBox3").Find("BoxOff").GetComponent<Button>().onClick.AddListener(() => SetSeparationInputClick(3, "Out", "Off"));
        SeparationOutBoxs.transform.Find("SeparationOutBox4").Find("BoxOn").GetComponent<Button>().onClick.AddListener(() => SetSeparationInputClick(4, "Out", "On"));
        SeparationOutBoxs.transform.Find("SeparationOutBox4").Find("BoxOff").GetComponent<Button>().onClick.AddListener(() => SetSeparationInputClick(4, "Out", "Off"));
    }

    // 각 버튼 별 클릭 이벤트 정의
    public void SetButtonClickEvent(string Division)
    {
        if (Division.Equals("Close"))
        {
            // 창 닫기 버튼 이벤트
            UIElementSetting.Instance.PanelSetting.SetActive(false);
        }
        else if (Division.Equals("Exit"))
        {
            // 게임종료 버튼 이벤트
            UIElementSetting.Instance.PanelSetting.SetActive(false);
            UIManager.Instance.GoPanelMain();
            SoundManager.Instance.ForceAudioStop();
        }
        else if (Division.Equals("IntegrationOn"))
        {
            // 키 설정 > 통합 On 클릭 이벤트
            ButtonIntegrationOn.gameObject.SetActive(false);
            ButtonIntegrationOff.gameObject.SetActive(true);
            ButtonSeparationOn.gameObject.SetActive(true);
            ButtonSeparationOff.gameObject.SetActive(false);

            SeparationGroup.SetActive(true);
        }
        else if (Division.Equals("IntegrationOff"))
        {
            // 키 설정 > 통합 Off 클릭 이벤트
            ButtonIntegrationOn.gameObject.SetActive(true);
            ButtonIntegrationOff.gameObject.SetActive(false);
            ButtonSeparationOn.gameObject.SetActive(false);
            ButtonSeparationOff.gameObject.SetActive(true);

            SeparationGroup.SetActive(false);
        }
        else if (Division.Equals("SeparationOn"))
        {
            // 키 설정 > 분리 On 클릭 이벤트
            ButtonIntegrationOn.gameObject.SetActive(true);
            ButtonIntegrationOff.gameObject.SetActive(false);
            ButtonSeparationOn.gameObject.SetActive(false);
            ButtonSeparationOff.gameObject.SetActive(true);

            SeparationGroup.SetActive(false);
        }
        else if (Division.Equals("SeparationOff"))
        {
            // 키 설정 > 분리 Off 클릭 이벤트
            ButtonIntegrationOn.gameObject.SetActive(false);
            ButtonIntegrationOff.gameObject.SetActive(true);
            ButtonSeparationOn.gameObject.SetActive(true);
            ButtonSeparationOff.gameObject.SetActive(false);

            SeparationGroup.SetActive(true);
        }
        else if (Division.Equals("SeparationClose"))
        {
            // 키 설정 > 분리 > 팝업 닫기 버튼 이벤트
            ButtonIntegrationOn.gameObject.SetActive(true);
            ButtonIntegrationOff.gameObject.SetActive(false);
            ButtonSeparationOn.gameObject.SetActive(false);
            ButtonSeparationOff.gameObject.SetActive(true);

            SeparationGroup.SetActive(false);
        }
    }

    // 효과/배경음 변경 이벤트
    public void setSoundChange(string Division)
    {
        if(Division.Equals("Effect"))
        {
            EffectGauge = EffectSlider.value;
            SoundManager.Instance.CtrlSFXVolume(EffectGauge);
        }
        else
        {
            BackgroundGauge = BackgroundSlider.value;
            SoundManager.Instance.CtrlBGMVolume(BackgroundGauge);
        }
    }

    // 키 설정 > 분리 > Input Box Click 이벤트
    public void SetSeparationInputClick(int Index, string Division, string OnOff)
    {
        if (OnOff.Equals("On"))
        {
            if(Division.Equals("In"))
            {
                SeparationInBoxs.transform.Find("Separation" + Division + "Box" + Index).Find("BoxOn").gameObject.SetActive(false);
                SeparationInBoxs.transform.Find("Separation" + Division + "Box" + Index).Find("BoxOff").gameObject.SetActive(true);
            } else if (Division.Equals("Out"))
            {
                SeparationOutBoxs.transform.Find("Separation" + Division + "Box" + Index).Find("BoxOn").gameObject.SetActive(false);
                SeparationOutBoxs.transform.Find("Separation" + Division + "Box" + Index).Find("BoxOff").gameObject.SetActive(true);
            }
        }
        else if (OnOff.Equals("Off"))
        {
            if (Division.Equals("In"))
            {
                SeparationInBoxs.transform.Find("Separation" + Division + "Box" + Index).Find("BoxOn").gameObject.SetActive(true);
                SeparationInBoxs.transform.Find("Separation" + Division + "Box" + Index).Find("BoxOff").gameObject.SetActive(false);
            }
            else if (Division.Equals("Out"))
            {
                SeparationOutBoxs.transform.Find("Separation" + Division + "Box" + Index).Find("BoxOn").gameObject.SetActive(true);
                SeparationOutBoxs.transform.Find("Separation" + Division + "Box" + Index).Find("BoxOff").gameObject.SetActive(false);
            }
        }
    }

    void Start()
    {
        SetButtonEvent();

        SetButtonClickEvent("IntegrationOff");
    }

    void Update()
    {
        
    }
}
