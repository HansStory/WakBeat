using UnityEngine;
using UnityEngine.UI;

public class GameProgress : MonoBehaviour
{
    private Image m_progressImage;
    private Text m_progressText;

    private void Awake()
    {
        m_progressImage = GetComponent<Image>();
        m_progressText = GetComponentInChildren<Text>();
    }

    public void SetProgress(float progress)
    {
        m_progressImage.fillAmount = progress;
        m_progressText.text = $"{(progress * 100).ToString()}%";
    }
}
