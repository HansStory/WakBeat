using System;
using UnityEngine;
using UnityEngine.UI;

// 스킨 항목 정보
[Serializable]
public class SkinItemInfo
{
    public Sprite TitleSprite;
    public Sprite IconSprite;
}

public class UIObjectShopSkinItem : MonoBehaviour
{
    // 스킨 항목 정보
    [SerializeField] private Image SkinTitleImage;
    [SerializeField] private Image SkinIconImage;

    // 스킨 정보 세팅
    public void SetSkin(SkinItemInfo info)
    {
        SkinTitleImage.sprite = info.TitleSprite;
        SkinTitleImage.SetNativeSize();
        SkinIconImage.sprite = info.IconSprite;
        SkinIconImage.SetNativeSize();
    }
}
