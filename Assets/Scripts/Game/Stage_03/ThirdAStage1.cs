using UnityEngine;
using UnityEngine.UI;

public class ThirdAStage1 : Stage
{
    public Animation anima;

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
            case 0:
                ChangeSkinAlpha();
                break;
            case 1:
                Invoke(nameof(ChangeSkinWhite), _tick);
                break;
            case 9:
                Invoke(nameof(ChangeSkinAlpha), _tick);
                break;
            case 11:
                ChangeSkinWhite();
                break;
            case 13:
                Invoke(nameof(ChangeSkinAlpha), _tick);
                break;
            case 14:
                Invoke(nameof(ChangeSkinWhite), _tick);
                break;
        }
    }

    void ChangeSkinAlpha()
    {
        BallSkin.color = new Color(1, 1, 1, 0.5f);
        CircleSkin.color = new Color(1, 1, 1, 0.5f);
    }

    void ChangeSkinWhite()
    {
        BallSkin.color = Color.white;
        CircleSkin.color = Color.white;
    }

    protected override void Update()
    {
        base.Update();
    }
}