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
}