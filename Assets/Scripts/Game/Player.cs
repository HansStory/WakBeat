using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private StageBase _stageBase;

    private GameObject _objCenter;
    
    private float ballRadius = 0;
    private float outRadius = 355f;
    private float inRadius = 312f;

    private CountDownStage _countDownStage;
    // Start is called before the first frame update
    private void Awake()
    {
        _stageBase = new StageBase();
        _objCenter = GameObject.Find("Center");
        _countDownStage = new CountDownStage();
    }

    void Start()
    {
        ballRadius = outRadius;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GlobalState.Instance.IsPlayerDied)
        {
            OperateBallMovement();    
        }
        else
        {
            this.transform.localPosition = Vector3.zero;
        }
        
    }
    
    void OperateBallMovement()
    {
        this.transform.localPosition = _objCenter.transform.localPosition + _objCenter.transform.up * ballRadius;
        ChangeDirection();
    }

    private bool isUpState = true;
    void ChangeDirection()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isUpState = !isUpState;

            if (isUpState)
            {
                ballRadius = outRadius;
            }
            else
            {
                ballRadius = inRadius;
            }
        }
    }
    
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("SavePoint"))
        {
            Debug.Log("세이브 포인트!!");
            Destroy(col.gameObject);
            _stageBase.SavePointEnter();
        }

        if (col.gameObject.CompareTag("Obstacle"))
        {
            PlayerDie();
        }
        
    }

    void PlayerDie()
    {
        _stageBase.PlayerDieAndSavePointPlay();
    }
    
}
