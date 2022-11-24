using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StageCharting : Stage
{
    [Space(10)]
    [Header ("----- [Charting Componenet] -----")]

    public TMP_Dropdown DropDownAlbum;
    public TMP_Dropdown DropDownStage;
    public TMP_Text TextGameMode;

    public TMP_Text TextPause;

    private bool isplay = false;
    private bool _isGameMode = false;

    protected override void Init()
    {
        base.Init();

        _isGameMode = Config.Instance.GameMode;
        TextGameMode.text = $"Game Mode \n {_isGameMode}";
    }

    protected override void Start()
    {
        
    }

    protected override void Update()
    {
        base.Update();
    }


    private void ResetStage()
    {
        HideChartingItems();
        InitVariable();

        // Read .bmw file
        bmwReader = new BMWReader();
        bmwReader.ReadFile(Directory + "/" + BMWFile);

        // Get Values
        GetMusicInfo();
        InitBallPosition();
        InitBallSpeed();
        InitCalculateTick();

        // Calculate Beat
        _totalBeatCount = bmwReader.ChartingItem.Count;

        audioSource.Stop();
        CurrentLineText.text = $"Current Line : {_currentLine}";
        TextPause.text = "Pause";
    }


    private void InitVariable()
    {
        _bpm = 0;
        _bar = 0;
        _interval = 0f;
        _tick = 0;
        _beatTime = 0;
        _currentLine = -1;
        _timer = 0f;
        _playTime = 0f;
        savePointNum = 0;
        _totalBeatCount = 0;

        _currentBeat = 0;
        _isPlay = false;
        _isPause = false;
        Time.timeScale = 1;
    }

    private void HideChartingItems()
    {
        foreach (var dodgeList in DodgePointList)
        {
            dodgeList.SetActive(false);
        }

        foreach (var inList in InObstacleList)
        {
            inList.SetActive(false);
        }

        foreach (var outList in OutObstacleList)
        {
            outList.SetActive(false);
        }

        if (GameObject.Find("SavePoint(Clone)"))
        {
            GameObject savePoint = GameObject.Find("SavePoint(Clone)");
            Destroy(savePoint);
        }
    }

    protected override void StartGame()
    {
        ResetStage(); 
        base.StartGame();
    }

    protected override void FinishGame()
    {
        _isPlay = false;
        ResetStage();
    }

    public void DropDownAlbumValueChanged()
    {
        int index = DropDownAlbum.value;

        GlobalState.Instance.AlbumIndex = index;
        Debug.Log($"{GlobalState.Instance.AlbumIndex}");
    }

    public void DropDownStageValueChanged()
    {
        int index = DropDownStage.value;

        GlobalState.Instance.StageIndex = index;
        Debug.Log($"{GlobalState.Instance.StageIndex}");
    }
    
    public void OnClickGameMode()
    {
        _isGameMode = !_isGameMode;
        
        //To do : Game Mode Restart 力绢 贸府 秦具窃
        //if (Ball.GetComponent<Rigidbody2D>() != null)
        //{
        //    Rigidbody2D rigid = Ball.GetComponent<Rigidbody2D>();
        //    rigid.simulated = _isGameMode;
        //}

        TextGameMode.text = $"Game Mode \n {_isGameMode}";
    }

    public void OnClickLoad()
    {
        ResetStage();
    }

    public void OnClickStart()
    {
        StartGame();
    }

    private bool _isPause = false;
    public void OnClickPause()
    {
        if (_isPlay)
        {
            _isPause = !_isPause;

            if (_isPause)
            {
                TextPause.text = "Play";

                Time.timeScale = 0;
                audioSource.Pause();
            }
            else
            {
                TextPause.text = "Pause";

                Time.timeScale = 1;
                audioSource.Play();
            }
        }
    }


    public void OnClickStop()
    {
        ResetStage();
    }

}
