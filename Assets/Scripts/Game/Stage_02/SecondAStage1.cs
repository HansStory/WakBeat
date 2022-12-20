using TMPro;
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

    protected override void Update()
    {
        base.Update();
        if (_currentLine >= 5 && _timer > 2.1f)
        {
            BackGroundSkin.enabled = false;
        }
        else if (_currentLine < 5)
        {
            BackGroundSkin.enabled = true;
        }
        // if (Stage._currentLine == 2 && _timer > 1.5f)
        // {
        //     if (_timer <= 2.13)
        //     {
        //         _tmpText.text = "12:23";
        //     }
        //     else
        //     {
        //         _tmpText.text = "12:24";
        //     }
        // }
        // else if (Stage._currentLine == 3)
        // {
        //     _tmpText.text = "12:25";
        // }
        // else if (_currentLine < 2)
        // {
        //     _tmpText.alpha = 255f;
        //     _tmpText.text = "12:22";
        // }
        //
        // if (Stage._currentLine >= 5)
        // {
        //     float _inRadius = inRadius - obstacleHeightHalf + ballHeightHalf;
        //     float _outRadius = outRadius + obstacleHeightHalf - ballHeightHalf;
        //
        //     CreateInObstacle(1, _inRadius);
        //     CreateOutObstacle(1, _outRadius);
        // }
        
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
    

    protected override void ShowOutObstacles()
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

    protected override void ShowInObstacles()
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

    protected override void ResetSavePointState()
    {
        base.ResetSavePointState();
        if (_currentLine >= bmwReader.ChartingItem.Count) return;

        var beatItem = bmwReader.ChartingItem[_currentLine];

        if (beatItem != null)
        {
            // Hide
            foreach (var dodgeList in DodgePointLists)
            {
                dodgeList.SetActive(false);
            }
            
            // Hide
            foreach (var inList in InObstacleLists)
            {
                inList.SetActive(false);
            }
            
            // Hide
            foreach (var inList in OutObstacleLists)
            {
                inList.SetActive(false);
            }
        }
    }

}
