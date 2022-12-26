using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class SecondAStage1 : Stage
{
    [SerializeField] private RectTransform spotLightTransform;
    [SerializeField] private Image _backGround;
    [SerializeField] private Image _maskingImage;

    private bool isLight = true;

    protected override void Init()
    {
        base.Init();

        SetBallSkin(GlobalData.Instance.StageInfo.BallSkins[4]);
        SetCircleSprite(GlobalData.Instance.StageInfo.CircleSkins[1]);
        SetBackGroundSprite(GlobalData.Instance.StageInfo.BackGroundSkins[1]);
    }

    private float ballHeightHalf = 15.5f;
    private float obstacleHeightHalf = 56.5f;
    protected override void CreateObstacles()
    {
        float _inRadius = inRadius - obstacleHeightHalf + ballHeightHalf;
        float _outRadius = outRadius + obstacleHeightHalf - ballHeightHalf;
        
        CreateInObstacle(0, inRadius);
        CreateOutObstacle(0, outRadius);

        CreateInObstacle(1, _inRadius);
        CreateOutObstacle(1, _outRadius);
    }

    protected override void ShowChartingItems()
    {
        if (_isAutoMode || state.ShowDodge)
        {
            ShowDodgePoint();
        }

        if (_currentLine <= 5)
        {
            ShowInObstacles();
            ShowOutObstacles();
        }
        else
        {
            ShowInTreeObstacles();
            ShowOutTreeObstacles();
        }

        ShowSavePoint();
    }


    private void ShowOutTreeObstacles()
    {
        var beatItem = bmwReader.ChartingItem[_currentLine];

        if (beatItem != null)
        {

            // Show Objects
            if (beatItem.OutObstacleElements[0].Index > -1)
            {
                foreach (var outObstacle in beatItem.OutObstacleElements)
                {
                    OutObstacleLists[outObstacle.Index].gameObject.SetActive(true);
                }
            }

            // Show Dummy
            if (beatItem.DummyOutObstacleElements[0].Index > -1)
            {
                foreach (var dummy in beatItem.DummyOutObstacleElements)
                {
                    OutObstacleLists[dummy.Index].gameObject.SetActive(true);
                }
            }
        }
    }

    private void ShowInTreeObstacles()
    {
        if (_currentLine >= bmwReader.ChartingItem.Count) return;

        var beatItem = bmwReader.ChartingItem[_currentLine];

        if (beatItem != null)
        {
            // Show Objects
            if (beatItem.InObstacleElements[0].Index > -1)
            {
                foreach (var inObstacle in beatItem.InObstacleElements)
                {
                    InObstacleLists[inObstacle.Index].gameObject.SetActive(true);
                }
            }

            // Show Dummy
            if (beatItem.DummyInObstacleElements[0].Index > -1)
            {
                foreach (var dummy in beatItem.DummyInObstacleElements)
                {
                    InObstacleLists[dummy.Index].gameObject.SetActive(true);
                }
            }
        }
    }

    protected override void PlayProcess()
    {
        base.PlayProcess();

        switch (_currentLine)
        {
            case 5:
                DestroySpotLight();
                Invoke(nameof(HideObstacles), 3 * _tick);
                break;
        }
    }

    void HideObstacles()
    {
        for (int i = 0; i < 72; i++)
        {
            InObstacleLists[i].SetActive(false);
            OutObstacleLists[i].SetActive(false);
        }
    }

    void DestroySpotLight()
    {
        var tween = spotLightTransform.DOScale(Vector3.one * 200f, _tick).SetAutoKill();
        tween.SetDelay(3 * _tick).SetEase(Ease.OutQuart).OnComplete(() => { OnCompleteTween(); }) ;
    }

    private void OnCompleteTween()
    {
        isLight = false;
        _backGround.gameObject.SetActive(isLight);
    }

    protected override void ResetSavePointState()
    {
        base.ResetSavePointState();

        if (state.SavePointLine <= 0)
        {
            spotLightTransform.localScale = Vector3.one * 78f;

            isLight = true;
            _backGround.gameObject.SetActive(isLight);
        }
    }

    protected override void Update()
    {
        base.Update();

        if (isLight && spotLightTransform)
        {
            spotLightTransform.localPosition = Center.transform.localPosition + Center.transform.up * inRadius;
        }
    }

}
