using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Mono.Cecil;

public class UIObjectShop : MonoBehaviour
{
    /*
    [SerializeField] private ShopInfo ShopInfo;
    */
    public Button ButtonClose;
    public GameObject TabObject;
    private int SkinCount = DataManager.dataSkinCount;
    private int SkillCount = DataManager.dataSkillCount;
    // 팝업창 호출 시 UI 제어
    private float _duration = 0.15f;
    // 버튼 사운드
    const int SFX_Home = 1;
    const int SFX_Setting = 4;
    // 스킨 아이템 오브젝트
    public GameObject SkinGroup;
    [SerializeField] private GameObject SkinPrefab;
    [SerializeField] private Transform SkinPanel;
    // 스킬 아이템 오브젝트
    public GameObject SkillGroup;
    [SerializeField] private GameObject SkillPrefab;
    [SerializeField] private Transform SkillPanel;
    // 비디오 아이템 오브젝트
    public GameObject VideoGroup;
    // 글로벌 데이터 용 전역변수
    public string[] _SkinUnLockYn = new string[DataManager.dataSkinCount];
    public string[] _SkinUsingYn = new string[DataManager.dataSkinCount];
    public string[] _SkillUnLockYn = new string[DataManager.dataSkillCount];
    public string[] _SkillUsingYn = new string[DataManager.dataSkillCount];

    private Boolean isStart = false;

    public void SetPrefab()
    {
        // 스킨 데이터 가져오기
        var SkinTitle = GlobalData.Instance.Shop.SkinTitle;
        var SkinIcon = GlobalData.Instance.Shop.SkinIcon;

        // 스킬 데이터 가져오기
        var SkillTitle = GlobalData.Instance.Shop.SkillTitle;
        var SkillIcon = GlobalData.Instance.Shop.SkillIcon;
        var SkillExplanation = GlobalData.Instance.Shop.SkillExplanation;
        var SkillLockExplanation = GlobalData.Instance.Shop.SkillLockExplanation;

        // 스킨 프리팹 복제
        for(int SkinIndex = 0; SkinIndex < SkinTitle.Length; SkinIndex++)
        {
            var _Skin = (GameObject)Instantiate(SkinPrefab, SkinPanel);
            var SkinInfo = _Skin.GetComponent<UIObjectShopSkinItem>();

            if (SkinInfo)
            {
                if (SkinTitle.Length == SkinIcon.Length)
                {
                    SkinInfo.name = "Skin_Prefab_" + SkinIndex;
                    SkinInfo.SkinTitleSprite = SkinTitle[SkinIndex];
                    SkinInfo.SkinIconSprite = SkinIcon[SkinIndex];
                    SkinInfo.SkinIndex = SkinIndex;
                    // 스킨 버튼 이벤트 호출
                    //SkinInfo.SetButtonEvent();
                    SkinInfo.gameObject.SetActive(true);
                }
            }
        }

        // 스킬 프리팹 복제
        for(int SkillIndex = 0; SkillIndex < SkillTitle.Length; SkillIndex++)
        {
            var _Skill = (GameObject)Instantiate(SkillPrefab, SkillPanel);
            var SkillInfo = _Skill.GetComponent<UIObjectShopSkillItem>();

            if (SkillInfo)
            {
                if(SkillTitle.Length == SkillIcon.Length 
                    && SkillTitle.Length == SkillExplanation.Length 
                        && SkillTitle.Length == SkillLockExplanation.Length)
                {
                    SkillInfo.name = "Skill_Prefab_" + SkillIndex;
                    SkillInfo.SkillTitleSprite = SkillTitle[SkillIndex];
                    SkillInfo.SkillIconSprite = SkillIcon[SkillIndex];
                    SkillInfo.SkillExplanationSprite = SkillExplanation[SkillIndex];
                    SkillInfo.SkillLockExplanationSprite = SkillLockExplanation[SkillIndex];
                    SkillInfo.SkillIndex = SkillIndex;
                    // 스킬 버튼 이벤트 호출
                    //SkillInfo.SetButtonEvent();
                    SkillInfo.gameObject.SetActive(true);
                }
            }
        }
    }

