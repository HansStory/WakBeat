using UnityEngine.UI;

public class FourthAStage2 : Stage
{
    protected override void Init()
    {
        base.Init();        

        SetBallSkin(GlobalData.Instance.StageInfo.BallSkins[6]);
        SetCircleSprite(GlobalData.Instance.StageInfo.CircleSkins[2]);
    }

    protected override void Update()
    {
        base.Update();
    }
}