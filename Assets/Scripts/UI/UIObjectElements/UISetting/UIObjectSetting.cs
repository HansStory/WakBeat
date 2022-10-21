using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class UIObjectSetting : MonoBehaviour
{
    // 버튼 오브젝트 선언
    public Button ButtonClose;
    public Button ButtonExit;
    public GameObject ButtonKeySetting;
    const int SFX_Home = 1;
    const int SFX_Setting = 4;
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
    public Sprite OnBoxImage;
    public Sprite OffBoxImage;
    public string[] InBoxValues = new string[4];
    public string[] OutBoxValues = new string[4];

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
        SFXSlider.onValueChanged.AddListener(delegate { setSFXSoundChange(); });
        BGMSlider.onValueChanged.AddListener(delegate { setBGMSoundChange(); });

        // 설정 > 키 설정 > 분리 팝업 버튼 이벤트
        SeparationClose.onClick.AddListener(() => SetButtonClickEvent("SeparationClose"));

        SeparationInBoxs.transform.Find("SeparationInBox1").GetComponent<InputField>().onEndEdit.AddListener(delegate { SetSeparationOffInputField("In", 1); });
        SeparationInBoxs.transform.Find("SeparationInBox2").GetComponent<InputField>().onEndEdit.AddListener(delegate { SetSeparationOffInputField("In", 2); });
        SeparationInBoxs.transform.Find("SeparationInBox3").GetComponent<InputField>().onEndEdit.AddListener(delegate { SetSeparationOffInputField("In", 3); });
        SeparationInBoxs.transform.Find("SeparationInBox4").GetComponent<InputField>().onEndEdit.AddListener(delegate { SetSeparationOffInputField("In", 4); });
        SeparationOutBoxs.transform.Find("SeparationOutBox1").GetComponent<InputField>().onEndEdit.AddListener(delegate { SetSeparationOffInputField("Out", 1); });
        SeparationOutBoxs.transform.Find("SeparationOutBox2").GetComponent<InputField>().onEndEdit.AddListener(delegate { SetSeparationOffInputField("Out", 2); });
        SeparationOutBoxs.transform.Find("SeparationOutBox3").GetComponent<InputField>().onEndEdit.AddListener(delegate { SetSeparationOffInputField("Out", 3); });
        SeparationOutBoxs.transform.Find("SeparationOutBox4").GetComponent<InputField>().onEndEdit.AddListener(delegate { SetSeparationOffInputField("Out", 4); });
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

    // 배경음 변경 이벤트
    public void setBGMSoundChange()
    {
        BGMGauge = BGMSlider.value;
        SoundManager.Instance.CtrlBGMVolume(BGMGauge);
    }

    // 효과음 변경 시 1회 효과음 출력
    public void setSFXSoundChange()
    {
        SFXGauge = SFXSlider.value;
        SoundManager.Instance.CtrlSFXVolume(SFXGauge);
        SoundManager.Instance.PlaySoundFX(SFX_Home);
    }

    // 포커스 나갈 시 Input Field 비활성화
    public void SetSeparationOffInputField(string Division, int Index)
    {
        if(Division.Equals("In"))
        {
            var inputFieldImage = SeparationInBoxs.transform.Find("Separation" + Division + "Box" + Index).GetComponent<Image>();
            var inputField = SeparationInBoxs.transform.Find("Separation" + Division + "Box" + Index).GetComponent<InputField>();

            inputField.readOnly = true;
            inputFieldImage.sprite = OffBoxImage;

            InBoxValues[Index - 1] = inputField.text;
        }
        else
        {
            var inputFieldImage = SeparationOutBoxs.transform.Find("Separation" + Division + "Box" + Index).GetComponent<Image>();
            var inputField = SeparationOutBoxs.transform.Find("Separation" + Division + "Box" + Index).GetComponent<InputField>();

            inputField.readOnly = true;
            inputFieldImage.sprite = OffBoxImage;

            OutBoxValues[Index - 1] = inputField.text;
        }
    }

    // 포커스 들어올 시 Input Field 활성화
    public void SetSeparationOnInputField()
    {
        if (SeparationInBoxs.transform.Find("SeparationInBox1").GetComponent<InputField>().isFocused)
        {
            SeparationInBoxs.transform.Find("SeparationInBox1").GetComponent<InputField>().readOnly = false;
            SeparationInBoxs.transform.Find("SeparationInBox1").GetComponent<Image>().sprite = OnBoxImage;
        }
        else if (SeparationInBoxs.transform.Find("SeparationInBox2").GetComponent<InputField>().isFocused)
        {
            SeparationInBoxs.transform.Find("SeparationInBox2").GetComponent<InputField>().readOnly = false;
            SeparationInBoxs.transform.Find("SeparationInBox2").GetComponent<Image>().sprite = OnBoxImage;
        }
        else if (SeparationInBoxs.transform.Find("SeparationInBox3").GetComponent<InputField>().isFocused)
        {
            SeparationInBoxs.transform.Find("SeparationInBox3").GetComponent<InputField>().readOnly = false;
            SeparationInBoxs.transform.Find("SeparationInBox3").GetComponent<Image>().sprite = OnBoxImage;
        }
        else if (SeparationInBoxs.transform.Find("SeparationInBox4").GetComponent<InputField>().isFocused)
        {
            SeparationInBoxs.transform.Find("SeparationInBox4").GetComponent<InputField>().readOnly = false;
            SeparationInBoxs.transform.Find("SeparationInBox4").GetComponent<Image>().sprite = OnBoxImage;
        }
        else if (SeparationOutBoxs.transform.Find("SeparationOutBox1").GetComponent<InputField>().isFocused)
        {
            SeparationOutBoxs.transform.Find("SeparationOutBox1").GetComponent<InputField>().readOnly = false;
            SeparationOutBoxs.transform.Find("SeparationOutBox1").GetComponent<Image>().sprite = OnBoxImage;
        }
        else if (SeparationOutBoxs.transform.Find("SeparationOutBox2").GetComponent<InputField>().isFocused)
        {
            SeparationOutBoxs.transform.Find("SeparationOutBox2").GetComponent<InputField>().readOnly = false;
            SeparationOutBoxs.transform.Find("SeparationOutBox2").GetComponent<Image>().sprite = OnBoxImage;
        }
        else if (SeparationOutBoxs.transform.Find("SeparationOutBox3").GetComponent<InputField>().isFocused)
        {
            SeparationOutBoxs.transform.Find("SeparationOutBox3").GetComponent<InputField>().readOnly = false;
            SeparationOutBoxs.transform.Find("SeparationOutBox3").GetComponent<Image>().sprite = OnBoxImage;
        }
        else if (SeparationOutBoxs.transform.Find("SeparationOutBox4").GetComponent<InputField>().isFocused)
        {
            SeparationOutBoxs.transform.Find("SeparationOutBox4").GetComponent<InputField>().readOnly = false;
            SeparationOutBoxs.transform.Find("SeparationOutBox4").GetComponent<Image>().sprite = OnBoxImage;
        }
    }

    // 글로벌 변수 화면 세팅
    public void GetGlobalValue()
    {
        SetButtonClickEvent("IntegrationOff");
    }

    // 글로벌 변수 저장
    public void SetGlobalValue()
    {
        float SFXValue = SFXSlider.value;
        float BGMValue = BGMSlider.value;
    }

    void Start()
    {
        // 버튼 이벤트 설정
        SetButtonEvent();

        // 글로벌 변수 화면 적용
        GetGlobalValue();
    }

    void Update()
    {
        // Input Field 포커스 시 활성화
        SetSeparationOnInputField();
    }
}
