using UnityEngine;
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
            case 0:
                ChangeBackGroundBlack();
                break;
            case 2:
                ChangeBackGroundOrange();
                break;
            case 17:
                HidePlayGround();
                break;
            case 18:
                ShowPlayGround();
                break;
            case 33:
                Invoke(nameof(ChangeBackGroundBlack),_tick);
                Debug.Log(StageAnim[_animationName].time);
                break;
            case 41:
                Invoke(nameof(ChangeBackGroundOrange), _tick + (_tick/2f));
                Debug.Log(StageAnim[_animationName].time); 
                break;
            case 47:
                Invoke(nameof(ChangeBackGroundBlack), _tick);
                break;
            case 49:
                Invoke(nameof(ChangeBackGroundOrange), _tick + (_tick / 2f));
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
        SetObstaclesSkin(7, Color.white);
    }

    private void ChangeBackGroundOrange()
    {
        SetBallSkin(GlobalData.Instance.StageInfo.BallSkins[6]);
        SetCircleSprite(GlobalData.Instance.StageInfo.CircleSkins[2]);
        SetObstaclesSkin(3, Color.white);
    }
}