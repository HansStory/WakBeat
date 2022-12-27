
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
            case 39:
                Invoke(nameof(WinterSpringGiMic), _beatTime - _timing);
                break;
        }
    }

    float _timing = 0.033333f;
    void WinterSpringGiMic()
    {
        var speed = _ballSpeed;
        StartCoroutine(RollBackSpeed(speed, _tick + _timing));
    }

    IEnumerator RollBackSpeed(float speed, float delay)
    {
        _ballSpeed = 0;
        Debug.Log("Stop Ball Time : " + StageAnim[_animationName].time);

        yield return new WaitForSeconds(delay);
        Center.transform.localEulerAngles = new Vector3(0f, 0f, -135f);
        _ballSpeed = speed;

        Debug.Log("Stop Ball Time : " + StageAnim[_animationName].time);
    }


    protected override void Update()
    {
        base.Update();
    }
}