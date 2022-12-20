using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UIElementResult : MonoBehaviour
{
    public Transform ResultBanner;
    public Image OriginLink;
    public Image ReMixLink;

    private GlobalState state;

    private Vector2 originStartVector = new Vector2(-416f, -180f);
    private Vector2 originTargetVector = new Vector2(21.2f, -180f);

    private Vector2 remixLinkStartVector = new Vector2(-416f, -281.5f);
    private Vector2 remixLinkTargetVector = new Vector2(21.2f, -281.5f);

    private string _wakZooURL = string.Empty;
    private string _originURL = string.Empty;
    private string _reMixURL = string.Empty;

    void Start()
    {
        GetWakZooURL();
        state = GlobalState.Instance;
    }

    private void OnEnable()
    {
        SoundManager.Instance.TurnOnResultAudio();

        GetMusicURL();
        InitTexts();

        TweenResultBanner();

        TweenLink(OriginLink, originStartVector, originTargetVector, 0.5f);
        TweenLink(ReMixLink, remixLinkStartVector, remixLinkTargetVector, 0.8f);

        //TweenTexts();
        ShowText();
    }

    private void TweenResultBanner()
    {
        var fadeImages = ResultBanner.GetComponentsInChildren<Image>();

        foreach (var image in fadeImages)
        {
            image.color = Color.clear;
            image.DOColor(Color.white, 1f).SetEase(Ease.OutCubic).SetAutoKill();
        }

        var startVector = new Vector2(700f, 35f);
        var targetVector = new Vector2(-38f, 35f);

        ResultBanner.localPosition = startVector;
        ResultBanner.DOLocalMove(targetVector, 1f).SetEase(Ease.OutCubic).SetAutoKill();
    }

    private void TweenLink(Image linkImage, Vector2 startVector, Vector2 targetVector, float delay)
    {
        linkImage.color = Color.clear;
        linkImage.transform.localPosition = startVector;

        linkImage.DOColor(Color.white, 1f).SetDelay(delay).SetEase(Ease.OutCubic).SetAutoKill();
        linkImage.transform.DOLocalMove(targetVector, 1f).SetDelay(delay).SetEase(Ease.OutCubic).SetAutoKill();
    }

    //----------------------------------------------------------------
    public Text[] LeftTexts;
    public Text[] RightTexts;

    private static int MusicLength = 0;
    private static int TotalPlayTime = 1;
    private static int DeathCount = 2;
    private static int UsedItem = 3;

    private void InitTexts()
    {
        //ColorClearTexts(LeftTexts);
        //ColorClearTexts(RightTexts);

        //ColorClearTMPTexts(TestLeftTexts);
        //ColorClearTMPTexts(TestRightTexts);

        //SetTextStageResult();
    }
    private void ColorClearTexts(Text[] texts)
    {
        for (int i = 0; i < texts.Length; i++)
        {
            texts[i].color = Color.clear;
        }
    }

    private void ColorClearTMPTexts(TMP_Text[] texts)
    {
        for (int i = 0; i < texts.Length; i++)
        {
            texts[i].color = Color.clear;
        }
    }

    private void SetTextStageResult()
    {
        SetStageMusicLength();
        SetPlayTime();
        SetPlayerDeathCount();
        SetUsedItem();
    }

    private void SetStageMusicLength()
    {
        if (state.StageMusicLength == 0) return;

        int min = state.StageMusicLength / 60;
        int sec = state.StageMusicLength % 60;
        TestRightTexts[MusicLength].text = $"{min} : {sec}";
    }

    private void SetPlayTime()
    {
        if (state.StageMusicLength == 0) return;

        int min = state.StagePlayTime / 60;
        int sec = state.StagePlayTime % 60;
        TestRightTexts[TotalPlayTime].text = $"{min} : {sec}";
    }

    private void SetPlayerDeathCount()
    {
        TestRightTexts[DeathCount].text = $"{state.PlayerDeadCount}ȸ";
    }

    private void SetUsedItem()
    {
        TestRightTexts[UsedItem].text = $"{state.UsedItems}";
    }


    private void TweenTexts()
    {
        float delay = 2f;

        for (int i = 0; i < LeftTexts.Length; i++)
        {
            TweenText(i, 0.3f, delay);
            delay += 0.3f;
        }
    }

    private void TweenText(int index, float duration, float delay)
    { 
        LeftTexts[index].DOColor(Color.black, duration).SetAutoKill().SetEase(Ease.InOutCubic).SetDelay(delay);
        RightTexts[index].DOColor(Color.black, duration).SetAutoKill().SetEase(Ease.InOutCubic).SetDelay(delay);
    }

    public TMP_Text[] TestLeftTexts;
    public TMP_Text[] TestRightTexts;

    private void ShowText()
    {
        Invoke($"{nameof(ShowText0)}", 2.0f);
        Invoke($"{nameof(ShowText1)}", 2.3f);
        Invoke($"{nameof(ShowText2)}", 2.6f);
        Invoke($"{nameof(ShowText3)}", 2.9f);
    }

    private void ShowText0()
    {
        TestLeftTexts[0].gameObject.SetActive(true);
        TestRightTexts[0].gameObject.SetActive(true);
    }

    private void ShowText1()
    {
        TestLeftTexts[1].gameObject.SetActive(true);
        TestRightTexts[1].gameObject.SetActive(true);
    }

    private void ShowText2()
    {
        TestLeftTexts[2].gameObject.SetActive(true);
        TestRightTexts[2].gameObject.SetActive(true);
    }

    private void ShowText3()
    {
        TestLeftTexts[3].gameObject.SetActive(true);
        TestRightTexts[3].gameObject.SetActive(true);
    }

    void Update()
    {
        InputExcute();
    }

    void InputExcute()
    {
         if (DataManager.dataBackgroundProcActive)
         {
            if (!GlobalState.Instance.IsTweening)
            {
                if (GlobalState.Instance.DevMode)
                {
                    InputReturn();
                    InputEscape();
                }
            }
         }
    }

    void InputReturn()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            UIElementFadePanel.Instance.ResultToMusicSelect();
        }
    }

    void InputEscape()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UIElementFadePanel.Instance.ResultToStage();
        }
    }

    private void GetWakZooURL()
    {
        _wakZooURL = Config.Instance.WakZoo;
    }

    private void SetMusicURL(string originURL, string remixURL)
    {
        _originURL = originURL;
        _reMixURL = remixURL;
    }

    private void GetMusicURL()
    {
        var config = Config.Instance;

        switch (GlobalState.Instance.AlbumIndex)
        {
            case (int)GlobalData.ALBUM.ISEDOL:
                switch (GlobalState.Instance.StageIndex)
                {
                    case (int)GlobalData.STAGE.STAGE1:
                        SetMusicURL(config.Origin_Rewind, config.ReMix_Rewind);
                        break;
                    case (int)GlobalData.STAGE.STAGE2:
                        SetMusicURL(config.Origin_WinterSpring, config.ReMix_WinterSpring);
                        break;
                }
                break;
            case (int)GlobalData.ALBUM.CONTEST:
                switch (GlobalState.Instance.StageIndex)
                {
                    case (int)GlobalData.STAGE.STAGE1:
                        SetMusicURL(config.Origin_NobleLick, config.ReMix_NobleLick);
                        break;
                    case (int)GlobalData.STAGE.STAGE2:
                        SetMusicURL(config.Origin_Wakaloid, config.ReMix_Wakaloid);
                        break;
                    case (int)GlobalData.STAGE.STAGE3:
                        SetMusicURL(config.Origin_WakGoodAroma100, config.ReMix_WakGoodAroma100);
                        break;
                    case (int)GlobalData.STAGE.STAGE4:
                        SetMusicURL(config.Origin_AvantGarde, config.ReMix_AvantGarde);
                        break;
                }
                break;
            case (int)GlobalData.ALBUM.GOMIX:
                switch (GlobalState.Instance.StageIndex)
                {
                    case (int)GlobalData.STAGE.STAGE1:
                        SetMusicURL(config.Origin_YouHi, config.ReMix_YouHi);
                        break;
                    case (int)GlobalData.STAGE.STAGE2:
                        SetMusicURL(config.Origin_Gotterfly, config.ReMix_Gotterfly);
                        break;
                    case (int)GlobalData.STAGE.STAGE3:
                        SetMusicURL(config.Origin_KingADance, config.ReMix_KingADance);
                        break;
                    case (int)GlobalData.STAGE.STAGE4:
                        SetMusicURL(config.Origin_IPad, config.ReMix_IPad);
                        break;
                    case (int)GlobalData.STAGE.STAGE5:
                        SetMusicURL(config.Origin_ReviveLikeADog, config.ReMix_ReviveLikeADog);
                        break;
                }
                break;
            case (int)GlobalData.ALBUM.WAKALOID:
                switch (GlobalState.Instance.StageIndex)
                {
                    case (int)GlobalData.STAGE.STAGE1:
                        SetMusicURL(config.Origin_GalUseGirl, config.ReMix_GalUseGirl);
                        break;
                    case (int)GlobalData.STAGE.STAGE2:
                        SetMusicURL(config.Origin_BangOff, config.ReMix_BangOff);
                        break;
                    case (int)GlobalData.STAGE.STAGE3:
                        SetMusicURL(config.Origin_TwistedLove, config.ReMix_TwistedLove);
                        break;
                    case (int)GlobalData.STAGE.STAGE4:
                        SetMusicURL(config.Origin_Waklio, config.ReMix_Waklio);
                        break;
                }
                break;
        }
    }

    public void OnClickOpenWakZooURL()
    {
        Application.OpenURL(_wakZooURL);
    }

    public void OnClickOpenOriginURL()
    {
        Application.OpenURL(_originURL);
    }

    public void OnClickOpenRemixURL()
    {
        Application.OpenURL(_reMixURL);
    }

    public void OnClickReplay()
    {
        if (DataManager.dataBackgroundProcActive)
        {
            UIElementFadePanel.Instance.ResultToStage();
            //UIManager.Instance.GoPanelGamePlay();
        }
    }

    public void OnClickConfirm()
    {
        UIElementFadePanel.Instance.ResultToMusicSelect();
    }
}
