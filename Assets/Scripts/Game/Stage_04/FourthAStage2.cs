using UnityEngine;

public class FourthAStage2 : Stage
{
    Color _colorMain = new Color32(91, 82, 65, 255);

    protected override void Init()
    {
        base.Init();        

        SetBallSkin(GlobalData.Instance.StageInfo.BallSkins[10]);

        SetObstaclesColor(Color.white);
    }


    private void ChangeObstacleWhite()
    {
        SetObstaclesColor(Color.white);
    }

    private void ChangeObstacleOrange()
    {
        SetObstaclesColor(_colorMain);
    }
}