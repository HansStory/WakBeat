using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

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

    [SerializeField] private RectTransform panelStage;
    float smallSize = 0.8f;

    void Start()
    {
        Init();
    }

    private void Init()
    {
        if (StageIndex != GlobalState.Instance.StageIndex)
        {
            stageLevel.gameObject.SetActive(false);
            panelStage.localScale = Vector3.one * smallSize;
        }
    }


    void Update()
    {
        InputExecute();
    }

    public void InputExecute()
    {
        if (GlobalState.Instance.UserData.data.BackgroundProcActive)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                ShowMyIndex();
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                ShowMyIndex();
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                //SelectAlbum();
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                //ExitAlbumSelect();
            }
        }
    }

    float _duration = 0.5f;
    void ShowMyIndex()
    {
        if (GlobalState.Instance.StageIndex == StageIndex)
        {
            stageLevel.gameObject.SetActive(GlobalState.Instance.StageIndex == StageIndex);
            panelStage.DOScale(Vector3.one, _duration);
        }
        else
        {
            stageLevel.gameObject.SetActive(GlobalState.Instance.StageIndex == StageIndex);
            panelStage.DOScale(Vector3.one * smallSize, _duration);
        }
    }
}
