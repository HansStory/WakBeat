using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FifthAStage1 : Stage
{
    public ParticleSystem CircleParticle;
    private float frame;

    protected override void Init()
    {
        base.Init();

        SetBallSkin(GlobalData.Instance.StageInfo.BallSkins[12]);
        BallSkin.color = Color.black;

        SetCircleSprite(GlobalData.Instance.StageInfo.CircleSkins[9]);
        CircleSkin.color = Color.black;

        SetObstaclesSkin(9, Color.red);
        frame = 1f / 60f;
        Debug.Log(frame);
    }

    protected override void PlayProcess()
    {
        base.PlayProcess();

        switch (_currentLine)
        {
            case 0:
                break;
            case 12:
                Invoke(nameof(ChangeWhite), _tick * 2f);
                break;
            case 18:
                ChangeBlack();
                break;
            case 50:
                Invoke(nameof(ChangeWhite), 0.266563f);
                Invoke(nameof(ChangeBlack), 0.433229f);
                Invoke(nameof(ChangeWhite), 0.533229f);
                Invoke(nameof(ChangeBlack), 0.633229f);
                Invoke(nameof(ChangeWhite), 0.766563f);
                Debug.Log(_playTime);
                break;
            case 51:
                //Invoke(nameof(ChangeWhite), 0.266563f); 
                //Invoke(nameof(ChangeBlack), 0.433229f);
                //Invoke(nameof(ChangeWhite), 0.533229f);
                //Invoke(nameof(ChangeBlack), 0.633229f);
                //Invoke(nameof(ChangeWhite), 0.766563f);
                //Invoke(nameof(ChangeBlack), 0.433229f);
                //Invoke(nameof(ChangeWhite), 0.533229f);
                break;
            case 52:
                Debug.Log(_playTime);
                break;
        }
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