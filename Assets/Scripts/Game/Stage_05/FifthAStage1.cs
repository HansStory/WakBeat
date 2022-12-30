using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FifthAStage1 : Stage
{
    protected override void Init()
    {
        base.Init();

        SetBallSkin(GlobalData.Instance.StageInfo.BallSkins[12]);
        SetCircleSprite(GlobalData.Instance.StageInfo.CircleSkins[9]);
        SetObstaclesSkin(9, Color.white);
    }

    protected override void PlayProcess()
    {
        base.PlayProcess();

        switch (_currentLine)
        {
            case 0:
                SetObstaclesSkin(9, Color.white);
                break;
            case 5:
                SetObstaclesSkin(10, Color.white);
                break;
            case 10:
                SetObstaclesSkin(11, Color.white);
                break;
            case 12:
                break;
            case 13:
                break;
            case 14:
                break;
            case 20:
                break;
            case 23:
                break;
        }
    }

    protected override void Update()
    {
        base.Update();
    }
}