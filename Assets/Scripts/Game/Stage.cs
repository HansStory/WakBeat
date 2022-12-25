using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using static UnityEngine.Rendering.DebugUI;
using UnityEngine.Video;

public abstract class Stage : MonoBehaviourSingleton<Stage>
{
    [Header("Stage Base")]
    public Image BallSkin;
    public Image CircleSkin;
    public Image BackGroundSkin;

    public RectTransform PlayGround;
    public GameObject Center;
    public GameObject Player;

    [Header("[ Animation ]")]
    public Animation StageAnim;

    [Header("[ Tween Elements ]")]
    public Image InCircleFade;
    public Image OutCircleFade;
    public Text ClearRate;

    [Header("[ Instance Object Container ]")]
    public Transform Container;
    public GameObject CenterPivot;      //게임 로직과 관계없이 중심 Pivot

    [Header("[ Dodge Point ]")]
    public GameObject DodgePoint;
    public Transform DodgePointBase;
    public List<GameObject> DodgePointLists = new List<GameObject>();

    [Header("[ Obstacles ]")]
    public GameObject[] Obstacles;
    public Transform ObstacleInBase;
    public List<GameObject> InObstacleLists = new List<GameObject>();

    public Transform ObstacleOutBase;
    public List<GameObject> OutObstacleLists = new List<GameObject>();

    [Header("[ Save Point ]")]
    public GameObject[] SavePoint;

    [Header("[ Default Save Point Effect ]")]
    public Image[] SavePointCircles;            // TO DO : Effect 분리작업 필요

    public Text TextCurrentLine = null;        // 테스트 끝난후 배포시에 제거하겠습니다.

    //---------------------------------------------------
    protected float dodgeRadius = 334f;
    protected float outRadius = 355f;
    protected float inRadius = 312f;
    protected float ballRadius = 0;
    protected float variableRadius;

    protected BMWReader bmwReader = null;        // 채보 스크립트
    protected VideoPlayer videoPlayer = null;
    protected AudioSource audioSource = null;
    protected GlobalState state;
    protected UserData userData;

    protected string _title = "";                // 곡 제목
    protected string _artist = "";               // 작곡가

    protected float _bpm = 0;                    // Bar Per Minute
    protected float _bar = 0;                    // Bar
    protected float _interval = 0f;              // Interval

    protected float _tick = 0;                   // 1 Bar(칸)에 소요되는 시간
    protected float _beatTime = 0;               // 1 Line에 소요되는 시간
    protected float _ballSpeed = 360;            // 1 Line동안 움직이는 Ball의 스피드

    public static int _currentLine = -1;         // 현재 진행중인 Line의 아이템들
    protected int _totalBeatCount = 0;           // 총 Beat 수

    protected float _timer = 0f;                 // 음악 로직을 처리하기 위한 타이머
    protected float _playTime = 0f;              // Stage 시작후 총 흐른 시간
    protected float _musicCurrentTime = 0f;      // 곡의 현재 진행중인 시간
    protected float _musicTotalTime = 0f;        // 곡의 총 시간

    protected static int _spawnCount = 72;       // TO DO : Object Pooling
    protected static float _spawnAngle = -5f;

    protected bool _isPlay = false;              // 게임 진행중 체크
    protected bool _isPause = false;             // 일시정지 체크
    protected bool _isGameMode = false;          // 게임 모드 체크
    protected bool _isAutoMode = false;          // 오토 모드 체크

    // Key 입력 부
    private string _keyDivision = string.Empty;

    // Player HP
    private int _currentHP = 1;
      

public virtual string Directory
    {
        get
        {
            string dir = GlobalState.Instance.BMWFolderPath;

            return dir;
        }
    }

    public virtual string BMWFile
    {
        get
        {
            string file = GlobalState.Instance.BMWFile;

            return file;
        }
    }

    private void Awake()
    {
        Init();
    }

