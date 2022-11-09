using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChartingTest : MonoBehaviour
{
    [SerializeField] private GameObject center;
    [SerializeField] private GameObject obstacle;
    [SerializeField] private Transform obstacleInBase;
    [SerializeField] private Transform obstacleOutBase;

    [SerializeField] private GameObject Ball;
    public float speed = 1;
    public float nutBackAmount = 10f;
    private static float OutRadius = 355f;
    private static float InRadius = 312f;
    private float jumpAmount = 30f;

    private static float BallRadius = 0;
    private bool isUpState = false;

    // Start is called before the first frame update
    void Start()
    {
        CreateInObstacle();
        CreateOutObstacle();
        BallRadius = OutRadius;
        isUpState = true;
    }


    void CreateInObstacle()
    {
        for (int i = 0; i < 72; i++)
        {
            GameObject obj = GameObject.Instantiate(obstacle, obstacleInBase);

            if (obj)
            {
                obj.transform.localPosition = center.transform.localPosition + center.transform.up * InRadius;
                obj.transform.localEulerAngles = center.transform.localEulerAngles + new Vector3(0f, 0f, 180f);
                obj.SetActive(false);

                center.transform.Rotate(0f, 0f, -5f);
            }
        }
    }

    void CreateOutObstacle()
    {
        center.transform.localEulerAngles = Vector3.zero;

        for (int i = 0; i < 72; i++)
        {
            GameObject obj = GameObject.Instantiate(obstacle, obstacleOutBase);

            if (obj)
            {
                obj.transform.localPosition = center.transform.localPosition + center.transform.up * OutRadius;
                obj.transform.localEulerAngles = center.transform.localEulerAngles;
                obj.SetActive(false);

                center.transform.Rotate(0f, 0f, -5f);
            }
        }
    }

    public void OnClickBackCircle()
    {
        center.transform.Rotate(0f, 0f, center.transform.localRotation.z + nutBackAmount);
    }

    private bool isJump = false;
    public void OnClickBallJump()
    {
        isJump = !isJump;

        if (isJump)
        {
            if (isUpState)
            {
                BallRadius = OutRadius + jumpAmount;
            }
            else
            {
                BallRadius = InRadius - jumpAmount;
            }
        }
        else
        {
            if (isUpState)
            {
                BallRadius = OutRadius;
            }
            else
            {
                BallRadius = InRadius;
            }
        }

    }


    // Update is called once per frame
    void Update()
    {
        center.transform.Rotate(0f, 0f, -Time.deltaTime * speed);
        OperateBallMovement();
    }

    void OperateBallMovement()
    {
        Ball.transform.localPosition = center.transform.localPosition + center.transform.up * BallRadius;
        ChangeDirection();
    }

    void ChangeDirection()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isUpState = !isUpState;

            if (isUpState)
            {
                BallRadius = OutRadius;
            }
            else
            {
                BallRadius = InRadius;
            }
        }
    }
}
