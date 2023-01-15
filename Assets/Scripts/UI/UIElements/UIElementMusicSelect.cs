using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using static GlobalData;

public class UIElementMusicSelect : MonoBehaviour
{
    // 쌍방 참조... 나중에 해결책 찾기...
    public List<UIObjectStage> UIObjectStages = new List<UIObjectStage>();

    [SerializeField] private GameObject _uiObjectStage;
    [SerializeField] private Transform _uiObjectStageBase;

    [SerializeField] private Image _stageText;
    [SerializeField] private Image _backGround;

    [SerializeField] private GameObject _uiObjectProgressBar;
    [SerializeField] private Transform _uiObjectProgressBarBase;

    [SerializeField] private GameObject _uiObjectProgressCircle;
    [SerializeField] private Transform _uiObjectProgressCircleBase;

    [SerializeField] private ScrollRect _scrollRect;

    [SerializeField] 
    private GameObject _uiObjectMusicTutorial;
    private Image _imageTutorial = null;

    [SerializeField] private TMP_Text _musicLength;

    [SerializeField] private Slider _musicClearSlider;

    // Start is called before the first frame update
    void Start()
    {
        UIManager.Instance.MakeTutorial(_uiObjectMusicTutorial, this.transform, _imageTutorial, 0.4f, 1f, 2f);
    }

    private void OnEnable()
    {
        SetAudio();
        ResetMusicSelect();
        ChangeMusicLength();
        SetStageClearRate();
    }

    void SetStageClearRate()
    {
        var alubmIndex = GlobalState.Instance.AlbumIndex;

        switch (alubmIndex)
        {
            case (int)GlobalData.ALBUM.ISEDOL:
                CheckClear(DataManager.dataAlbum1ClearYn, DataManager.dataAlbum1StageCount);
                break;
            case (int)GlobalData.ALBUM.CONTEST:
                CheckClear(DataManager.dataAlbum2ClearYn, DataManager.dataAlbum2StageCount);
                break;
            case (int)GlobalData.ALBUM.GOMIX:
                CheckClear(DataManager.dataAlbum3ClearYn, DataManager.dataAlbum3StageCount);
                break;
            case (int)GlobalData.ALBUM.WAKALOID:
                CheckClear(DataManager.dataAlbum4ClearYn, DataManager.dataAlbum4StageCount);
                break;
            case (int)GlobalData.ALBUM.CONTEST2:
                CheckClear(DataManager.dataAlbum5ClearYn, DataManager.dataAlbum5StageCount);
                break;
        }
    }

    void CheckClear(string[] album, int alubmCount)
    {
        int albumClearCount = 0;

        for (int i = 0; i < album.Length; i++)
        {
            if (album[i].Contains("Y") || album[i].Contains("P"))
            {
                albumClearCount++;
            }
        }

        float rate = 0f;

        if (albumClearCount == 0)
        {
            rate = 0f;
            _musicClearSlider.value = rate;
        }
        else
        {
            rate = (float)albumClearCount / (float)alubmCount;
            _musicClearSlider.value = rate;
        }
    }

    private void SetAudio()
    {
        var soundManager = SoundManager.Instance;

        GlobalState.Instance.StageIndex = 0;
        soundManager.TurnOffGameBackground();
        soundManager.TurnOnSelectedMusic();
        soundManager.FadeInMusicVolume(1f);

        _scrollRect.content.anchoredPosition = Vector2.zero;
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
        ALBUM album = (ALBUM)GlobalState.Instance.AlbumIndex;

        switch (album)
        {
            case ALBUM.ISEDOL:
                MakeAlbumStage(albumData.FirstAlbumMusicCircle, albumData.FirstAlbumMusicLevel);
                break;
            case ALBUM.CONTEST:
                MakeAlbumStage(albumData.SecondAlbumMusicCircle, albumData.SecondAlbumMusicLevel);
                break;
            case ALBUM.GOMIX:
                MakeAlbumStage(albumData.ThirdAlbumMusicCircle, albumData.ThirdAlbumMusicLevel);
                break;
            case ALBUM.WAKALOID:
                MakeAlbumStage(albumData.ForthAlbumMusicCircle, albumData.ForthAlbumMusicLevel);
                break;
            case ALBUM.CONTEST2:
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
            var _stage = GameObject.Instantiate(_uiObjectStage, _uiObjectStageBase);
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
            var progressBar = GameObject.Instantiate(_uiObjectProgressBar, _uiObjectProgressBarBase);

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
            var progressCircle = GameObject.Instantiate(_uiObjectProgressCircle, _uiObjectProgressCircleBase);
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
        Transform[] childList = _uiObjectStageBase.gameObject.GetComponentsInChildren<Transform>();

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
        Transform[] childList = _uiObjectProgressBarBase.gameObject.GetComponentsInChildren<Transform>();

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
        Transform[] childList = _uiObjectProgressCircleBase.gameObject.GetComponentsInChildren<Transform>();

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
        _bmwReader.ReadFile(GlobalState.Instance.BMWFolderPath, GlobalState.Instance.BMWFile);

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

        _scrollRect.DOHorizontalNormalizedPos(wantScrollRect, _duration);
    }

    void ChangeBackGround()
    {
        var albumData = GlobalData.Instance.Album;
        var state = GlobalState.Instance;

        ALBUM album = (ALBUM)GlobalState.Instance.AlbumIndex;

        switch (album)
        {
            case ALBUM.ISEDOL:
                _backGround.sprite = albumData.FirstAlbumMusicBackground[state.StageIndex];
                break;
            case ALBUM.CONTEST:
                _backGround.sprite = albumData.SecondAlbumMusicBackground[state.StageIndex];
                break;
            case ALBUM.GOMIX:
                _backGround.sprite = albumData.ThirdAlbumMusicBackground[state.StageIndex];
                break;
            case ALBUM.WAKALOID:
                _backGround.sprite = albumData.ForthAlbumMusicBackground[state.StageIndex];
                break;
            case ALBUM.CONTEST2:
                _backGround.sprite = albumData.FifthAlbumMusicBackground[state.StageIndex];
                break;
        }

        // stage Text 변경 시켜주는곳
        _stageText.sprite = albumData.StageIcons[state.StageIndex];
    }

}

