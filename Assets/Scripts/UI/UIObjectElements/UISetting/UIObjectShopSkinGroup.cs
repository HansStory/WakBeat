using UnityEngine;

public class UIObjectShopSkinGroup : MonoBehaviour
{
    [SerializeField] ShopInfo ShopInfo;
    [SerializeField] UIObjectShopSkinItem SkinItemPrefab;
    private void Start()
    {
        // 스킨 항목 Set
        foreach (var info in ShopInfo.SkinItemInfos)
        {
            var item = Instantiate(SkinItemPrefab, SkinItemPrefab.transform.parent);
            item.SetSkin(info);
            item.gameObject.SetActive(true);
        }
    }
}
