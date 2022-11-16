
using UnityEngine;

public class StageBase : Stage
{
    private GameObject _obj;

    private CountDownStage _countDownStage;
    
    protected override void Init()
    {
        base.Init();
        _countDownStage = GameObject.Find("Text_CountDown").GetComponent<CountDownStage>();
    }

    public void SavePointEnter()
    {
        GlobalState.Instance.SavePoint = currentItem;
        GlobalState.Instance.SaveMusicPlayingTime = SoundManager.Instance.MusicAudio.time;
        
    }

    public void PlayerDieAndSavePointPlay()
    {
        Debug.Log("Player Die!!");
        GlobalState.Instance.IsPlayerDied = true;
        SoundManager.Instance.MusicAudio.Pause();
        SoundManager.Instance.MusicAudio.time = GlobalState.Instance.SaveMusicPlayingTime;
        if (GlobalState.Instance.SavePoint != 0)
        {
            currentItem = GlobalState.Instance.SavePoint -1;    
        }
        else
        {
            currentItem = GlobalState.Instance.SavePoint;
        }
    }
    
}
