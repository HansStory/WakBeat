
public class FourthAStage1 : Stage
{

    protected override void Init()
    {
        base.Init();

        SetBallSkin(GlobalData.Instance.StageInfo.BallSkins[8]);
        SetCircleSprite(GlobalData.Instance.StageInfo.CircleSkins[4]);
        SetBackGroundSprite(GlobalData.Instance.StageInfo.BackGroundSkins[2]);
    }

    protected override void Update()
    {
        base.Update();
    }
}