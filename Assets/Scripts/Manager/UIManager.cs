using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviourSingleton<UIManager>
{    
    // Toggle Fade In Out => bool (isShow)
    public void FadeInOut(Image fadeImage, float fadeTime, bool isShow)
    {
        // Deep to Black
        if (isShow == true)
        {
            //Fully fade in Image (1) with the duration of 2
            fadeImage.CrossFadeAlpha(1, fadeTime, false);
        }
        // Deep to White
        if (isShow == false)
        {
            fadeImage.CrossFadeAlpha(0, fadeTime, false);
        }
    }

    public GameObject[] MainPanels;

    public void WantShowPanel(int index)
    {
        if (MainPanels != null)
        {
            for (int i = 0; i < MainPanels.Length; i++)
            {
                MainPanels[i].SetActive(index == i);
            }
        }
    }

    public void GoPanelMain()
    {
        WantShowPanel(1);
    }

    public void GoPanelAlbumSelect()
    {
        WantShowPanel(2);
    }

    public void GoPanelMusicSelect()
    {
        WantShowPanel(3);
    }

    public void GoPanelGamePlay()
    {
        WantShowPanel(4);
    }

    public void GoPanelResult()
    {
        WantShowPanel(5);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