    protected virtual void Init()
    {
        // Read .bmw file
        bmwReader = new BMWReader();
        bmwReader.ReadFile(Directory + "/" + BMWFile);

        //InitConfigSettings();
        InitGlobalSettings();

        //Get User Data (.json file)
        GetBallSkin();

        // Create Stage Objects
        CreateDodgePoint();
        CreateObstacles();

        // Get Values
        GetMusicInfo();

        // Set Values
        SetMusicInfo();

        InitHP();
        Debug.Log(_currentHP);

        InitBallPosition();
        InitBallSpeed();
        InitCalculateTick();

        // Calculate Beat
        _totalBeatCount = bmwReader.ChartingItem.Count;

        // Key 입력 부
        _keyDivision = null == DataManager.dataKeyDivision ? "Integration" : DataManager.dataKeyDivision;

        ClearRate.text = $"Clear Rate\n0%";

        ShowCurrentLine();

        // 음악 정보 팝업 호출
        if (UIManager.Instance.UIElementPopUp)
        {
            UIManager.Instance.UIElementPopUp.SetPopUpMusicInfo();
        }
    }

    void ShowCurrentLine()
    {
        if (TextCurrentLine != null)
        {
            TextCurrentLine.text = $"Current Line : {_currentLine}";

            if (!state.DevMode)
            {
                TextCurrentLine.gameObject.SetActive(false);
            }
        }
    }

    protected virtual void InitGlobalSettings()
    {
        state = GlobalState.Instance;
        userData = GlobalState.Instance.UserData.data;

        _isGameMode = state.GameMode;
        _isAutoMode = state.AutoMode;

        var col = Player.GetComponent<Rigidbody2D>();
        if (col)
        {
            col.simulated = _isGameMode;
        }

        ResetGlobalState();
    }

    public virtual void ResetGlobalState()
    {        
        state.SavePointAngle = bmwReader.ChartingItem[0].BallAngle;
        state.IsPlayerDied = false;
        state.PlayerDeadCount = 0;
        state.SaveMusicPlayingTime = 0.0f;
        state.SavePoint = 0;
    }

    protected virtual void Start()
    {
        StartGame();
        //Invoke($"{nameof(StartGame)}", 1f);
    }

    protected virtual void StartGame()
    {
        _currentLine = 0;       
        _isPlay = true;

        SoundManager.Instance.TurnOnStageMusic();
        PlayProcess();
    }

    protected virtual void GetMusicInfo()
    {
        if (bmwReader)
        {
            _title = bmwReader.MusicInfoItem.Title;
            _artist = bmwReader.MusicInfoItem.Artist;
            _bpm = bmwReader.MusicInfoItem.BPM;
            _bar = bmwReader.MusicInfoItem.Bar;
        }

        // Sound Fade In 
        SoundManager.Instance.FadeInMusicVolume(0.5f);
    }

    protected virtual void SetMusicInfo()
    {
        // Sound 제어 부
        audioSource = SoundManager.Instance.MusicAudio;

        SoundManager.Instance.SetStageMusic();
        state.StageMusicLength = (int)audioSource.clip.length;
    }

    protected virtual void InitHP()
    {
        if (state.UseBonusHP) // Shop Skill에서 처리 해줘야 함
        {
            _currentHP = 2;
        }
        else
        {
            _currentHP = 1;
        }
    }

    protected virtual void InitBallPosition()
    {
        variableRadius = outRadius;
        ballRadius = variableRadius;     // Init Ball Position 

        // Init Start Ball Rotation
        float ballAngle = 0;

        if (bmwReader.ChartingItem[0].BallAngle >= 0)
        {
            ballAngle = bmwReader.ChartingItem[0].BallAngle;
        }
        else
        {
            ballAngle = 0;
        }

        Center.transform.localEulerAngles = new Vector3(0f, 0f, -ballAngle);

        Player.transform.localPosition = Center.transform.localPosition + Center.transform.up * ballRadius;
    }

    protected virtual void GetBallSkin()
    {
        for (int i = 0; i < userData.shopData.skinUsingYn.Length; i++)
        {
            if (userData.shopData.skinUsingYn[i].Contains("Y"))
            {
                if (BallSkin)
                {
                    BallSkin.sprite = GlobalData.Instance.StageInfo.BallSkins[i];
                }
            }
        }
    }

    protected virtual void SetBallSkin(Sprite ball)
    {
        if (BallSkin)
        {
            BallSkin.sprite = ball;
        }
        else
        {
            Debug.LogError("Ball Skin Is Null. Check Setup Album Info");
        }
    }

