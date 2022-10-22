using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIObjectAudioSpectrum : MonoBehaviour
{
    [SerializeField] private Image[] spectrums;
    private float[] spectrumData = new float[64];

    [SerializeField] private float sensibility;
    [SerializeField] private float spectrumHeight;
    [SerializeField] private Image imageBGMText;

    private void Start()
    {
        ResetSpectrums();

        SoundManager.Instance.OnChanged += OnChanged;
        OnChanged(SoundManager.Instance.CurrentSongSprite);
    }

    private void OnChanged(Sprite songTitle)
    {
        imageBGMText.sprite = songTitle;
        imageBGMText.SetNativeSize();
    }

    private void OnDestroy()
    {

    }

    private void OnDisable()
    {
        ResetSpectrums();
    }

    void Update()
    {
        if (SoundManager.Instance.MusicAudio == null)
        {
            return;
        }

        SoundManager.Instance.MusicAudio.GetSpectrumData(spectrumData, 0, FFTWindow.Rectangular);

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
