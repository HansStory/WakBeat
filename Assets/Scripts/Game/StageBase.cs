
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
        GlobalState.Instance.SavePoint = _currentLine;
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
            _currentLine = GlobalState.Instance.SavePoint -1;    
        }
        else
        {
            _currentLine = GlobalState.Instance.SavePoint;
        }
    }
    
}
