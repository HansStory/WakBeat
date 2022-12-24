using UnityEngine.UI;

public class FourthAStage2 : Stage
{
    private GlobalData globalData;

    protected override void Init()
    {
        base.Init();
        globalData = GlobalData.Instance;

        SetBallSkin(globalData.StageInfo.BallSkins[6]);
        SetCircleSprite(globalData.StageInfo.CircleSkins[2]);
        SetObstacleSkin();
    }

    void SetObstacleSkin()
    {
        foreach (var inObjstacle in InObstacleLists)
        {
            var skin = inObjstacle.GetComponent<Image>();
            if (skin = null) return;

            skin.sprite = globalData.StageInfo.ObstacleSkins[3];
        }

        foreach (var outObjstacle in OutObstacleLists)
        {
            var skin = outObjstacle.GetComponent<Image>();
            if (skin = null) return;

            skin.sprite = globalData.StageInfo.ObstacleSkins[3];
        }
    }

    protected override void Update()
    {
        base.Update();
    }
}