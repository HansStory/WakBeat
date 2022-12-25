using System;
using System.Collections.Generic;
using UnityEngine;

public class GlobalState : MonoBehaviourSingleton<GlobalState>
{
    private int _currentPanelIndex = 0;
    public int CurrentPanelIndex
    {
        get { return _currentPanelIndex; }
        set { _currentPanelIndex = value; }
    }

    private int _albumIndex = 0;
    public int AlbumIndex
    {
        get { return _albumIndex; }
        set { _albumIndex = value; }
    }

    public void SetMyAlbumIndex(int index)
    {
        _albumIndex = index;
    }

    private int _stageIndex = 0;
    public int StageIndex
    {
        get { return _stageIndex; }
        set { _stageIndex = value; }
    }

    public void SetMyStageIndex(int index)
    {
        _stageIndex = index;
    }

    //public List<int> AlbumStageCount = new List<int>();

    // 앨범의 스테이지 수 체크
    private int _albumStageCount = 0;
    public int AlbumStageCount
    {
        get { return _albumStageCount; }
        set { _albumStageCount = value; }
    }

    private int _album1StageCount = 0;
    public int Album1StageCount
    {
        get { return _album1StageCount; }
        set { _album1StageCount = value; }
    }

    private int _album2StageCount = 0;
    public int Album2StageCount
    {
        get { return _album2StageCount; }
        set { _album2StageCount = value; }
    }

    private int _album3StageCount = 0;
    public int Album3StageCount
    {
        get { return _album3StageCount; }
        set { _album3StageCount = value; }
    }

    private int _album4StageCount = 0;
    public int Album4StageCount
    {
        get { return _album4StageCount; }
        set { _album4StageCount = value; }
    }

    private int _album5StageCount = 0;
    public int Album5StageCount
    {
        get { return _album5StageCount; }
        set { _album5StageCount = value; }
    }

    // Tweening 상태 체크 (여러 입력을 방지하기 위해서)
    private bool isTweening = false;
    public bool IsTweening
    {
        get { return isTweening; }
        set { isTweening = value; }
    }

    //--------------------------- Global Sound State ---------------------------------
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

    // 효과음 볼륨
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

    // 음악 볼륨
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
        //AutoMode = Config.Instance.AutoMode;      
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

    // -------------------------- Skill ------------------------------------
    private bool _useBonusHP = false;
    public bool UseBonusHP
    {
        get { return _useBonusHP; }
        set { _useBonusHP = value; }
    }

    private bool _useBarrier = false;
    public bool UseBarrier
    {
        get { return _useBarrier; }
        set { _useBarrier = value; }
    }

    private bool _useNewGaMe = false;
    public bool UseNewGaMe
    {
        get { return _useNewGaMe; }
        set { _useNewGaMe = value; }
    }

    // Dodge Point만 보이고 충돌처리 X (아이템 사용시(객관안) 해당 변수로 사용) [완료]
    private bool _isShowDodge = false;
    public bool ShowDodge
    {
        get { return _isShowDodge; }
        set { _isShowDodge = value; }
    }

    // TO DO : AutoMode(자율주행 아이템) 일시에 Dodge 포인트도 보이고 충돌처리도 해야함 [완료]
    // TO DO : Dodge포이는튼 안보이고 충돌처리만 해야함
    private bool _isAutoMode = false;
    public bool AutoMode
    {
        get { return _isAutoMode; }
        set { _isAutoMode = value; }
    }

    // ------------------------- Save Point -----------------------------------
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

    // Player 사망 체크
    private bool _isPlayerDied = false;

    public bool IsPlayerDied
    {
        get => _isPlayerDied;
        set => _isPlayerDied = value;
    }

    //---------------------- Game Result Variable -----------------
    private int _stageMusicLength = 1;
    public int StageMusicLength
    {
        get { return _stageMusicLength; }
        set { _stageMusicLength = value; }
    }

    // Stage의 총 Play Time
    private int _stagePlayTime = 1;
    public int StagePlayTime
    {
        get { return _stagePlayTime; }
        set { _stagePlayTime = value; }
    }

    // Player 사망 횟수
    private int _playerDeadCount = 0;
    public int PlayerDeadCount
    {
        get { return _playerDeadCount; }
        set { _playerDeadCount = value; }
    }

    // 사용한 아이템 종류
    private string _usedItems = "없음";
    public string UsedItems
    {
        get { return _usedItems; }
        set { _usedItems = value; }
    }
    #endregion
}