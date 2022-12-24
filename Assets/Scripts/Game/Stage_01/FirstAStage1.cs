
public class FirstAStage1 : Stage
{

    protected override void Init()
    {
        base.Init();

        SetBallSkin(GlobalData.Instance.StageInfo.BallSkins[5]);
        SetCircleSprite(GlobalData.Instance.StageInfo.CircleSkins[0]);
        //SetBackGroundSprite(GlobalData.Instance.StageInfo.BackGroundSkins[0]);
    }

    protected override void Update()
    {
        base.Update();
    }
}
