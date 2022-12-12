using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

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

    [SerializeField] private ScrollRect scrollRect;

    // 음악 정보 팝업
    [SerializeField] private UIElementPopUp UIElementPopUp;

    // Start is called before the first frame update
    void Start()
    {
        var soundManager = SoundManager.Instance;

        GlobalState.Instance.StageIndex = 0;

        soundManager.TurnOffGameBackground();
        soundManager.TurnOnSelectedMusic();
        soundManager.FadeInMusicVolume(1f);
    }

    private void OnEnable()
    {
        var soundManager = SoundManager.Instance;

        GlobalState.Instance.StageIndex = 0;

        soundManager.TurnOnSelectedMusic();
        soundManager.FadeInMusicVolume(1f);

        scrollRect.content.anchoredPosition = Vector2.zero;

        MakeAlbumStages();
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
    void MakeAlbumStages()
    {
        var albumData = GlobalData.Instance.Album;

        switch (GlobalState.Instance.AlbumIndex)
        {
            case (int)GlobalData.ALBUM.ISEDOL:
                MakeAlbumStage(albumData.FirstAlbumMusicCircle, albumData.FirstAlbumMusicLevel);
                break;
            case (int)GlobalData.ALBUM.CONTEST:
                MakeAlbumStage(albumData.SecondAlbumMusicCircle, albumData.SecondAlbumMusicLevel);
                break;
            case (int)GlobalData.ALBUM.GOMIX:
                MakeAlbumStage(albumData.ThirdAlbumMusicCircle, albumData.ThirdAlbumMusicLevel);
                break;
            case (int)GlobalData.ALBUM.WAKALOID:
                MakeAlbumStage(albumData.ForthAlbumMusicCircle, albumData.ForthAlbumMusicLevel);
                break;
        }
    }

    void MakeAlbumStage(Sprite[] albumCircle, Sprite[] albumLevel)
    {
        MakeStages(albumCircle, albumLevel);
        MakeProgressBar(albumCircle);
        MakeProgressCicle(albumCircle);
    }

    void MakeStages(Sprite[] _stageCircles, Sprite[] _stageLevel)
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


    public void InputExecute()
    {

        if (DataManager.dataBackgroundProcActive)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (GlobalState.Instance.StageIndex < SoundManager.Instance.selectedAlbumMusicLength)
                {
                    GlobalState.Instance.StageIndex++;
                    SoundManager.Instance.TurnOnSelectedMusic();
                    ChangeBackGround();
                    MoveScrollRect();

                    Debug.Log($"Selecte My Stage Index : {GlobalState.Instance.StageIndex}");
                }
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (0 < GlobalState.Instance.StageIndex)
                {
                    GlobalState.Instance.StageIndex--;
                    SoundManager.Instance.TurnOnSelectedMusic();
                    ChangeBackGround();
                    MoveScrollRect();

                    Debug.Log($"Selecte My Stage Index : {GlobalState.Instance.StageIndex}");
                }
            }
        }
    }

    float _duration = 0.5f;
    void MoveScrollRect()
    {
        float StageLength = SoundManager.Instance.selectedAlbumMusicLength;
        float currentStage = GlobalState.Instance.StageIndex;

        float wantScrollRect = currentStage / StageLength;

        scrollRect.DOHorizontalNormalizedPos(wantScrollRect, _duration);
    }
    void ChangeBackGround()
    {
        var albumData = GlobalData.Instance.Album;
        var state = GlobalState.Instance;

        switch (GlobalState.Instance.AlbumIndex)
        {
            case (int)GlobalData.ALBUM.ISEDOL:
                backGround.sprite = albumData.FirstAlbumMusicBackground[state.StageIndex];
                break;
            case (int)GlobalData.ALBUM.CONTEST:
                backGround.sprite = albumData.SecondAlbumMusicBackground[state.StageIndex];
                break;
            case (int)GlobalData.ALBUM.GOMIX:
                backGround.sprite = albumData.ThirdAlbumMusicBackground[state.StageIndex];
                break;
            case (int)GlobalData.ALBUM.WAKALOID:
                backGround.sprite = albumData.ForthAlbumMusicBackground[state.StageIndex];
                break;
        }

        // stage Text 변경 시켜주는곳
        stageText.sprite = albumData.StageIcons[state.StageIndex];
    }

    void SelectMusic()
    {
        if (DataManager.dataBackgroundProcActive)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                UIManager.Instance.GoPanelGamePlay();

                // 음악 정보 팝업 호출
                UIElementPopUp.SetPopUpMusicInfo();
            }
        }
    }

    void OnClickEsc()
    {
        if (DataManager.dataBackgroundProcActive)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                UIManager.Instance.GoPanelAlbumSelect();
                SoundManager.Instance.ForceAudioStop();
            }
        }
    }

}

