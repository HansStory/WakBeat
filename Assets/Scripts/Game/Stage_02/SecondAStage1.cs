using TMPro;
using UnityEditor;
using UnityEngine;

public class SecondAStage1 : Stage
{
    [Header("[ Animation Asset ]")]
    public TextMeshProUGUI _tmpText;

    private float startToBeforeTempup = 4.62f;
    
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
                Invoke(nameof(HideObstacles), 2.11f);
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

}
