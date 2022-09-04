using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicSlotCtrl : Changer
{
    private Image[] m_imageArr = new Image[(int)LEN.SELECT_CIRCLE];
    private int m_index = 1;
    [SerializeField]
    private Vector2 m_range = new Vector2(960, -185);

    //코루틴에 사용할 lerp 인수 값
    private float m_multiDest = 0f;
    private float m_repeatRange = 0f;

    //코루틴 진행중 게임 시작될 시 예외 처리를 위한 저장변수
    private Sprite m_saveSprite = null;
    private Image m_centerImage = null;
    private Image m_rightImage = null;
    private Image m_leftImage = null;

    private int Index
    {
        get { return m_index; }
        set { m_index = MyUtil.RepeatIndex(value, m_imageArr.Length); }
    }

    private void Awake()
    {
        for(int i = 0; i < m_imageArr.Length; i++)
        {
            m_imageArr[i] = transform.GetChild(i).GetComponent<Image>();
        }

        m_multiDest = m_range.x * 2;
        m_repeatRange = m_range.x + m_range.x / 2;
    }

    public override void Init(params Sprite[] sprites)
    {
        m_imageArr[0].gameObject.SetActive(false);
        for(int i = 0; i < m_imageArr.Length; i++, Index++)
        {
            m_imageArr[Index].sprite = sprites[i];
            m_imageArr[Index].rectTransform.anchoredPosition = GetAnchoredPosition((CHANGESIDE)Index - 1);
        }
    }

    private Vector2 GetAnchoredPosition(CHANGESIDE cs)
    {
        switch(cs)
        {
            case CHANGESIDE.LEFT:
                return new Vector2(-m_range.x, m_range.y);
            case CHANGESIDE.CENTER:
                return Vector2.zero;
            case CHANGESIDE.RIGHT:
                return m_range;
        }

        return Vector2.zero;
    }

    protected override IEnumerator ChangeAnim(Sprite nextSprite, CHANGESIDE side, HIDE hide)
    {
        m_saveSprite = nextSprite;

        m_centerImage = m_imageArr[Index];
        m_leftImage = m_imageArr[MyUtil.RepeatIndex(Index + (int)CHANGESIDE.LEFT, m_imageArr.Length)];
        m_rightImage = m_imageArr[MyUtil.RepeatIndex(Index + (int)CHANGESIDE.RIGHT, m_imageArr.Length)];

        Index += (int)side;

        float lerpTime = 0;
        bool spriteChange = false;

        while(true)
        {
            lerpTime = MyUtil.CalcLerpTime(lerpTime, 5f);

            MoveCircles(side, lerpTime);
            ChangeScales(side, lerpTime);
            ChangeSprite(side, hide, lerpTime, ref spriteChange);

            if (lerpTime.Equals(1))
            {
                m_TurnCoroutine = null;
                break;
            }

            yield return null;
        }
    }

    private void MoveCircles(CHANGESIDE side, float lerpTime)
    {
        m_centerImage.rectTransform.anchoredPosition = CalcLerpPosition(CHANGESIDE.CENTER, side, lerpTime);
        m_leftImage.rectTransform.anchoredPosition = CalcLerpPosition(CHANGESIDE.LEFT, side, lerpTime);
        m_rightImage.rectTransform.anchoredPosition = CalcLerpPosition(CHANGESIDE.RIGHT, side, lerpTime);
    }

    private void ChangeScales(CHANGESIDE side, float lerpTime)
    {
        m_centerImage.rectTransform.localScale = Vector2.Lerp(Vector2.one, MyUtil.MusicCircle_SmallScale, lerpTime);
        //왼쪽으로 회전시 오른쪽 circle이 중앙에 위치하니 스케일을 키운다.
        if (side == CHANGESIDE.LEFT)
        {
            m_leftImage.rectTransform.localScale = Vector2.Lerp(MyUtil.MusicCircle_SmallScale, Vector2.one, lerpTime);
        }
        //왼쪽으로 회전시 왼쪽 circle이 중앙에 위치하니 스케일을 키운다.
        else if (side == CHANGESIDE.RIGHT)
        {
            m_rightImage.rectTransform.localScale = Vector2.Lerp(MyUtil.MusicCircle_SmallScale, Vector2.one, lerpTime);
        }
    }

    private void ChangeSprite(CHANGESIDE side, HIDE hide, float lerpTime, ref bool spriteChange)
    {
        //lerpTime 값이 0.5를 넘어가면 circle이 화면을 벗어나게 되니 이미지를 교체해준다.
        if (lerpTime >= 0.5f && !spriteChange)
        {
            spriteChange = true;
            if (side == CHANGESIDE.LEFT)
            {
                m_rightImage.sprite = m_saveSprite;
            }
            else if (side == CHANGESIDE.RIGHT)
            {
                m_leftImage.sprite = m_saveSprite;
            }

            //hide가 left면 최종적으로 왼쪽에 있어야하는게 사라져야하니 오른쪽에 있는 이미지를 비활성화해준다.
            m_rightImage.gameObject.SetActive(hide != HIDE.LEFT);
            //마찬가지로 오른쪽에 있어야하는게 사라져야하니 왼쪽에 있는 이미지를 비활성화해준다.
            m_leftImage.gameObject.SetActive(hide != HIDE.RIGHT);
        }
    }

    private Vector2 CalcLerpPosition(CHANGESIDE from, CHANGESIDE to, float lerpTime)
    {
        //교체 애니메이션 방향을 바꾸려면 CENTER는 x의 Mathf.Lerp의 부호를 변경해주고
        //나머지 case에서는 if문 안의 문장들의 위치를 변경 및 Mathf.Lerp의 부호를 변경해주면 된다.
        switch(from)
        {
            case CHANGESIDE.LEFT:
                if(to == CHANGESIDE.LEFT)
                {
                    float x = Mathf.Lerp(-m_range.x, 0f, lerpTime);
                    float y = Mathf.Lerp(m_range.y, 0f, lerpTime);
                    return new Vector2(x, y);
                }
                else if(to == CHANGESIDE.RIGHT)
                {
                    float x = Mathf.Lerp(-m_range.x, -m_multiDest, lerpTime);
                    x = MyUtil.RepeatRange(x, m_repeatRange);
                    return new Vector2(x, m_range.y);
                }
                break;
            case CHANGESIDE.CENTER:
                if (to == CHANGESIDE.LEFT)
                {
                    float x = Mathf.Lerp(0f, m_range.x, lerpTime);
                    float y = Mathf.Lerp(0f, m_range.y, lerpTime);
                    return new Vector2(x, y);
                }
                else if (to == CHANGESIDE.RIGHT)
                {
                    float x = Mathf.Lerp(0f, -m_range.x, lerpTime);
                    float y = Mathf.Lerp(0f, m_range.y, lerpTime);
                    return new Vector2(x, y);
                }
                break;
            case CHANGESIDE.RIGHT:
                if (to == CHANGESIDE.LEFT)
                {
                    float x = Mathf.Lerp(m_range.x, m_multiDest, lerpTime);
                    x = MyUtil.RepeatRange(x, m_repeatRange);
                    return new Vector2(x, m_range.y);
                }
                else if (to == CHANGESIDE.RIGHT)
                {
                    float x = Mathf.Lerp(m_range.x, 0f, lerpTime);
                    float y = Mathf.Lerp(m_range.y, 0f, lerpTime);
                    return new Vector2(x, y);
                }
                break;
        }

        return Vector2.zero;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        //TODO : 코루틴 진행 중 게임 시작 시, 위치 및 이미지 재설정
    }
}
