using System;
using UnityEngine;
using UnityEngine.UI;

public class UIObjectShopSkinItem : MonoBehaviour
{
    // 내부 오브젝트 선언
    public Button buttonOn;
    public Button buttonOff;
    public Button buttonLock;

    public UIObjectShop UIObjectShop { get; set; }

    // 스킨 항목 정보 > 타이틀
    public Sprite _TitleSprite;
    [SerializeField] private Image SkinTitleImage;
    public Sprite SkinTitleSprite
    {
        get { return _TitleSprite; }
        set
        {
            _TitleSprite = value;
            SkinTitleImage.sprite = _TitleSprite;
            SkinTitleImage.SetNativeSize();
        }
    }

    // 스킨 항목 정보 > 아이콘
    public Sprite _IconSprite;
    [SerializeField] private Image SkinIconImage;
    public Sprite SkinIconSprite
    {
        get { return _IconSprite; }
        set
        {
            _IconSprite = value;
            SkinIconImage.sprite = _IconSprite;
            SkinIconImage.SetNativeSize();
        }
    }

    // 스킨 항목 정보 > 인덱스
    private int _SkinIndex;
    public int SkinIndex
    {
        get { return _SkinIndex; }
        set
        {
            _SkinIndex = value;
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
        buttonLock.gameObject.SetActive(false);
    }

    // 스킬 사용 > 미사용
    private void On()
    {
        buttonOn.gameObject.SetActive(false);
        buttonOff.gameObject.SetActive(true);
    }

    // 스킬 미사용 > 사용
    private void Off()
    {
        buttonOn.gameObject.SetActive(true);
        buttonOff.gameObject.SetActive(false);
    }
}
