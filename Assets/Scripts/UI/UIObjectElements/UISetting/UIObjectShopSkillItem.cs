using System;
using UnityEngine;
using UnityEngine.UI;

// 스킬 항목 정보
[Serializable]
public class SkillItemInfo
{
    public Sprite TitleSprite;
    public Sprite ExplanationSprite;
    public Sprite IconSprite;
    public Sprite LockExplanationSprite;
}

public class UIObjectShopSkillItem : MonoBehaviour
{
    // 스킬 항목 정보
    [SerializeField] private Image SkillTitleImage;
    [SerializeField] private Image SkillExplanationImage;
    [SerializeField] private Image SkillIconImage;
    [SerializeField] private Image SkillLockExplanationImage;

    // 스킬 정보 세팅
    public void SetSkill(SkillItemInfo info)
    {
        SkillTitleImage.sprite = info.TitleSprite;
        SkillExplanationImage.sprite = info.ExplanationSprite;
        SkillIconImage.sprite = info.IconSprite;
        SkillIconImage.SetNativeSize();
        SkillLockExplanationImage.sprite = info.LockExplanationSprite; 
    }
}
