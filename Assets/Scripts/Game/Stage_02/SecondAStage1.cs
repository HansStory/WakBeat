using UnityEngine;

public class SecondAStage1 : Stage
{
    protected override void Init()
    {
        base.Init();

        SetBallSkin(GlobalData.Instance.StageInfo.BallSkins[4]);
        SetCircleSprite(GlobalData.Instance.StageInfo.CircleSkins[1]);
        SetBackGroundSprite(GlobalData.Instance.StageInfo.BackGroundSkins[1]);
    }

    protected override void Update()
    {
        base.Update();
    }

    private float ballHeightHalf = 15.5f;
    private float obstacleHeightHalf = 56.5f;
    protected override void CreateObstacles()
    {
        float _inRadius = inRadius - obstacleHeightHalf + ballHeightHalf;
        float _outRadius = outRadius + obstacleHeightHalf - ballHeightHalf;

        CreateInObstacle(1, _inRadius);
        CreateOutObstacle(1, _outRadius);
    }

}
