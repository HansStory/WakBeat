using UnityEngine;

public class UIObjectShopSkinGroup : MonoBehaviour
{
    [SerializeField] ShopInfo ShopInfo;
    [SerializeField] UIObjectShopSkinItem SkinItemPrefab;

    private void Start()
    {
        int Index = 1;
        // 스킨 항목 Set
        foreach (var info in ShopInfo.SkinItemInfos)
        {
            var item = Instantiate(SkinItemPrefab, SkinItemPrefab.transform.parent);
            item.SetSkin(info);
            item.name = "Skin_Prefab_" + Index;
            item.gameObject.SetActive(true);
            Index++;
        }
    }
}
