using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIElementGamePlay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FinishGame();
        OnClickEsc();
    }

    void FinishGame()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            UIManager.Instance.GoPanelResult();
        }
    }

    void OnClickEsc()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UIManager.Instance.GoPanelMusicSelect();
        }
    }
}
