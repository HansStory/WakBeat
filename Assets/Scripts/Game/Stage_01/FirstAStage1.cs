
public class FirstAStage1 : Stage
{

    protected override void Init()
    {
        base.Init();

        SetCircleSprite(GlobalData.Instance.StageInfo.CircleSkins[0]);
        SetBackGroundSprite(GlobalData.Instance.StageInfo.BackGroundSkins[0]);
    }
}