    // 각 버튼 별 이벤트 정의
    public void SetButtonEvent()
    {
        // 상점 버튼 이벤트
        ButtonClose.onClick.AddListener(() => SetButtonClickEvent("Close"));
        TabObject.transform.Find("TabSkin").Find("TabOn").GetComponent<Button>().onClick.AddListener(() => SetButtonClickEvent("SkinOn"));
        TabObject.transform.Find("TabSkin").Find("TabOff").GetComponent<Button>().onClick.AddListener(() => SetButtonClickEvent("SkinOff"));
        TabObject.transform.Find("TabSkill").Find("TabOn").GetComponent<Button>().onClick.AddListener(() => SetButtonClickEvent("SkillOn"));
        TabObject.transform.Find("TabSkill").Find("TabOff").GetComponent<Button>().onClick.AddListener(() => SetButtonClickEvent("SkillOff"));
        TabObject.transform.Find("TabVideo").Find("TabOn").GetComponent<Button>().onClick.AddListener(() => SetButtonClickEvent("VideoOn"));
        TabObject.transform.Find("TabVideo").Find("TabOff").GetComponent<Button>().onClick.AddListener(() => SetButtonClickEvent("VideoOff"));

        // 스킨 버튼 이벤트 > 스킬 버튼은 생성자에 들어가 있음 > 스킨은 하나만 On 되야 하는 구문 때문에 이쪽에서 작성
        for(int Index = 0; Index < SkinCount; Index++)
        {
            var Prefab = SkinGroup.transform.Find("SkinItems").Find("Viewport").Find("Content").Find("Skin_Prefab_" + Index).GetComponent<UIObjectShopSkinItem>();
            int PrefabIndex = Prefab.SkinIndex;

            SkinGroup.transform.Find("SkinItems").Find("Viewport").Find("Content").Find("Skin_Prefab_" + Index).Find("ItemButtons").Find("ButtonBuy").GetComponent<Button>().onClick.AddListener(() => SetSkinButtonEvent(PrefabIndex, "Buy"));
            SkinGroup.transform.Find("SkinItems").Find("Viewport").Find("Content").Find("Skin_Prefab_" + Index).Find("ItemButtons").Find("ButtonOn").GetComponent<Button>().onClick.AddListener(() => SetSkinButtonEvent(PrefabIndex, "On"));
            SkinGroup.transform.Find("SkinItems").Find("Viewport").Find("Content").Find("Skin_Prefab_" + Index).Find("ItemButtons").Find("ButtonOff").GetComponent<Button>().onClick.AddListener(() => SetSkinButtonEvent(PrefabIndex, "Off"));
        }

        // 2022.12.24 > 스킬 사용도 단수로 변경으로 인해 스킬도 하나만 On 되어야 하므로 이쪽에서 작성
        for(int Index = 0; Index < SkillCount; Index++)
        {
            var Prefab = SkillGroup.transform.Find("SkillItems").Find("Viewport").Find("Content").Find("Skill_Prefab_" + Index).GetComponent<UIObjectShopSkillItem>();
            int PrefabIndex = Prefab.SkillIndex;

            SkillGroup.transform.Find("SkillItems").Find("Viewport").Find("Content").Find("Skill_Prefab_" + Index).Find("Open").Find("SkillButton").Find("ButtonOn").GetComponent<Button>().onClick.AddListener(() => SetSkillButtonEvent(PrefabIndex, "On"));
            SkillGroup.transform.Find("SkillItems").Find("Viewport").Find("Content").Find("Skill_Prefab_" + Index).Find("Open").Find("SkillButton").Find("ButtonOff").GetComponent<Button>().onClick.AddListener(() => SetSkillButtonEvent(PrefabIndex, "Off"));
            SkillGroup.transform.Find("SkillItems").Find("Viewport").Find("Content").Find("Skill_Prefab_" + Index).Find("Lock").Find("SkillButton").Find("ButtonLock").GetComponent<Button>().onClick.AddListener(() => SetSkillButtonEvent(PrefabIndex, "Lock"));
        }
    }

