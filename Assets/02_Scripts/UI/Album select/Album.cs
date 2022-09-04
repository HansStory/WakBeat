using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

//TODO : 앨범이 중앙점에 배치가 완료되면 앨범정보 보여주기, 반대로 벗어나면 숨기기

public class Album : MonoBehaviour
{
    private static readonly Vector2 StartPoint = Vector2.left * 900f;
    private static readonly Vector2 TargetPoint = Vector2.left * 100f;
    private static readonly Vector2 SmallSize = new Vector2(.6f, .6f);

    [SerializeField]
    private Image m_icon;
    [SerializeField]
    private Image m_title;
    [SerializeField]
    private RectTransform m_rect;
    [SerializeField]
    private AnimCurve m_curve;
    private float m_curveTarget;
    private int m_poolIndex;

    private Coroutine m_coroutine;

    private float m_titleElapsed = 0f;
    private Coroutine m_titleCoroutine;

    public float CurveTarget
    {
        get => m_curveTarget;
        private set
        {
            //그냥 repeat을 하면 1f값에 도달하자마자 0으로 변경되므로 조건을 추가한다.
            if(value < 0f || value > 1f)
            {
                value = Mathf.Repeat(value, 1f);
            }
            m_curveTarget = value;
        }
    }
    public bool CoroutineEnd => m_coroutine == null;
    public bool TitleCoroutineEnd => m_titleCoroutine == null;

    public void Init(int poolIndex, float curveTarget, Vector2 anchoredPos)
    {
        m_poolIndex = poolIndex;
        m_curveTarget = curveTarget;
        m_rect.anchoredPosition = anchoredPos;
    }

    public void SetAlbumSprites(AlbumData albumData)
    {
        m_icon.sprite = albumData.Icon;
        m_title.sprite = albumData.Title;
    }

    public void ActiveTitle(bool active)
    {
        m_title.gameObject.SetActive(active);
    }

    public void ActiveTitle(int currentIndex)
    {
        ActiveTitle(currentIndex == m_poolIndex);
    }

    public void SetAlbumInfo(int currentAlbumIndex)
    {
        m_rect.localScale = currentAlbumIndex == m_poolIndex ? Vector2.one : SmallSize;
        //ActiveTitle(currentAlbumIndex);
    }

    public void Move(int centerAlbumIndex, float curveInterval, Func<float, Vector2> func)
    {
        if(m_coroutine != null)
        {
            StopCoroutine(m_coroutine);
        }

        ActiveTitle(false);
        m_coroutine = StartCoroutine(StartMove(centerAlbumIndex, curveInterval, func));
    }

    IEnumerator StartMove(int centerAlbumIndex, float curveInterval, Func<float, Vector2> func)
    {
        float elapsed = 0f;
        float prevCurveTarget = RefreshCurveTarget(CurveTarget, curveInterval > 0);
        CurveTarget += curveInterval;

        while(true)
        {
            elapsed = MyUtil.CalcLerpTime(elapsed, Speeds.AlbumMoveSpeed);
            float curveElapsed = Mathf.Lerp(prevCurveTarget, CurveTarget, elapsed);

            m_rect.anchoredPosition = func.Invoke(curveElapsed);
            SetAlbumSize(centerAlbumIndex, elapsed);

            if(elapsed >= 1f)
            {
                StartTitleAnim(centerAlbumIndex);
                m_coroutine = null;
                break;
            }

            yield return null;
        }
    }

    //화면에 벗어난 앨범은 시작점을 재조정한다.
    //(그대로 두면 0에서 0.75 혹은 1에서 0.25로 배지어 커브가 이루어지므로 비정상적으로 보임)
    private float RefreshCurveTarget(float curveTarget, bool isPositive)
    {
        //화면에 보여지는 앨범의 CurveTarget값은 그대로 반환한다.
        if(curveTarget > 0f && curveTarget < 1f)
        {
            return curveTarget;
        }

        return isPositive ? 0f : 1f;
    }

    private void SetAlbumSize(int centerAlbumIndex, float elapsed)
    {
        if (centerAlbumIndex == m_poolIndex)
        {
            m_rect.localScale = Vector2.Lerp(SmallSize, Vector2.one, elapsed);
        }
        else if ((Vector2)m_rect.localScale != SmallSize)
        {
            m_rect.localScale = Vector2.Lerp(Vector2.one, SmallSize, elapsed);
        }
    }

    public void StartOpenAnim(AnimationCurve curve, int centerIndex)
    {
        m_coroutine =  StartCoroutine(OpenAnim(curve, centerIndex));
    }

    private IEnumerator OpenAnim(AnimationCurve curve, int centerIndex)
    {
        float elapsed = 0f;
        Vector2 originSize = m_rect.localScale;

        while(true)
        {
            elapsed = MyUtil.CalcLerpTime(elapsed, 1f);
            m_rect.localScale = originSize * curve.Evaluate(elapsed);

            if(elapsed >= 1f)
            {
                m_rect.localScale = originSize;
                m_coroutine = null;
                StartTitleAnim(centerIndex);
                break;
            }

            yield return null;
        }
    }

    public void StartTitleAnim(int centerIndex)
    {
        if(m_titleCoroutine != null)
        {
            StopCoroutine(m_titleCoroutine);
        }

        if (m_poolIndex == centerIndex)
        {
            m_titleCoroutine = StartCoroutine(ShowTitle());
        }
    }

    private IEnumerator ShowTitle()
    {
        m_titleElapsed = 0f;
        
        m_title.gameObject.SetActive(true);

        while(true)
        {
            m_titleElapsed = MyUtil.CalcLerpTime(m_titleElapsed, 2f);
            m_title.rectTransform.anchoredPosition = 
                Vector2.Lerp(StartPoint, TargetPoint, m_curve.Curve.Evaluate(m_titleElapsed));

            if(m_titleElapsed >= 1f)
            {
                m_titleCoroutine = null;
                break;
            }

            yield return null;
        }
    }

    public void StartHideTitle()
    {
        if (m_titleCoroutine != null)
        {
            StopCoroutine(m_titleCoroutine);
        }

        m_titleCoroutine = StartCoroutine(HideTitle());
    }

    private IEnumerator HideTitle()
    {
        if(m_titleElapsed <= 0f)
        {
            m_titleElapsed = 1f;
        }

        while (true)
        {
            m_titleElapsed -= Time.deltaTime * 2f;
            m_title.rectTransform.anchoredPosition =
                Vector2.Lerp(StartPoint, TargetPoint, m_curve.Curve.Evaluate(m_titleElapsed));

            if (m_titleElapsed <= 0f)
            {
                m_title.gameObject.SetActive(false);
                m_titleCoroutine = null;
                break;
            }

            yield return null;
        }
    }

    public void SetCenterAlbumTitle()
    {
        m_title.gameObject.SetActive(true);
        m_title.rectTransform.anchoredPosition = TargetPoint;
    }

    private void OnDisable()
    {
        m_title.rectTransform.anchoredPosition = StartPoint;
    }
}
