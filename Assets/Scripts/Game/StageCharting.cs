using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class StageCharting : Stage
{
    [Space(10)]
    [Header ("----- [Charting Componenet] -----")]

    public TMP_Dropdown DropDownAlbum;
    public TMP_Dropdown DropDownStage;
    public TMP_Text TextGameMode;
    public TMP_Text TextAutoMode;

    public TMP_Text TextPause;

    public GameObject DebugTab;
    public TMP_Text TextDebug;

    public GameObject SpawnPoint;
    public Transform SpawnPointBase;
    public TMP_Text TextSpawnPoint;

    public Image InCircleFade;
    public Image OutCircleFade;

    private CircleCollider2D playerCollider;

    private bool isplay = false;
    private bool _isGameMode = false;
    private bool _isShowDebugtab = true;

    //------------ For Debuging -----------
    public TMP_Text[] DebugElements;

    private const int Title = 0;
    private const int Artist = 1;
    private const int BPM = 2;
    private const int Bar = 3;
    private const int TickTime = 4;
    private const int LineTime = 5;
    private const int BallSpeed = 6;
    private const int BallAngle = 7;
    private const int SongTotalTime = 8;
    private const int CurrentSongTime = 9;
    private const int CurrentLine = 10;
    private const int TotalLine = 11;
 

    protected override void Init()
    {
        base.Init();

        CreateSpawnPoint();

        _isGameMode = Config.Instance.GameMode;
        _isAutoMode = Config.Instance.AutoMode;

        if (GetComponent<Rigidbody2D>() != null)
        {
            Rigidbody2D rigid = GetComponent<Rigidbody2D>();
            rigid.simulated = _isGameMode;
        }

        if (this.GetComponent<CircleCollider2D>() != null)
        {
            playerCollider = this.GetComponent<CircleCollider2D>();
            playerCollider.offset = new Vector2(Ball.transform.localPosition.x, Ball.transform.localPosition.y);
        }

        TextGameMode.text = $"Game Mode \n {_isGameMode}";
        TextAutoMode.text = $"Auto Mode \n {_isAutoMode}";
    }

    private void CreateSpawnPoint()
    {
        Center.transform.localEulerAngles = Vector3.zero;

        for (int i = 0; i < _spawnCount; i++)
        {
            GameObject point = GameObject.Instantiate(SpawnPoint, SpawnPointBase);
            var pointInfo = point.GetComponent<SpawnPoint>();

            if (pointInfo)
            {
                point.transform.localPosition = Center.transform.localPosition + Center.transform.up * dodgeRadius;
                point.transform.localEulerAngles = Center.transform.localEulerAngles;
                pointInfo.Index = i.ToString();

                Center.transform.Rotate(0f, 0f, _spawnAngle);
            }
        }

        SpawnPointBase.gameObject.SetActive(_isShowSpawnPoint);
    }

    protected override void Start()
    {
        
    }

    protected override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            AppManager.Instance.Quit();
        }
    }

    protected override void PlayGame()
    {
        base.PlayGame();

        OperateBallMovement();
        playerCollider.offset = new Vector2((Ball.transform.localPosition.x * PlayGround.localScale.x) + PlayGround.localPosition.x, (Ball.transform.localPosition.y *  PlayGround.localScale.y) + PlayGround.localPosition.y);
        playerCollider.radius = 15f * Mathf.Abs(PlayGround.localScale.x);

        DebugElements[BallAngle].text = $"Ball Angle : {Mathf.Abs(Center.transform.localEulerAngles.z - 360f).ToString("F2")}";
        DebugElements[SongTotalTime].text = $"현재 곡의 진행 시간 : {audioSource.time.ToString("F2")}";
    }

    protected override void PlayProcess()
    {
        base.PlayProcess();

        DebugElements[CurrentLine].text = $"Current Line : {_currentLine}";
    }

    protected override void InputChangeDirection()
    {
        if (!_isAutoMode)
        {
            base.InputChangeDirection();
        }
    }

    protected override void IntegrationChangeDirection()
    {
        base.IntegrationChangeDirection();

        if (_isInState)
        {
            FadeInOutCircle(InCircleFade, 0.2f);
        }
        else
        {
            FadeInOutCircle(OutCircleFade, 0.2f);
        }
    }

    Tween FadeTween;
    private void FadeInOutCircle(Image circle, float duration)
    {
        circle.color = Color.black;
        FadeTween = circle.DOColor(Color.clear, duration);
    }

    protected override void ChangeBar()
    {
        var beatItem = bmwReader.ChartingItem[_currentLine];

        if (beatItem.Bar >= 0)
        {
            _bar = beatItem.Bar;

            CalculateTick();

            DebugElements[Bar].text = $"Bar : {beatItem.Bar}";
            DebugElements[TickTime].text = $"1Bar에 걸리는 시간 : {_tick.ToString("F3")}초";
            DebugElements[LineTime].text = $"1줄에 걸리는 시간 : {_beatTime.ToString("F3")}초";
        }
    }


    protected override void ChangeBallSpeed()
    {
        base.ChangeBallSpeed();

        var beatItem = bmwReader.ChartingItem[_currentLine];

        if (beatItem.Speed >= 0)
        {
            DebugElements[BallSpeed].text = $"Ball Speed : {beatItem.Speed}";
        }
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

        ResetAudio();

        ResetDebugValue();

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

    private void ResetAudio()
    {
        audioSource.Stop();
        SoundManager.Instance.SetStageMusic();
    }

    private void ResetDebugValue()
    {
        var musicInfo = bmwReader.MusicInfoItem;

        CurrentLineText.text = $"Current Line : {_currentLine}";

        DebugElements[Title].text = $"곡명 : {musicInfo.Title}";
        DebugElements[Artist].text = $"작곡가 : {musicInfo.Artist}";
        DebugElements[BPM].text = $"BPM : {musicInfo.BPM}";
        DebugElements[Bar].text = $"Bar : {musicInfo.Bar}";
        DebugElements[TickTime].text = $"1Bar에 걸리는 시간 : {_tick.ToString("F3")}초";
        DebugElements[LineTime].text = $"1줄에 걸리는 시간 : {_beatTime.ToString("F3")}초";
        DebugElements[BallSpeed].text = $"Ball Speed : {(int)speed}";
        DebugElements[BallAngle].text = $"Ball Angle : {Center.transform.localEulerAngles.z}";
        DebugElements[SongTotalTime].text = $"현재 곡의 진행 시간 : {audioSource.time.ToString("F2")}";
        DebugElements[CurrentSongTime].text = $"현재 곡의 총 시간 : {audioSource.clip.length}";
        DebugElements[CurrentLine].text = $"Current Line : {_currentLine}";
        DebugElements[TotalLine].text = $"Total Line : {bmwReader.ChartingItem.Count - 1}";
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

        //To do : Game Mode Restart 제어 처리 해야함
        if (GetComponent<Rigidbody2D>() != null)
        {
            Rigidbody2D rigid = GetComponent<Rigidbody2D>();
            rigid.simulated = _isGameMode;
        }

        TextGameMode.text = $"Game Mode \n {_isGameMode}";
    }

    private bool _isAutoMode = false;
    public void OnClickAutoMode()
    {
        _isAutoMode = !_isAutoMode;

        foreach (var dodge in DodgePointList)
        {
            if (dodge.GetComponent<BoxCollider2D>() != null)
            {
                dodge.GetComponent<BoxCollider2D>().enabled = _isAutoMode;
            }
        }

        TextAutoMode.text = $"Auto Mode \n {_isAutoMode}";
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
    
    public void OnClickDebugTab()
    {
        _isShowDebugtab = !_isShowDebugtab;

        DebugTab.SetActive(_isShowDebugtab);

        if (_isShowDebugtab)
        {
            TextDebug.text = "Show\nDebug Tab";
        }
        else
        {
            TextDebug.text = "Hide\nDebug Tab";
        }
    }


    private bool _isShowSpawnPoint = false;
    public void OnClickSpawnPointTab()
    {
        _isShowSpawnPoint = !_isShowSpawnPoint;

        SpawnPointBase.gameObject.SetActive(_isShowSpawnPoint);

        if (_isShowSpawnPoint)
        {
            TextSpawnPoint.text = "Show\nSpawn Points";
        }
        else
        {
            TextSpawnPoint.text = "Hide\nSpawn Points";
        }
    }

    LineRenderer LineRenderer = new LineRenderer();
    public void AutoMode()
    {
        //Ball.transform.
    }


    public void SavePointEnter()
    {
        GlobalState.Instance.SavePoint = _currentLine;
        GlobalState.Instance.SaveMusicPlayingTime = SoundManager.Instance.MusicAudio.time;

    }

    public void PlayerDieAndSavePointPlay()
    {
        Debug.Log("Player Die!!");
        GlobalState.Instance.IsPlayerDied = true;
        SoundManager.Instance.MusicAudio.Pause();
        SoundManager.Instance.MusicAudio.time = GlobalState.Instance.SaveMusicPlayingTime;
        if (GlobalState.Instance.SavePoint != 0)
        {
            _currentLine = GlobalState.Instance.SavePoint - 1;
        }
        else
        {
            _currentLine = GlobalState.Instance.SavePoint;
        }
    }

    void PlayerDie()
    {
        PlayerDieAndSavePointPlay();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("SavePoint"))
        {
            Debug.Log("부모객체 : 세이브 포인트!!");
            Destroy(other.gameObject);
            //SavePointEnter();
        }

        if (other.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("부모객체 : Heat Obstacle");
            //PlayerDie();
        }

        if (other.gameObject.CompareTag("DodgePoint"))
        {
            Debug.Log("부모객체 : Heat Dodge Point");
            IntegrationChangeDirection();
        }
    }
}
