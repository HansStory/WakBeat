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
    [SerializeField] private UIElementIntro uiElementIntro;
    [SerializeField] private UIElementMain uiElementMain;
    [SerializeField] private UIElementAlbumSelect uiElementAlbumSelect;
    [SerializeField] private UIElementMusicSelect uiElementMusicSelect;
    [SerializeField] private UIElementGamePlay uiElementGamePlay;
    [SerializeField] private UIElementResult uiElementResult;
    [Header("UI Setting Panel")]
    [SerializeField] private UIElementSetting uiElementSetting;
    [Header("UI Fade Panel")]
    [SerializeField] private UIElementFadePanel uiElementFadePanel;

    public const int startPanel = 0;

    void Start()
    {
        Init();
    }

    private void Init()
    {
        if (uiElementSetting == null)
        {
            Debug.LogError("UIElementSetting 이 없습니다.");
        }

        if (uiElementFadePanel == null)
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

        //// 화면 전환 시 버튼 영역 제어  // 잠시 하이드 처리하겠습니다 KD_Han
        uiElementSetting.PanelViewController(index);
    }


    public void GoPanelIntro()
    {
        WantShowPanel((int)GlobalData.UIMODE.INTRO);
    }

    public void GoPanelMain()
    {
        if (GlobalState.Instance.CurrentPanelIndex == (int)GlobalData.UIMODE.SELECT_ALBUM)
        {
            if (uiElementFadePanel)
            {
                uiElementFadePanel.BetweenMainToAlbumTransition();
                uiElementFadePanel.TransitionSequence.InsertCallback(1f, () => WantShowPanel((int)GlobalData.UIMODE.MAIN));
            }
        }
        else if(GlobalState.Instance.CurrentPanelIndex == (int)GlobalData.UIMODE.INTRO)
        {
            if (uiElementFadePanel)
            {
                uiElementFadePanel.IntroToMain(1f, 1f, 1f, 2f);

                uiElementFadePanel.TransitionSequence.InsertCallback(2f, () => SoundManager.Instance.TurnOnGameBackGround());
                uiElementFadePanel.TransitionSequence.InsertCallback(3f, () => WantShowPanel((int)GlobalData.UIMODE.MAIN));
            }
        }
    }

    public void GoPanelAlbumSelect()
    {
        if (GlobalState.Instance.CurrentPanelIndex == (int)GlobalData.UIMODE.SELECT_MUSIC)
        {
            uiElementAlbumSelect.ShowHideAlbumList(1.5f);
            uiElementFadePanel.BetweenAlbumToMusicTransition(0.5f, 0f);
            uiElementFadePanel.TransitionSequence.InsertCallback(1f, () => WantShowPanel((int)GlobalData.UIMODE.SELECT_ALBUM));
            SoundManager.Instance.TurnOnGameBackGround();
        }
        else if (GlobalState.Instance.CurrentPanelIndex == (int)GlobalData.UIMODE.MAIN)
        {
            if (uiElementFadePanel)
            {
                uiElementFadePanel.BetweenMainToAlbumTransition();
                uiElementFadePanel.TransitionSequence.InsertCallback(1f, () => WantShowPanel((int)GlobalData.UIMODE.SELECT_ALBUM));
            }
        }
    }

    public void GoPanelMusicSelect()
    {
        if (GlobalState.Instance.CurrentPanelIndex == (int)GlobalData.UIMODE.SELECT_ALBUM)
        {
            //uiElementAlbumSelect.SelectAlbum();
            uiElementFadePanel.BetweenAlbumToMusicTransition(0.5f, 0f);
            uiElementFadePanel.TransitionSequence.InsertCallback(1f, () => WantShowPanel((int)GlobalData.UIMODE.SELECT_MUSIC));
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
}
