using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIElementMusicSelect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SelectMusic();
        OnClickEsc();
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
        }
    }
}
