using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIObjectStage : MonoBehaviour
{
    [SerializeField] private Image stageThumnail;
    private Sprite _stageThumnail;
    public Sprite StageThumnail
    {
        get { return _stageThumnail; }
        set
        {
            _stageThumnail = value;
            stageThumnail.sprite = _stageThumnail;
        }
    }

    [SerializeField] private Image stageLevel;
    private Sprite _stageLevel;
    public Sprite StageLevel
    {
        get { return _stageLevel; }
        set
        {
            _stageLevel = value;
            stageLevel.sprite = _stageLevel;
        }
    }

    private int _stageIndex = 0;
    public int StageIndex
    {
        get { return _stageIndex; }
        set
        {
            _stageIndex = value;
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
