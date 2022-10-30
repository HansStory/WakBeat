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
    // 버튼 사운드
    const int SFX_Home = 1;
    const int SFX_Setting = 4;
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
        SoundManager.Instance.PlaySoundFX(SFX_Setting);
    }

    // 스킬 사용 > 미사용
    private void On()
    {
        buttonOn.gameObject.SetActive(false);
        buttonOff.gameObject.SetActive(true);

        // 버튼 사운드 출력
        SoundManager.Instance.PlaySoundFX(SFX_Setting);
    }

    // 스킬 미사용 > 사용
    private void Off()
    {
        buttonOn.gameObject.SetActive(true);
        buttonOff.gameObject.SetActive(false);

        // 버튼 사운드 출력
        SoundManager.Instance.PlaySoundFX(SFX_Setting);
    }
}
