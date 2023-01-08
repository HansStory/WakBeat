using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIElementGamePlay : MonoBehaviour
{
    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        GameFactory.Instance.CreateStage();
    }


    // Update is called once per frame
    void Update()
    {
        if (DataManager.dataBackgroundProcActive)
        {
            if (!GlobalState.Instance.IsTweening)
            {
                if (GlobalState.Instance.DevMode)
                {
                    FinishGame();
                    OnClickEsc();
                }
            }
        }
    }

    private void OnEnable()
    {

    }

    private void OnDisable()
    {
        GameFactory.Instance.DistroyStage();
    }

    void FinishGame()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Stage.Instance.FinishGame();
        }
    }

    void OnClickEsc()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Stage.Instance.GoBackSelectStage();
        }
    }
}
