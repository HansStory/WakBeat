using System.Collections;
using UnityEngine;

public class BackgroundCtrl : Changer
{
    private SpriteRenderer[] m_backgroundArr;

    private void Awake()
    {
        m_backgroundArr = GetComponentsInChildren<SpriteRenderer>();
    }

    public override void Init(params Sprite[] sprites)
    {
        m_backgroundArr[0].sprite = sprites[0];
        m_backgroundArr[1].enabled = false;
    }

    protected override IEnumerator ChangeAnim(Sprite forwardSprite)
    {
        float lerpTime = 0;
        SpriteRenderer forward = GetForwardRenderer();
        SpriteRenderer backward = GetBackRenderer();

        forward.enabled = true;
        forward.sprite = forwardSprite;
        forward.sortingOrder = 1;
        backward.sortingOrder = 0;

        while (true)
        {
            lerpTime = MyUtil.CalcLerpTime(lerpTime, 5f);

            forward.color = Color.Lerp(MyUtil.whiteClear, Color.white, lerpTime);

            if (lerpTime.Equals(1))
            {
                backward.enabled = false;
                m_TurnCoroutine = null;
                break;
            }

            yield return null;
        }
    }

    private SpriteRenderer GetForwardRenderer()
    {
        return m_backgroundArr[0].enabled ? m_backgroundArr[1] : m_backgroundArr[0];
    }

    private SpriteRenderer GetBackRenderer()
    {
        return m_backgroundArr[0].enabled ? m_backgroundArr[0] : m_backgroundArr[1];
    }
}
