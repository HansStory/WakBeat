using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIObjectSetting : MonoBehaviour
{
    // 버튼 오브젝트 선언
    public Button ButtonClose;
    public Button ButtonExit;
    public GameObject ButtonKeySetting;
    // 효과/배경음 슬라이더 선언
    public Slider SFXSlider;
    public Slider BGMSlider;
    public float SFXGauge;
    public float BGMGauge;
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
        ButtonKeySetting.transform.Find("ButtonIntegrationOn").GetComponent<Button>().onClick.AddListener(() => SetButtonClickEvent("IntegrationOn"));
        ButtonKeySetting.transform.Find("ButtonIntegrationOff").GetComponent<Button>().onClick.AddListener(() => SetButtonClickEvent("IntegrationOff"));
        ButtonKeySetting.transform.Find("ButtonSeparationOn").GetComponent<Button>().onClick.AddListener(() => SetButtonClickEvent("SeparationOn"));
        ButtonKeySetting.transform.Find("ButtonSeparationOff").GetComponent<Button>().onClick.AddListener(() => SetButtonClickEvent("SeparationOff"));

        // 효과/배경음 변경 이벤트
        SFXSlider.onValueChanged.AddListener(delegate { setSoundChange("SFX"); });
        BGMSlider.onValueChanged.AddListener(delegate { setSoundChange("BGM"); });

        // 설정 > 키 설정 > 분리 팝업 버튼 이벤트
        SeparationClose.onClick.AddListener(() => SetButtonClickEvent("SeparationClose"));
        SeparationInBoxs.transform.Find("SeparationInBox1").Find("BoxOn").GetComponent<InputField>().onValueChanged.AddListener(delegate { SetSeparationInputField(1, "In", false); });
        SeparationInBoxs.transform.Find("SeparationInBox1").Find("BoxOff").GetComponent<Button>().onClick.AddListener(() => SetSeparationInputClick(1, "In"));
        SeparationInBoxs.transform.Find("SeparationInBox2").Find("BoxOn").GetComponent<InputField>().onValueChanged.AddListener(delegate { SetSeparationInputField(2, "In", false); });
        SeparationInBoxs.transform.Find("SeparationInBox2").Find("BoxOff").GetComponent<Button>().onClick.AddListener(() => SetSeparationInputClick(2, "In"));
        SeparationInBoxs.transform.Find("SeparationInBox3").Find("BoxOn").GetComponent<InputField>().onValueChanged.AddListener(delegate { SetSeparationInputField(3, "In", false); });
        SeparationInBoxs.transform.Find("SeparationInBox3").Find("BoxOff").GetComponent<Button>().onClick.AddListener(() => SetSeparationInputClick(3, "In"));
        SeparationInBoxs.transform.Find("SeparationInBox4").Find("BoxOn").GetComponent<InputField>().onValueChanged.AddListener(delegate { SetSeparationInputField(4, "In", false); });
        SeparationInBoxs.transform.Find("SeparationInBox4").Find("BoxOff").GetComponent<Button>().onClick.AddListener(() => SetSeparationInputClick(4, "In"));
        SeparationOutBoxs.transform.Find("SeparationOutBox1").Find("BoxOn").GetComponent<InputField>().onValueChanged.AddListener(delegate { SetSeparationInputField(1, "Out", false); });
        SeparationOutBoxs.transform.Find("SeparationOutBox1").Find("BoxOff").GetComponent<Button>().onClick.AddListener(() => SetSeparationInputClick(1, "Out"));
        SeparationOutBoxs.transform.Find("SeparationOutBox2").Find("BoxOn").GetComponent<InputField>().onValueChanged.AddListener(delegate { SetSeparationInputField(2, "Out", false); });
        SeparationOutBoxs.transform.Find("SeparationOutBox2").Find("BoxOff").GetComponent<Button>().onClick.AddListener(() => SetSeparationInputClick(2, "Out"));
        SeparationOutBoxs.transform.Find("SeparationOutBox3").Find("BoxOn").GetComponent<InputField>().onValueChanged.AddListener(delegate { SetSeparationInputField(3, "Out", false); });
        SeparationOutBoxs.transform.Find("SeparationOutBox3").Find("BoxOff").GetComponent<Button>().onClick.AddListener(() => SetSeparationInputClick(3, "Out"));
        SeparationOutBoxs.transform.Find("SeparationOutBox4").Find("BoxOn").GetComponent<InputField>().onValueChanged.AddListener(delegate { SetSeparationInputField(4, "Out", false); });
        SeparationOutBoxs.transform.Find("SeparationOutBox4").Find("BoxOff").GetComponent<Button>().onClick.AddListener(() => SetSeparationInputClick(4, "Out"));

        // 입력창 forcus out 이벤트
        SeparationInBoxs.transform.Find("SeparationInBox1").Find("BoxOn").GetComponent<InputField>().onEndEdit.AddListener(delegate { SetSeparationInputField(1, "In", true); });
        SeparationInBoxs.transform.Find("SeparationInBox2").Find("BoxOn").GetComponent<InputField>().onEndEdit.AddListener(delegate { SetSeparationInputField(2, "In", true); });
        SeparationInBoxs.transform.Find("SeparationInBox3").Find("BoxOn").GetComponent<InputField>().onEndEdit.AddListener(delegate { SetSeparationInputField(3, "In", true); });
        SeparationInBoxs.transform.Find("SeparationInBox4").Find("BoxOn").GetComponent<InputField>().onEndEdit.AddListener(delegate { SetSeparationInputField(4, "In", true); });
        SeparationOutBoxs.transform.Find("SeparationOutBox1").Find("BoxOn").GetComponent<InputField>().onEndEdit.AddListener(delegate { SetSeparationInputField(1, "Out", true); });
        SeparationOutBoxs.transform.Find("SeparationOutBox2").Find("BoxOn").GetComponent<InputField>().onEndEdit.AddListener(delegate { SetSeparationInputField(2, "Out", true); });
        SeparationOutBoxs.transform.Find("SeparationOutBox3").Find("BoxOn").GetComponent<InputField>().onEndEdit.AddListener(delegate { SetSeparationInputField(3, "Out", true); });
        SeparationOutBoxs.transform.Find("SeparationOutBox4").Find("BoxOn").GetComponent<InputField>().onEndEdit.AddListener(delegate { SetSeparationInputField(4, "Out", true); });
    }

    // 각 버튼 별 클릭 이벤트 정의
    public void SetButtonClickEvent(string Division)
    {
        if (Division.Equals("Close"))
        {
            // 입력 되 있는 모든 값 가져와서 글로벌에 저장
            SetGlobalValue();

            // 창 닫기 버튼 이벤트
            UIElementSetting.Instance.ButtonClickControll("Setting", "Close");

        }
        else if (Division.Equals("Exit"))
        {
            // 입력 되 있는 모든 값 가져와서 글로벌에 저장
            SetGlobalValue();

            // 게임종료 버튼 이벤트
            UIElementSetting.Instance.ButtonClickControll("Setting", "Close");
            UIManager.Instance.GoPanelMain();
            SoundManager.Instance.ForceAudioStop();
        }
        else if (Division.Equals("IntegrationOn"))
        {
            // 키 설정 > 통합 On 클릭 이벤트
            ButtonKeySetting.transform.Find("ButtonIntegrationOn").gameObject.SetActive(false);
            ButtonKeySetting.transform.Find("ButtonIntegrationOff").gameObject.SetActive(true);
            ButtonKeySetting.transform.Find("ButtonSeparationOn").gameObject.SetActive(true);
            ButtonKeySetting.transform.Find("ButtonSeparationOff").gameObject.SetActive(false);
            
            SeparationGroup.SetActive(true);
        }
        else if (Division.Equals("IntegrationOff"))
        {
            // 키 설정 > 통합 Off 클릭 이벤트
            ButtonKeySetting.transform.Find("ButtonIntegrationOn").gameObject.SetActive(true);
            ButtonKeySetting.transform.Find("ButtonIntegrationOff").gameObject.SetActive(false);
            ButtonKeySetting.transform.Find("ButtonSeparationOn").gameObject.SetActive(false);
            ButtonKeySetting.transform.Find("ButtonSeparationOff").gameObject.SetActive(true);
           
            SeparationGroup.SetActive(false);
        }
        else if (Division.Equals("SeparationOn"))
        {
            // 키 설정 > 분리 On 클릭 이벤트
            ButtonKeySetting.transform.Find("ButtonIntegrationOn").gameObject.SetActive(true);
            ButtonKeySetting.transform.Find("ButtonIntegrationOff").gameObject.SetActive(false);
            ButtonKeySetting.transform.Find("ButtonSeparationOn").gameObject.SetActive(false);
            ButtonKeySetting.transform.Find("ButtonSeparationOff").gameObject.SetActive(true);
            
            SeparationGroup.SetActive(false);
        }
        else if (Division.Equals("SeparationOff"))
        {
            // 키 설정 > 분리 Off 클릭 이벤트
            ButtonKeySetting.transform.Find("ButtonIntegrationOn").gameObject.SetActive(false);
            ButtonKeySetting.transform.Find("ButtonIntegrationOff").gameObject.SetActive(true);
            ButtonKeySetting.transform.Find("ButtonSeparationOn").gameObject.SetActive(true);
            ButtonKeySetting.transform.Find("ButtonSeparationOff").gameObject.SetActive(false);

            SeparationGroup.SetActive(true);
        }
        else if (Division.Equals("SeparationClose"))
        {
            // 키 설정 > 분리 > 팝업 닫기 버튼 이벤트
            ButtonKeySetting.transform.Find("ButtonIntegrationOn").gameObject.SetActive(true);
            ButtonKeySetting.transform.Find("ButtonIntegrationOff").gameObject.SetActive(false);
            ButtonKeySetting.transform.Find("ButtonSeparationOn").gameObject.SetActive(false);
            ButtonKeySetting.transform.Find("ButtonSeparationOff").gameObject.SetActive(true);

            SeparationGroup.SetActive(false);
        }
    }

    // 효과/배경음 변경 이벤트
    public void setSoundChange(string Division)
    {
        if (Division.Equals("SFX"))
        {
            SFXGauge = SFXSlider.value;
            SoundManager.Instance.CtrlSFXVolume(SFXGauge);
        }
        else
        {
            BGMGauge = BGMSlider.value;
            SoundManager.Instance.CtrlBGMVolume(BGMGauge);
        }
    }

    // 키 설정 > 분리 > Input Box Click 이벤트
    public void SetSeparationInputClick(int Index, string Division)
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

    // 키 설정 > 분리 > Input box Field 이벤트
    public void SetSeparationInputField(int Index, string Division, Boolean flag)
    {
        string InputValue = Division.Equals("In") ?
            SeparationInBoxs.transform.Find("Separation" + Division + "Box" + Index).Find("BoxOn").GetComponent<InputField>().text :
                SeparationOutBoxs.transform.Find("Separation" + Division + "Box" + Index).Find("BoxOn").GetComponent<InputField>().text;

        // 입력창에서 입력 후 Backspace 를 누르거나 마우스 포커스 아웃 일 때 입력 창 비활성화
        if (Input.GetKey(KeyCode.Backspace) || flag == true)
        {
            if (Division.Equals("In"))
            {
                SeparationInBoxs.transform.Find("Separation" + Division + "Box" + Index).Find("BoxOn").gameObject.SetActive(false);
                SeparationInBoxs.transform.Find("Separation" + Division + "Box" + Index).Find("BoxOff").gameObject.SetActive(true);
            }
            else if (Division.Equals("Out"))
            {
                SeparationOutBoxs.transform.Find("Separation" + Division + "Box" + Index).Find("BoxOn").gameObject.SetActive(false);
                SeparationOutBoxs.transform.Find("Separation" + Division + "Box" + Index).Find("BoxOff").gameObject.SetActive(true);
            }
        }
    }

    // 1. Out 버튼을 교체 해야 함 > 입력 후 비활성화 시 입력 된 값이 비활성화에 출력 되며 입력 값이 이미지가 바뀜
    //     - 버튼에 입력 받은 텍스트에 대한 이미지 출력
    // 2. 글로벌 변수에 값 세팅 하는 프로세스 작업

    // 글로벌 변수 세팅 
    public void SetGlobalValue()
    {
        /*
        string InputValue = Division.Equals("In") ?
            SeparationInBoxs.transform.Find("Separation" + Division + "Box" + Index).Find("BoxOn").GetComponent<InputField>().text :
                SeparationOutBoxs.transform.Find("Separation" + Division + "Box" + Index).Find("BoxOn").GetComponent<InputField>().text;
        */
        string[] Inner = new string[4];
        string[] Outer = new string[4];
        float SFXValue = SFXSlider.value;
        float BGMValue = BGMSlider.value;

        for (int Index = 1; Index >= 4; Index++)
        {
            string InBoxValue = SeparationInBoxs.transform.Find("SeparationInBox" + Index).Find("BoxOn").GetComponent<InputField>().text;
            string OutBoxValue = SeparationOutBoxs.transform.Find("SeparationOutBox" + Index).Find("BoxOn").GetComponent<InputField>().text;

            Inner.Append(InBoxValue);
            Outer.Append(OutBoxValue);
        }

        Debug.Log(">>>>>>>>>>>>>>>>>>>>> Sound : " + SFXValue + " // " + BGMValue);
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
