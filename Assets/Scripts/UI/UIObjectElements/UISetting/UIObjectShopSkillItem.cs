using System;
using UnityEngine;
using UnityEngine.UI;

public class UIObjectShopSkillItem : MonoBehaviour
{
    // 내부 오브젝트 선언
    public Button buttonOn;
    public Button buttonOff;
    public Button buttonLock;
    public GameObject LockObject;

    // 버튼 사운드 --> Global Data에서 가져와서 쓰는 방법으로 변경
    //const int SFX_Home = 1;
    //const int SFX_Setting = 4;

    public UIObjectShop UIObjectShop { get; set; }

    // 스킬 정보 > 타이틀
    public Sprite _TitleSprite;
    [SerializeField] private Image SkillTitleImage;
    public Sprite SkillTitleSprite
    {
        get { return _TitleSprite; }
        set
        {
            _TitleSprite = value;
            SkillTitleImage.sprite = _TitleSprite;
            SkillTitleImage.SetNativeSize();
        }
    }

    // 스킬 정보 > 설명
    public Sprite _ExplanationSprite;
    [SerializeField] private Image SkillExplanationImage;
    public Sprite SkillExplanationSprite
    {
        get { return _ExplanationSprite; }
        set
        {
            _ExplanationSprite = value;
            SkillExplanationImage.sprite = _ExplanationSprite;
            SkillExplanationImage.SetNativeSize();
        }
    }

    // 스킬 정보 > 아이콘
    public Sprite _IconSprite;
    [SerializeField] private Image SkillIconImage;
    public Sprite SkillIconSprite
    {
        get { return _IconSprite; }
        set
        {
            _IconSprite = value;
            SkillIconImage.sprite = _IconSprite;
            SkillIconImage.SetNativeSize();
        }
    }

    // 스킬 정보 > 잠금 > 설명
    public Sprite _LockExplanationSprite;
    [SerializeField] private Image SkillLockExplanationImage;
    public Sprite SkillLockExplanationSprite
    {
        get { return _LockExplanationSprite; }
        set
        {
            _LockExplanationSprite = value;
            SkillLockExplanationImage.sprite = _LockExplanationSprite;
            SkillLockExplanationImage.SetNativeSize();
        }
    }

    // 스킨 항목 정보 > 인덱스
    private int _SkillIndex;
    public int SkillIndex
    {
        get { return _SkillIndex; }
        set
        {
            _SkillIndex = value;
        }
    }

    // 버튼 이벤트 세팅
    public void SetButtonEvent()
    {
        buttonOn.onClick.AddListener(On);
        buttonOff.onClick.AddListener(Off);
        buttonLock.onClick.AddListener(Unlock);
    }

    // 스킬 언락
    private void Unlock()
    {
        LockObject.SetActive(false);

        // 버튼 사운드 출력
        SoundManager.Instance.PlaySoundFX((int)GlobalData.SFX.SettingIn);
    }

    // 스킬 사용 > 미사용
    private void On()
    {
        buttonOn.gameObject.SetActive(false);
        buttonOff.gameObject.SetActive(true);

        var state = GlobalState.Instance;
        switch (SkillIndex)
        {
            case 0:
                state.UseBonusHP = false;
                break;
            case 1:
                state.UseBarrier = false;
                break;
            case 2:
                state.UseNewGaMe = false;
                break;
            case 3:
                state.ShowDodge = false;
                break;
            case 4:
                state.AutoMode = false;
                Debug.Log(state.AutoMode);
                break;
        }

        // 버튼 사운드 출력
        SoundManager.Instance.PlaySoundFX((int)GlobalData.SFX.SettingIn);
    }

    // 스킬 미사용 > 사용
    private void Off()
    {
        buttonOn.gameObject.SetActive(true);
        buttonOff.gameObject.SetActive(false);

        var state = GlobalState.Instance;
        switch (SkillIndex)
        {
            case 0:
                state.UseBonusHP = true;
                break;
            case 1:
                state.UseBarrier = true;
                break;
            case 2:
                state.UseNewGaMe = true;
                break;
            case 3:
                state.ShowDodge = true;
                break;
            case 4:
                state.AutoMode = true;
                Debug.Log(state.AutoMode); 
                break;
        }

        // 버튼 사운드 출력
        SoundManager.Instance.PlaySoundFX((int)GlobalData.SFX.SettingIn);
    }
}
