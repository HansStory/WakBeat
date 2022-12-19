using System;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIObjectSetting : MonoBehaviour
{
    // 버튼 오브젝트 선언
    public Button ButtonClose;
    public Button ButtonExit;
    public GameObject ButtonKeySetting;
    // 팝업창 호출 시 UI 제어
    private float _duration = 0.15f;
    // 버튼 사운드 --> Global Data에서 가져와서 쓰는 방법으로 변경
    //const int SFX_Home = 1;
    //const int SFX_Setting = 4;
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
    public int SeparationSize = 4;
    public string[] _InBoxValues = new string[4];
    public string[] _OutBoxValues = new string[4];
    private Boolean isStart = false;

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
        //SFXSlider.onValueChanged.AddListener(delegate { setSFXSoundChange(); });
        BGMSlider.onValueChanged.AddListener(delegate { setBGMSoundChange(); });

        // 설정 > 키 설정 > 분리 팝업 버튼 이벤트
        SeparationClose.onClick.AddListener(() => SetButtonClickEvent("SeparationClose"));
        SeparationInBoxs.transform.Find("SeparationInBox1").GetComponent<InputField>().onEndEdit.AddListener(delegate { SetSeparationOffInputField("In", 1); });
        SeparationInBoxs.transform.Find("SeparationInBox1").GetComponent<InputField>().onValueChanged.AddListener((word) => SeparationInBoxs.transform.Find("SeparationInBox1").GetComponent<InputField>().text = Regex.Replace(word, @"[^a-z]", ""));
        SeparationInBoxs.transform.Find("SeparationInBox2").GetComponent<InputField>().onEndEdit.AddListener(delegate { SetSeparationOffInputField("In", 2); });
        SeparationInBoxs.transform.Find("SeparationInBox2").GetComponent<InputField>().onValueChanged.AddListener((word) => SeparationInBoxs.transform.Find("SeparationInBox2").GetComponent<InputField>().text = Regex.Replace(word, @"[^a-z]", ""));
        SeparationInBoxs.transform.Find("SeparationInBox3").GetComponent<InputField>().onEndEdit.AddListener(delegate { SetSeparationOffInputField("In", 3); });
        SeparationInBoxs.transform.Find("SeparationInBox3").GetComponent<InputField>().onValueChanged.AddListener((word) => SeparationInBoxs.transform.Find("SeparationInBox3").GetComponent<InputField>().text = Regex.Replace(word, @"[^a-z]", ""));
        SeparationInBoxs.transform.Find("SeparationInBox4").GetComponent<InputField>().onEndEdit.AddListener(delegate { SetSeparationOffInputField("In", 4); });
        SeparationInBoxs.transform.Find("SeparationInBox4").GetComponent<InputField>().onValueChanged.AddListener((word) => SeparationInBoxs.transform.Find("SeparationInBox4").GetComponent<InputField>().text = Regex.Replace(word, @"[^a-z]", ""));
        SeparationOutBoxs.transform.Find("SeparationOutBox1").GetComponent<InputField>().onEndEdit.AddListener(delegate { SetSeparationOffInputField("Out", 1); });
        SeparationOutBoxs.transform.Find("SeparationOutBox1").GetComponent<InputField>().onValueChanged.AddListener((word) => SeparationOutBoxs.transform.Find("SeparationOutBox1").GetComponent<InputField>().text = Regex.Replace(word, @"[^a-z]", ""));
        SeparationOutBoxs.transform.Find("SeparationOutBox2").GetComponent<InputField>().onEndEdit.AddListener(delegate { SetSeparationOffInputField("Out", 2); });
        SeparationOutBoxs.transform.Find("SeparationOutBox2").GetComponent<InputField>().onValueChanged.AddListener((word) => SeparationOutBoxs.transform.Find("SeparationOutBox2").GetComponent<InputField>().text = Regex.Replace(word, @"[^a-z]", ""));
        SeparationOutBoxs.transform.Find("SeparationOutBox3").GetComponent<InputField>().onEndEdit.AddListener(delegate { SetSeparationOffInputField("Out", 3); });
        SeparationOutBoxs.transform.Find("SeparationOutBox3").GetComponent<InputField>().onValueChanged.AddListener((word) => SeparationOutBoxs.transform.Find("SeparationOutBox3").GetComponent<InputField>().text = Regex.Replace(word, @"[^a-z]", ""));
        SeparationOutBoxs.transform.Find("SeparationOutBox4").GetComponent<InputField>().onEndEdit.AddListener(delegate { SetSeparationOffInputField("Out", 4); });
        SeparationOutBoxs.transform.Find("SeparationOutBox4").GetComponent<InputField>().onValueChanged.AddListener((word) => SeparationOutBoxs.transform.Find("SeparationOutBox4").GetComponent<InputField>().text = Regex.Replace(word, @"[^a-z]", ""));
    }

    // 각 버튼 별 클릭 이벤트 정의
    public void SetButtonClickEvent(string Division)
    {
        if (Division.Equals("Close"))
        {
            // 입력 되 있는 모든 값 가져와서 글로벌에 저장
            SetGlobalValue();

            // 버튼 이벤트 Unlock
            DataManager.dataBackgroundProcActive = true;

            // 창 닫기 버튼 이벤트
            UIElementSetting.Instance.ButtonClickControll("Setting", "Close");

            // 버튼 사운드 출력
            setSoundPrint();
        }
        else if (Division.Equals("Exit"))
        {
            // 입력 되 있는 모든 값 가져와서 글로벌에 저장
            SetGlobalValue();

            // 버튼 사운드 출력
            setSoundPrint();

            // 게임종료 버튼 이벤트
            UIElementSetting.Instance.ButtonClickControll("Setting", "Close");
            //UIManager.Instance.GoPanelMain();
            SoundManager.Instance.ForceAudioStop();

            AppManager.Instance.Quit();
        }
        else if (Division.Equals("IntegrationOn"))
        {
            // 키 설정 > 통합 On 클릭 이벤트
            ButtonKeySetting.transform.Find("ButtonIntegrationOn").gameObject.SetActive(false);
            ButtonKeySetting.transform.Find("ButtonIntegrationOff").gameObject.SetActive(true);
            ButtonKeySetting.transform.Find("ButtonSeparationOn").gameObject.SetActive(true);
            ButtonKeySetting.transform.Find("ButtonSeparationOff").gameObject.SetActive(false);
            
            SeparationGroup.SetActive(true);

            // 버튼 사운드 출력
            setSoundPrint();
        }
        else if (Division.Equals("IntegrationOff"))
        {
            // 키 설정 > 통합 Off 클릭 이벤트
            ButtonKeySetting.transform.Find("ButtonIntegrationOn").gameObject.SetActive(true);
            ButtonKeySetting.transform.Find("ButtonIntegrationOff").gameObject.SetActive(false);
            ButtonKeySetting.transform.Find("ButtonSeparationOn").gameObject.SetActive(false);
            ButtonKeySetting.transform.Find("ButtonSeparationOff").gameObject.SetActive(true);
           
            SeparationGroup.SetActive(false);

            // 버튼 사운드 출력
            
        }
        else if (Division.Equals("SeparationOn"))
        {
            // 키 설정 > 분리 On 클릭 이벤트
            SeparationGroup.SetActive(true);

            // 버튼 사운드 출력
            setSoundPrint();
        }
        else if (Division.Equals("SeparationOff"))
        {
            // 키 설정 > 분리 Off 클릭 이벤트
            ButtonKeySetting.transform.Find("ButtonIntegrationOn").gameObject.SetActive(false);
            ButtonKeySetting.transform.Find("ButtonIntegrationOff").gameObject.SetActive(true);
            ButtonKeySetting.transform.Find("ButtonSeparationOn").gameObject.SetActive(true);
            ButtonKeySetting.transform.Find("ButtonSeparationOff").gameObject.SetActive(false);

            // 키 설정 > 분리 > 최초 팝업 호출 시엔 입력 팝업 미호출
            if (!isStart)
            {
                SeparationGroup.SetActive(true);
            }

            // 버튼 사운드 출력
            setSoundPrint();
        }
        else if (Division.Equals("SeparationClose"))
        {
            // 키 설정 > 분리 > 팝업 닫기 버튼 이벤트
            ButtonKeySetting.transform.Find("ButtonIntegrationOn").gameObject.SetActive(false);
            ButtonKeySetting.transform.Find("ButtonIntegrationOff").gameObject.SetActive(true);
            ButtonKeySetting.transform.Find("ButtonSeparationOn").gameObject.SetActive(true);
            ButtonKeySetting.transform.Find("ButtonSeparationOff").gameObject.SetActive(false);

            // 키 설정 > 분리 안/밖 중 하나만 있을 경우 데이터 제거
            SetCompatibiltityBlankValue();

            SeparationGroup.SetActive(false);

            // 버튼 사운드 출력
            setSoundPrint();
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
        SoundManager.Instance.PlaySoundFX((int)GlobalData.SFX.VolumeControl);
    }

    // 버튼 사운드 출력
    public void setSoundPrint()
    {
        if (!isStart)
        {
            SoundManager.Instance.PlaySoundFX((int)GlobalData.SFX.SettingIn);
        }
    }

    // 포커스 나갈 시 Input Field 비활성화
    public void SetSeparationOffInputField(string Division, int Index)
    {
        if (Division.Equals("In"))
        {
            var inputFieldImage = SeparationInBoxs.transform.Find("Separation" + Division + "Box" + Index).GetComponent<Image>();
            var inputField = SeparationInBoxs.transform.Find("Separation" + Division + "Box" + Index).GetComponent<InputField>();

            inputField.readOnly = true;
            inputFieldImage.sprite = OffBoxImage;

            if (_InBoxValues.Length <= 0) { _InBoxValues = new string[SeparationSize]; }

            _InBoxValues[Index - 1] = inputField.text;
        }
        else
        {
            var inputFieldImage = SeparationOutBoxs.transform.Find("Separation" + Division + "Box" + Index).GetComponent<Image>();
            var inputField = SeparationOutBoxs.transform.Find("Separation" + Division + "Box" + Index).GetComponent<InputField>();

            inputField.readOnly = true;
            inputFieldImage.sprite = OffBoxImage;

            if (_OutBoxValues.Length <= 0) { _OutBoxValues = new string[SeparationSize]; }

            _OutBoxValues[Index - 1] = inputField.text;
        }

        // 중복 값 입력 방지
        SetCompatibiltityReDuplication();
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
    public void GetGlobalValue(Boolean flag)
    {
        string KeyDivision = "";

        // 저장 파일 불러오기 성공 시
        if (flag)
        {
            KeyDivision = GlobalState.Instance.UserData.data.settingData.keyDivision.Equals("Integration") ? "IntegrationOff" : "SeparationOff";

            SFXSlider.value = GlobalState.Instance.UserData.data.settingData.SFXValue;
            BGMSlider.value = GlobalState.Instance.UserData.data.settingData.BGMValue;
            _InBoxValues = GlobalState.Instance.UserData.data.settingData.innerOperationKey;
            _OutBoxValues = GlobalState.Instance.UserData.data.settingData.outerOperationKey;

            for (int i = 0; i < GlobalState.Instance.UserData.data.settingData.innerOperationKey.Length; i++)
            {
                if (null != GlobalState.Instance.UserData.data.settingData.innerOperationKey[i] && !"".Equals(GlobalState.Instance.UserData.data.settingData.innerOperationKey[i]))
                {
                    SeparationInBoxs.transform.Find("SeparationInBox" + (i + 1)).GetComponent<InputField>().text = GlobalState.Instance.UserData.data.settingData.innerOperationKey[i];
                }
                SeparationInBoxs.transform.Find("SeparationInBox" + (i + 1)).GetComponent<InputField>().readOnly = true;
                SeparationInBoxs.transform.Find("SeparationInBox" + (i + 1)).GetComponent<Image>().sprite = OffBoxImage;
            }

            for (int i = 0; i < GlobalState.Instance.UserData.data.settingData.outerOperationKey.Length; i++)
            {
                if (null != GlobalState.Instance.UserData.data.settingData.outerOperationKey[i] && !"".Equals(GlobalState.Instance.UserData.data.settingData.outerOperationKey[i]))
                {
                    SeparationOutBoxs.transform.Find("SeparationOutBox" + (i + 1)).GetComponent<InputField>().text = GlobalState.Instance.UserData.data.settingData.outerOperationKey[i];
                }
                SeparationOutBoxs.transform.Find("SeparationOutBox" + (i + 1)).GetComponent<InputField>().readOnly = true;
                SeparationOutBoxs.transform.Find("SeparationOutBox" + (i + 1)).GetComponent<Image>().sprite = OffBoxImage;
            }
        } 
        else
        {
            KeyDivision = "IntegrationOff";

            for (int Index = 0; Index < DataManager.dataInnerOperationKey.Length; Index++)
            {
                var inputInFieldImage = SeparationInBoxs.transform.Find("SeparationInBox" + (Index + 1)).GetComponent<Image>();
                var inputInField = SeparationInBoxs.transform.Find("SeparationInBox" + (Index + 1)).GetComponent<InputField>();

                inputInField.readOnly = true;
                inputInFieldImage.sprite = OffBoxImage;

                var inputOutFieldImage = SeparationOutBoxs.transform.Find("SeparationOutBox" + (Index + 1)).GetComponent<Image>();
                var inputOutField = SeparationOutBoxs.transform.Find("SeparationOutBox" + (Index + 1)).GetComponent<InputField>();

                inputOutField.readOnly = true;
                inputOutFieldImage.sprite = OffBoxImage;
            }
        }

        // 스타트 구분 > true > 첫 호출 시 버튼 사운드 미출력
        isStart = true;

        SetButtonClickEvent(KeyDivision);

        // 스타트 구분 > false > 사운드 출력
        isStart = false;
    }

    // 글로벌 변수 저장
    public void SetGlobalValue()
    {
        float SFXValue = SFXSlider.value;
        float BGMValue = BGMSlider.value;
        string KeyDivision = "";

        if(ButtonKeySetting.transform.Find("ButtonSeparationOn").gameObject.activeSelf)
        {
            Boolean SeparationYn = false;
            for(int i = 0; i < _InBoxValues.Length; i++)
            {
                if(null != _InBoxValues[i] && !"".Equals(_InBoxValues[i]))
                {
                    SeparationYn = true;
                }
            }
            for (int i = 0; i < _OutBoxValues.Length; i++)
            {
                if (null != _OutBoxValues[i] && !"".Equals(_OutBoxValues[i]))
                {
                    SeparationYn = true;
                }
            }

            if (SeparationYn)
            {
                KeyDivision = "Separation";
            } else
            {
                KeyDivision = "Integration";
            }
        }
        else
        {
            KeyDivision = "Integration";
        }

        // 설정 데이터 변경
        DataManager.dataKeyDivision = KeyDivision;
        DataManager.dataBGMValue = BGMValue;
        DataManager.dataSFXValue = SFXValue;
        DataManager.dataInnerOperationKey = _InBoxValues;
        DataManager.dataOuterOperationKey = _OutBoxValues;

        // 설정 데이터 변경 후 파일 저장
        DataManager.SaveUserData();
    }

    // 데이터 정합성 검증
    public void SetCompatibiltityBlankValue()
    {
        // 키 설정 > 분리 안/밖 중 하나만 있을 경우 값 제거
        for (int InIndex = 0; InIndex < GlobalState.Instance.UserData.data.settingData.innerOperationKey.Length; InIndex++)
        {
            for (int OutIndex = 0; OutIndex < GlobalState.Instance.UserData.data.settingData.outerOperationKey.Length; OutIndex++)
            {
                if(InIndex == OutIndex)
                {
                    if (!_InBoxValues[InIndex].Equals("") && _OutBoxValues[OutIndex].Equals(""))
                    {
                        _InBoxValues[InIndex] = "";
                        SeparationInBoxs.transform.Find("SeparationInBox" + (InIndex + 1)).GetComponent<InputField>().text = "";
                    }
                    if (_InBoxValues[InIndex].Equals("") && !_OutBoxValues[OutIndex].Equals(""))
                    {
                        _OutBoxValues[OutIndex] = "";
                        SeparationOutBoxs.transform.Find("SeparationOutBox" + (OutIndex + 1)).GetComponent<InputField>().text = "";
                    }
                }
            }
        }
    }

    // 데이터 정합성 검증
    public void SetCompatibiltityReDuplication()
    {
        // 키 설정 > 분리 같은 키 중복 입력 금지
        for(int InIndex = 0; InIndex < GlobalState.Instance.UserData.data.settingData.innerOperationKey.Length; InIndex++)
        {
            for (int OutIndex = 0; OutIndex < GlobalState.Instance.UserData.data.settingData.outerOperationKey.Length; OutIndex++)
            {
                if (_InBoxValues[InIndex] == _OutBoxValues[OutIndex])
                {
                    _InBoxValues[InIndex] = "";
                    _OutBoxValues[OutIndex] = "";
                    SeparationInBoxs.transform.Find("SeparationInBox" + (InIndex + 1)).GetComponent<InputField>().text = "";
                    SeparationOutBoxs.transform.Find("SeparationOutBox" + (OutIndex + 1)).GetComponent<InputField>().text = "";
                }
                if(InIndex != OutIndex)
                {
                    if(_InBoxValues[InIndex] == _InBoxValues[OutIndex])
                    {
                        _InBoxValues[InIndex] = "";
                        _InBoxValues[OutIndex] = "";
                        SeparationInBoxs.transform.Find("SeparationInBox" + (InIndex + 1)).GetComponent<InputField>().text = "";
                        SeparationInBoxs.transform.Find("SeparationInBox" + (OutIndex + 1)).GetComponent<InputField>().text = "";
                    }
                    if(_OutBoxValues[InIndex] == _OutBoxValues[OutIndex])
                    {
                        _OutBoxValues[InIndex] = "";
                        _OutBoxValues[OutIndex] = "";
                        SeparationOutBoxs.transform.Find("SeparationOutBox" + (InIndex + 1)).GetComponent<InputField>().text = "";
                        SeparationOutBoxs.transform.Find("SeparationOutBox" + (OutIndex + 1)).GetComponent<InputField>().text = "";
                    }
                }
            }
        }
    }

    // 팝업 창 호출 시 UI 출력 제어
    private void OnEnable()
    {
        // 사이즈 0부터 시작
        this.transform.localScale = Vector3.zero;
        // 1까지 커지면서 시간은 0.2초, 변환 시 큐빅 형태로 등장
        this.transform.DOScale(Vector3.one, _duration).SetEase(Ease.InCubic);
    }

    void Start()
    {
        // 버튼 이벤트 설정
        SetButtonEvent();

        // 글로벌 변수 화면 적용
        GetGlobalValue(DataManager.dataFileYn);
    }

    void Update()
    {
        // Input Field 포커스 시 활성화
        SetSeparationOnInputField();
    }
}
