using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnterAnimation : MonoBehaviour
{
    private GameObject m_startBtn;
    private ThornRotator[] m_rotatorArr;

    private void Awake()
    {
        m_startBtn = transform.GetChild(transform.childCount - 1).gameObject;
        m_rotatorArr = GetComponentsInChildren<ThornRotator>();
    }

    public void ApplyLerpAnim(float lerpTime)
    {
        transform.localScale = Vector2.Lerp(Vector2.one, Vector2.one * 2, lerpTime);
        for (int i = 0; i < m_rotatorArr.Length; i++)
        {
            m_rotatorArr[i].ApplyLerpAnim(lerpTime);
        }
    }

    private void OnDisable()
    {
        m_startBtn.SetActive(true);
        transform.localScale = Vector2.one;
    }
}
