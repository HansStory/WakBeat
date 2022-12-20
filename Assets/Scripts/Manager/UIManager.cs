using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UIManager : MonoBehaviourSingleton<UIManager>
{
    // ----------------------------- Fade --------------------------------
    #region Fade Management
    // Toggle Fade In Out => bool (isShow)
    public void FadeInOut(Image fadeImage, float fadeTime, bool isShow)
    {
        if (isShow == true)
        {
            fadeImage.CrossFadeAlpha(1f, fadeTime, false);
        }

        if (isShow == false)
        {
            fadeImage.CrossFadeAlpha(0f, fadeTime, false);
        }
    }

    public void FadeToBlack(Image fadeImage, float fadeTime)
    {
        fadeImage.CrossFadeAlpha(0f, 0f, false);
        fadeImage.CrossFadeAlpha(1f, fadeTime, false);
    }

    public void FadeToWhite(Image fadeImage, float fadeTime)
    {
        fadeImage.CrossFadeAlpha(1f, 0f, false);
        fadeImage.CrossFadeAlpha(0f, fadeTime, false);
    }
    #endregion


    // -------------------------------------------------------------------
    public GameObject[] MainPanels;

    [Header("UI Main Panels")]
    public UIElementIntro UIElementIntro;
    public UIElementMain UIElementMain;
    public UIElementAlbumSelect UIElementAlbumSelect;
    public UIElementMusicSelect UIElementMusicSelect;
    public UIElementGamePlay UIElementGamePlay;
    public UIElementResult UIElementResult;

    [Header("UI Setting Panel")]
    public UIElementSetting UIElementSetting;

    [Header("UI Fade Panel")]
    public UIElementPopUp UIElementPopUp;

    [Header("UI Fade Panel")]
    public UIElementFadePanel UIElementFadePanel;

    public const int startPanel = 0;

    void Start()
    {
        Init();
    }

    private void Init()
    {
        if (UIElementSetting == null)
        {
            Debug.LogError("UIElementSetting 이 없습니다.");
        }

        if (UIElementFadePanel == null)
        {
            Debug.LogError("UIElementFadePanel 이 없습니다.");
        }

        WantShowPanel(startPanel);
    }

    public void WantShowPanel(int index)
    {
        if (MainPanels != null)
        {
            for (int i = 0; i < MainPanels.Length; i++)
            {
                MainPanels[i].SetActive(index == i);
                if (index == i)
                {
                    GlobalState.Instance.CurrentPanelIndex = i;

                    Debug.Log($"{nameof(GlobalState.Instance.CurrentPanelIndex)} : {GlobalState.Instance.CurrentPanelIndex}");
                }
            }
        }

        // 화면 전환 시 버튼 영역 제어  // 잠시 하이드 처리하겠습니다 KD_Han
        if (UIElementSetting)
        {
            UIElementSetting.PanelViewController(index);
        }
    }


    public void GoPanelIntro()
    {
        WantShowPanel((int)GlobalData.UIMODE.INTRO);
    }

    public void GoPanelMain()
    {
        if (GlobalState.Instance.CurrentPanelIndex == (int)GlobalData.UIMODE.SELECT_ALBUM)
        {
            if (UIElementFadePanel)
            {
                UIElementFadePanel.MainToAlbum();
                UIElementFadePanel.TransitionSequence.InsertCallback(1f, () => WantShowPanel((int)GlobalData.UIMODE.MAIN));
            }
        }
        else if(GlobalState.Instance.CurrentPanelIndex == (int)GlobalData.UIMODE.INTRO)
        {
            if (UIElementFadePanel)
            {
                UIElementFadePanel.IntroToMain(1f, 1f, 1f, 2f);

                UIElementFadePanel.TransitionSequence.InsertCallback(2f, () => SoundManager.Instance.TurnOnGameBackGround());
                UIElementFadePanel.TransitionSequence.InsertCallback(3f, () => WantShowPanel((int)GlobalData.UIMODE.MAIN));
            }
        }
    }

    public void GoPanelAlbumSelect()
    {
        if (GlobalState.Instance.CurrentPanelIndex == (int)GlobalData.UIMODE.SELECT_MUSIC)
        {
            UIElementAlbumSelect.ShowHideAlbumList(1.5f);
            UIElementFadePanel.AlbumToMusic(0.5f, 0f);
            UIElementFadePanel.TransitionSequence.InsertCallback(1f, () => WantShowPanel((int)GlobalData.UIMODE.SELECT_ALBUM));
            SoundManager.Instance.TurnOnGameBackGround();
        }
        else if (GlobalState.Instance.CurrentPanelIndex == (int)GlobalData.UIMODE.MAIN)
        {
            if (UIElementFadePanel)
            {
                UIElementFadePanel.MainToAlbum();
                UIElementFadePanel.TransitionSequence.InsertCallback(1f, () => WantShowPanel((int)GlobalData.UIMODE.SELECT_ALBUM));
            }
        }
    }

    public void GoPanelMusicSelect()
    {
        if (GlobalState.Instance.CurrentPanelIndex == (int)GlobalData.UIMODE.SELECT_ALBUM)
        {
            //uiElementAlbumSelect.SelectAlbum();
            UIElementFadePanel.AlbumToMusic(0.5f, 0f);
            UIElementFadePanel.TransitionSequence.InsertCallback(1f, () => WantShowPanel((int)GlobalData.UIMODE.SELECT_MUSIC));
            //WantShowPanel((int)GlobalData.UIMODE.SELECT_MUSIC);
        }
        else if (GlobalState.Instance.CurrentPanelIndex == (int)GlobalData.UIMODE.GAME)
        {
            WantShowPanel((int)GlobalData.UIMODE.SELECT_MUSIC);
        }
        else
        {
            WantShowPanel((int)GlobalData.UIMODE.SELECT_MUSIC);
        }
    }

    public void GoPanelGamePlay()
    {
        if (GlobalState.Instance.CurrentPanelIndex == (int)GlobalData.UIMODE.SELECT_MUSIC 
            || GlobalState.Instance.CurrentPanelIndex == (int)GlobalData.UIMODE.RESULT)
        {
            WantShowPanel((int)GlobalData.UIMODE.GAME);
        }
    }

    public void GoPanelResult()
    {
        WantShowPanel((int)GlobalData.UIMODE.RESULT);
    }


    public void MakeTutorial(GameObject tutorial, Transform tutorialBase, Image image, float duration, float delay, float stay)
    {
        if (!DataManager.dataFileYn)
        {
            var uiTutorial = Instantiate(tutorial, this.transform);
            image = uiTutorial.GetComponent<Image>();

            if (!image) return;

            image.color = Color.clear;
            ShowTutorial(image, duration, delay, stay);
        }
    }


    void ShowTutorial(Image image, float duration, float delay, float stay)
    {
        var tween = image.DOColor(Color.white, duration);
        tween.SetAutoKill().SetEase(Ease.OutQuad).SetDelay(delay).OnComplete(() => HideAlbumTutorial(image, duration, stay));
    }

    void HideAlbumTutorial(Image image, float duration, float stay)
    {
        var tween = image.DOColor(Color.clear, duration);
        tween.SetAutoKill().SetEase(Ease.OutQuad).SetDelay(stay).OnComplete(() => OnCompleteTutorialTween(image));
    }

    void OnCompleteTutorialTween(Image image)
    {
        if (image)
        {
            Destroy(image);
        }
    }
}
