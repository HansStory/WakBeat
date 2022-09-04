using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    #region Events
    public delegate void AudioChangeHandler(string songTitle);
    #endregion
    public event AudioChangeHandler OnChanged;

    private AudioSource m_cSoundFx;
    private AudioSource m_cMusic;

    [SerializeField]
    private AudioClip[] m_backgroundClips;
    [SerializeField]
    private AudioClip m_soundFXClip;

    private int m_backgroundIndex;
    public int BackgroundIndex
    {
        get => m_backgroundIndex;
        private set
        {
            m_backgroundIndex = MyUtil.RepeatIndex(value, m_backgroundClips.Length);
        }
    }

    private bool m_playBackground = false;
    public string CurrentSongTitle => m_cMusic.clip.name;

    public AudioSource MusicAudio => m_cMusic;

    protected override void Init()
    {
        m_cSoundFx = transform.GetChild(0).GetComponent<AudioSource>();
        m_cMusic = transform.GetChild(1).GetComponent<AudioSource>();
    }

    private void Start()
    {
        CtrlSFXVolume(DataManager.Instance.SoundFX);
        CtrlBGMVolume(DataManager.Instance.Music);
    }

    private void Update()
    {
        if(!m_cMusic.isPlaying && m_playBackground)
        {
            m_cMusic.clip = m_backgroundClips[BackgroundIndex];
            BackgroundIndex++;
            m_cMusic.Play();
            OnChanged?.Invoke(m_cMusic.clip.name);
        }
    }

    public void PlaySoundFX()
    {
        m_cSoundFx.Stop();
        m_cSoundFx.PlayOneShot(m_soundFXClip);
    }

    public void CtrlSFXVolume(float volume)
    {
        DataManager.Instance.SoundFX = volume;
        m_cSoundFx.volume = volume;
    }

    public void CtrlBGMVolume(float volume)
    {
        DataManager.Instance.Music = volume;
        m_cMusic.volume = volume;
    }

    public void TurnOnGameBackGround()
    {
        BackgroundIndex = 0;
        m_playBackground = true;
        m_cMusic.loop = false;
    }

    public void TurnOffGameBackground()
    {
        m_playBackground = false;
        m_cMusic.Stop();
        m_cMusic.loop = true;
    }
}
