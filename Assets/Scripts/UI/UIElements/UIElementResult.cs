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

    private string url;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
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

    // Update is called once per frame
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

    public void OnClickOpenWakZooURL()
    {
        url = Config.Instance.WakZoo;
        Application.OpenURL(url);
    }

    public void OnClickOpenOriginURL()
    {
        url = Config.Instance.Origin_Rewind;
        Application.OpenURL(url);
    }

    public void OnClickOpenRemixURL()
    {
        url = Config.Instance.ReMix_Rewind;
        Application.OpenURL(url);
    }

    public void OnClickReplay()
    {

    }

    public void OnClickConfirm()
    {

    }
}
