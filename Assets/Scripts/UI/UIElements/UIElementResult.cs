using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIElementResult : MonoBehaviour
{
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
        if (Input.GetKeyDown(KeyCode.Return))
        {
            UIManager.Instance.GoPanelMusicSelect();
        }
    }
}