    protected virtual void SetCircleSprite(Sprite circle)
    {
        if (CircleSkin)
        {
            CircleSkin.sprite = circle;
        }
    }

    protected virtual void SetBackGroundSprite(Sprite backGround)
    {
        if (BackGroundSkin)
        {
            BackGroundSkin.sprite = backGround;
        }
    }

    protected virtual void CreateDodgePoint()
    {
        for (int i = 0; i < _spawnCount; i++)
        {
            GameObject dodge = Instantiate(DodgePoint, DodgePointBase);

            if (dodge)
            {
                dodge.transform.localPosition = Center.transform.localPosition + Center.transform.up * dodgeRadius;
                dodge.transform.localEulerAngles = Center.transform.localEulerAngles;

                var col = dodge.GetComponent<BoxCollider2D>();
                if (col)
                {
                    col.enabled = _isAutoMode;
                }

                DodgePointLists.Add(dodge);

                dodge.SetActive(false);

                Center.transform.Rotate(0f, 0f, _spawnAngle);
            }
        }

        if (_isAutoMode)
        {
            DodgePointBase.gameObject.SetActive(_isAutoMode);
        }
        else if(state.ShowDodge)
        {
            DodgePointBase.gameObject.SetActive(state.ShowDodge);
        }
    }

    // TO DO : 각자 Stage에서 구현할것 부모 객체는 추상화만 (각자 스크립트에서 코드2줄만 추가하면됨)
    protected virtual void CreateObstacles()
    {
        CreateInObstacle(0, inRadius);
        CreateOutObstacle(0, outRadius);
    }

    protected virtual void CreateInObstacle(int obstacleType, float radius)
    {
        for (int i = 0; i < _spawnCount; i++)
        {
            GameObject inObstacle = Instantiate(Obstacles[obstacleType], ObstacleInBase);

            if (inObstacle)
            {
                inObstacle.transform.localPosition = Center.transform.localPosition + Center.transform.up * radius;
                inObstacle.transform.localEulerAngles = Center.transform.localEulerAngles + new Vector3(0f, 0f, 180f);
                InObstacleLists.Add(inObstacle);

                inObstacle.SetActive(false);

                Center.transform.Rotate(0f, 0f, _spawnAngle);
            }
        }
    }

    protected virtual void CreateOutObstacle(int obstacleType, float radius)
    {
        Center.transform.localEulerAngles = Vector3.zero;

        for (int i = 0; i < _spawnCount; i++)
        {
            GameObject outObstacle = Instantiate(Obstacles[obstacleType], ObstacleOutBase);

            if (outObstacle)
            {
                outObstacle.transform.localPosition = Center.transform.localPosition + Center.transform.up * radius;
                outObstacle.transform.localEulerAngles = Center.transform.localEulerAngles;
                OutObstacleLists.Add(outObstacle);

                outObstacle.SetActive(false);

                Center.transform.Rotate(0f, 0f, _spawnAngle);
            }
        }
    }

    protected virtual void CreateSavePoint(int savePointType)
    {
        GameObject savePoint = Instantiate(SavePoint[savePointType], Container);

        if (savePoint)
        {
            savePoint.transform.localPosition = CenterPivot.transform.localPosition + CenterPivot.transform.up * dodgeRadius;
            savePoint.transform.localScale = Vector3.zero;

            savePoint.transform.DOScale(Vector3.one, 0.2f).SetAutoKill();
        }
    }

    protected virtual void InitBallSpeed()
    {
        if (bmwReader.ChartingItem[0].Speed == -1)
        {
            _ballSpeed = 360f;
        }
        else
        {
            _ballSpeed = bmwReader.ChartingItem[0].Speed;
        }
    }


    protected virtual void InitCalculateTick()
    {
        _bpm = bmwReader.MusicInfoItem.BPM;
        _bar = bmwReader.MusicInfoItem.Bar;

        CalculateTick();
    }

    protected virtual void CalculateTick()
    {
        float _bps = _bpm / 60;
        _tick = 1 / _bps;

        _beatTime = _tick * _bar;

        Debug.Log($"Tick : {_tick}, Bar : {_bar}, Beat Time : {_beatTime}");
    }

    private void ReadProcess()
    {

    }