    // 각 버튼 별 클릭 이벤트 정의
    public void SetButtonClickEvent(string Division)
    {
        if (Division.Equals("Close"))
        {
            // 입력 되 있는 모든 값 가져와서 글로벌에 저장
            SetGlobalValue();

            // 상점 창 초기화
            SetButtonClickEvent("SkinOn");

            // 버튼 이벤트 Unlock
            DataManager.dataBackgroundProcActive = true;

            // 창 닫기 버튼 이벤트
            UIElementSetting.Instance.ButtonClickControll("Shop", "Close");

            // 버튼 사운드 출력
            setSoundPrint();
        }
        else if (Division.Equals("SkinOn") || Division.Equals("SkinOff"))
        {
            // 상점 > 스킨 버튼 이벤트
            SkinGroup.SetActive(true);
            SkillGroup.SetActive(false);
            VideoGroup.SetActive(false);
            TabObject.transform.Find("TabSkin").Find("TabOn").gameObject.SetActive(true);
            TabObject.transform.Find("TabSkin").Find("TabOff").gameObject.SetActive(false);
            TabObject.transform.Find("TabSkill").Find("TabOn").gameObject.SetActive(false);
            TabObject.transform.Find("TabSkill").Find("TabOff").gameObject.SetActive(true);
            TabObject.transform.Find("TabVideo").Find("TabOn").gameObject.SetActive(false);
            TabObject.transform.Find("TabVideo").Find("TabOff").gameObject.SetActive(true);

            // 버튼 사운드 출력
            setSoundPrint();
        }
        else if (Division.Equals("SkillOn") || Division.Equals("SkillOff"))
        {
            // 상점 > 스킬 버튼 이벤트
            SkinGroup.SetActive(false);
            SkillGroup.SetActive(true);
            VideoGroup.SetActive(false);
            TabObject.transform.Find("TabSkin").Find("TabOn").gameObject.SetActive(false);
            TabObject.transform.Find("TabSkin").Find("TabOff").gameObject.SetActive(true);
            TabObject.transform.Find("TabSkill").Find("TabOn").gameObject.SetActive(true);
            TabObject.transform.Find("TabSkill").Find("TabOff").gameObject.SetActive(false);
            TabObject.transform.Find("TabVideo").Find("TabOn").gameObject.SetActive(false);
            TabObject.transform.Find("TabVideo").Find("TabOff").gameObject.SetActive(true);

            // 버튼 사운드 출력
            setSoundPrint();
        }
        else if (Division.Equals("VideoOn") || Division.Equals("VideoOff"))
        {
            // 상점 > 비디오 버튼 이벤트
            SkinGroup.SetActive(false);
            SkillGroup.SetActive(false);
            VideoGroup.SetActive(true);
            TabObject.transform.Find("TabSkin").Find("TabOn").gameObject.SetActive(false);
            TabObject.transform.Find("TabSkin").Find("TabOff").gameObject.SetActive(true);
            TabObject.transform.Find("TabSkill").Find("TabOn").gameObject.SetActive(false);
            TabObject.transform.Find("TabSkill").Find("TabOff").gameObject.SetActive(true);
            TabObject.transform.Find("TabVideo").Find("TabOn").gameObject.SetActive(true);
            TabObject.transform.Find("TabVideo").Find("TabOff").gameObject.SetActive(true);

            // 버튼 사운드 출력
            setSoundPrint();
        }
    }

    // 버튼 사운드 출력
    public void setSoundPrint()
    {
        if (!isStart)
        {
            SoundManager.Instance.PlaySoundFX((int)GlobalData.SFX.SettingIn);
        }
    }

    // 상점 > 스킨 > 각 버튼 별 이벤트 정의
    public void SetSkinButtonEvent(int Index, string Division)
    {
        if (Division.Equals("Buy"))
        {
            // 해금 미구현 > 차후 필요 스테이지 등 생기면 버튼 Active 활성화
            SetUnLockSkin(Index);
        }
        else if (Division.Equals("On"))
        {
            // 사용 > 미사용
            SkinGroup.transform.Find("SkinItems").Find("Viewport").Find("Content").Find("Skin_Prefab_" + Index).Find("ItemButtons").Find("ButtonOn").gameObject.SetActive(false);
            SkinGroup.transform.Find("SkinItems").Find("Viewport").Find("Content").Find("Skin_Prefab_" + Index).Find("ItemButtons").Find("ButtonOff").gameObject.SetActive(true);
        }
        else if (Division.Equals("Off"))
        {
            // 미사용 > 사용 > 전환 시 나머지 스킨 미사용으로 전환
            for(int i = 0; i < SkinCount; i++)
            {
                if(i != Index)
                {
                    SkinGroup.transform.Find("SkinItems").Find("Viewport").Find("Content").Find("Skin_Prefab_" + i).Find("ItemButtons").Find("ButtonOn").gameObject.SetActive(false);
                    SkinGroup.transform.Find("SkinItems").Find("Viewport").Find("Content").Find("Skin_Prefab_" + i).Find("ItemButtons").Find("ButtonOff").gameObject.SetActive(true);
                } 
                else
                {
                    SkinGroup.transform.Find("SkinItems").Find("Viewport").Find("Content").Find("Skin_Prefab_" + i).Find("ItemButtons").Find("ButtonOn").gameObject.SetActive(true);
                    SkinGroup.transform.Find("SkinItems").Find("Viewport").Find("Content").Find("Skin_Prefab_" + i).Find("ItemButtons").Find("ButtonOff").gameObject.SetActive(false);
                }
            }
        }

        // 모두 Off 일때 0번 스킨 On
        SetSkinButtonDefault();

        SoundManager.Instance.PlaySoundFX((int)GlobalData.SFX.SettingIn);
    }

