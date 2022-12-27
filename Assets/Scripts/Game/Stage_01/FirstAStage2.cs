
using System.Collections;
using UnityEngine;

public class FirstAStage2 : Stage
{

    protected override void Init()
    {
        base.Init();

        SetBallSkin(GlobalData.Instance.StageInfo.BallSkins[7]);
        SetCircleSprite(GlobalData.Instance.StageInfo.CircleSkins[3]);
        SetBackGroundSprite(GlobalData.Instance.StageInfo.BackGroundSkins[1]);
    }

    protected override void PlayProcess()
    {
        base.PlayProcess();

        switch (_currentLine)
        {
            case 40:
                var speed = _ballSpeed;
                StartCoroutine(RollBackSpeed(speed, _tick));
                break;
        }
    }

    IEnumerator RollBackSpeed(float speed, float delay)
    {
        _ballSpeed = 0;

        yield return new WaitForSeconds(delay);
        Center.transform.localEulerAngles = new Vector3(0f, 0f, -135f);
        _ballSpeed = speed;
    }


    protected override void Update()
    {
        base.Update();
    }
}