using UnityEngine;

public class FourthAStage1 : Stage
{
    Color _colorMain = new Color32(253, 220, 136, 255);
    Color _gray = new Color32(225, 218, 200, 255);

    protected override void Init()
    {
        base.Init();

        SetBallSkin(GlobalData.Instance.StageInfo.BallSkins[10]);
        SetObstaclesColor(_colorMain);
    }


    void SetObstacleColorOrigin()
    {
        SetObstaclesColor(_colorMain);
    }

    void SetObstacleColorWhite()
    {
        SetObstaclesColor(Color.white);
    }

    void SetObstacleColorGray()
    {
        SetObstaclesColor(_gray);
    }

    void SetBackGroundOrigin()
    {
        SetBackGroundSprite(GlobalData.Instance.StageInfo.BackGroundSkins[2]);
    }

    void SetBackGroundSpace()
    {
        SetBackGroundSprite(GlobalData.Instance.StageInfo.BackGroundSkins[3]);
    }


    protected override void Update()
    {
        base.Update();
    }
}