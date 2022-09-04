using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class WindowAnim
{
    public static IEnumerator OpenPopup(GraphicRaycaster raycaster, GameObject boundary, RectTransform popup,
        MonoBehaviour monoBehaviour, IEnumerator coroutine = null)
    {
        float elapsed = 0f;

        popup.gameObject.SetActive(true);
        boundary.SetActive(true);
        raycaster.enabled = false;

        while (true)
        {
            elapsed = MyUtil.CalcLerpTime(elapsed);

            popup.localScale = Vector2.Lerp(Vector2.zero, Vector2.one, elapsed);
            if (elapsed.Equals(1))
            {
                if (coroutine != null)
                {
                    yield return monoBehaviour.StartCoroutine(coroutine);
                }
                raycaster.enabled = true;
                break;
            }

            yield return null;
        }
    }

    public static IEnumerator ClosePopup(GraphicRaycaster raycaster, GameObject boundary, RectTransform popup, Action action = null)
    {
        float elapsed = 0f;
        raycaster.enabled = false;

        while (true)
        {
            elapsed = MyUtil.CalcLerpTime(elapsed);

            popup.localScale = Vector2.Lerp(Vector2.one, Vector2.zero, elapsed);

            if (elapsed.Equals(1))
            {
                action?.Invoke();
                boundary.gameObject.SetActive(false);
                raycaster.enabled = true;
                break;
            }

            yield return null;
        }
    }
}
