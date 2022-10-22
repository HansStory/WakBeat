using UnityEngine;

public class UIObjectShopSkillGroup : MonoBehaviour
{
    [SerializeField] ShopInfo ShopInfo;
    [SerializeField] UIObjectShopSkillItem SkillItemPrefab;

    private void Start()
    {
        int Index = 1;
        // 스킬 항목 Set
        foreach (var info in ShopInfo.SkillItemInfos)
        {
            var item = Instantiate(SkillItemPrefab, SkillItemPrefab.transform.parent);
            item.SetSkill(info);
            item.name = "Skill_Prefab_" + Index;
            item.gameObject.SetActive(true);
            Index++;
        }
    }
}
