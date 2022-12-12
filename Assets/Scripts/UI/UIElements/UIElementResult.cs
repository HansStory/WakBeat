using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIElementResult : MonoBehaviour
{
    public Transform ResultBanner;
    public Image OriginLink;
    public Image ReMixLink;

    private Vector2 originStartVector = new Vector2(-416f, -180f);
    private Vector2 originTargetVector = new Vector2(21.2f, -180f);

    private Vector2 remixLinkStartVector = new Vector2(-416f, -281.5f);
    private Vector2 remixLinkTargetVector = new Vector2(21.2f, -281.5f);

    [SerializeField] private UIElementPopUp UIElementPopUp;

    private string _wakZooURL = string.Empty;
    private string _originURL = string.Empty;
    private string _reMixURL = string.Empty;

    void Start()
    {
        GetWakZooURL();
    }

    private void OnEnable()
    {
        GetMusicURL();

        TweenResultBanner();

        TweenLink(OriginLink, originStartVector, originTargetVector, 0.5f);
        TweenLink(ReMixLink, remixLinkStartVector, remixLinkTargetVector, 0.8f);
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

    void Update()
    {
        OnClickCheckButton();
    }

    void OnClickCheckButton()
    {
         if (DataManager.dataBackgroundProcActive)
         {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                UIManager.Instance.GoPanelMusicSelect();
            }
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
            UIManager.Instance.GoPanelGamePlay();
            //UIElementPopUp.MakePopUpMusicInfo();
        }
    }

    public void OnClickConfirm()
    {
        UIManager.Instance.GoPanelMusicSelect();
    }
}
