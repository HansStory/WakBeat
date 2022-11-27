using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
//using System.IO;
using DG.Tweening;

public abstract class Stage : MonoBehaviour
{
    [Header("Stage Base")]
    public Image BallSkin;
    public Image CircleSkin;
    public Image BackGroundSkin;

    public RectTransform PlayGround;
    public GameObject Center;
    public GameObject Ball;

    [Header("[ Instance Object Container ]")]
    public Transform Container;
    public GameObject CenterPivot;

    [Header("[ Dodge Point ]")]
    public GameObject DodgePoint;
    public Transform DodgePointBase;
    public List<GameObject> DodgePointList = new List<GameObject>();


    [Header("[ Obstacles ]")]
    public GameObject[] Obstacles;

    public Transform ObstacleInBase;
    public List<GameObject> InObstacleList = new List<GameObject>();

    public Transform ObstacleOutBase;
    public List<GameObject> OutObstacleList = new List<GameObject>();

    [Header("[ Save Point ]")]
    public GameObject[] SavePoint;

    public TMP_Text CurrentLineText;

    [Header("[ Count Down ]")] 
    public TMP_Text countDownText;

    [Header("[ Animation ]")]
    public Animation StageAnim;

    //---------------------------------------------------
    protected float dodgeRadius = 334f;
    protected float outRadius = 355f;
    protected float inRadius = 312f;
    protected float ballRadius = 0;

    protected float variableRadius;
    protected float speed = 360;

    protected BMWReader bmwReader = null;
    protected AudioSource audioSource = null;

    protected string _title = "";                // 곡 제목
    protected string _artist = "";               // 작곡가
    protected float _bpm = 0;                    // Bar Per Minute
    protected float _bar = 0;                    // Bar
    protected float _interval = 0f;              // Interval
    protected float _tick = 0;                   // 1 Bar(칸)에 소요되는 시간
    protected float _beatTime = 0;               // 1 Line에 소요되는 시간
    public static int _currentLine = -1;         // 현재 진행중인 Line의 아이템들
    protected float _timer = 0f;                 // 음악 진행 시간
    protected float _playTime = 0f;              // Stage 시작후 총 흐른 시간
    protected static int savePointNum = 0;       // Save Point Beat Item Line
    protected int _totalBeatCount = 0;           // 총 Beat 수

    private float _musicPlayTime = 0f;           // 곡의 총 시간

    protected static int _spawnCount = 72;
    protected static float _spawnAngle = -5f;

    protected int _currentBeat = 0;              // 현재 진행중인 Beat 수
    protected bool _isPlay = false;              // 게임 진행중 체크

    // Key 입력 부
    private string _keyDivision = "";

