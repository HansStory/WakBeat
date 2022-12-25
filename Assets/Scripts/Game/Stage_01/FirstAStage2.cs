
public class FirstAStage2 : Stage
{

    protected override void Init()
    {
        base.Init();

        SetBallSkin(GlobalData.Instance.StageInfo.BallSkins[7]);
        SetCircleSprite(GlobalData.Instance.StageInfo.CircleSkins[3]);
        SetBackGroundSprite(GlobalData.Instance.StageInfo.BackGroundSkins[1]);
    }

    protected override void Update()
    {
        base.Update();
    }
}