using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundChanger : MonoBehaviour
{
    private enum BACKGROUND
    {
        BACK,
        FORWARD
    }
    private Image[] m_backgroundImages;
    private Sprite m_prevBackground;
    private Coroutine m_coroutine;

    public void Init(Sprite background)
    {
        m_backgroundImages = GetComponentsInChildren<Image>();

        m_prevBackground = background;
        m_backgroundImages[(int)BACKGROUND.BACK].sprite = background;
        m_backgroundImages[(int)BACKGROUND.BACK].color = Color.white;
        m_backgroundImages[(int)BACKGROUND.FORWARD].color = MyUtil.whiteClear;
    }

    public void ChangeBackground(Sprite currentBackground)
    {
        if(m_coroutine != null)
        {
            StopCoroutine(m_coroutine);
        }

        m_backgroundImages[(int)BACKGROUND.BACK].color = Color.white;
        m_backgroundImages[(int)BACKGROUND.FORWARD].color = Color.white;

        m_backgroundImages[(int)BACKGROUND.BACK].sprite = currentBackground;
        m_backgroundImages[(int)BACKGROUND.FORWARD].sprite = m_prevBackground;
        m_prevBackground = currentBackground;

        m_coroutine = StartCoroutine(Change());
    }

    IEnumerator Change()
    {
        float elapsed = 0f;

        while (true)
        {
            elapsed = MyUtil.CalcLerpTime(elapsed, Speeds.AlbumMoveSpeed);

            m_backgroundImages[(int)BACKGROUND.FORWARD].color = Color.Lerp(Color.white, MyUtil.whiteClear, elapsed);

            if (elapsed >= 1f)
            {
                m_coroutine = null;
                break;
            }

            yield return null;
        }
    }
}