    // 스킨 버튼 모두 Off 일때 0번 스킨 On
    public void SetSkinButtonDefault()
    {
        Boolean flag = false;
        for (int i = 0; i < SkinCount; i++)
        {
            if (SkinGroup.transform.Find("SkinItems").Find("Viewport").Find("Content").Find("Skin_Prefab_" + i).Find("ItemButtons").Find("ButtonOn").gameObject.activeSelf
                && !SkinGroup.transform.Find("SkinItems").Find("Viewport").Find("Content").Find("Skin_Prefab_" + i).Find("ItemButtons").Find("ButtonOff").gameObject.activeSelf)
            {
                flag = true;
                break;
            }
        }

        if (!flag)
        {
            SkinGroup.transform.Find("SkinItems").Find("Viewport").Find("Content").Find("Skin_Prefab_0").Find("ItemButtons").Find("ButtonOn").gameObject.SetActive(true);
            SkinGroup.transform.Find("SkinItems").Find("Viewport").Find("Content").Find("Skin_Prefab_0").Find("ItemButtons").Find("ButtonOff").gameObject.SetActive(false);
        }
    }

    // 스킨 잠금 해제
    public void SetUnLockSkin(int Index)
    {
        SkinGroup.transform.Find("SkinItems").Find("Viewport").Find("Content").Find("Skin_Prefab_" + Index).Find("ItemButtons").Find("ButtonBuy").gameObject.SetActive(false);
    }

    // 상점 > 스킬 > 각 버튼 별 이벤트 정의
    public void SetSkillButtonEvent(int Index, string Division)
    {
        if (Division.Equals("Lock"))
        {
            SetUnLockSkill(Index);
        }
        else if (Division.Equals("On"))
        {
            SkillGroup.transform.Find("SkillItems").Find("Viewport").Find("Content").Find("Skill_Prefab_" + Index).Find("Open").Find("SkillButton").Find("ButtonOn").gameObject.SetActive(false);
            SkillGroup.transform.Find("SkillItems").Find("Viewport").Find("Content").Find("Skill_Prefab_" + Index).Find("Open").Find("SkillButton").Find("ButtonOff").gameObject.SetActive(true);

            switch(Index)
            {
                case 0: GlobalState.Instance.UseBonusHP = false; break;
                case 1: GlobalState.Instance.UseBarrier = false; break;
                case 2: GlobalState.Instance.UseNewGaMe = false; break;
                case 3: GlobalState.Instance.ShowDodge = false; break;
                case 4: GlobalState.Instance.AutoMode = false; break;
                default: break;
            }

            GlobalState.Instance.UsedItems = "없음";
        }
        else if (Division.Equals("Off"))
        {
            // 미사용 > 사용 > 전환 시 나머지 스킨 미사용으로 전환
            for (int i = 0; i < SkillCount; i++)
            {
                if (i != Index)
                {
                    SkillGroup.transform.Find("SkillItems").Find("Viewport").Find("Content").Find("Skill_Prefab_" + i).Find("Open").Find("SkillButton").Find("ButtonOn").gameObject.SetActive(false);
                    SkillGroup.transform.Find("SkillItems").Find("Viewport").Find("Content").Find("Skill_Prefab_" + i).Find("Open").Find("SkillButton").Find("ButtonOff").gameObject.SetActive(true);
                }
                else
                {
                    SkillGroup.transform.Find("SkillItems").Find("Viewport").Find("Content").Find("Skill_Prefab_" + i).Find("Open").Find("SkillButton").Find("ButtonOn").gameObject.SetActive(true);
                    SkillGroup.transform.Find("SkillItems").Find("Viewport").Find("Content").Find("Skill_Prefab_" + i).Find("Open").Find("SkillButton").Find("ButtonOff").gameObject.SetActive(false);

                    // 인 게임에서 사용 될 현 사용 스킬 명 저장
                    string _SkillName = "";
                    switch(i)
                    {
                        case 0: 
                            _SkillName = "까방권";
                            GlobalState.Instance.UseBonusHP = true;
                            break;
                        case 1: 
                            _SkillName = "일시무적";
                            GlobalState.Instance.UseBarrier = true;
                            break;
                        case 2: 
                            _SkillName = "뉴가메";
                            GlobalState.Instance.UseNewGaMe = true;
                            break;
                        case 3: 
                            _SkillName = "분석안";
                            GlobalState.Instance.ShowDodge = true;
                            break;
                        case 4: 
                            _SkillName = "자율주행";
                            GlobalState.Instance.AutoMode = true;
                            break;
                        default: _SkillName = "없음"; break;

                    }
                    GlobalState.Instance.UsedItems = _SkillName;
                }
            }
        }

        SoundManager.Instance.PlaySoundFX((int)GlobalData.SFX.SettingIn);
    }

