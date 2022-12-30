using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class UIElementMusicSelect : MonoBehaviour
{
    // 쌍방 참조... 나중에 해결책 찾기...
    public List<UIObjectStage> UIObjectStages = new List<UIObjectStage>();

    [SerializeField] private GameObject uiObjectStage;
    [SerializeField] private Transform uiObjectStageBase;

    [SerializeField] private Image stageText;
    [SerializeField] private Image backGround;

    [SerializeField] private GameObject uiObjectProgressBar;
    [SerializeField] private Transform uiObjectProgressBarBase;

    [SerializeField] private GameObject uiObjectProgressCircle;
    [SerializeField] private Transform uiObjectProgressCircleBase;

    [SerializeField] private ScrollRect scrollRect;

    [SerializeField] 
    private GameObject uiObjectMusicTutorial;
    private Image _imageTutorial = null;


    [SerializeField] private TMP_Text _musicLength;

    // Start is called before the first frame update
    void Start()
    {
        UIManager.Instance.MakeTutorial(uiObjectMusicTutorial, this.transform, _imageTutorial, 0.4f, 1f, 2f);
    }

    private void OnEnable()
    {
        SetAudio();
        ResetMusicSelect();
        ChangeMusicLength();
    }

    private void SetAudio()
    {
        var soundManager = SoundManager.Instance;

        GlobalState.Instance.StageIndex = 0;
        soundManager.TurnOffGameBackground();
        soundManager.TurnOnSelectedMusic();
        soundManager.FadeInMusicVolume(1f);

        scrollRect.content.anchoredPosition = Vector2.zero;
    }

    private void ResetMusicSelect()
    {
        MakeAlbumStages();
        ChangeBackGround();
    }

    private void OnDisable()
    {
        DestroyAlbumObjects();
    }

    void Update()
    {
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
            case (int)GlobalData.ALBUM.CONTEST2:
                MakeAlbumStage(albumData.FifthAlbumMusicCircle, albumData.FifthAlbumMusicLevel);
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
                    stageInfo.UIElementMusicSelect = this;
                    stageInfo.name = $"Stage_{_stageIndex}";
                    stageInfo.StageThumnail = stageCircles[_stageIndex];
                    stageInfo.StageLevel = stageLevel[_stageIndex];
                    stageInfo.StageIndex = _stageIndex;

                    UIObjectStages.Add(stageInfo);
                }
                else
                {
                    Debug.LogError("리소스가 빠져있습니다.");
                }
            }

            GlobalState.Instance.AlbumStageCount = _stageIndex;
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

        // Init List
        UIObjectStages.Clear();
    }
    #endregion

    #region Input Excute
    public void InputExecute()
    {        
        if (DataManager.dataBackgroundProcActive)
        {
            if (!GlobalState.Instance.IsTweening)
            {
                InputRightArrow();
                InputLeftArrow();

                if (GlobalState.Instance.DevMode)
                {
                    InputEscape();
                    InputReturn();
                }
            }
        }
    }

    void InputRightArrow()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            InputRightFunction();
        }
    }

    BMWReader _bmwReader;

    public void InputRightFunction()
    {
        var state = GlobalState.Instance;
        if (state.StageIndex < state.AlbumStageCount)
        {
            state.StageIndex++;
            SoundManager.Instance.TurnOnSelectedMusic();
            ChangeBackGround();
            MoveScrollRect();
            ChangeMusicLength();
            SoundManager.Instance.PlaySoundFX((int)GlobalData.SFX.AlbumMove);

            Debug.Log($"Selecte My Stage Index : {state.StageIndex}");
        }
    }

    void ChangeMusicLength()
    {
        _bmwReader = new BMWReader();
        _bmwReader.ReadFile(GlobalState.Instance.BMWFolderPath + "/" + GlobalState.Instance.BMWFile);

        int time = _bmwReader.MusicInfoItem.Time;
        int min = time / 60;
        int sec = time % 60;

        _musicLength.text = $"{min}:{sec.ToString("D2")}";
    }

    void InputLeftArrow()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            InputLeftFunction();
        }
    }

    public void InputLeftFunction()
    {
        var state = GlobalState.Instance;
        if (0 < state.StageIndex)
        {
            state.StageIndex--;
            SoundManager.Instance.TurnOnSelectedMusic();
            ChangeBackGround();
            MoveScrollRect();
            ChangeMusicLength();
            SoundManager.Instance.PlaySoundFX((int)GlobalData.SFX.AlbumMove);

            Debug.Log($"Selecte My Stage Index : {state.StageIndex}");
        }
    }

    void InputReturn()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            UIManager.Instance.UIElementFadePanel.MusicToStage();
        }
    }

    void InputEscape()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnClickEsc();
        }
    }

    public void OnClickEsc()
    {
        UIManager.Instance.GoPanelAlbumSelect();
        SoundManager.Instance.ForceAudioStop();
    }
    #endregion

    public void ShowSelectedStage()
    {
        foreach (var stage in UIObjectStages)
        {
            stage.ShowMyIndex();
        }
    }

    float _duration = 0.5f;
    void MoveScrollRect()
    {
        var state = GlobalState.Instance;
        float StageLength = state.AlbumStageCount;
        float currentStage = state.StageIndex;

        if (state.AlbumStageCount == 0) return;

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
            case (int)GlobalData.ALBUM.CONTEST2:
                backGround.sprite = albumData.FifthAlbumMusicBackground[state.StageIndex];
                break;
        }

        // stage Text 변경 시켜주는곳
        stageText.sprite = albumData.StageIcons[state.StageIndex];
    }

}

