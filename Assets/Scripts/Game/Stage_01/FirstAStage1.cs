
using UnityEngine;
using UnityEngine.UI;

public class FirstAStage1 : Stage
{
    public ParticleSystem ClockEffect;

    private bool isClockEffect = false;

    protected override void Init()
    {
        base.Init();

        SetBallSkin(GlobalData.Instance.StageInfo.BallSkins[5]);
        SetCircleSprite(GlobalData.Instance.StageInfo.CircleSkins[0]);
        //SetBackGroundSprite(GlobalData.Instance.StageInfo.BackGroundSkins[0]);

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
                ChangeObstaclesSkin();
                ChangeBallSkin();
                ChangeCircle();
                break;
        }
    }

    void ChangeObstaclesSkin()
    {
        var stageInfo = GlobalData.Instance.StageInfo;

        foreach (var obstacle in InObstacleLists)
        {
            var skin = obstacle.GetComponent<Image>();
            if (skin)
            {
                skin.sprite = stageInfo.ObstacleSkins[7];
            }
        }

        foreach (var obstacle in OutObstacleLists)
        {
            var skin = obstacle.GetComponent<Image>();
            if (skin)
            {
                skin.sprite = stageInfo.ObstacleSkins[7];
            }
        }
    }

    void ChangeBallSkin()
    {
        BallSkin.sprite = GlobalData.Instance.StageInfo.BallSkins[10];
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
            ClockEffect.gameObject.SetActive(true);
            ClockEffect.Play();

            Invoke(nameof(HideClockEffect), 3f);
        }
    }

    void HideClockEffect()
    {
        ClockEffect.gameObject.SetActive(false);
    }

    protected override void Update()
    {
        base.Update();
    }
}
