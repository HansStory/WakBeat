using System;
using UnityEngine;
using UnityEngine.UI;

public class Spectrum : MonoBehaviour
{
    [SerializeField]
    private Image[] m_spectrums;
    private AudioSource m_audio;
    private float[] m_spectrumData = new float[64];
    [SerializeField]
    private float m_sensibility;
    [SerializeField]
    private float m_spectrumHeight;
    [SerializeField]
    private Text m_songText;

    private void Start()
    {
        m_audio = SoundManager.Instance.MusicAudio;
        ResetSpectrums();

        SoundManager.Instance.OnChanged += OnChanged;
        OnChanged(SoundManager.Instance.CurrentSongTitle);
    }

    private void OnChanged(string songTitle)
    {
        m_songText.text = string.Format("♪ {0}", songTitle);
    }

    private void OnDestroy()
    {
        //SoundManager.Instance.OnChanged -= OnChanged;
    }

    private void OnDisable()
    {
        ResetSpectrums();
    }

    void Update()
    {
        if(m_audio == null)
        {
            return;
        }

        m_audio.GetSpectrumData(m_spectrumData, 0, FFTWindow.Rectangular);

        for (int i = 0; i < m_spectrums.Length; i++)
        {
            float newSize = m_spectrums[i].fillAmount;
            newSize = Mathf.Lerp(newSize, m_spectrumData[i] * m_spectrumHeight, m_sensibility * Time.deltaTime);
            m_spectrums[i].fillAmount = newSize;
        }
    }

    public void ResetSpectrums()
    {
        for(int i = 0; i < m_spectrums.Length; i++)
        {
            m_spectrums[i].fillAmount = 0f;
        }
    }
}