    // 스킬 잠금 해제
    public void SetUnLockSkill(int Index)
    {
        SkillGroup.transform.Find("SkillItems").Find("Viewport").Find("Content").Find("Skill_Prefab_" + Index).Find("Lock").gameObject.SetActive(false);
    }

    // 클리어 스테이지 수에 따라 혹은 잠금 해제한 스킬 잠금 영역 비활성화
    public void SetUnLockSkill()
    {
        for (int Index = 0; Index < SkillCount; Index++)
        {
            if (DataManager.dataClearStageCount >= DataManager.dataSkillUnLockCondition[Index] || _SkillUnLockYn[Index].Equals("Y"))
            {
                SkillGroup.transform.Find("SkillItems").Find("Viewport").Find("Content").Find("Skill_Prefab_" + Index).Find("Lock").gameObject.SetActive(false);
            }
        }
    }

    // 글로벌 변수 화면 세팅
    public void GetGlobalValue()
    {
        Boolean _ShopCompulsionActive = DataManager.dataShopCompulsionActive;

        // 강제 해금 On Off 처리 > True : 강제 해금 사용 / False : 강제 해금 미사용
        if (_ShopCompulsionActive)
        {
            // 스킨은 차후 구현
            for (int Index = 0; Index < SkinCount; Index++)
            {
                SkinGroup.transform.Find("SkinItems").Find("Viewport").Find("Content").Find("Skin_Prefab_" + Index).Find("ItemButtons").Find("ButtonBuy").gameObject.SetActive(false);
            }

            // 스킬 강제 해금 미사용 처리
            for (int Index = 0; Index < SkillCount; Index++)
            {
                SkillGroup.transform.Find("SkillItems").Find("Viewport").Find("Content").Find("Skill_Prefab_" + Index).Find("Lock").Find("SkillButton").Find("ButtonLock").gameObject.SetActive(false);
            }
        } 

        // 스테이지 클리어 수
        int _ClearStageCount = (int)DataManager.dataClearStageCount;
        // 스킨 글로벌 변수
        _SkinUnLockYn = DataManager.dataSkinUnLockYn;
        _SkinUsingYn = DataManager.dataSkinUsingYn;
        // 스킬 글로벌 변수
        _SkillUnLockYn = DataManager.dataSkillUnLockYn;
        _SkillUsingYn = DataManager.dataSkillUsingYn;

        // 이미 구매한 스킨의 경우 버튼 비활성화
        for(int Index = 0; Index < SkinCount; Index++)
        {
            if (_SkinUnLockYn[Index].Equals("Y"))
            {
                SkinGroup.transform.Find("SkinItems").Find("Viewport").Find("Content").Find("Skin_Prefab_" + Index).Find("ItemButtons").Find("ButtonBuy").gameObject.SetActive(false);
            }
        }

        // 스킬 해금 제어
        SetUnLockSkill();

        // 스킨 사용 반영
        for (int Index = 0; Index < SkinCount; Index++)
        {
            if (null != _SkinUsingYn[Index] && _SkinUsingYn[Index].Equals("Y"))
            {
                SkinGroup.transform.Find("SkinItems").Find("Viewport").Find("Content").Find("Skin_Prefab_" + Index).Find("ItemButtons").Find("ButtonOn").gameObject.SetActive(true);
                SkinGroup.transform.Find("SkinItems").Find("Viewport").Find("Content").Find("Skin_Prefab_" + Index).Find("ItemButtons").Find("ButtonOff").gameObject.SetActive(false);
            }
        }

        // 스킬 사용 반영
        for (int Index = 0; Index < SkillCount; Index++)
        {
            if (null != _SkillUsingYn && _SkillUsingYn[Index].Equals("Y"))
            {
                SkillGroup.transform.Find("SkillItems").Find("Viewport").Find("Content").Find("Skill_Prefab_" + Index).Find("Open").Find("SkillButton").Find("ButtonOn").gameObject.SetActive(true);
                SkillGroup.transform.Find("SkillItems").Find("Viewport").Find("Content").Find("Skill_Prefab_" + Index).Find("Open").Find("SkillButton").Find("ButtonOff").gameObject.SetActive(false);
            }
        }

        // 스타트 구분 > true > 첫 호출 시 버튼 사운드 미출력
        isStart = true;

        // 최초 상점 텝 활성화
        SetButtonClickEvent("SkinOn");

        // 스타트 구분 > false > 사운드 출력
        isStart = false;
    }

