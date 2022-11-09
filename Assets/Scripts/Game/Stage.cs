using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
//using System.IO;
//using DG.Tweening;

public abstract class Stage : MonoBehaviour
{
    [Header("Stage Base")]
    public Image BallSkin;
    public Image CircleSkin;
    public Image BackGroundSkin;

    [SerializeField] private GameObject center;
    [SerializeField] private GameObject Ball;

    [SerializeField] private GameObject DodgePoint;
    [SerializeField] private Transform DodgePointBase;
    private static float dodgeRadius = 334f;

    [Header("[ Obstacle ]")]
    [SerializeField] private GameObject[] Obstacles;
    [SerializeField] private Transform ObstacleInBase;
    [SerializeField] private Transform ObstacleOutBase;
    private static float OutRadius = 355f;
    private static float InRadius = 312f;

    private float speed = 180f;
    private static float BallRadius = 0;

    private BMWReader bmwReader = null;

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
        bmwReader = new BMWReader();
        bmwReader.ReadFile(Directory + "/" + BMWFile);

        GetBallSkin();
        //SetCircleSprite(GlobalData.Instance.StageInfo.CircleSkins[0]);
        //SetBackGroundSprite(GlobalData.Instance.StageInfo.BackGroundSkins[0]);

        CreateDodgePoint();
        CreateObstacles();

        BallRadius = OutRadius;

        SoundManager.Instance.TurnOnStageMusic();
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

    public virtual void SetBallSkin(Sprite ball)
    {
        if (BallSkin)
        {
            BallSkin.sprite = ball;
        }
    }

    public virtual void SetCircleSprite(Sprite circle)
    {
        if (CircleSkin)
        {
            CircleSkin.sprite = circle;
        }
    }

    public virtual void SetBackGroundSprite(Sprite backGround)
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
                dodge.transform.localPosition = center.transform.localPosition + center.transform.up * dodgeRadius;
                dodge.transform.localEulerAngles = center.transform.localEulerAngles;
                dodge.SetActive(false);

                center.transform.Rotate(0f, 0f, -5f);
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
                inObstacle.transform.localPosition = center.transform.localPosition + center.transform.up * InRadius;
                inObstacle.transform.localEulerAngles = center.transform.localEulerAngles + new Vector3(0f, 0f, 180f);
                inObstacle.SetActive(false);

                center.transform.Rotate(0f, 0f, -5f);
            }
        }
    }

    void CreateOutObstacle(int obstacleType)
    {
        center.transform.localEulerAngles = Vector3.zero;

        for (int i = 0; i < 72; i++)
        {
            GameObject outObstacle = GameObject.Instantiate(Obstacles[obstacleType], ObstacleOutBase);

            if (outObstacle)
            {
                outObstacle.transform.localPosition = center.transform.localPosition + center.transform.up * OutRadius;
                outObstacle.transform.localEulerAngles = center.transform.localEulerAngles;
                outObstacle.SetActive(false);

                center.transform.Rotate(0f, 0f, -5f);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        center.transform.Rotate(0f, 0f, -Time.deltaTime * speed);
        OperateBallMovement();
    }

    void OperateBallMovement()
    {
        Ball.transform.localPosition = center.transform.localPosition + center.transform.up * BallRadius;
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
                BallRadius = OutRadius;
            }
            else
            {
                BallRadius = InRadius;
            }
        }
    }
}
