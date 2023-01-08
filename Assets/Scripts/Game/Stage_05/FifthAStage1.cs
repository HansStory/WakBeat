using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FifthAStage1 : Stage
{
    public ParticleSystem CircleParticle;

    protected override void Init()
    {
        base.Init();

        SetBallSkin(GlobalData.Instance.StageInfo.BallSkins[12]);
        BallSkin.color = Color.black;

        SetCircleSprite(GlobalData.Instance.StageInfo.CircleSkins[9]);
        CircleSkin.color = Color.black;

        SetObstaclesSkin(9, Color.red);
    }

    protected override void PlayProcess()
    {
        base.PlayProcess();
    }

    void ChangeBlack()
    {
        ChangeBallBlack();
        ChangeCircleBlack();
    }

    void ChangeWhite()
    {
        ChangeBallWhite();
        ChangeCircleWhite();
    }

    void ChangeBallBlack()
    {
        BallSkin.color = Color.black;
    }

    void ChangeBallWhite()
    {
        BallSkin.color = Color.white;
    }
    void ChangeCircleBlack()
    {
        CircleParticle.startColor = Color.black;
    }

    void ChangeCircleWhite()
    {
        CircleParticle.startColor = Color.white;
    }

    public override void OnClickPause()
    {
        if (_isPlay)
        {
            _isPause = !_isPause;

            if (_isPause)
            {
                Time.timeScale = 0;
                audioSource.Pause();
                CircleParticle.playbackSpeed = 0f;

                if (videoPlayer) videoPlayer.Pause();
            }
            else
            {
                Time.timeScale = 1;
                audioSource.Play();
                CircleParticle.playbackSpeed = 1f;

                if (videoPlayer) videoPlayer.Play();
            }
        }
    }

    protected override void Update()
    {
        base.Update();
    }
}