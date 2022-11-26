using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SoundManager : MonoBehaviourSingleton<SoundManager>
{
    #region Events
    public delegate void AudioChangeHandler(Sprite songTitle);
    #endregion
    public event AudioChangeHandler OnChanged;

    [Header("[ Sound Manager Audio Source ]")]
    [SerializeField] private AudioSource SFXAudioSource;
    [SerializeField] private AudioSource MusicAudioSource;

    [Space(10)]
    [SerializeField] private AudioClip[] soundFXClip;

    [SerializeField] private AudioClip[] backgroundClips;

    [SerializeField] private Sprite[] bgmTextSprites;

    [SerializeField] private AudioClip[] album1HighLightClips;
    [SerializeField] private AudioClip[] album2HighLightClips;
    [SerializeField] private AudioClip[] album3HighLightClips;
    [SerializeField] private AudioClip[] album4HighLightClips;

    [SerializeField] private AudioClip[] album1StageClips;
    [SerializeField] private AudioClip[] album2StageClips;
    [SerializeField] private AudioClip[] album3StageClips;
    [SerializeField] private AudioClip[] album4StageClips;


    private int backgroundMusicIndex;
    public int BackgroundMusicIndex
    {
        get => backgroundMusicIndex;
        private set
        {
            backgroundMusicIndex = MyUtil.RepeatIndex(value, backgroundClips.Length);
        }
    }

    private bool isPlayBackground = false;
    public string CurrentSongTitle => MusicAudioSource.clip.name;
    public Sprite CurrentSongSprite => bgmTextSprites[GlobalState.Instance.BGMIndex];

    public AudioSource MusicAudio => MusicAudioSource;

    private void Awake()
    {
        Init();
    }

    private void  Init()
    {
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        CtrlSFXVolume (GlobalState.Instance.SFXVolume);
        CtrlBGMVolume(GlobalState.Instance.MusicVolume);
    }

    public bool isBackGroundPlay()
    {
        if (!MusicAudioSource.isPlaying && isPlayBackground)
        {

        }
        return true;
    }

    private void Update()
    {
        if (!MusicAudioSource.isPlaying && isPlayBackground)
        {
            MusicAudioSource.volume = GlobalState.Instance.MusicVolume;

            MusicAudioSource.clip = backgroundClips[BackgroundMusicIndex];
            MusicAudioSource.Play();
            GlobalState.Instance.BGMIndex = BackgroundMusicIndex;

            OnChanged?.Invoke(bgmTextSprites[GlobalState.Instance.BGMIndex]);

            BackgroundMusicIndex++;
        }
    }

    public void PlaySoundFX(int index)
    {
        SFXAudioSource.Stop();
        SFXAudioSource.PlayOneShot(soundFXClip[index]);
    }

    public void CtrlSFXVolume(float volume)
    {
        GlobalState.Instance.SFXVolume = volume;
        SFXAudioSource.volume = volume;
    }

    public void CtrlBGMVolume(float volume)
    {
        GlobalState.Instance.MusicVolume = volume;
        MusicAudioSource.volume = volume;
    }

    public int selectedAlbumMusicLength = 0;
    public void TurnOnSelectedMusic()
    {
        switch (GlobalState.Instance.AlbumIndex)
        {
            case (int)GlobalData.ALBUM.ISEDOL:
                selectedAlbumMusicLength = album1HighLightClips.Length - 1;
                MusicAudioSource.clip = album1HighLightClips[GlobalState.Instance.StageIndex];
                MusicAudioSource.Play();
                MusicAudioSource.loop = true;
                break;
            case (int)GlobalData.ALBUM.CONTEST:
                selectedAlbumMusicLength = album2HighLightClips.Length - 1;
                MusicAudioSource.clip = album2HighLightClips[GlobalState.Instance.StageIndex];
                MusicAudioSource.Play();
                MusicAudioSource.loop = true;
                break;
            case (int)GlobalData.ALBUM.GOMIX:
                selectedAlbumMusicLength = album3HighLightClips.Length - 1;
                MusicAudioSource.clip = album3HighLightClips[GlobalState.Instance.StageIndex];
                MusicAudioSource.Play();
                MusicAudioSource.loop = true;
                break;
            case (int)GlobalData.ALBUM.WAKALOID:
                selectedAlbumMusicLength = album4HighLightClips.Length - 1;
                MusicAudioSource.clip = album4HighLightClips[GlobalState.Instance.StageIndex];
                MusicAudioSource.Play();
                MusicAudioSource.loop = true;
                break;
        }
    }

    public void TurnOnStageMusic()
    {
        SetStageMusic();

        MusicAudioSource.Play();
        MusicAudioSource.loop = false;
    }

    public void SetStageMusic()
    {
        switch (GlobalState.Instance.AlbumIndex)
        {
            case (int)GlobalData.ALBUM.ISEDOL:
                MusicAudioSource.clip = album1StageClips[GlobalState.Instance.StageIndex];
                break;
            case (int)GlobalData.ALBUM.CONTEST:
                MusicAudioSource.clip = album2StageClips[GlobalState.Instance.StageIndex];
                break;
            case (int)GlobalData.ALBUM.GOMIX:
                MusicAudioSource.clip = album3StageClips[GlobalState.Instance.StageIndex];
                break;
            case (int)GlobalData.ALBUM.WAKALOID:
                MusicAudioSource.clip = album4StageClips[GlobalState.Instance.StageIndex];
                break;
        }
    }

    private float _audioClipLength = 0f;
    public float AudioClipLength
    {
        get { return _audioClipLength; }
        set
        {
            _audioClipLength = value;
        }
    }

    private float _musicPlayTime = 0f;
    public float MusicPlayTime
    {
        get { return _musicPlayTime; }
        set
        {
            _musicPlayTime = value;
        }
    }

    public void GetAudioClipInfo()
    {
        // 음원의 총 길이 (초)
        AudioClipLength = MusicAudioSource.clip.length;

        //현재 진행중인 음악의 시간
        MusicPlayTime = MusicAudioSource.time;
    }

    public void TurnOnGameBackGround()
    {
        BackgroundMusicIndex = 0;
        isPlayBackground = true;
        MusicAudioSource.loop = false;
    }

    public void TurnOffGameBackground()
    {
        isPlayBackground = false;
        MusicAudioSource.Stop();
        MusicAudioSource.loop = true;
    }

    public void FadeOutMusicVolume(float duration)
    {
        MusicAudioSource.volume = GlobalState.Instance.MusicVolume;
        MusicAudioSource.DOFade(0f, duration);
    }

    public void FadeInMusicVolume(float duration)
    {
        MusicAudioSource.volume = 0f;
        MusicAudioSource.DOFade(GlobalState.Instance.MusicVolume, duration);
    }

    public void ForceAudioStop()
    {
        MusicAudioSource.Stop();
    }

    public void ForceAudioPlay()
    {

    }
}