    protected virtual void PlayProcess()
    {
        var beatItem = bmwReader.ChartingItem[_currentLine];    //beatItems[_currentBeat];
        if (beatItem == null)
        {
            Debug.LogError("PlayPorocess beat Item is null");
            return;
        }

        PlayAnimation(_animationName);

        ChangeBar();

        AddInterval(beatItem);

        ChangeBallAngle();

        ChangeBallSpeed();

        ShowChartingItems();

        TweenClearRate();

        // 테스트 끝난후 배포시에 삭제 예정
        if (TextCurrentLine != null)
        {
            if (state.DevMode)
            {
                TextCurrentLine.text = $"Current Line : {_currentLine}";
            }
        }
    }

    protected virtual void TweenClearRate()
    {
        if (_currentLine > 0)
        {
            float _rate = (((float)_currentLine + 1) / (float)_totalBeatCount) * 100f;
            ClearRate.DOText($"Clear Rate\n{((int)_rate)}%", _tick);
        }
        else
        {

            ClearRate.DOText($"Clear Rate\n0%", _beatTime);
        }
    }

    private string _animationName = string.Empty;
    protected virtual void PlayAnimation(string animationName)
    {
        var beatItem = bmwReader.ChartingItem[_currentLine];

        //if (StageAnim.clip == null) return;

        if (beatItem.AnimationIndex >= 0 && bmwReader.AnimationItem.Count > beatItem.AnimationIndex)
        {
            var animItem = bmwReader.AnimationItem[beatItem.AnimationIndex];

            _animationName = animItem.AnimationName;

            if (!StageAnim[_animationName]) return;


            StageAnim.Rewind(_animationName);
            StageAnim[_animationName].normalizedTime = 0f;
            StageAnim[_animationName].speed = 1f;
            StageAnim.Play(_animationName);
        }
    }

    protected virtual void ChangeBar()
    {
        var beatItem = bmwReader.ChartingItem[_currentLine];

        if (beatItem.Bar >= 0)
        {
            _bar = beatItem.Bar;

            CalculateTick();
        }
    }

    protected virtual void AddInterval(ChartingItem item)
    {
        item = bmwReader.ChartingItem[_currentLine];

        if (item.Interval.ToUpper() != "NONE")
        {
            float _bmwInterval = float.Parse(item.Interval);
            _timer += _bmwInterval;
        }
    }

    protected virtual void ChangeBallAngle()
    {
        var beatItem = bmwReader.ChartingItem[_currentLine];

        if (beatItem.BallAngle >= 0)
        {
            Vector3 targetRotate = new Vector3(0f, 0f, -beatItem.BallAngle);

            if (beatItem.BallAngleTime < 0)
            {
                beatItem.BallAngleTime = 0;
            }

            Center.transform.localRotation = Center.transform.localRotation;
            Center.transform.DORotate(targetRotate, beatItem.BallAngleTime);
        }
    }

    protected virtual void ChangeBallSpeed()
    {
        var beatItem = bmwReader.ChartingItem[_currentLine];

        if (beatItem.Speed >= 0)
        {
            if (beatItem.SpeedTime < 0)
            {
                beatItem.SpeedTime = 0;
            }

            DOTween.To(() => _ballSpeed, x => _ballSpeed = x, beatItem.Speed, beatItem.SpeedTime);
        }
    }

    protected virtual void ShowChartingItems()
    {
        if (_isAutoMode || state.ShowDodge)
        {
            ShowDodgePoint();
        }

        ShowInObstacles();
        ShowOutObstacles();

        ShowSavePoint();
    }

    //protected virtual void ShowItems()
    //{
    //    var beatItem = bmwReader.ChartingItem[_currentLine];
    //}

    protected virtual void ShowDodgePoint()
    {
        if (_currentLine >= bmwReader.ChartingItem.Count) return;

        var beatItem = bmwReader.ChartingItem[_currentLine];

        if (beatItem != null)
        {
            // Hide
            foreach (var dodgeList in DodgePointLists)
            {
                dodgeList.SetActive(false);
            }

            // Show Objects
            if (beatItem.DodgePointElements[0].Index > -1)
            {
                foreach (var dodge in beatItem.DodgePointElements)
                {
                    DodgePointLists[dodge.Index].gameObject.SetActive(true);
                }
            }

            // Show Dummy
            if (beatItem.DummyDodgePointElements[0].Index > -1)
            {
                foreach (var dummy in beatItem.DummyDodgePointElements)
                {
                    DodgePointLists[dummy.Index].gameObject.SetActive(true);
                }
            }
        }
    }

