using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using static GlobalData;

public class UIElementResult : MonoBehaviour
{
    public Transform ResultBanner;
    public Image ResultTitle;
    public Image ResultThumnail;
    public Image OriginLink;
    public Image ReMixLink;

    public Image[] ImageLevels;

    public TMP_Text[] RightTexts;

    private GlobalState state;
    private ResultInfo resultInfo;

    private Vector2 originStartVector = new Vector2(-416f, -180f);
    private Vector2 originTargetVector = new Vector2(21.2f, -180f);

    private Vector2 remixLinkStartVector = new Vector2(-416f, -281.5f);
    private Vector2 remixLinkTargetVector = new Vector2(21.2f, -281.5f);

    private string _wakZooURL = string.Empty;
    private string _originURL = string.Empty;
    private string _reMixURL = string.Empty;

    void Awake()
    {
        GetWakZooURL();

        state = GlobalState.Instance;
        resultInfo = GlobalData.Instance.ResultInfo;
    }

    private void OnEnable()
    {
        SoundManager.Instance.TurnOnResultAudio();

        GetMusicURL();

        SetResultImages();
        SetTextStageResult();

        TweenResultBanner();

        TweenLink(OriginLink, originStartVector, originTargetVector, 0.5f);
        TweenLink(ReMixLink, remixLinkStartVector, remixLinkTargetVector, 0.8f);
    }

    private void OnDisable()
    {
        ReSetStageLevel();
    }

    //---------------------------------------------------------------------------------------------
    void SetResultImages()
    {
        ALBUM album = (ALBUM)GlobalState.Instance.AlbumIndex;
        STAGE stage = (STAGE)GlobalState.Instance.StageIndex;

        switch (album)
        {
            case ALBUM.ISEDOL:
                switch (stage)
                {
                    case STAGE.STAGE1:
                        GetResultImages(resultInfo.FirstAlbumTitles, resultInfo.FirstAlbumThumnails, STAGE.STAGE1, 2);
                        break;
                    case STAGE.STAGE2:
                        GetResultImages(resultInfo.FirstAlbumTitles, resultInfo.FirstAlbumThumnails, STAGE.STAGE2, 3);
                        break;
                }
                break;
            case ALBUM.CONTEST:
                switch (stage)
                {
                    case STAGE.STAGE1:
                        GetResultImages(resultInfo.SecondAlbumTitles, resultInfo.SecondAlbumThumnails, STAGE.STAGE1, 2);
                        break;
                    case STAGE.STAGE2:
                        GetResultImages(resultInfo.SecondAlbumTitles, resultInfo.SecondAlbumThumnails, STAGE.STAGE2, 4);
                        break;
                    case STAGE.STAGE3:
                        GetResultImages(resultInfo.SecondAlbumTitles, resultInfo.SecondAlbumThumnails, STAGE.STAGE3, 7);
                        break;
                    case STAGE.STAGE4:
                        GetResultImages(resultInfo.SecondAlbumTitles, resultInfo.SecondAlbumThumnails, STAGE.STAGE4, 10);
                        break;
                }
                break;
            case ALBUM.GOMIX:
                switch (stage)
                {
                    case STAGE.STAGE1:
                        GetResultImages(resultInfo.ThirdAlbumTitles, resultInfo.ThirdAlbumThumnails, STAGE.STAGE1, 5);
                        break;
                    case STAGE.STAGE2:
                        GetResultImages(resultInfo.ThirdAlbumTitles, resultInfo.ThirdAlbumThumnails, STAGE.STAGE2, 6);
                        break;
                    case STAGE.STAGE3:
                        GetResultImages(resultInfo.ThirdAlbumTitles, resultInfo.ThirdAlbumThumnails, STAGE.STAGE3, 7);
                        break;
                    case STAGE.STAGE4:
                        GetResultImages(resultInfo.ThirdAlbumTitles, resultInfo.ThirdAlbumThumnails, STAGE.STAGE4, 8);
                        break;
                    case STAGE.STAGE5:
                        GetResultImages(resultInfo.ThirdAlbumTitles, resultInfo.ThirdAlbumThumnails, STAGE.STAGE5, 10);
                        break;
                }
                break;
            case ALBUM.WAKALOID:
                switch (stage)
                {
                    case STAGE.STAGE1:
                        GetResultImages(resultInfo.FourthAlbumTitles, resultInfo.FourthAlbumThumnails, STAGE.STAGE1, 4);
                        break;
                    case STAGE.STAGE2:
                        GetResultImages(resultInfo.FourthAlbumTitles, resultInfo.FourthAlbumThumnails, STAGE.STAGE2, 6);
                        break;
                    case STAGE.STAGE3:
                        GetResultImages(resultInfo.FourthAlbumTitles, resultInfo.FourthAlbumThumnails, STAGE.STAGE3, 8);
                        break;
                    case STAGE.STAGE4:
                        GetResultImages(resultInfo.FourthAlbumTitles, resultInfo.FourthAlbumThumnails, STAGE.STAGE4, 10);
                        break;
                }      
                break;
            case ALBUM.CONTEST2:
                switch (stage)
                {
                    case STAGE.STAGE1:
                        GetResultImages(resultInfo.FifthAlbumTitles, resultInfo.FifthAlbumThumnails, STAGE.STAGE1, 7);
                        break;
                    case STAGE.STAGE2:
                        GetResultImages(resultInfo.FifthAlbumTitles, resultInfo.FifthAlbumThumnails, STAGE.STAGE2, 8);
                        break;
                }
                break;
        }
    }

