using UnityEngine;

public class UIObjectShopSkillGroup : MonoBehaviour
{
    [SerializeField] ShopInfo ShopInfo;
    [SerializeField] UIObjectShopSkillItem SkillItemPrefab;

    private void Start()
    {
        // 스킬 항목 Set
        foreach (var info in ShopInfo.SkillItemInfos)
        {
            var item = Instantiate(SkillItemPrefab, SkillItemPrefab.transform.parent);
            item.SetSkill(info);
            item.gameObject.SetActive(true);
        }
    }
}
