using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class CountDownStage : MonoBehaviour
{
    private TextMeshProUGUI _textMeshPro;
    
    private int cnt;
    private void Awake()
    {
        _textMeshPro = this.GetComponent<TextMeshProUGUI>();
        cnt = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        // PlayCountDown();
    }

    // Update is called once per frame
    void Update()
    {
        if (GlobalState.Instance.IsPlayerDied && cnt == 0)
        {
            cnt++;
            PlayCountDown();
        }
    }

    private void PlayCountDown()
    {
        var sequence = DOTween.Sequence();

        sequence
            .OnStart(() => UpdateText("3"))
            .Append(FadeOutText())
            .AppendCallback(() => UpdateText("2"))
            .Append(FadeOutText())
            .AppendCallback(() => UpdateText("1"))
            .Append(FadeOutText())
            .AppendCallback(() => UpdateText("GO!"))
            .Append(FadeOutText())
            .OnComplete(PlayCountDownEnd);
    }

    private void UpdateText(string text)
    {
        InitializeAlpha();

        _textMeshPro.text = text;
    }

    private Tween FadeOutText()
    {
        return _textMeshPro.DOFade(0, 1.0f);
    }
    
    private void InitializeAlpha()
    {
        _textMeshPro.alpha = 1.0f;
    }

    private void PlayCountDownEnd()
    {
        SoundManager.Instance.MusicAudio.Play();
        GlobalState.Instance.IsPlayerDied = false;
        cnt = 0;
    }
}
