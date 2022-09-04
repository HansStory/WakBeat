using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThornRotator : MonoBehaviour
{
    [SerializeField]
    private float m_radiusCorrection = 25f;
    private float m_fRadius = 0f;
    private Image m_image;
    private Thorn[] m_thornArr;

    private void Awake()
    {
        m_image = GetComponent<Image>();
        m_thornArr = GetComponentsInChildren<Thorn>();

        m_fRadius = m_image.rectTransform.sizeDelta.x / 2;
    }

    private void Start()
    {
        for (int i = 0; i < m_thornArr.Length; i++)
        {
            m_thornArr[i].SetThornInfo(Random.Range(0f, 360f), Random.Range(5f, 30f));
        }
    }

    private void Update()
    {
        float radiusParam = m_fRadius - m_radiusCorrection;
        for (int i = 0; i < m_thornArr.Length; i++)
        {
            if (UIManager.Instance.Raycaster.enabled)
            {
                m_thornArr[i].Rotate(radiusParam);
            }
            else
            {
                m_thornArr[i].Rotate(radiusParam , 20f);
            }
        }
    }

    public void ApplyLerpAnim(float lerpTime)
    {
        m_image.rectTransform.localScale = Vector2.Lerp(Vector2.one, Vector2.one * 2, lerpTime);
        m_image.color = Color.Lerp(Color.white, new Color(1, 1, 1, 0.4f), lerpTime);
        for(int i = 0; i < m_thornArr.Length; i++)
        {
            m_thornArr[i].ApplyLerpAnim(lerpTime);
        }
    }

    private void OnDisable()
    {
        m_image.rectTransform.localScale = Vector2.one;
        m_image.color = Color.white;
    }
}
