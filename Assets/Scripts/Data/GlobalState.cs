using System;
using UnityEngine;

public class GlobalState : MonoBehaviourSingleton<GlobalState>
{
    private int _currentPanelIndex = 0;
    public int CurrentPanelIndex
    {
        get { return _currentPanelIndex; }
        set { _currentPanelIndex = value; }
    }

    private int _albumIndex = (int)GlobalData.ALBUM.ISEDOL;
    public int AlbumIndex
    {
        get { return _albumIndex; }
        set { _albumIndex = value; }
    }

    public void SetmyAlbumIndex(int index)
    {
        _albumIndex = index;
    }

    private int _stageIndex = (int)GlobalData.STAGE.STAGE1;
    public int StageIndex
    {
        get { return _stageIndex; }
        set { _stageIndex = value; }
    }

    public void SetmyStageIndex(int index)
    {
        _stageIndex = index;
    }

    //---------------------------Global Sound State ---------------------------------
    private int _bgmIndex = 0;
    public int BGMIndex
    {
        get { return _bgmIndex; }
        set { _bgmIndex = value; }
    }

    private int _stageMusic = 0;
    public int StageMusic
    {
        get { return _stageMusic; }
        set { _stageMusic = value; }
    }

    // È¿°úÀ½ º¼·ý
    private float _sfxVolume = 0.5f;
    public float SFXVolume
    {
        get { return _sfxVolume; }
        set
        {
            value = Mathf.Clamp01(value);
            _sfxVolume = value;
        }
    }

    // À½¾Ç º¼·ý
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

    private int _playTime = 0;
    public int PlayTime
    {
        get { return _playTime; }
        set { _playTime = value; }
    }

    // Json User Data
    public JsonUserData UserData { get; set; } = new JsonUserData();

    private void Start()
    {
        InitConfigSettings();
    }

    // Config Settings
    public void InitConfigSettings()
    {
        DevMode = Config.Instance.DevMode;
        GameMode = Config.Instance.GameMode;
        AutoMode = Config.Instance.AutoMode;      
    }


    #region Global State Stage 
    //------------------------ Global State Stage ----------------------------------
    // Folder path
    private string _bmwFolderPath = string.Empty;
    public string BMWFolderPath
    {        
        get
        {
            var path = Application.streamingAssetsPath + "/BMW/";

            switch (AlbumIndex)
            {
                case (int)GlobalData.ALBUM.ISEDOL:
                    _bmwFolderPath = path + "Album_01";
                    break;
                case (int)GlobalData.ALBUM.CONTEST:
                    _bmwFolderPath = path + "Album_02";
                    break;
                case (int)GlobalData.ALBUM.GOMIX:
                    _bmwFolderPath = path + "Album_03";
                    break;
                case (int)GlobalData.ALBUM.WAKALOID:
                    _bmwFolderPath = path + "Album_04";
                    break;
            }

            return _bmwFolderPath;
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

    private bool _isDevMode = false;
    public bool DevMode
    {
        get { return _isDevMode; }
        set { _isDevMode = value; }
    }

    private bool _isGameMode = false;
    public bool GameMode
    {
        get { return _isGameMode; }
        set { _isGameMode = value; }
    }

    private bool _isAutoMode = false;
    public bool AutoMode
    {
        get { return _isAutoMode; }
        set { _isAutoMode = value; }
    }

    private bool _isShowDodge = false;
    public bool ShowDodge
    {
        get { return _isShowDodge; }
        set { _isShowDodge = value; }
    }

    private int _savePoint = 0;
    public int SavePoint
    {
        get => _savePoint;
        set => _savePoint = value;
    }

    private float _savePointAngle = 0;
    public float SavePointAngle
    {
        get => _savePointAngle;
        set => _savePointAngle = value;
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

    private int _playerDeadCount = 0;
    public int PlayerDeadCount
    {
        get { return _playerDeadCount; }
        set { _playerDeadCount = value; }
    }
    #endregion
}