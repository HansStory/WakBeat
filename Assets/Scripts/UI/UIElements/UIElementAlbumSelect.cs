using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIElementAlbumSelect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SelectAlbum();
        OnClickEsc();
    }

    void SelectAlbum()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            UIManager.Instance.GoPanelMusicSelect();
        }
    }

    void OnClickEsc()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UIManager.Instance.GoPanelMain();
        }
    }
}
