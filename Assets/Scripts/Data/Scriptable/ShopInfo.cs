using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SetupShopInfo", menuName = "ScriptableObjects/SetupShopInfo", order = 3)]

public class ShopInfo : ScriptableObject
{
    [Space(10)]
    [Header("[ Shop Skin Info]")]
    public Sprite[] SkinTitle;
    public Sprite[] SkinIcon;

    [Space(10)]
    [Header("[ Shop Skill Info]")]
    public Sprite[] SkillTitle;
    public Sprite[] SkillExplanation;
    public Sprite[] SkillIcon;
    public Sprite[] SkillLockExplanation;

    /*
    [Header("[ Shop Info List (Prefabs) ]")]
    public List<SkillItemInfo> SkillItemInfos;
    public List<SkinItemInfo> SkinItemInfos;
    */
    /*
    [Header("[ Shop List (Prefabs) ]")]
    public GameObject[] SkinLists;
    public GameObject[] SkillLists;

    [Space(10)]
    [Header("[ Skin Infomation ]")]
    public Sprite[] SkinBackgournds;  // 스킨 항목 배경
    public Sprite[] SkinTitles;               // 스킨 명
    public Sprite[] SkinIcons;               // 스킨 아이콘

    [Space(10)]
    [Header("[ Skin Button Infomation ]")]
    public Sprite[] SkinButtonOns;      // 스킨 On 버튼
    public Sprite[] SkinButtonOffs;     // 스킨 Off 버튼
    public Sprite[] SkinButtonBuys;     // 스킨 Buy 버튼

    [Space(10)]
    [Header("[ Skill Open Infomation ]")]
    public Sprite[] SkillOpenBackgournds;       // Open 된 스킬 배경
    public Sprite[] SkillOpenTitles;        // 스킬 명
    public Sprite[] SkillOpenTexts;        // 스킬 설명
    public Sprite[] SkillOpenIcons;        // 스킬 아이콘

    [Space(10)]
    [Header("[ Skill Open Button Infomation ]")]
    public Sprite[] SkillOpenButtonOns;        // 스킬 On 버튼
    public Sprite[] SkillOpenButtonOffs;        // 스킬 Off 버튼

    [Space(10)]
    [Header("[ Skill Lock Infomation ]")]
    public Sprite[] SkillLockBackgournds;       // Lock 된 스킬 배경
    public Sprite[] SkillLockTexts;        // 스킬 잠김 설명
    public Sprite[] SkillLockIcons;        // 스킬 잠김 아이콘

    [Space(10)]
    [Header("[ Skill Lock Button Infomation ]")]
    public Sprite[] SkillLockButtonUnlocks;        // 스킬 Unlock 버튼
    */
}
