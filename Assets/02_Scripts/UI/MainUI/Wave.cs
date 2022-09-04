using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    [SerializeField]
    private float m_range;
    private RectTransform m_rect;

    private void Awake()
    {
        m_rect = GetComponent<RectTransform>();
    }

    private void Update()
    {
        m_rect.anchoredPosition = Vector3.right * Mathf.Sin(Time.time) * m_range;
    }
}
