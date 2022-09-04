using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicSelectCtrl : MonoBehaviour
{
    [SerializeField]
    private Graphic[] m_graphics;

    private RectTransform m_rect;
    private BackgroundChanger m_backgroundChanger;

    private ALBUM m_currentAlbum;

    public RectTransform Rect => m_rect;

    private void Awake()
    {
        m_rect = GetComponent<RectTransform>();
        m_backgroundChanger = GetComponentInChildren<BackgroundChanger>();
    }

    private void OnEnable()
    {
        StartCoroutine(FadeIn());
    }

    public void SetMusics(ALBUM album)
    {
        m_currentAlbum = album;
        //TODO : 음악 선택 세팅
    }

    public void InputExecute()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {

        }
        else if(Input.GetKeyDown(KeyCode.RightArrow))
        {

        }
    }

    IEnumerator FadeIn()
    {
        float elapsed = 0f;

        for(int i = 0; i < m_graphics.Length; i++)
        {
            m_graphics[i].color = MyUtil.whiteClear;
        }
        
        while(true)
        {
            elapsed = MyUtil.CalcLerpTime(elapsed, 2f);
            Color lerpColor = Color.Lerp(MyUtil.whiteClear, Color.white, elapsed);
            for(int i = 0; i < m_graphics.Length; i++)
            {
                m_graphics[i].color = lerpColor;
            }

            if(elapsed >= 1f)
            {
                UIManager.Instance.Raycaster.enabled = true;
                break;
            }

            yield return null;
        }
    }
}