    void GetResultImages(Sprite[] title, Sprite[] thumnail, STAGE stage, int level)
    {
        if (ResultTitle == null) return;
        ResultTitle.sprite = title[(int)stage];
        ResultTitle.SetNativeSize();

        if (ResultThumnail == null) return;
        ResultThumnail.sprite = thumnail[(int)stage];
        ResultThumnail.SetNativeSize();

        GetStageLevel(level);
    }

    private int _maxStar = 5;
    void GetStageLevel(int level)
    {
        bool isLevelEndHP = false;

        for (int i = 0; i < _maxStar; i++)
        {
            level -= 2;

            if (!isLevelEndHP)
            {
                if (level >= 0)
                {
                    if (ImageLevels[i] == null) return;
                    ImageLevels[i].sprite = resultInfo.StarOn;

                    if (level == 0)
                    {
                        isLevelEndHP = true;
                    }
                }
                else
                {
                    if (ImageLevels[i] == null) return;
                    ImageLevels[i].sprite = resultInfo.StarHalf;
                    isLevelEndHP = true;
                }
            }
            else
            {
                ImageLevels[i].sprite = resultInfo.StarOff;
            }
        }
    }

    void ReSetStageLevel()
    {
        for (int i = 0; i < ImageLevels.Length; i++)
        {
            ImageLevels[i].sprite = resultInfo.StarOff;
        }
    }