    protected virtual void ShowInObstacles()
    {
        if (_currentLine >= bmwReader.ChartingItem.Count) return;

        var beatItem = bmwReader.ChartingItem[_currentLine];

        if (beatItem != null)
        {
            // Hide
            foreach (var inList in InObstacleLists)
            {
                inList.SetActive(false);
            }

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

    protected virtual void ShowOutObstacles()
    {
        var beatItem = bmwReader.ChartingItem[_currentLine];

        if (beatItem != null)
        {
            // Hide
            foreach (var outList in OutObstacleLists)
            {
                outList.SetActive(false);
            }

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

    protected virtual void ShowSavePoint()
    {
        var beatItem = bmwReader.ChartingItem[_currentLine];

        if (beatItem.SavePoint != -1)
        {
            CenterPivot.transform.localEulerAngles = Vector3.zero;

            CenterPivot.transform.Rotate(0f, 0f, beatItem.SavePoint * _spawnAngle);
            CreateSavePoint(0);

            //GlobalState.Instance.SavePointAngle = beatItem.SavePoint * _spawnAngle;
        }
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (_isPlay)
        {
            PlayGame();
        }
    }

    protected virtual void PlayGame()
    {
        _playTime += Time.deltaTime;
        _timer += Time.deltaTime;

        Center.transform.Rotate(0f, 0f, (Time.deltaTime / _beatTime) * -_ballSpeed);
        OperateBallMovement();

        if (_currentLine < bmwReader.ChartingItem.Count - 1)
        {
            if (_timer > _beatTime)
            {                
                _currentLine++;
                PlayProcess();

                _timer -= _timer;
            }
        }
        else
        {
            FinishGame();
        }
    }

    public virtual void FinishGame()
    {
        _isPlay = false;

        if (DataManager.dataBackgroundProcActive)
        {
            //Save Stage Result At GlobalState 
            SaveGameResult();

            //ResetGlobalState();

            // TO DO : Go Panel Result
            SoundManager.Instance.ForceAudioStop();
            UIManager.Instance.UIElementFadePanel.StageToResult();
        }
    }

    public virtual void GoBackSelectStage()
    {
        _isPlay = false;

        if (DataManager.dataBackgroundProcActive)
        {
            SaveGameResult();

            //Reset GlobalState Value
            ResetGlobalState();

            // TO DO : Go Panel Music Select
            SoundManager.Instance.ForceAudioStop();

            //UIManager.Instance.GoPanelMusicSelect();
            UIManager.Instance.UIElementFadePanel.StageToMusic();
        }
    }

    // TO DO : 게임 플레이 결과 Global Data나 Global Stage에 저장 -> 그 데이터로 결과창 구현 로직 처리
    protected virtual void SaveGameResult()
    {
        int _stageIndex = GlobalState.Instance.StageIndex;
        int _albumIndex = GlobalState.Instance.AlbumIndex;

        // Save Total Play Time
        state.StagePlayTime = (int)_playTime;

        switch (_albumIndex)
        {
            case 0: DataManager.dataAlbum1ClearYn[_stageIndex] = "Y"; break;
            case 1: DataManager.dataAlbum2ClearYn[_stageIndex] = "Y"; break;
            case 2: DataManager.dataAlbum3ClearYn[_stageIndex] = "Y"; break;
            case 3: DataManager.dataAlbum4ClearYn[_stageIndex] = "Y"; break;
        }

        int _clearCount = 0;
        for (int i = 0; i < DataManager.dataAlbum1ClearYn.Length; i++)
        {
            _clearCount += DataManager.dataAlbum1ClearYn[i] == "Y" || DataManager.dataAlbum1ClearYn[i] == "P" ? 1 : 0;
        }
        for (int j = 0; j < DataManager.dataAlbum2ClearYn.Length; j++)
        {
            _clearCount += DataManager.dataAlbum2ClearYn[j] == "Y" || DataManager.dataAlbum2ClearYn[j] == "P" ? 1 : 0;
        }
        for (int k = 0; k < DataManager.dataAlbum3ClearYn.Length; k++)
        {
            _clearCount += DataManager.dataAlbum3ClearYn[k] == "Y" || DataManager.dataAlbum3ClearYn[k] == "P" ? 1 : 0;
        }
        for (int m = 0; m < DataManager.dataAlbum4ClearYn.Length; m++)
        {
            _clearCount += DataManager.dataAlbum4ClearYn[m] == "Y" || DataManager.dataAlbum4ClearYn[m] == "P" ? 1 : 0;
        }

        DataManager.dataClearStageCount = _clearCount;
    }

    protected virtual void OperateBallMovement()
    {
        // 공의 움직임 구현
        Player.transform.localEulerAngles = new Vector3(0f, 0f, Center.transform.localEulerAngles.z);
        Player.transform.localPosition = Center.transform.localPosition + Center.transform.up * ballRadius;

        // Input Change Direction
        if (!_isAutoMode)
        {
            InputChangeDirection();
        }
    }

    // 키 입력 처리
    #region Change Direction
    protected bool _isInState = false;
    public virtual void InputChangeDirection()
    {
        if (Input.anyKey && null != Input.inputString && "" != Input.inputString)
        {
            if (_keyDivision.Equals("Separation"))
            {
                var innerKey = DataManager.dataInnerOperationKey;
                var outKey = DataManager.dataOuterOperationKey;

                // 키 분리 구분 > 분리
                if (innerKey != null && innerKey.Length > 0 && outKey != null && outKey.Length > 0)
                {
                    SeperateChangeDirection();                   
                }
                else
                {
                    // 키 분리 구분 > 분리 > 커스텀 한 키가 없을 땐 스페이스로 통일
                    IntegrationChangeDirection();
                }
            }
            else
            {
                // 키 분리 구분 > 통합
                if (Input.anyKeyDown)
                {
                    IntegrationChangeDirection();                    
                }
            }
        }
    }

    public virtual void SeperateChangeDirection()
    {
        var innerOperationKey = DataManager.dataInnerOperationKey;
        var outOperationKey = DataManager.dataOuterOperationKey;

        for (int i = 0; i < innerOperationKey.Length; i++)
        {
            if (!"".Equals(innerOperationKey[i]) && Input.inputString.Equals(innerOperationKey[i]))
            {
                _isInState = true;
                ChangeDirection(_isInState);
            }

            if (!"".Equals(outOperationKey[i]) && Input.inputString.Equals(outOperationKey[i]))
            {
                _isInState = false;
                ChangeDirection(_isInState);
            }
        }
    }

    public virtual void IntegrationChangeDirection()
    {
        _isInState = !_isInState;
        ChangeDirection(_isInState);
    }

    public virtual void ChangeDirection(bool isIn)
    {
        if (isIn)
        {
            ballRadius = inRadius;
            ChangeDirectionEffect();
        }
        else
        {
            ballRadius = outRadius;
            ChangeDirectionEffect();
        }
    }

    protected float _changeDirectionDuration = 0.2f;
    protected virtual void ChangeDirectionEffect()
    {
        if (_isInState)
        {
            TweenChangeDirection(InCircleFade, _changeDirectionDuration);
        }
        else
        {
            TweenChangeDirection(OutCircleFade, _changeDirectionDuration);
        }
    }

    protected virtual void TweenChangeDirection(Image circle, float duration)
    {
        circle.color = Color.black;
        circle.DOColor(Color.clear, duration);
    }
    #endregion

    protected float _savePointTime = 0f;
    public virtual void SavePointEnter()
    {
        //if (SavePointTween.IsPlaying())
        //{
        //    SavePointTween.Kill();
        //}

        _savePointTime = _timer;

        state.SavePoint = _currentLine;
        state.SaveMusicPlayingTime = audioSource.time;
        state.SavePointAngle = Center.transform.localEulerAngles.z;

        //TO DO : 각자 스테이지에서 구현할 것 
        EnterSavePointEffect();
    }

    protected virtual void ResetSavePointState()
    {
        _currentLine = GlobalState.Instance.SavePoint;
        _timer = _savePointTime;
        audioSource.time = GlobalState.Instance.SaveMusicPlayingTime;

        InitHP();

        TweenClearRate();
        
        if (GlobalState.Instance.SavePoint > 0)
        {
            var beatItem = bmwReader.ChartingItem[_currentLine];

            GlobalState.Instance.SavePointAngle = beatItem.BallAngle;

            AddInterval(beatItem);

            Center.transform.localEulerAngles = new Vector3(0f, 0f, -GlobalState.Instance.SavePointAngle);
            Player.transform.localPosition = Center.transform.localPosition + Center.transform.up * ballRadius;
        }
        else
        {
            InitBallPosition();
            _isInState = false;
        }

        if (StageAnim.isPlaying)
        {
            StageAnim.Stop();
        }

        PlayProcess();
    }


    protected virtual void PlayerDieAndSavePointPlay()
    {        
        _currentHP--;

        if (_currentHP <= 0)
        {
            state.PlayerDeadCount++;
            ResetSavePointState();

            Debug.Log("Player Die!!");
        }
        else
        {
            EffectHP();
        }
    }

    protected virtual void EffectHP()
    {

    }

    public virtual void PlayerDie()
    {
        PlayerDieAndSavePointPlay();
    }

    //-------------------------- Save Point Effect --------------------------------
    public Color[] EffectColor;
    private Color _whiteAlpha0 = new Color(1,1,1,0);
    Tween savePointEffect;
    protected virtual void EnterSavePointEffect()
    {
        if (SavePointCircles == null) return;

        float _effectDuration = 0.2f;

        for (int i = 0; i < SavePointCircles.Length; i++)
        {
            // Set Scale
            SavePointCircles[i].rectTransform.localScale = Vector2.zero;

            // Set Color
            if (EffectColor[i] == null) EffectColor[i] = Color.red;
            SavePointCircles[i].color = EffectColor[i];

            // Enable
            SavePointCircles[i].gameObject.SetActive(true);

            // Tween
            var tween = SavePointCircles[i].rectTransform.DOScale(Vector2.one, _effectDuration);
            tween.SetEase(Ease.InCubic).OnComplete(() => { HideEffect(); });
        }
    }

    protected virtual void HideEffect()
    {
        float _fadeDuration = 0.6f;

        for (int i = 0; i < SavePointCircles.Length; i++)
        {
            var tween = SavePointCircles[i].DOColor(_whiteAlpha0, _fadeDuration);
            tween.SetEase(Ease.InQuart);//.OnComplete(() => SavePointCircles[i].gameObject.SetActive(false));
        }
    }

    protected virtual void GetDefaultColor()
    {
        EffectColor[0] = new Color32(100, 100, 185, 255);
        EffectColor[1] = new Color32(255, 130, 126, 255);
        EffectColor[2] = new Color32(255, 148, 255, 255);
        EffectColor[3] = new Color32(242, 252, 137, 255);
        EffectColor[4] = new Color32(147, 255, 255, 200);
        EffectColor[5] = new Color32(0, 0, 0, 90);
        EffectColor[6] = new Color32(115, 181, 116, 200);
    }

    protected virtual void SetEffectCircleColor(Color fst, Color snd, Color third, Color fourth, Color fifth, Color six, Color seven)
    {
        EffectColor[0] = fst;
        EffectColor[1] = snd;
        EffectColor[2] = third;
        EffectColor[3] = fourth;
        EffectColor[4] = fifth;
        EffectColor[5] = six;
        EffectColor[6] = seven;
    }

    // -------------- UI Setting Function ---------------
    public virtual void OnClickPause()
    {
        if (_isPlay)
        {
            _isPause = !_isPause;

            if (_isPause)
            {
                Time.timeScale = 0;
                audioSource.Pause();
            }
            else
            {
                Time.timeScale = 1;
                audioSource.Play();
            }
        }
    }

    public void InputExcute()
    {
        if (state.UseNewGaMe)
        {
            InputX();
        }

        if (state.UseBarrier)
        {
            InputZ();
        }
    }

    protected virtual void InputX()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            CenterPivot.transform.localEulerAngles = Center.transform.localEulerAngles - new Vector3(0, 0, _spawnAngle);
            CreateSavePoint(0);
        }
    }

    protected virtual void InputZ()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {

        }
    }

    void Barrier()
    {

    }
}