    // 글로벌 변수 저장
    public void SetGlobalValue()
    {
        if (_SkinUnLockYn.Length <= 0) { _SkinUnLockYn = new string[DataManager.dataSkinCount]; }
        if (_SkinUsingYn.Length <= 0) { _SkinUsingYn = new string[DataManager.dataSkinCount]; }
        if (_SkillUnLockYn.Length <= 0) { _SkillUnLockYn = new string[DataManager.dataSkillCount]; }
        if (_SkillUsingYn.Length <= 0) { _SkillUsingYn = new string[DataManager.dataSkillCount]; }

        // 글로벌 저장 용 스킨 정보 생성
        for (int Index = 0; Index < DataManager.dataSkinCount; Index++)
        {
            // 스킨은 아직 해금 정보 안들어가있음
            if(SkinGroup.transform.Find("SkinItems").Find("Viewport").Find("Content").Find("Skin_Prefab_" + Index).Find("ItemButtons").Find("ButtonBuy").gameObject.activeSelf)
            {
                _SkinUnLockYn[Index] = "N";
            } 
            else
            {
                _SkinUnLockYn[Index] = "Y";
            }

            // 스킨 사용 여부
            if (SkinGroup.transform.Find("SkinItems").Find("Viewport").Find("Content").Find("Skin_Prefab_" + Index).Find("ItemButtons").Find("ButtonOn").gameObject.activeSelf
                && !SkinGroup.transform.Find("SkinItems").Find("Viewport").Find("Content").Find("Skin_Prefab_" + Index).Find("ItemButtons").Find("ButtonOff").gameObject.activeSelf)
            {
                _SkinUsingYn[Index] = "Y";
            }
            else
            {
                _SkinUsingYn[Index] = "N";
            }
        }

        // 글로벌 저장 용 스킬 정보 생성
        for (int Index = 0; Index < DataManager.dataSkillCount; Index++)
        {
            // 스킬 잠금 해제 여부
            if(SkillGroup.transform.Find("SkillItems").Find("Viewport").Find("Content").Find("Skill_Prefab_" + Index).Find("Lock").gameObject.activeSelf)
            {
                _SkillUnLockYn[Index] = "N";
            }
            else
            {
                _SkillUnLockYn[Index] = "Y";
            }
            
            // 스킬 사용 여부
            if(SkillGroup.transform.Find("SkillItems").Find("Viewport").Find("Content").Find("Skill_Prefab_" + Index).Find("Open").Find("SkillButton").Find("ButtonOn").gameObject.activeSelf
                && !SkillGroup.transform.Find("SkillItems").Find("Viewport").Find("Content").Find("Skill_Prefab_" + Index).Find("Open").Find("SkillButton").Find("ButtonOff").gameObject.activeSelf)
            {
                _SkillUsingYn[Index] = "Y";
            }
            else
            {
                _SkillUsingYn[Index] = "N";
            }
        }

        // 설정 데이터 변경
        DataManager.dataSkinUnLockYn = _SkinUnLockYn;
        DataManager.dataSkinUsingYn = _SkinUsingYn;
        DataManager.dataSkillUnLockYn = _SkillUnLockYn;
        DataManager.dataSkillUsingYn = _SkillUsingYn;

        // 설정 데이터 변경 후 파일 저장
        DataManager.SaveUserData();
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
        // 스킨 / 스킬 프리펩 인스턴스화
        SetPrefab();

        // 버튼 이벤트 설정
        SetButtonEvent();

        // 글로벌 변수 화면 적용
        GetGlobalValue();
    }

    void Update()
    {
        // 스킨 해금 제어


        // 스킬 해금 제어
        SetUnLockSkill();
    }
}
