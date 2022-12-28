using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ThirdAStage1 : Stage
{

    protected override void Init()
    {
        base.Init();

        SetBallSkin(GlobalData.Instance.StageInfo.BallSkins[9]);
        SetCircleSprite(GlobalData.Instance.StageInfo.CircleSkins[5]);
    }

    protected override void PlayProcess()
    {
        base.PlayProcess();

        switch (_currentLine)
        {
            case 0:
                SetBallSkin(GlobalData.Instance.StageInfo.BallSkins[9]);
                SetCircleSprite(GlobalData.Instance.StageInfo.CircleSkins[5]);
                ChangeSkinAlpha();
                break;
            case 1:
                Invoke(nameof(ChangeSkinWhite), _tick - 0.2f);
                break;
            case 9:
                Invoke(nameof(ChangeSkinAlpha), _tick + 0.2f);
                break;
            case 12:
                Invoke(nameof(ChangeSkinWhite), _tick - 0.5f);
                break;
            case 13:
                Invoke(nameof(ChangeSkinAlpha), _tick - 2f);
                break;
            case 14:
                Invoke(nameof(ChangeSkinWhite), _tick - 0.2f);
                break;
            case 20:
                Invoke(nameof(ChangeSkinAlpha), _tick + 0.15f);
                break;
            case 23:
                Invoke(nameof(ChangeSkinPixel), _tick - 0.2f);
                break;
        }
    }

    void ChangeSkinAlpha()
    {
        var alphaColor = new Color(1, 1, 1, 0.5f);

        BallSkin.color = alphaColor;
        CircleSkin.color = alphaColor;
        SetObstaclesSkin(6, alphaColor);
    }

    void ChangeSkinWhite()
    {
        BallSkin.color = Color.white;
        CircleSkin.color = Color.white;

        SetObstaclesSkin(6, Color.white);
    }

    void ChangeSkinPixel()
    {
        CircleSkin.sprite = GlobalData.Instance.StageInfo.CircleSkins[8];
        CircleSkin.color = Color.black;

        BallSkin.sprite = GlobalData.Instance.StageInfo.BallSkins[11];
        BallSkin.color = Color.black;

        SetObstaclesSkin(8, Color.black);
    }


    protected override void Update()
    {
        base.Update();
    }
}