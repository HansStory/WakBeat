using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIElementMain : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        OnClickEsc();
    }

    public void OnClickStartButton()
    {
        UIManager.Instance.GoPanelAlbumSelect();
    }

    void OnClickEsc()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            AppManager.Instance.Quit();
        }
    }
}
