using UnityEngine;

public class ThirdAStage1 : Stage
{
    private Color _gray = new Color32(79, 79, 79, 255);
    private Color _alpha = new Color32(79, 79, 79, 128);

    protected override void Init()
    {
        base.Init();

        SetBallSkin(GlobalData.Instance.StageInfo.BallSkins[10]);
        //SetObstaclesColor(_alpha);
    }

    void ChangeObstacleAlpha()
    {
        SetObstaclesColor(_alpha);
    }

    void ChangeObstacleGray()
    {
        SetObstaclesColor(_gray);
    }

    void ChangeObstacleBlack()
    {       
        SetObstaclesColor(Color.black);
    }

    void ChangeSkinPixel()
    {
        CircleSkin.sprite = GlobalData.Instance.StageInfo.CircleSkins[8];

        BallSkin.sprite = GlobalData.Instance.StageInfo.BallSkins[11];

        SetObstaclesSkin(8, Color.black);
    }


    protected override void Update()
    {
        base.Update();
    }
}