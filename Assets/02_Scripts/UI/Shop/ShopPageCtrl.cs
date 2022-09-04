using UnityEngine;
using UnityEngine.UI;

public class ShopPageCtrl : MonoBehaviour
{
    //private ScrollRect m_scrollRect = null;
    private RectTransform[] m_pageRectArr = new RectTransform[(int)SHOPKIND.END];

    private void Awake()
    {
        for(int i = 0; i < m_pageRectArr.Length; i++)
        {
            m_pageRectArr[i] = transform.GetChild(i).GetComponent<RectTransform>();
        }

        //m_scrollRect = GetComponent<ScrollRect>();
    }

    public void ChangePage(SHOPKIND kind)
    {
        //페이지 활성화
        for(int i = 0; i < m_pageRectArr.Length; i++)
        {
            bool active = i == (int)kind;
            m_pageRectArr[i].gameObject.SetActive(active);
        }
        ////페이지 위치 고정
        //m_pageRectArr[(int)kind].anchoredPosition = Vector2.zero;
        ////페이지 스크롤 렉트 설정
        //m_scrollRect.velocity = Vector2.zero;
        //m_scrollRect.content = m_pageRectArr[(int)kind];
    }
}
