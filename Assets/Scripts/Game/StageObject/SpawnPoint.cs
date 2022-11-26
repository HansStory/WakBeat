using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private TMP_Text textIndex;

    private string _index = "0";
    public string Index
    {
        get { return _index; }
        set 
        { 
            _index = value;
            textIndex.text = _index;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