    //---------------------------------------------------------------------------------------------
    private static int MusicLength = 0;
    private static int TotalPlayTime = 1;
    private static int DeathCount = 2;
    private static int UsedItem = 3;

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
        RightTexts[MusicLength].text = $"{min} : {sec.ToString("D2")}";
    }

    private void SetPlayTime()
    {
        if (state.StageMusicLength == 0) return;

        int min = state.StagePlayTime / 60;
        int sec = state.StagePlayTime % 60;
        RightTexts[TotalPlayTime].text = $"{min} : {sec.ToString("D2")}";
    }

    private void SetPlayerDeathCount()
    {
        RightTexts[DeathCount].text = $"{state.PlayerDeadCount} 회";
    }

    private void SetUsedItem()
    {
        string _usingItems = "";
        string PrintString = "";

        for(int i = 0; i < DataManager.dataSkillCount; i++)
        {
            if (state.UsedItems[i].Equals("Y"))
            {
                switch(i)
                {
                    case 0: _usingItems = "애송이 모드"; break;
                    case 1: _usingItems = "까방권";  break;
                    case 2: _usingItems = "일시무적"; break;
                    case 3: _usingItems = "뉴가메"; break;
                    case 4: _usingItems = "분석안"; break;
                    case 5: _usingItems = "자율주행"; break;
                }

                if(PrintString.Length > 0)
                {
                    PrintString += ", " + _usingItems;
                } 
                else
                {
                    PrintString += _usingItems;
                }
            }
        }

        if (PrintString.Length <= 0)
        {
            PrintString = "없음";
        }

        RightTexts[UsedItem].text = $"{PrintString}";
    }

    //---------------------------------------------------------------------------------------------
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
    
    //---------------------------------------------------------------------------------------------
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

        ALBUM album = (ALBUM)GlobalState.Instance.AlbumIndex;
        STAGE stage = (STAGE)GlobalState.Instance.StageIndex;

        switch (album)
        {
            case ALBUM.ISEDOL:
                switch (stage)
                {
                    case STAGE.STAGE1:
                        SetMusicURL(config.Origin_Rewind, config.ReMix_Rewind);
                        break;
                    case STAGE.STAGE2:
                        SetMusicURL(config.Origin_WinterSpring, config.ReMix_WinterSpring);
                        break;
                }
                break;
            case ALBUM.CONTEST:
                switch (stage)
                {
                    case STAGE.STAGE1:
                        SetMusicURL(config.Origin_NobleLick, config.ReMix_NobleLick);
                        break;
                    case STAGE.STAGE2:
                        SetMusicURL(config.Origin_Wakaloid, config.ReMix_Wakaloid);
                        break;
                    case STAGE.STAGE3:
                        SetMusicURL(config.Origin_WakGoodAroma100, config.ReMix_WakGoodAroma100);
                        break;
                    case STAGE.STAGE4:
                        SetMusicURL(config.Origin_AvantGarde, config.ReMix_AvantGarde);
                        break;
                }
                break;
            case ALBUM.GOMIX:
                switch (stage)
                {
                    case STAGE.STAGE1:
                        SetMusicURL(config.Origin_YouHi, config.ReMix_YouHi);
                        break;
                    case STAGE.STAGE2:
                        SetMusicURL(config.Origin_Gotterfly, config.ReMix_Gotterfly);
                        break;
                    case STAGE.STAGE3:
                        SetMusicURL(config.Origin_KingADance, config.ReMix_KingADance);
                        break;
                    case STAGE.STAGE4:
                        SetMusicURL(config.Origin_IPad, config.ReMix_IPad);
                        break;
                    case STAGE.STAGE5:
                        SetMusicURL(config.Origin_ReviveLikeADog, config.ReMix_ReviveLikeADog);
                        break;
                }
                break;
            case ALBUM.WAKALOID:
                switch (stage)
                {
                    case STAGE.STAGE1:
                        SetMusicURL(config.Origin_GalUseGirl, config.ReMix_GalUseGirl);
                        break;
                    case STAGE.STAGE2:
                        SetMusicURL(config.Origin_BangOff, config.ReMix_BangOff);
                        break;
                    case STAGE.STAGE3:
                        SetMusicURL(config.Origin_TwistedLove, config.ReMix_TwistedLove);
                        break;
                    case STAGE.STAGE4:
                        SetMusicURL(config.Origin_Waklio, config.ReMix_Waklio);
                        break;
                }
                break;
            case ALBUM.CONTEST2:
                switch (stage)
                {
                    case STAGE.STAGE1:
                        SetMusicURL(config.Origin_Jungtur, config.ReMix_Jungtur);
                        break;
                    case STAGE.STAGE2:
                        SetMusicURL(config.Origin_Jungtur, config.ReMix_Jungtur);
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
        }
    }

    public void OnClickConfirm()
    {
        UIElementFadePanel.Instance.ResultToMusicSelect();
    }
}
