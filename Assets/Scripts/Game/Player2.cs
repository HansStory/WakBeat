using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : MonoBehaviour
{
    private Rigidbody2D _myRigid;

    // Start is called before the first frame update
    void Start()
    {
        if (this.GetComponent<Rigidbody2D>() != null)
        {
            _myRigid = this.GetComponent<Rigidbody2D>();

            _myRigid.simulated = Config.Instance.GameMode;
        }

        var parent = this.transform.parent;
        Debug.Log(parent.name);
    }

    void inputTest()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            var parent = this.transform.parent.parent;
            Debug.Log(parent.name);
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("SavePoint"))
        {
            Debug.Log("세이브 포인트!!");
            Destroy(col.gameObject);
            //_stageBase.SavePointEnter();
        }

        if (col.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Heat Obstacle");
            //PlayerDie();
        }

    }

    // Update is called once per frame
    void Update()
    {
        inputTest();
    }
}