    public virtual string Directory
    {
        get
        {
            string dir = GlobalState.Instance.ResourceFolder;

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

        //Get User Data (.json file)
        GetBallSkin();

        // Create Stage Objects
        CreateDodgePoint();
        CreateObstacles();

        // Get Values
        GetMusicInfo();
        InitBallPosition();
        InitBallSpeed();
        InitCalculateTick();

        // Calculate Beat
        _totalBeatCount = bmwReader.ChartingItem.Count;

        // Sound 제어 부
        audioSource = SoundManager.Instance.MusicAudio;

        // Key 입력 부
        _keyDivision = null == GlobalState.Instance.UserData.data.KeyDivision ? "Integration" : GlobalState.Instance.UserData.data.KeyDivision;

    }

    protected virtual void Start()
    {
        Invoke($"{nameof(StartGame)}", 1f);
    }

    protected virtual void StartGame()
    {
        _currentLine = 0;       
        SoundManager.Instance.TurnOnStageMusic();

        _isPlay = true;
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
    }

    protected virtual void InitBallPosition()
    {
        variableRadius = outRadius;
        ballRadius = variableRadius;     // Init Ball Position 

        // Init Start Ball Rotation
        float ballAngle = 0;

        if (bmwReader.ChartingItem[_currentBeat].BallAngle >= 0)
        {
            ballAngle = bmwReader.ChartingItem[_currentBeat].BallAngle;
        }
        else
        {
            ballAngle = 0;
        }

        Center.transform.localEulerAngles = new Vector3(0f, 0f, -ballAngle);

        Ball.transform.localPosition = Center.transform.localPosition + Center.transform.up * ballRadius;
    }

    protected virtual void GetBallSkin()
    {
        for (int i = 0; i < GlobalState.Instance.UserData.data.SkinUsingYn.Length; i++)
        {
            if (GlobalState.Instance.UserData.data.SkinUsingYn[i].Contains("Y"))
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
            GameObject dodge = GameObject.Instantiate(DodgePoint, DodgePointBase);

            if (dodge)
            {
                dodge.transform.localPosition = Center.transform.localPosition + Center.transform.up * dodgeRadius;
                dodge.transform.localEulerAngles = Center.transform.localEulerAngles;
                DodgePointList.Add(dodge);

                dodge.SetActive(false);

                Center.transform.Rotate(0f, 0f, _spawnAngle);
            }
        }
    }

    protected virtual void CreateObstacles()
    {
        switch (GlobalState.Instance.AlbumIndex)
        {
            case (int)GlobalData.ALBUM.ISEDOL:
                switch (GlobalState.Instance.StageIndex)
                {
                    case (int)GlobalData.STAGE.STAGE1:
                    case (int)GlobalData.STAGE.STAGE2:
                    case (int)GlobalData.STAGE.STAGE3:
                    case (int)GlobalData.STAGE.STAGE4:
                    case (int)GlobalData.STAGE.STAGE5:
                        CreateInObstacle(0);
                        CreateOutObstacle(0);
                        break;
                }
                break;
            case (int)GlobalData.ALBUM.CONTEST:
                switch (GlobalState.Instance.StageIndex)
                {
                    case (int)GlobalData.STAGE.STAGE1:
                        CreateInObstacle(1);
                        CreateOutObstacle(1);
                        break;
                    case (int)GlobalData.STAGE.STAGE2:
                    case (int)GlobalData.STAGE.STAGE3:
                    case (int)GlobalData.STAGE.STAGE4:
                    case (int)GlobalData.STAGE.STAGE5:
                        CreateInObstacle(0);
                        CreateOutObstacle(0);
                        break;
                }
                break;
            case (int)GlobalData.ALBUM.GOMIX:
                switch (GlobalState.Instance.StageIndex)
                {
                    case (int)GlobalData.STAGE.STAGE1:
                    case (int)GlobalData.STAGE.STAGE2:
                    case (int)GlobalData.STAGE.STAGE3:
                    case (int)GlobalData.STAGE.STAGE4:
                    case (int)GlobalData.STAGE.STAGE5:
                        CreateInObstacle(0);
                        CreateOutObstacle(0);
                        break;
                }
                break;
            case (int)GlobalData.ALBUM.WAKALOID:
                switch (GlobalState.Instance.StageIndex)
                {
                    case (int)GlobalData.STAGE.STAGE1:
                    case (int)GlobalData.STAGE.STAGE2:
                    case (int)GlobalData.STAGE.STAGE3:
                    case (int)GlobalData.STAGE.STAGE4:
                    case (int)GlobalData.STAGE.STAGE5:
                        CreateInObstacle(0);
                        CreateOutObstacle(0);
                        break;
                }
                break;
        }
    }

    protected virtual void CreateInObstacle(int obstacleType)
    {
        for (int i = 0; i < _spawnCount; i++)
        {
            GameObject inObstacle = GameObject.Instantiate(Obstacles[obstacleType], ObstacleInBase);

            if (inObstacle)
            {
                inObstacle.transform.localPosition = Center.transform.localPosition + Center.transform.up * inRadius;
                inObstacle.transform.localEulerAngles = Center.transform.localEulerAngles + new Vector3(0f, 0f, 180f);
                InObstacleList.Add(inObstacle);

                inObstacle.SetActive(false);

                Center.transform.Rotate(0f, 0f, _spawnAngle);
            }
        }
    }

    protected virtual void CreateOutObstacle(int obstacleType)
    {
        Center.transform.localEulerAngles = Vector3.zero;

        for (int i = 0; i < _spawnCount; i++)
        {
            GameObject outObstacle = GameObject.Instantiate(Obstacles[obstacleType], ObstacleOutBase);

            if (outObstacle)
            {
                outObstacle.transform.localPosition = Center.transform.localPosition + Center.transform.up * outRadius;
                outObstacle.transform.localEulerAngles = Center.transform.localEulerAngles;
                OutObstacleList.Add(outObstacle);

                outObstacle.SetActive(false);

                Center.transform.Rotate(0f, 0f, _spawnAngle);
            }
        }
    }

    protected virtual void CreateSavePoint(int savePointType)
    {
        GameObject savePoint = GameObject.Instantiate(SavePoint[savePointType], Container);

        if (savePoint)
        {
            savePoint.transform.localPosition = CenterPivot.transform.localPosition + CenterPivot.transform.up * dodgeRadius;
        }
    }

    protected virtual void InitBallSpeed()
    {
        if (bmwReader.ChartingItem[0].Speed == -1)
        {
            speed = 360f;
        }
        else
        {
            speed = bmwReader.ChartingItem[0].Speed;
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
        var beatItem = bmwReader.ChartingItem[_currentLine]; //beatItems[_currentBeat];
        if (beatItem == null)
        {
            Debug.LogError("PlayPorocess beat Item is null");
            return;
        }
        //_processPlaying = true;

        PlayAnimation(_animationName);

        ChangeBar();

        AddInterval();

        ChangeBallAngle();

        ChangeBallSpeed();

        ShowChartingItems();


        CurrentLineText.text = $"Current Line : {_currentLine}";    
    }

    protected virtual void PlayBeat()
    {
        //tick
    }

    private string _animationName = string.Empty;
    protected virtual void PlayAnimation(string animationName)
    {
        var beatItem = bmwReader.ChartingItem[_currentLine];

        if (beatItem.AnimationIndex >= 0 && bmwReader.AnimationItem.Count > beatItem.AnimationIndex)
        {
            var animItem = bmwReader.AnimationItem[beatItem.AnimationIndex];

            _animationName = animItem.AnimationName;

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

    protected virtual void AddInterval()
    {
        var beatItem = bmwReader.ChartingItem[_currentLine];

        if (beatItem.Interval >= 0)
        {
            _timer -= beatItem.Interval;
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

            DOTween.To(() => speed, x => speed = x, beatItem.Speed, beatItem.SpeedTime);
        }
    }

    protected virtual void ShowChartingItems()
    {
        ShowDodgePoint();
        ShowInObstacles();
        ShowOutObstacles();
        ShowSavePoint();
    }

    protected virtual void ShowItems()
    {
        var beatItem = bmwReader.ChartingItem[_currentLine];
    }

    protected virtual void ShowDodgePoint()
    {
        var beatItem = bmwReader.ChartingItem[_currentLine];

        if (beatItem != null)
        {
            // Hide
            foreach (var dodgeList in DodgePointList)
            {
                dodgeList.SetActive(false);
            }

            // Show Objects
            if (beatItem.DodgePointElements[0].Index > -1)
            {
                foreach (var dodge in beatItem.DodgePointElements)
                {
                    DodgePointList[dodge.Index].gameObject.SetActive(true);
                }
            }

            // Show Dummy
            if (beatItem.DummyDodgePointElements[0].Index > -1)
            {
                foreach (var dummy in beatItem.DummyDodgePointElements)
                {
                    DodgePointList[dummy.Index].gameObject.SetActive(true);
                }
            }
        }
    }

    protected virtual void ShowInObstacles()
    {
        var beatItem = bmwReader.ChartingItem[_currentLine];

        if (beatItem != null)
        {
            // Hide
            foreach (var inList in InObstacleList)
            {
                inList.SetActive(false);
            }

            // Show Objects
            if (beatItem.InObstacleElements[0].Index > -1)
            {
                foreach (var inObstacle in beatItem.InObstacleElements)
                {
                    InObstacleList[inObstacle.Index].gameObject.SetActive(true);
                }
            }

            // Show Dummy
            if (beatItem.DummyInObstacleElements[0].Index > -1)
            {
                foreach (var dummy in beatItem.DummyInObstacleElements)
                {
                    InObstacleList[dummy.Index].gameObject.SetActive(true);
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
            foreach (var outList in OutObstacleList)
            {
                outList.SetActive(false);
            }

            // Show Objects
            if (beatItem.OutObstacleElements[0].Index > -1)
            {
                foreach (var outObstacle in beatItem.OutObstacleElements)
                {
                    OutObstacleList[outObstacle.Index].gameObject.SetActive(true);
                }
            }

            // Show Dummy
            if (beatItem.DummyOutObstacleElements[0].Index > -1)
            {
                foreach (var dummy in beatItem.DummyOutObstacleElements)
                {
                    OutObstacleList[dummy.Index].gameObject.SetActive(true);
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
        //if (GlobalState.Instance.IsPlayerDied)
        //{
        //    InitBallPosition();
        //    PlayProcess();
        //    return;
        //}

        if (_isPlay)
        {
            PlayGame();
        }

    }

    protected virtual void PlayGame()
    {
        _playTime += Time.deltaTime;
        _timer += Time.deltaTime;

        Center.transform.Rotate(0f, 0f, (Time.deltaTime / _beatTime) * -speed);
        //OperateBallMovement();

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

    protected virtual void FinishGame()
    {
        _isPlay = false;

        if (GlobalState.Instance.UserData.data.BackgroundProcActive)
        {
            SoundManager.Instance.ForceAudioStop();

            GameFactory.Instance.DistroyStage();
            UIManager.Instance.GoPanelResult();

            SoundManager.Instance.TurnOnGameBackGround();
        }
    }

    protected virtual void OperateBallMovement()
    {
        Ball.transform.localEulerAngles = new Vector3(0f, 0f, Center.transform.localEulerAngles.z);
        Ball.transform.localPosition = Center.transform.localPosition + Center.transform.up * ballRadius;

        InputChangeDirection();
    }

    // 키 입력 처리
    protected bool _isInState = false;
    protected virtual void InputChangeDirection()
    {
        if (Input.anyKey && null != Input.inputString && "" != Input.inputString)
        {
            if (_keyDivision.Equals("Separation"))
            {
                // 키 분리 구분 > 분리
                if (null != GlobalState.Instance.UserData.data.InnerOperationKey && GlobalState.Instance.UserData.data.InnerOperationKey.Length > 0
                    && null != GlobalState.Instance.UserData.data.OuterOperationKey && GlobalState.Instance.UserData.data.OuterOperationKey.Length > 0)
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
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    IntegrationChangeDirection();
                }
            }
        }
    }

    protected virtual void SeperateChangeDirection()
    {
        for (int i = 0; i < GlobalState.Instance.UserData.data.InnerOperationKey.Length; i++)
        {
            if (!"".Equals(GlobalState.Instance.UserData.data.InnerOperationKey[i])
                    && Input.inputString.Equals(GlobalState.Instance.UserData.data.InnerOperationKey[i]))
            {
                _isInState = true;
                ballRadius = inRadius;
            }
            if (!"".Equals(GlobalState.Instance.UserData.data.OuterOperationKey[i])
                    && Input.inputString.Equals(GlobalState.Instance.UserData.data.OuterOperationKey[i]))
            {
                _isInState = false;
                ballRadius = outRadius;
            }
        }
    }

    protected virtual void IntegrationChangeDirection()
    {
        _isInState = !_isInState;

        if (_isInState)
        {
            ballRadius = inRadius;
        }
        else
        {
            ballRadius = outRadius;
        }
    }

    #region Test Scripts
    void ViewItemsTest(int currentItem)
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            var beatItem = bmwReader.ChartingItem[currentItem];

            if (beatItem != null)
            {
                for (int i = 0; i < DodgePointList.Count; i++)
                {
                    DodgePointList[i].SetActive(false);
                }

                for (int i = 0; i < beatItem.DodgePointElements.Count; i++)
                {
                    DodgePointList[beatItem.DodgePointElements[i].Index].gameObject.SetActive(true);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            var beatItem = bmwReader.ChartingItem[currentItem];

            if (beatItem != null)
            {
                for (int i = 0; i < InObstacleList.Count; i++)
                {
                    InObstacleList[i].SetActive(false);
                }

                for (int i = 0; i < beatItem.InObstacleElements.Count; i++)
                {
                    InObstacleList[beatItem.InObstacleElements[i].Index].gameObject.SetActive(true);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            var beatItem = bmwReader.ChartingItem[currentItem];

            if (beatItem != null)
            {
                for (int i = 0; i < OutObstacleList.Count; i++)
                {
                    OutObstacleList[i].SetActive(false);
                }

                for (int i = 0; i < beatItem.OutObstacleElements.Count; i++)
                {
                    OutObstacleList[beatItem.OutObstacleElements[i].Index].gameObject.SetActive(true);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ShowSavePoint();
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            ShowChartingItems();
        }
    }

    void TweenTest()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            DoMovePlayGround(new Vector3(0f, -300f, 0f), 2f, 0f, Ease.Unset);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            DoScalePlayGround(Vector3.one * 0.5f, 2f, 0f, Ease.Unset) ;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            DoRotatePlayGround(new Vector3(0f, 0f, 90f), 2f, 0f, Ease.Unset);
        }
    }

    void AnimaTest()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            StageAnim.Play("Common_Bounce");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            StageAnim.Play("Common_Heave");
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            StageAnim.Play("Common_Surge");
        }
    }
    #endregion

    #region Basic Tween
    Tween moveTween;
    protected virtual void DoMovePlayGround(Vector3 targetVector, float Duration, float delay, Ease easeType)
    {
        moveTween.Pause();
        PlayGround.localPosition = PlayGround.localPosition;

        moveTween = PlayGround.DOLocalMove(targetVector, Duration);
        moveTween.SetDelay(delay);
        moveTween.SetEase(easeType);
    }

    Tween doScaleTween;
    protected virtual void DoScalePlayGround(Vector3 targetScale, float Duration, float delay, Ease easeType)
    {
        doScaleTween.Pause();
        PlayGround.localScale = PlayGround.localScale;

        doScaleTween = PlayGround.DOScale(targetScale, Duration);
        doScaleTween.SetDelay(delay);
        doScaleTween.SetEase(easeType);
    }

    Tween doRotateTween;
    protected virtual void DoRotatePlayGround(Vector3 targetRotate, float Duration, float delay, Ease easeType)
    {
        doRotateTween.Pause();
        PlayGround.localEulerAngles = PlayGround.localEulerAngles;

        doRotateTween = PlayGround.DOLocalRotate(targetRotate, Duration);
        doRotateTween.SetDelay(delay);
        doRotateTween.SetEase(easeType);
    }

    protected void PlayerInit()
    {
        Ball.transform.localPosition = Vector3.zero;
    }
    #endregion

}
