
using UnityEngine;

public class StageBase : Stage
{
    private GameObject _obj;
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
        Timer = 0;
        currentItem = GlobalState.Instance.SavePoint;
        
        // Hide
        for (int i = 0; i < DodgePointList.Count; i++)
        {
            DodgePointList[i].SetActive(false);
        }
        
        for (int i = 0; i < InObstacleList.Count; i++)
        {
            InObstacleList[i].SetActive(false);
        }
        
        for (int i = 0; i < OutObstacleList.Count; i++)
        {
            OutObstacleList[i].SetActive(false);
        }
        
        SoundManager.Instance.MusicAudio.Play();
    }
    
}
