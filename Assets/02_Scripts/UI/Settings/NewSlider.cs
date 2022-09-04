using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NewSlider : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private Image m_fillImage;

    [SerializeField]
    private UnityEvent<float> m_onValueChanged;

    [SerializeField]
    private UnityEvent m_onPointerUp;

    void Awake()
    {
        m_fillImage = GetComponent<Image>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        SetSliderValue(GetSliderValue(eventData));
        m_onValueChanged?.Invoke(m_fillImage.fillAmount);
    }

    public void OnDrag(PointerEventData eventData)
    {
        SetSliderValue(GetSliderValue(eventData));
        m_onValueChanged?.Invoke(m_fillImage.fillAmount);
    }

    public void SetSliderValue(float value)
    {
        m_fillImage.fillAmount = value;
    }

    private float GetSliderValue(PointerEventData eventData)
    {
        Vector2 outVec2;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(m_fillImage.rectTransform, eventData.position, UIManager.Instance.CanvasCam, out outVec2);
        outVec2.x = Mathf.Clamp(outVec2.x, 0, m_fillImage.rectTransform.sizeDelta.x);

        //해당 값은 RectTransform의 pivot.x가 0일경우를 가정한다.
        return outVec2.x / m_fillImage.rectTransform.sizeDelta.x;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        m_onPointerUp?.Invoke();
    }
}
