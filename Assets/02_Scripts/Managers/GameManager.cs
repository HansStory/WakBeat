using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    //private CircleCtrl m_circleCtrl = null;
    //private BackgroundCtrl m_backgroundCtrl = null;

    //IEnumerator m_enterAnimCoroutine = null;

    protected override void Init()
    {
        SetApplicationOptions();
    }

    private void SetApplicationOptions()
    {
        Application.targetFrameRate = 90;
    }

    //private void Start()
    //{
    //    m_circleCtrl.gameObject.SetActive(false);
    //}

    //private void Update()
    //{
    //    //if(UIManager.Instance.Raycaster.enabled)
    //    //{
    //    //    m_circleCtrl.ReverseBall();
    //    //}
    //}

    //public void PlayGame()
    //{
    //    m_backgroundCtrl.gameObject.SetActive(false);
    //    m_circleCtrl.gameObject.SetActive(true);
    //    m_circleCtrl.PlayEnterAnim(UIManager.Instance.Raycaster);
    //}
}
