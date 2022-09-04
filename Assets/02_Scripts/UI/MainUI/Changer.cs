using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Changer : MonoBehaviour
{
    protected IEnumerator m_TurnCoroutine = null;
    public bool IsPlaying { get { return m_TurnCoroutine != null; } }

    public virtual void Init(params Sprite[] sprites)
    {

    }

    public void StartChangeAnim(Sprite forwardSprite)
    {
        m_TurnCoroutine = ChangeAnim(forwardSprite);
        StartCoroutine(m_TurnCoroutine);
    }

    public void StartChangeAnim(Sprite nextSprite, CHANGESIDE side, HIDE hide = HIDE.NONE)
    {
        m_TurnCoroutine = ChangeAnim(nextSprite, side, hide);
        StartCoroutine(m_TurnCoroutine);
    }

    public void StopChangeAnim()
    {
        if (m_TurnCoroutine != null)
        {
            StopCoroutine(m_TurnCoroutine);
            m_TurnCoroutine = null;
        }
    }

    protected virtual IEnumerator ChangeAnim(Sprite forwardSprite)
    {
        yield return null;
    }

    protected virtual IEnumerator ChangeAnim(Sprite nextSprite, CHANGESIDE cs, HIDE hide)
    {
        yield return null;
    }

    protected virtual void OnDisable()
    {
        StopChangeAnim();
    }
}
