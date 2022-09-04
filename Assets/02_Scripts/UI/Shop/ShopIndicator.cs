using UnityEngine;
using UnityEngine.UI;

public class ShopIndicator : MonoBehaviour
{
    private Image m_indicatorImage;

    public bool Enabled { set { m_indicatorImage.enabled = value; } }

    private void Awake()
    {
        if(m_indicatorImage == null)
        {
            m_indicatorImage = transform.GetChild(0).GetComponent<Image>();
        }
    }
}
