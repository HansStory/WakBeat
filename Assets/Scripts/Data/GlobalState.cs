using UnityEngine;

public class GlobalState : MonoBehaviourSingleton<GlobalState>
{
    private int _albumIndex = (int)GlobalData.ALBUM.ISEDOL;
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
    public float SFXVolume
    {
        get { return _sfxVolume; }
        set
        {
            value = Mathf.Clamp01(value);
            _sfxVolume = value;
        }
    }

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
}