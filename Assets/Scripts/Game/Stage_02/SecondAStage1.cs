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

    protected override void ShowOutObstacles()
    {
        var beatItem = bmwReader.ChartingItem[_currentLine];

        if (beatItem != null)
        {

            // Show Objects
            if (beatItem.OutObstacleElements[0].Index > -1)
            {
                foreach (var outObstacle in beatItem.OutObstacleElements)
                {
                    OutObstacleLists[outObstacle.Index].gameObject.SetActive(true);
                }
            }

            // Show Dummy
            if (beatItem.DummyOutObstacleElements[0].Index > -1)
            {
                foreach (var dummy in beatItem.DummyOutObstacleElements)
                {
                    OutObstacleLists[dummy.Index].gameObject.SetActive(true);
                }
            }
        }
    }

    protected override void ShowInObstacles()
    {
        if (_currentLine >= bmwReader.ChartingItem.Count) return;

        var beatItem = bmwReader.ChartingItem[_currentLine];

        if (beatItem != null)
        {
            // Show Objects
            if (beatItem.InObstacleElements[0].Index > -1)
            {
                foreach (var inObstacle in beatItem.InObstacleElements)
                {
                    InObstacleLists[inObstacle.Index].gameObject.SetActive(true);
                }
            }

            // Show Dummy
            if (beatItem.DummyInObstacleElements[0].Index > -1)
            {
                foreach (var dummy in beatItem.DummyInObstacleElements)
                {
                    InObstacleLists[dummy.Index].gameObject.SetActive(true);
                }
            }
        }
    }

}
