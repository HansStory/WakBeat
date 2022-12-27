using System;
using UnityEngine;
using UnityEngine.UI;

public class UIObjectShopVideoItem : MonoBehaviour
{
    public Button buttonPlay;

    public UIObjectShop UIObjectShop { get; set; }

    // 스킨 항목 정보 > 타이틀
    public Sprite _TitleSprite;
    [SerializeField] private Image VideoTitleImage;
    public Sprite VideoTitleSprite
    {
        get { return _TitleSprite; }
        set
        {
            _TitleSprite = value;
            VideoTitleImage.sprite = _TitleSprite;
            VideoTitleImage.SetNativeSize();
        }
    }

    // 스킨 항목 정보 > 아이콘
    public Sprite _IconSprite;
    [SerializeField] private Image VideoIconImage;
    public Sprite VideoIconSprite
    {
        get { return _IconSprite; }
        set
        {
            _IconSprite = value;
            VideoIconImage.sprite = _IconSprite;
            VideoIconImage.SetNativeSize();
        }
    }

    // 스킨 항목 정보 > 인덱스
    private int _VideoIndex;
    public int VideoIndex
    {
        get { return _VideoIndex; }
        set
        {
            _VideoIndex = value;
        }
    }
}
