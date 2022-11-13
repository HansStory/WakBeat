
using UnityEngine;

public class StageBase : Stage
{
    protected override void Init()
    {
        base.Init();
    }
    
    public void SavePointEnter()
    {
        GlobalState.Instance.SavePoint = currentItem;
        GlobalState.Instance.SaveMusicPlayingTime = SoundManager.Instance.MusicAudio.time;
        
    }

    public void PlayerDieAndSavePointPlay()
    {
        SoundManager.Instance.MusicAudio.Pause();
        SoundManager.Instance.MusicAudio.time = GlobalState.Instance.SaveMusicPlayingTime;
        SoundManager.Instance.MusicAudio.Play();
        Timer = 0;
        currentItem = GlobalState.Instance.SavePoint;
    }
}
