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

    //---------------------------------------------------
    private float dodgeRadius = 334f;
    private float outRadius = 355f;
    private float inRadius = 312f;
    private float ballRadius = 0;

    private float variableRadius;
    private float speed = 360;

    private BMWReader bmwReader = null;
    private AudioSource audioSource = null;

    private string _title = "";                          // 곡 제목
    private string _artist = "";                         // 작곡가
    private float _bpm = 0;                           // Beat Per Minute
    private int _totalBeatCount = 0;                // 총 Beat 수
    private float _musicPlayTime = 0f;              // 곡의 총 시간

    private static float _spawnAngle = -5f;

    protected static int savePointNum = 0;
    private float saveMusicPlayingTime = 0;

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
        InitBallPosition();
        GetMusicInfo();

        // Calculate Beat
        _totalBeatCount = bmwReader.ChartingItem.Count;
        CalculateTick();

        // Sound 제어 부
        audioSource = SoundManager.Instance.MusicAudio;
        SoundManager.Instance.TurnOnStageMusic();
    }

    void GetMusicInfo()
    {
        if (bmwReader)
        {
            _title = bmwReader.MusicInfoItem.Title;
            _artist = bmwReader.MusicInfoItem.Artist;
            _bpm = bmwReader.MusicInfoItem.BPM;
        }
    }

    protected void InitBallPosition()
    {
        variableRadius = outRadius;
        ballRadius = variableRadius;     // Init Ball Position 

        // Init Start Ball Rotation
        float ballAngle = bmwReader.ChartingItem[_currentBeat].BallAngle;
        Center.transform.localEulerAngles = new Vector3(0f, 0f, -ballAngle);

        Ball.transform.localPosition = Center.transform.localPosition + Center.transform.up * ballRadius;
    }

    public void GetBallSkin()
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

    void CreateDodgePoint()
    {
        for (int i = 0; i < 72; i++)
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

    void CreateObstacles()
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

    void CreateInObstacle(int obstacleType)
    {
        for (int i = 0; i < 72; i++)
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

    void CreateOutObstacle(int obstacleType)
    {
        Center.transform.localEulerAngles = Vector3.zero;

        for (int i = 0; i < 72; i++)
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

    void CreateSavePoint(int savePointType)
    {
        GameObject savePoint = GameObject.Instantiate(SavePoint[savePointType], Container);

        if (savePoint)
        {
            savePoint.transform.localPosition = CenterPivot.transform.localPosition + CenterPivot.transform.up * dodgeRadius;
        }
    }

    private float tick = 0;        
    private int tickCount = 4;
    private float beatTime = 0;
    void CalculateTick()
    {
        _bpm = bmwReader.MusicInfoItem.BPM;

        float bps = _bpm / 60;
        tick = 1 / bps;             

        beatTime = tick * tickCount;

        if (bmwReader.ChartingItem[currentItem].Speed == -1)
        {
            speed = 360f;
        }
        else
        {
            speed = bmwReader.ChartingItem[currentItem].Speed;
        }
    }


    private int _currentBeat = 0;                   // 현재 진행중인 Beat 수
    private float _bpa = 0;                         // Beat Per Angle 한 비트당 회전하는 양
    private bool _processPlaying = false;           // 게임 진행중 체크
    private List<ChartingItem> beatItems = new List<ChartingItem>();    // 한 비트당 사용되는 아이템들

    private void ReadProcess()
    {

    }

    void PlayProcess()
    {
        if (bmwReader != null)
        {
            var beatItem = bmwReader.ChartingItem[currentItem];//beatItems[_currentBeat];
            if (beatItem == null)
            {
                Debug.LogError("PlayPorocess beat Item is null");
                return;
            }
            
            //_bpa = beatItem.Speed;           
        }

        ShowChartingItems();
    }

    void PlayBeat()
    {
        //tick
    }

    void ShowChartingItems()
    {
        ShowDodgePoint();
        ShowInObstacles();
        ShowOutObstacles();
        ShowSavePoint();
    }

    void ShowDodgePoint()
    {
        var beatItem = bmwReader.ChartingItem[currentItem];

        if (beatItem != null)
        {
            // Hide
            for (int i = 0; i < DodgePointList.Count; i++)
            {
                DodgePointList[i].SetActive(false);
            }

            // Show Objects
            if (beatItem.DodgePointElements[0].Index > -1)
            {
                for (int i = 0; i < beatItem.DodgePointElements.Count; i++)
                {
                    DodgePointList[beatItem.DodgePointElements[i].Index].gameObject.SetActive(true);

                }
            }
        }
    }

    void ShowInObstacles()
    {
        var beatItem = bmwReader.ChartingItem[currentItem];

        if (beatItem != null)
        {
            // Hide
            for (int i = 0; i < InObstacleList.Count; i++)
            {
                InObstacleList[i].SetActive(false);
            }

            // Show Objects
            if (beatItem.InObstacleElements[0].Index > -1)
            {
                for (int i = 0; i < beatItem.InObstacleElements.Count; i++)
                {
                    InObstacleList[beatItem.InObstacleElements[i].Index].gameObject.SetActive(true);
                }
            }           
        }
    }

    void ShowOutObstacles()
    {
        var beatItem = bmwReader.ChartingItem[currentItem];

        if (beatItem != null)
        {
            // Hide
            for (int i = 0; i < OutObstacleList.Count; i++)
            {
                OutObstacleList[i].SetActive(false);
            }

            // Show
            if (beatItem.OutObstacleElements[0].Index > -1)
            {
                for (int i = 0; i < beatItem.OutObstacleElements.Count; i++)
                {
                    OutObstacleList[beatItem.OutObstacleElements[i].Index].gameObject.SetActive(true);
                }
            }
        }
    }

    void ShowSavePoint()
    {
        var beatItem = bmwReader.ChartingItem[currentItem];

        if (beatItem.SavePoint != -1)
        {
            CenterPivot.transform.localEulerAngles = Vector3.zero;

            CenterPivot.transform.Rotate(0f, 0f, beatItem.SavePoint * _spawnAngle);
            CreateSavePoint(0);
        }
    }

    public static int currentItem = 0;
    public int dodgelistars = 0;
    private float timer = 0f;

    public float Timer
    {
        get => timer = Timer;
        set => timer = value;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        timer += Time.deltaTime;
        Center.transform.Rotate(0f, 0f, (Time.deltaTime / beatTime) * -speed);

        if (currentItem < bmwReader.ChartingItem.Count)
        {
            if (timer > beatTime)
            {
                PlayProcess();

                currentItem++;
                Debug.Log("currentItem :" + currentItem);
                timer -= timer;
            }
        }

        // OperateBallMovement();
        //TweenTest();
        //ViewItemsTest(currentItem);
    }

    void OperateBallMovement()
    {
        Ball.transform.localPosition = Center.transform.localPosition + Center.transform.up * ballRadius;
        ChangeDirection();
    }

    private bool isUpState = true;
    void ChangeDirection()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isUpState = !isUpState;

            if (isUpState)
            {
                ballRadius = outRadius;
            }
            else
            {
                ballRadius = inRadius;
            }
        }
    }

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

}
