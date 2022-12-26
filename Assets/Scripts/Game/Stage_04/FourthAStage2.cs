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

    protected override void PlayProcess()
    {
        base.PlayProcess();

        switch (_currentLine)
        {
            case 16:
                Invoke(nameof(HidePlayGround), _tick);
                break;
            case 17:
                Invoke(nameof(ShowPlayGround), _tick);
                break;
            case 33:
                ChangeBackGroundBlack();
                break;
            case 41:
                ChangeBackGroundOrange();
                break;
            case 47:
                ChangeBackGroundBlack();
                break;
            case 49:
                ChangeBackGroundOrange();
                break;
        }
    }

    private void HidePlayGround()
    {
        PlayGround.gameObject.SetActive(false);
    }

    private void ShowPlayGround()
    {
        PlayGround.gameObject.SetActive(true);
    }

    private void ChangeBackGroundBlack()
    {
        SetBallSkin(GlobalData.Instance.StageInfo.BallSkins[10]);
        SetCircleSprite(GlobalData.Instance.StageInfo.CircleSkins[6]);
        SetObstaclesSkin(7);
    }

    private void ChangeBackGroundOrange()
    {
        SetBallSkin(GlobalData.Instance.StageInfo.BallSkins[6]);
        SetCircleSprite(GlobalData.Instance.StageInfo.CircleSkins[2]);
        SetObstaclesSkin(3);
    }
}