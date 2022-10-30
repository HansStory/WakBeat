using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIElementMusicSelect : MonoBehaviour
{
    [SerializeField] private GameObject uiObjectStage;
    [SerializeField] private Transform uiObjectStageBase;

    [SerializeField] private Image stageText;
    [SerializeField] private Image backGround;

    [SerializeField] private GameObject uiObjectProgressBar;
    [SerializeField] private Transform uiObjectProgressBarBase;

    [SerializeField] private GameObject uiObjectProgressCircle;
    [SerializeField] private Transform uiObjectProgressCircleBase;

    // Start is called before the first frame update
    void Start()
    {
        GlobalState.Instance.StageIndex = 0;
        SoundManager.Instance.TurnOffGameBackground();
        SoundManager.Instance.TurnOnSelectedMusic();
    }
    private void OnEnable()
    {
        GlobalState.Instance.StageIndex = 0;
        SoundManager.Instance.TurnOnSelectedMusic();
        MakeAlbumStage();
        ChangeBackGround();
    }

    private void OnDisable()
    {
        DestroyAlbumObjects();
    }

    void Update()
    {
        SelectMusic();
        OnClickEsc();
        InputExecute();
    }

    #region Make Album Stages
    void MakeAlbumStage()
    {
        switch (GlobalState.Instance.AlbumIndex)
        {
            case (int)GlobalData.ALBUM.ISEDOL:
                makeStages(GlobalData.Instance.Album.FirstAlbumMusicCircle, GlobalData.Instance.Album.FirstAlbumMusicLevel);
                MakeProgressBar(GlobalData.Instance.Album.FirstAlbumMusicCircle);
                MakeProgressCicle(GlobalData.Instance.Album.FirstAlbumMusicCircle);
                break;
            case (int)GlobalData.ALBUM.CONTEST:
                makeStages(GlobalData.Instance.Album.SecondAlbumMusicCircle, GlobalData.Instance.Album.SecondAlbumMusicLevel);
                MakeProgressBar(GlobalData.Instance.Album.SecondAlbumMusicCircle);
                MakeProgressCicle(GlobalData.Instance.Album.SecondAlbumMusicCircle);
                break;
            case (int)GlobalData.ALBUM.GOMIX:
                makeStages(GlobalData.Instance.Album.ThirdAlbumMusicCircle, GlobalData.Instance.Album.ThirdAlbumMusicLevel);
                MakeProgressBar(GlobalData.Instance.Album.ThirdAlbumMusicCircle);
                MakeProgressCicle(GlobalData.Instance.Album.ThirdAlbumMusicCircle);
                break;
            case (int)GlobalData.ALBUM.WAKALOID:
                makeStages(GlobalData.Instance.Album.ForthAlbumMusicCircle, GlobalData.Instance.Album.ForthAlbumMusicLevel);
                MakeProgressBar(GlobalData.Instance.Album.ForthAlbumMusicCircle);
                MakeProgressCicle(GlobalData.Instance.Album.ForthAlbumMusicCircle);
                break;
        }
    }

    void makeStages(Sprite[] _stageCircles, Sprite[] _stageLevel)
    {
        var stageCircles = _stageCircles;
        var stageLevel = _stageLevel;

        int _stageIndex = 0;
        foreach (var stages in stageCircles)
        {
            var _stage = GameObject.Instantiate(uiObjectStage, uiObjectStageBase);
            var stageInfo = _stage.GetComponent<UIObjectStage>();

            if (stageInfo)
            {
                // null check
                if (stageCircles.Length == stageLevel.Length)
                {
                    stageInfo.name = $"Stage_{_stageIndex}";
                    stageInfo.StageThumnail = stageCircles[_stageIndex];
                    stageInfo.StageLevel = stageLevel[_stageIndex];
                    stageInfo.StageIndex = _stageIndex;
                }
                else
                {
                    Debug.LogError("리소스가 빠져있습니다.");
                }
            }

            _stageIndex++;
        }
    }

    void MakeProgressBar(Sprite[] _stageCircles)
    {
        int _progressIndex = 1;

        for (int i = 1; i < _stageCircles.Length; i++)
        {
            var progressBar = GameObject.Instantiate(uiObjectProgressBar, uiObjectProgressBarBase);

            if (progressBar)
            {
                progressBar.name = $"ProgressBar_{_progressIndex}";
            }
            _progressIndex++;
        }
    }

    void MakeProgressCicle(Sprite[] _stageCircles)
    {
        int _progressIndex = 0;
        foreach (var stage in _stageCircles)
        {
            var progressCircle = GameObject.Instantiate(uiObjectProgressCircle, uiObjectProgressCircleBase);
            //var progressCircleInfo = progressCircle.GetComponent<UIObjectProgressCircle>();
            if (progressCircle)
            {
                progressCircle.name = $"ProgressCircle_{_progressIndex + 1}";
            }

            _progressIndex++;
        }
    }
    #endregion

    #region Clear Maked Album Object (Destroy)
    void DestroyAlbumObjects()
    {
        DestroyAlbumStage();
        DestroyProgressBar();
        DestroyProgressCircle();
    }

    void DestroyAlbumStage()
    {
        Transform[] childList = uiObjectStageBase.gameObject.GetComponentsInChildren<Transform>();

        if (childList != null)
        {
            for (int i = 1; i < childList.Length; i++)
            {
                if (childList[i] != transform)
                {
                    Destroy(childList[i].gameObject);
                }
            }
        }
    }

    void DestroyProgressBar()
    {
        Transform[] childList = uiObjectProgressBarBase.gameObject.GetComponentsInChildren<Transform>();

        if (childList != null)
        {
            for (int i = 2; i < childList.Length; i++)
            {
                if (childList[i] != transform)
                {
                    Destroy(childList[i].gameObject);
                }
            }
        }
    }

    void DestroyProgressCircle()
    {
        Transform[] childList = uiObjectProgressCircleBase.gameObject.GetComponentsInChildren<Transform>();

        if (childList != null)
        {
            for (int i = 1; i < childList.Length; i++)
            {
                if (childList[i] != transform)
                {
                    Destroy(childList[i].gameObject);
                }
            }
        }
    }
    #endregion

    // Update is called once per frame

    public void InputExecute()
    {

        if (GlobalState.Instance.UserData.data.BackgroundProcActive)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (GlobalState.Instance.StageIndex < SoundManager.Instance.selectedAlbumMusicLength)
                {
                    GlobalState.Instance.StageIndex++;
                    ChangeBackGround();

                    Debug.Log($"Selecte My Stage Index : {GlobalState.Instance.StageIndex}");
                    SoundManager.Instance.TurnOnSelectedMusic();
                }
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (0 < GlobalState.Instance.StageIndex)
                {
                    GlobalState.Instance.StageIndex--;
                    ChangeBackGround();

                    Debug.Log($"Selecte My Stage Index : {GlobalState.Instance.StageIndex}");
                    SoundManager.Instance.TurnOnSelectedMusic();
                }
            }
        }

    }

    void ChangeBackGround()
    {
        switch (GlobalState.Instance.AlbumIndex)
        {
            case (int)GlobalData.ALBUM.ISEDOL:
                backGround.sprite = GlobalData.Instance.Album.FirstAlbumMusicBackground[GlobalState.Instance.StageIndex];
                break;
            case (int)GlobalData.ALBUM.CONTEST:
                backGround.sprite = GlobalData.Instance.Album.SecondAlbumMusicBackground[GlobalState.Instance.StageIndex];
                break;
            case (int)GlobalData.ALBUM.GOMIX:
                backGround.sprite = GlobalData.Instance.Album.ThirdAlbumMusicBackground[GlobalState.Instance.StageIndex];
                break;
            case (int)GlobalData.ALBUM.WAKALOID:
                backGround.sprite = GlobalData.Instance.Album.ForthAlbumMusicBackground[GlobalState.Instance.StageIndex];
                break;
        }

        // stage Text 변경 시켜주는곳
        stageText.sprite = GlobalData.Instance.Album.StageIcons[GlobalState.Instance.StageIndex];
    }

    void SelectMusic()
    {
        if (GlobalState.Instance.UserData.data.BackgroundProcActive)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                UIManager.Instance.GoPanelGamePlay();
            }
        }
    }

    void OnClickEsc()
    {
        if (GlobalState.Instance.UserData.data.BackgroundProcActive)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                UIManager.Instance.GoPanelAlbumSelect();
                SoundManager.Instance.ForceAudioStop();
            }
        }
    }

}

