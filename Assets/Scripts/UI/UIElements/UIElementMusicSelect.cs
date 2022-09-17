using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIElementMusicSelect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GlobalState.Instance.StageIndex = 0;
        SoundManager.Instance.TurnOffGameBackground();
        SoundManager.Instance.TurnOnSelectedMusic();
    }
    private void OnEnable()
    {
        GlobalState.Instance.StageIndex = 0;
        SoundManager.Instance.TurnOnSelectedMusic();
    }

    // Update is called once per frame
    void Update()
    {
        SelectMusic();
        OnClickEsc();
        InputExecute();
    }

    public void InputExecute()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (GlobalState.Instance.StageIndex < SoundManager.Instance.selectedAlbumMusicLength)
            {
                GlobalState.Instance.StageIndex++;

                Debug.Log($"Selecte My Stage Index : {GlobalState.Instance.StageIndex}");
                SoundManager.Instance.TurnOnSelectedMusic();
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (0 < GlobalState.Instance.StageIndex)
            {
                GlobalState.Instance.StageIndex--;

                Debug.Log($"Selecte My Stage Index : {GlobalState.Instance.StageIndex}");
                SoundManager.Instance.TurnOnSelectedMusic();
            }
        }
    }

    void SelectMusic()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            UIManager.Instance.GoPanelGamePlay();
        }
    }

    void OnClickEsc()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UIManager.Instance.GoPanelAlbumSelect();
            SoundManager.Instance.ForceAudioStop();
        }
    }
}
