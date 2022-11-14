using System;
using UnityEngine;

public class GlobalState : MonoBehaviourSingleton<GlobalState>
{
    private int _bgmIndex = 0;
    public int BGMIndex
    {
        get { return _bgmIndex; }
        set
        {
            _bgmIndex = value;
        }
    }

    private int _playTime = 0;
    public int PlayTime
    {
        get { return _playTime; }
        set
        {
            _playTime = value;
        }
    }

    private int _currentPanelIndex = 0;
    /// <summary>
    /// Current Opened Panel Index
    /// </summary>
    public int CurrentPanelIndex
    {
        get { return _currentPanelIndex; }
        set
        {
            _currentPanelIndex = value;
        }
    }

    private int _albumIndex = (int)GlobalData.ALBUM.ISEDOL;
    /// <summary>
    /// Current Album Index
    /// </summary>
    public int AlbumIndex 
    {
        get { return _albumIndex; }
        set
        {
            _albumIndex = value;
        }
    }

    public void SetmyAlbumIndex(int index)
    {
        _albumIndex = index;
    }

    private int _stageIndex = (int)GlobalData.STAGE.STAGE1;
    /// <summary>
    /// Current Stage Index
    /// </summary>
    public int StageIndex
    {
        get { return _stageIndex; }
        set
        {
            _stageIndex = value;
        }
    }

    public void SetmyStageIndex(int index)
    {
        _stageIndex = index;
    }

    private float _sfxVolume = 0.5f;
    /// <summary>
    /// Current SFX Volume Value
    /// </summary>
    public float SFXVolume
    {
        get { return _sfxVolume; }
        set
        {
            value = Mathf.Clamp01(value);
            _sfxVolume = value;
        }
    }

    /// <summary>
    /// Current Music Volume Value
    /// </summary>
    private float _musicVolume = 0.5f;
    public float MusicVolume
    {
        get { return _musicVolume; }
        set
        {
            value = Mathf.Clamp01(value);
            _musicVolume = value;
        }
    }

    public JsonUserData UserData { get; set; } = new JsonUserData();

    private void Start()
    {
        //Debug.Log(DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss:tt")));

    }
    private string _resourceFolder = string.Empty;
    public string ResourceFolder
    {
        get
        {
            switch (AlbumIndex)
            {
                case (int)GlobalData.ALBUM.ISEDOL:
                    _resourceFolder = Application.streamingAssetsPath + "/BMW/Album_01";
                    break;
                case (int)GlobalData.ALBUM.CONTEST:
                    _resourceFolder = Application.streamingAssetsPath + "/BMW/Album_02";
                    break;
                case (int)GlobalData.ALBUM.GOMIX:
                    _resourceFolder = Application.streamingAssetsPath + "/BMW/Album_03";
                    break;
                case (int)GlobalData.ALBUM.WAKALOID:
                    _resourceFolder = Application.streamingAssetsPath + "/BMW/Album_04";
                    break;
            }

            return _resourceFolder;
        }
    }

    private string _bmwFile = string.Empty;
    private string bmw = ".bmw"; //.bmw = Beat Making Wakgood
    public string BMWFile
    {
        get
        {
            switch (StageIndex)
            {
                case (int)GlobalData.STAGE.STAGE1:
                    _bmwFile = "Stage_01" + bmw;
                    break;
                case (int)GlobalData.STAGE.STAGE2:
                    _bmwFile = "Stage_02" + bmw;
                    break;
                case (int)GlobalData.STAGE.STAGE3:
                    _bmwFile = "Stage_03" + bmw;
                    break;
                case (int)GlobalData.STAGE.STAGE4:
                    _bmwFile = "Stage_04" + bmw;
                    break;
                case (int)GlobalData.STAGE.STAGE5:
                    _bmwFile = "Stage_05" + bmw;
                    break;
            }

            return _bmwFile;
        }
    }

    private bool _isAutoMode = false;
    public bool AutoMode
    {
        get { return _isAutoMode; }
        set
        {
            _isAutoMode = value;
        }
    }

    private bool _isShowDodge = false;
    public bool ShowDodge
    {
        get { return _isShowDodge; }
        set
        {
            _isShowDodge = value;
        }
    }

    private int _savePoint = 0;
    public int SavePoint
    {
        get => _savePoint;
        set => _savePoint = value;
    }

    private float _saveMusicPlayingTime = 0;

    public float SaveMusicPlayingTime
    {
        get => _saveMusicPlayingTime;
        set => _saveMusicPlayingTime = value;
    }

    private bool _isPlayerDied = false;

    public bool IsPlayerDied
    {
        get => _isPlayerDied;
        set => _isPlayerDied = value;
    }
}