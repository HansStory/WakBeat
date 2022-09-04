using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Thorn : MonoBehaviour
{
    

    [SerializeField]
    private THORNDIR m_thornDir = THORNDIR.INCIRCLE;

    private float m_fRotSpeed = 2f;
    private Image m_thornImage;
    private Color m_startColor;
    private Color m_targetColor;

    private void Awake()
    {
        if (m_thornImage == null)
        {
            m_thornImage = GetComponent<Image>();
        }

        m_startColor = m_thornImage.color;
        m_targetColor = m_startColor - new Color(0, 0, 0, 0.6f);
    }

    public void SetThornInfo(float angle, float speed = 2f)
    {
        m_fRotSpeed = speed;
        m_thornImage.rectTransform.anchoredPosition = MyUtil.ConvertRadAngleToVec2(angle * Mathf.Deg2Rad);
    }

    public void ApplyLerpAnim(float lerpTime)
    {
        m_thornImage.color = Color.Lerp(m_startColor, m_targetColor, lerpTime);
    }

    public void Rotate(float radius, float speed = 1f)
    {
        float degAngle = 0f;
        if (speed == 1f)
        {
            degAngle = MyUtil.GetRadAngle(m_thornImage.rectTransform.anchoredPosition) * Mathf.Rad2Deg + m_fRotSpeed * Time.deltaTime;
        }
        else
        {
            degAngle = MyUtil.GetRadAngle(m_thornImage.rectTransform.anchoredPosition) * Mathf.Rad2Deg - m_fRotSpeed * speed * Time.deltaTime;
        }

        m_thornImage.rectTransform.rotation = Quaternion.Euler(new Vector3(0, 0, degAngle + (int)m_thornDir));
        m_thornImage.rectTransform.anchoredPosition = MyUtil.ConvertRadAngleToVec2(degAngle * Mathf.Deg2Rad) * radius;
    }

    private void OnDisable()
    {
        m_thornImage.color = m_startColor;
    }
}
