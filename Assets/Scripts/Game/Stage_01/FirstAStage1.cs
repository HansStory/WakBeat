using UnityEngine;
using UnityEngine.UI;

public class FirstAStage1 : Stage
{
    public ParticleSystem Clock3Effect;
    public ParticleSystem Clock12Effect;

    private bool isClockEffect = false;

    protected override void Init()
    {
        base.Init();

        SetBallSkin(GlobalData.Instance.StageInfo.BallSkins[5]);
        SetCircleSprite(GlobalData.Instance.StageInfo.CircleSkins[0]);
    }

    protected override void PlayProcess()
    {
        base.PlayProcess();

        switch (_currentLine)
        {
            case 79:
                isClockEffect = true;
                break;
            case 80:
                SetObstaclesSkin(7);
                SetBallSkin(GlobalData.Instance.StageInfo.BallSkins[10]);
                ChangeCircle();
                break;
        }
    }

    void ChangeCircle()
    {
        CircleSkin.sprite = GlobalData.Instance.StageInfo.CircleSkins[7];
        CircleSkin.SetNativeSize();
        CircleSkin.rectTransform.localScale = Vector2.one * 0.92f;
    }

    protected override void EnterSavePointEffect()
    {
        if (!isClockEffect)
        {
            base.EnterSavePointEffect();
        }
        else
        {
            if (_currentLine < 95)
            {
                Clock3Effect.gameObject.SetActive(true);
                Clock3Effect.Play();

                Invoke(nameof(HideClock3Effect), 3f);
            }
            else
            {
                Clock12Effect.gameObject.SetActive(true);
                Clock12Effect.Play();

                Invoke(nameof(HideClock12Effect), 3f);
            }

        }
    }

    void HideClock3Effect()
    {
        Clock3Effect.gameObject.SetActive(false);
    }

    void HideClock12Effect()
    {
        Clock3Effect.gameObject.SetActive(false);
    }

    protected override void Update()
    {
        base.Update();
    }
}
