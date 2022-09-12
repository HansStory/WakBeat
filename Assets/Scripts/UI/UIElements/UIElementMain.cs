using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIElementMain : MonoBehaviour
{
    [SerializeField] private RectTransform backGround;
    private float range = 70f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        OnClickEsc();
        BackGroundMove();
    }

    void BackGroundMove()
    {
        backGround.anchoredPosition = Vector3.right * Mathf.Sin(Time.time) * range;
    }

    public void OnClickStartButton()
    {
        UIManager.Instance.GoPanelAlbumSelect();
    }

    public void OnClickEsc()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            AppManager.Instance.Quit();
        }
    }
}
