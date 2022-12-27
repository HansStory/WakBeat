using System.Collections;
using UnityEditorInternal;
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
        BallSkin.color = new Color(1, 1, 1, 0.5f);
        CircleSkin.color = new Color(1, 1, 1, 0.5f);

        var stageInfo = GlobalData.Instance.StageInfo;

        foreach (var obstacle in InObstacleLists)
        {
            var skin = obstacle.GetComponent<Image>();
            if (skin)
            {
                if (stageInfo.ObstacleSkins[6])
                {
                    skin.sprite = stageInfo.ObstacleSkins[6];
                    skin.color = new Color(1, 1, 1, 0.5f);
                }
            }
        }

        foreach (var obstacle in OutObstacleLists)
        {
            var skin = obstacle.GetComponent<Image>();
            if (skin)
            {
                if (stageInfo.ObstacleSkins[6])
                {
                    skin.sprite = stageInfo.ObstacleSkins[6];
                    skin.color = new Color(1, 1, 1, 0.5f);
                }
            }
        }
    }

    void ChangeSkinWhite()
    {
        BallSkin.color = Color.white;
        CircleSkin.color = Color.white;

        var stageInfo = GlobalData.Instance.StageInfo;

        foreach (var obstacle in InObstacleLists)
        {
            var skin = obstacle.GetComponent<Image>();
            if (skin)
            {
                if (stageInfo.ObstacleSkins[6])
                {
                    skin.sprite = stageInfo.ObstacleSkins[6];
                    skin.color = Color.white;
                }
            }
        }

        foreach (var obstacle in OutObstacleLists)
        {
            var skin = obstacle.GetComponent<Image>();
            if (skin)
            {
                if (stageInfo.ObstacleSkins[6])
                {
                    skin.sprite = stageInfo.ObstacleSkins[6];
                    skin.color = Color.white;
                }
            }
        }
    }

    void ChangeSkinPixel()
    {
        CircleSkin.sprite = GlobalData.Instance.StageInfo.CircleSkins[8];
        BallSkin.sprite = GlobalData.Instance.StageInfo.BallSkins[11];
        SetObstaclesSkin(8);
    }


    protected override void Update()
    {
        base.Update();
    }
}