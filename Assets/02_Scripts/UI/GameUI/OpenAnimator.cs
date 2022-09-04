using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenAnimator : MonoBehaviour
{
    [SerializeField]
    private float m_smoothTime = 0.05f;
    [SerializeField]
    private float m_targetXPos = 250;
    [SerializeField]
    private RectTransform[] m_animRectArr;

    public IEnumerator OpenPauseAnim()
    {
        float dampX = 0;
        float velocity = 0f;

        while(true)
        {
            dampX = Mathf.SmoothDamp(dampX, m_targetXPos, ref velocity, m_smoothTime, 10000, Time.unscaledDeltaTime);

            for(int i = 0; i < m_animRectArr.Length; i++)
            {
                m_animRectArr[i].anchoredPosition = new Vector2(i == 0 ? -dampX : dampX, m_animRectArr[i].anchoredPosition.y);
            }

            if(CheckBreak())
            {
                break;
            }

            yield return null;
        }
    }

    private bool CheckBreak()
    {
        int breakCheck = 0;
        for(int i = 0; i < m_animRectArr.Length; i++)
        {
            if(Mathf.Approximately(Mathf.Abs(m_animRectArr[i].anchoredPosition.x), m_targetXPos))
            {
                breakCheck++;
            }
        }

        return breakCheck >= m_animRectArr.Length;
    }

    private void OnDisable()
    {
        for(int i = 0; i < m_animRectArr.Length; i++)
        {
            m_animRectArr[i].anchoredPosition = new Vector2(0, m_animRectArr[i].anchoredPosition.y);
        }
    }
}
