using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIObjectAudioSpectrum : MonoBehaviour
{
    [SerializeField] private Image[] spectrums;
    private AudioSource audio;
    private float[] spectrumData = new float[64];

    [SerializeField] private float sensibility;
    [SerializeField] private float spectrumHeight;
    [SerializeField] private TMP_Text songText;

    private void Start()
    {
        audio = SoundManager.Instance.MusicAudio;
        ResetSpectrums();

        SoundManager.Instance.OnChanged += OnChanged;
        OnChanged(SoundManager.Instance.CurrentSongTitle);
    }

    private void OnChanged(string songTitle)
    {
        songText.text = string.Format("¢Ü {0}", songTitle);
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
        if (audio == null)
        {
            return;
        }

        audio.GetSpectrumData(spectrumData, 0, FFTWindow.Rectangular);

        for (int i = 0; i < spectrums.Length; i++)
        {
            float newSize = spectrums[i].fillAmount;
            newSize = Mathf.Lerp(newSize, spectrumData[i] * spectrumHeight, sensibility * Time.deltaTime);
            spectrums[i].fillAmount = newSize;
        }
    }

    public void ResetSpectrums()
    {
        for (int i = 0; i < spectrums.Length; i++)
        {
            spectrums[i].fillAmount = 0f;
        }
    }
}
