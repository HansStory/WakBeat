using UnityEngine;
using UnityEngine.UI;

public class ShopCtrl : MonoBehaviour
{
    [SerializeField]
    private GameObject[] m_categories;
    [SerializeField]
    private Image[] m_categoryBtnImages;

    private Sprite[] m_categoryOnSprites;
    private Sprite[] m_categoryOffSprites;

    private void Awake()
    {
        LoadResource();
    }

    private void LoadResource()
    {
        m_categoryOnSprites = Resources.LoadAll<Sprite>(ResourcePath.ShopCategoryOnSprites);
        m_categoryOffSprites = Resources.LoadAll<Sprite>(ResourcePath.ShopCategoryOffSprites);
    }

    private void OnEnable()
    {
        SelectCategory(CATEGORY.SKIN);
    }

    public void Init()
    {
        //SelectCategory(CATEGORY.SKIN);
    }

    public void SelectCategory(Category category) => SelectCategory(category.Arg1);

    public void SelectCategory(CATEGORY category)
    {
        for(int i = 0; i < m_categories.Length; i++)
        {
            if(i == (int)category)
            {
                m_categories[i].SetActive(true);
                m_categoryBtnImages[i].sprite = m_categoryOnSprites[i];
            }
            else
            {
                m_categories[i].SetActive(false);
                m_categoryBtnImages[i].sprite = m_categoryOffSprites[i];
            }
        }
    }
}
