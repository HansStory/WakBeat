using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIElementResult : MonoBehaviour
{
    private string url;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        OnClickCheckButton();
    }

    void OnClickCheckButton()
    {
         if (GlobalState.Instance.UserData.data.BackgroundProcActive)
         {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                UIManager.Instance.GoPanelMusicSelect();
            }
        }
    }

    public void OpenWakZooURL()
    {
        url = Config.Instance.WakZoo;
        Application.OpenURL(url);
    }

    public void OpenOriginURL()
    {
        url = Config.Instance.Origin_Rewind;
        Application.OpenURL(url);
    }

    public void OpenRemixURL()
    {
        url = Config.Instance.ReMix_Rewind;
        Application.OpenURL(url);
    }
}
