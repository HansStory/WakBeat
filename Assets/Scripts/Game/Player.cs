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
    // Start is called before the first frame update
    private void Awake()
    {
        _stageBase = new StageBase();
        _objCenter = GameObject.Find("Center");
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
            Debug.Log("죽었음");
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

    // private void OnTriggerEnter(Collider other)
    // {
    //     if (other.tag == "SavePoint")
    //     {
    //         Debug.Log("Hit");
    //         Destroy(other.gameObject);
    //     }
    // }

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
        GlobalState.Instance.IsPlayerDied = true;
        Debug.Log("Player Die!!");
        _stageBase.PlayerDieAndSavePointPlay();
        _objCenter.transform.eulerAngles = new Vector3(0,0,0);
        transform.eulerAngles = new Vector3(0, 0, 0);
        GlobalState.Instance.IsPlayerDied = false;
    }
    
}
