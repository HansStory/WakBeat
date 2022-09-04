using System;
using System.Collections;
using UnityEngine;

public class AlbumSelector : MonoBehaviour
{
    private readonly Vector2 OriginPoint = Vector2.left * 537;
    private readonly Vector2 TargetPoint = Vector2.left * 1400;
    private readonly Vector2[] m_bazierCurveCoords = { new Vector2(-295f, 777f), new Vector2(1120f, 0f), new Vector2(-295f, -777f) };

    private Album[] m_albums;
    private int m_currentIndex;
    private float m_interval;
    private RectTransform m_rect;

    [SerializeField, Header("Open anim")]
    private AnimCurve m_animCurve;
    [SerializeField]
    private AnimCurve m_HideCurve;

    public int CurrentIndex
    {
        get => m_currentIndex;
        private set
        {
            value = MyUtil.RepeatIndex(value, m_albums.Length);
            m_currentIndex = value;
        }
    }


    public void Init()
    {
        m_rect = GetComponent<RectTransform>();
        m_albums = GetComponentsInChildren<Album>();
        CurrentIndex = m_albums.Length / 2;
        m_interval = 1f / m_albums.Length;

        InitAlbums();
    }

    private void InitAlbums()
    {
        float startCurve = 0f;
        for(int i = 0; i < m_albums.Length; i++)
        {
            m_albums[i].Init(i, startCurve, GetBazierCurvePos(startCurve));
            m_albums[i].SetAlbumInfo(CurrentIndex);
            startCurve += m_interval;
        }
    }

    public void MoveAlbum(INCREASE increase)
    {
        switch(increase)
        {
            case INCREASE.INCREASE:
                CurrentIndex++;
                for (int i = 0; i < m_albums.Length; i++)
                {
                    m_albums[i].Move(CurrentIndex, -m_interval, GetBazierCurvePos);
                }
                break;
            case INCREASE.DECREASE:
                CurrentIndex--;
                for (int i = 0; i < m_albums.Length; i++)
                {
                    m_albums[i].Move(CurrentIndex, m_interval, GetBazierCurvePos);
                }
                break;
        }
    }

    private Vector2 GetBazierCurvePos(float elapsed)
    {
        Vector2 curve1 = Vector2.Lerp(m_bazierCurveCoords[0], m_bazierCurveCoords[1], elapsed);
        Vector2 curve2 = Vector2.Lerp(m_bazierCurveCoords[1], m_bazierCurveCoords[2], elapsed);

        return Vector2.Lerp(curve1, curve2, elapsed);
    }

    //화면에서 벗어나있는 앨범 클래스의 이미지를 세팅한다.
    public void SetAlbumSprites(INCREASE increase, AlbumData albumData)
    {
        switch(increase)
        {
            case INCREASE.INCREASE:
                m_albums[MyUtil.RepeatIndex(CurrentIndex + 1, m_albums.Length)].SetAlbumSprites(albumData);
                break;
            case INCREASE.DECREASE:
                m_albums[MyUtil.RepeatIndex(CurrentIndex - 1, m_albums.Length)].SetAlbumSprites(albumData);
                break;
        }
    }

    public void SetAlbumSprites(params AlbumData[] albumDatas)
    { 
        for(int i = -1; i < albumDatas.Length - 1; i++)
        {
            m_albums[CurrentIndex + i].SetAlbumSprites(
                albumDatas[MyUtil.RepeatIndex(i, albumDatas.Length)]
                );
        }
    }

    public void StartOpenAnim()
    {
        StartCoroutine(OpenAnim());
    }

    private IEnumerator OpenAnim()
    {
        WaitForSeconds delay = new WaitForSeconds(.1f);
        for (int i = -1; i < 2; i++)
        {
            int albumIndex = MyUtil.RepeatIndex(CurrentIndex + i, m_albums.Length);
            m_albums[albumIndex].StartOpenAnim(m_animCurve.Curve, CurrentIndex);
            yield return delay;
        }

        yield return new WaitUntil(() =>
        {
            for (int i = -1; i < 2; i++)
            {
                int albumIndex = MyUtil.RepeatIndex(CurrentIndex + i, m_albums.Length);
                if (!m_albums[albumIndex].CoroutineEnd)
                {
                    return false;
                }
            }

            return true;
        });

        UIManager.Instance.Raycaster.enabled = true;
    }

    public void SelectAlbum(Action action)
    {
        StartCoroutine(HideSelector(action));
    }

    private IEnumerator HideSelector(Action action)
    {
        m_albums[CurrentIndex].StartHideTitle();
        yield return new WaitUntil(() =>  m_albums[CurrentIndex].TitleCoroutineEnd);

        float elapsed = 1f;

        while(true)
        {
            elapsed -= Time.deltaTime * 2.5f;
            m_rect.anchoredPosition = Vector2.Lerp(TargetPoint, OriginPoint, m_HideCurve.Curve.Evaluate(elapsed));

            if(elapsed <= 0f)
            {
                action?.Invoke();
                m_albums[CurrentIndex].SetCenterAlbumTitle();
                break;
            }

            yield return null;
        }
    }

    private void OnDisable()
    {
        m_rect.anchoredPosition = OriginPoint;
    }
}
