using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIObjectSetting : MonoBehaviour
{
    // 세팅 화면 오브젝트
    public GameObject PanelSetting;
    // 버튼 제어
    public Button ButtonClose;
    public Button ButtonExit;
    public Button KeyIntegOn;
    public Button KeyIntegOff;
    public Button KeySeparOn;
    public Button KeySeparOff;
    // 효과음/배경음 Slider
    public Slider EffectSlider;
    public Slider BackgroundSlider;
    public float EffectGauge;
    public float BackgroundGauge;
    // (글로벌데이터로 변경필요) 키 설정 분류 : 통합 > I / 분리 > S
    // 글로벌 이펙트 사운드 수치, 글로벌 배경음 사운드 수치
    public string GBKeyboardDivision;
    public float GBEffectSound;
    public float GBBackgroundSound;
    // 키설정 > 분리 변수
    public GameObject SeparationPopup;
    public Button SeparationButtonClose;

    // 창 닫기 버튼 클릭 시
    public void buttonCloseClick()
    {
        // 글로벌 변수에 세팅에서 설정 한 값들 세팅
        SetGlobalValueSetting("CLOSE");
        // 팝업 닫기
        PanelSetting.SetActive(false);
    }

    // 게임종료 버튼 클릭 시
    public void buttonExitClick()
    {
        // 글로벌 변수에 세팅에서 설정 한 값들 세팅
        SetGlobalValueSetting("EXIT");
        // 팝업 닫기
        PanelSetting.SetActive(false);
        // 메인으로 돌아가기 > 차후에 게임 종료로 변경
        UIManager.Instance.GoPanelMain();
        SoundManager.Instance.ForceAudioStop();
    }

    // 키 설정 통합 On 버튼 클릭 시
    public void KeyIntegOnClick()
    {
        int InOnIdx = KeyIntegOn.transform.GetSiblingIndex();
        int InOffIdx = KeyIntegOff.transform.GetSiblingIndex();
        int SeOnIdx = KeySeparOn.transform.GetSiblingIndex();
        int SeOffIdx = KeySeparOff.transform.GetSiblingIndex();

        if (InOnIdx > InOffIdx)
        {
            if (SeOnIdx < SeOffIdx)
            {
                KeySeparOn.transform.SetSiblingIndex(SeOffIdx);
                KeySeparOff.transform.SetSiblingIndex(SeOnIdx);
            }

            KeyIntegOn.transform.SetSiblingIndex(InOffIdx);
            KeyIntegOff.transform.SetSiblingIndex(InOnIdx);

            // 글로벌데이터 KeyboardDivision 변경
            GBKeyboardDivision = "I";

            // 키 설정 > 분리 > 설정 창 On
            SeparationPopup.SetActive(true);
        }
    }

    // 키 설정 통합 Off 버튼 클릭 시
    public void KeyIntegOffClick()
    {
        int InOnIdx = KeyIntegOn.transform.GetSiblingIndex();
        int InOffIdx = KeyIntegOff.transform.GetSiblingIndex();
        int SeOnIdx = KeySeparOn.transform.GetSiblingIndex();
        int SeOffIdx = KeySeparOff.transform.GetSiblingIndex();

        if (InOnIdx < InOffIdx)
        {
            if (SeOnIdx > SeOffIdx)
            {
                KeySeparOn.transform.SetSiblingIndex(SeOffIdx);
                KeySeparOff.transform.SetSiblingIndex(SeOnIdx);
            }

            KeyIntegOff.transform.SetSiblingIndex(InOnIdx);
            KeyIntegOn.transform.SetSiblingIndex(InOffIdx);

            // 키 설정 > 분리 > 설정 창 Off
            SeparationPopup.SetActive(false);
        }
    }

    // 키 설정 분리 On 버튼 클릭 시
    public void KeySeparOnClick()
    {
        int SeOnIdx = KeySeparOn.transform.GetSiblingIndex();
        int SeOffIdx = KeySeparOff.transform.GetSiblingIndex();
        int InOnIdx = KeyIntegOn.transform.GetSiblingIndex();
        int InOffIdx = KeyIntegOff.transform.GetSiblingIndex();

        if (SeOnIdx > SeOffIdx)
        {
            if (InOnIdx < InOffIdx)
            {
                KeyIntegOff.transform.SetSiblingIndex(InOnIdx);
                KeyIntegOn.transform.SetSiblingIndex(InOffIdx);
            }

            KeySeparOn.transform.SetSiblingIndex(SeOffIdx);
            KeySeparOff.transform.SetSiblingIndex(SeOnIdx);

            // 글로벌데이터 KeyboardDivision 변경
            GBKeyboardDivision = "S";

            // 키 설정 > 분리 > 설정 창 Off
            SeparationPopup.SetActive(false);
        }
    }

    // 키 설정 분리 Off 버튼 클릭 시
    public void KeySeparOffClick()
    {
        int SeOnIdx = KeySeparOn.transform.GetSiblingIndex();
        int SeOffIdx = KeySeparOff.transform.GetSiblingIndex();
        int InOnIdx = KeyIntegOn.transform.GetSiblingIndex();
        int InOffIdx = KeyIntegOff.transform.GetSiblingIndex();

        if (SeOnIdx < SeOffIdx)
        {
            if (InOnIdx > InOffIdx)
            {
                KeyIntegOff.transform.SetSiblingIndex(InOnIdx);
                KeyIntegOn.transform.SetSiblingIndex(InOffIdx);
            }

            KeySeparOn.transform.SetSiblingIndex(SeOffIdx);
            KeySeparOff.transform.SetSiblingIndex(SeOnIdx);
        }

        // 키 설정 > 분리 > 설정 창 On
        SeparationPopup.SetActive(true);
    }

    // 효과음 소리 수치 변경 
    public void setEffectSoundChange()
    {
        EffectGauge = EffectSlider.value;
        SoundManager.Instance.CtrlSFXVolume(EffectGauge);
    }

    // 배경음 소리 수치 변경
    public void setBackgroundSoundChange()
    {
        BackgroundGauge = BackgroundSlider.value;
        SoundManager.Instance.CtrlBGMVolume(BackgroundGauge);
    }

    // 키 설정 > 분리 창 닫기 버튼 클릭 시
    public void SeparationButtonCloseClick()
    {
        // 입력 값 있을 시 값 세팅

        // 분리 Off 버튼 클릭 이벤트 호출
        KeySeparOnClick();
    }

    // 버튼 이벤트 세팅
    public void SetEvent()
    {
        // 설정 화면 버튼 이벤트 추가
        ButtonClose.onClick.AddListener(buttonCloseClick);
        ButtonExit.onClick.AddListener(buttonExitClick);
        KeyIntegOn.onClick.AddListener(KeyIntegOnClick);
        KeyIntegOff.onClick.AddListener(KeyIntegOffClick);
        KeySeparOn.onClick.AddListener(KeySeparOnClick);
        KeySeparOff.onClick.AddListener(KeySeparOffClick);
        EffectSlider.onValueChanged.AddListener(delegate { setEffectSoundChange(); });
        BackgroundSlider.onValueChanged.AddListener(delegate { setBackgroundSoundChange(); });
        // 키 설정 > 분리 버튼 이벤트 추가 
        SeparationButtonClose.onClick.AddListener(SeparationButtonCloseClick);

        // 키 설정이 통합 일 때
        if (GBKeyboardDivision.Equals("I"))
        {
            KeyIntegOffClick();
        }
        // 키 설정이 분리 일 때
        else if (GBKeyboardDivision.Equals("S"))
        {
            KeySeparOffClick();
        }
    }

    // 글로벌변수 가져오기
    public void GetGlobalValueSetting()
    {
        // 효과음 및 사운드 게이지 설정
        EffectSlider.value = GBEffectSound;
        BackgroundSlider.value = GBBackgroundSound;

        // 키설정 값 세팅 (통합/분리)

        // 키설정 > 분리 값 세팅 (8개 칸에 값 세팅)
    }

    // 글로벌변수 세팅
    public void SetGlobalValueSetting(string Division)
    {
        // 글로벌 변수 세팅

        // 게임 종료 시 글로벌 변수들 파일로 변환하여 Drop
        if (Division.Equals("EXIT"))
        {

        }
    }

    void Start()
    {
        // 버튼 이벤트 추가
        SetEvent();

        // 글로벌 변수 가져오기
        GetGlobalValueSetting();
    }

    void Update()
    {
        
    }
}
