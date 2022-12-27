using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIObjectStage : MonoBehaviour
{
    public UIElementMusicSelect UIElementMusicSelect { get; set; }

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

    [SerializeField] private Image _demoLockImage;
    [SerializeField] private Image _clearStamp;

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

        DestroyDemoLock();
        ShowClearStamps();
    }

    private void OnEnable()
    {
        //ShowClearStamps();
    }

    void Update()
    {
        InputExecute();
    }

    public void InputExecute()
    {
        if (DataManager.dataBackgroundProcActive)
        {
            if (!GlobalState.Instance.IsTweening)
            {
                InputRightArrow();
                InputLeftArrow();
                InputReturn();
                InputEscape();
            }
        }
    }

    void InputRightArrow()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            ShowMyIndex();
        }
    }

    void InputLeftArrow()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ShowMyIndex();
        }
    }

    void InputReturn()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {

        }
    }

    void InputEscape()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {

        }
    }

    float _duration = 0.5f;
    public void ShowMyIndex()
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

    public void OnClickStageCircle()
    {
        var direction = GlobalState.Instance.StageIndex - StageIndex;

        switch(direction)
        {
            case -1:
                UIElementMusicSelect.InputRightFunction();
                break;
            case 0:
                UIManager.Instance.UIElementFadePanel.MusicToStage();
                ShowMyIndex();
                break;
            case 1:
                UIElementMusicSelect.InputLeftFunction();
                ShowMyIndex();
                break;
        }

        UIElementMusicSelect.ShowSelectedStage();
    }

    public void OnClickDemoStageCircle()
    {
        var direction = GlobalState.Instance.StageIndex - StageIndex;

        switch (direction)
        {
            case -1:
                UIElementMusicSelect.InputRightFunction();
                break;
            case 1:
                UIElementMusicSelect.InputLeftFunction();
                ShowMyIndex();
                break;
        }

        UIElementMusicSelect.ShowSelectedStage();
    }

    void DestroyDemoLock()
    {
        var state = GlobalState.Instance;

        switch (state.AlbumIndex)
        {
            case (int)GlobalData.ALBUM.ISEDOL:
                switch (StageIndex)
                {
                    case (int)GlobalData.STAGE.STAGE1:
                        Destroy(_demoLockImage.gameObject);
                        break;
                    case (int)GlobalData.STAGE.STAGE2:
                        Destroy(_demoLockImage.gameObject);
                        break;
                }
                break;
            case (int)GlobalData.ALBUM.CONTEST:
                switch (StageIndex)
                {
                    case (int)GlobalData.STAGE.STAGE1:
                        Destroy(_demoLockImage.gameObject);
                        break;
                    case (int)GlobalData.STAGE.STAGE2:
                        if(state.DevMode) Destroy(_demoLockImage.gameObject);
                        break;
                    case (int)GlobalData.STAGE.STAGE3:
                        if (state.DevMode) Destroy(_demoLockImage.gameObject);
                        break;
                    case (int)GlobalData.STAGE.STAGE4:
                        if(state.DevMode) Destroy(_demoLockImage.gameObject);
                        break;
                }
                break;
            case (int)GlobalData.ALBUM.GOMIX:
                switch (StageIndex)
                {
                    case (int)GlobalData.STAGE.STAGE1:
                        Destroy(_demoLockImage.gameObject);
                        break;
                    case (int)GlobalData.STAGE.STAGE2:
                        if(state.DevMode) Destroy(_demoLockImage.gameObject);
                        break;
                    case (int)GlobalData.STAGE.STAGE3:
                        if(state.DevMode) Destroy(_demoLockImage.gameObject);
                        break;
                    case (int)GlobalData.STAGE.STAGE4:
                        if(state.DevMode) Destroy(_demoLockImage.gameObject);
                        break;
                    case (int)GlobalData.STAGE.STAGE5:
                        if(state.DevMode) Destroy(_demoLockImage.gameObject);
                        break;
                }
                break;
            case (int)GlobalData.ALBUM.WAKALOID:
                switch (StageIndex)
                {
                    case (int)GlobalData.STAGE.STAGE1:
                        if(state.DevMode) Destroy(_demoLockImage.gameObject);
                        break;
                    case (int)GlobalData.STAGE.STAGE2:
                        Destroy(_demoLockImage.gameObject);
                        break;
                    case (int)GlobalData.STAGE.STAGE3:
                        if(state.DevMode) Destroy(_demoLockImage.gameObject);
                        break;
                    case (int)GlobalData.STAGE.STAGE4:
                        if(state.DevMode) Destroy(_demoLockImage.gameObject);
                        break;
                }
                break;
        }
    }

    void ShowClearStamps()
    {
        switch (GlobalState.Instance.AlbumIndex)
        {
            case (int)GlobalData.ALBUM.ISEDOL:
                switch (StageIndex)
                {
                    case (int)GlobalData.STAGE.STAGE1:
                        CheckDataClear(DataManager.dataAlbum1ClearYn, 0);
                        break;
                    case (int)GlobalData.STAGE.STAGE2:
                        CheckDataClear(DataManager.dataAlbum1ClearYn, 1);
                        break;
                }
                break;
            case (int)GlobalData.ALBUM.CONTEST:
                switch (StageIndex)
                {
                    case (int)GlobalData.STAGE.STAGE1:
                        CheckDataClear(DataManager.dataAlbum2ClearYn, 0);
                        break;
                    case (int)GlobalData.STAGE.STAGE2:
                        CheckDataClear(DataManager.dataAlbum2ClearYn, 1);
                        break;
                    case (int)GlobalData.STAGE.STAGE3:
                        CheckDataClear(DataManager.dataAlbum2ClearYn, 2);
                        break;
                    case (int)GlobalData.STAGE.STAGE4:
                        CheckDataClear(DataManager.dataAlbum2ClearYn, 3);
                        break;
                }
                break;
            case (int)GlobalData.ALBUM.GOMIX:
                switch (StageIndex)
                {
                    case (int)GlobalData.STAGE.STAGE1:
                        CheckDataClear(DataManager.dataAlbum3ClearYn, 0);
                        break;
                    case (int)GlobalData.STAGE.STAGE2:
                        CheckDataClear(DataManager.dataAlbum3ClearYn, 1);
                        break;
                    case (int)GlobalData.STAGE.STAGE3:
                        CheckDataClear(DataManager.dataAlbum3ClearYn, 2);
                        break;
                    case (int)GlobalData.STAGE.STAGE4:
                        CheckDataClear(DataManager.dataAlbum3ClearYn, 3);
                        break;
                    case (int)GlobalData.STAGE.STAGE5:
                        CheckDataClear(DataManager.dataAlbum3ClearYn, 4);
                        break;
                }
                break;
            case (int)GlobalData.ALBUM.WAKALOID:
                switch (StageIndex)
                {
                    case (int)GlobalData.STAGE.STAGE1:
                        CheckDataClear(DataManager.dataAlbum4ClearYn, 0);
                        break;
                    case (int)GlobalData.STAGE.STAGE2:
                        CheckDataClear(DataManager.dataAlbum4ClearYn, 1);
                        break;
                    case (int)GlobalData.STAGE.STAGE3:
                        CheckDataClear(DataManager.dataAlbum4ClearYn, 2);
                        break;
                    case (int)GlobalData.STAGE.STAGE4:
                        CheckDataClear(DataManager.dataAlbum4ClearYn, 3);
                        break;
                }
                break;
        }
    }

    void CheckDataClear(string[] clearData, int stage)
    {
        if (clearData[stage] == "Y")
        {
            _clearStamp.gameObject.SetActive(true);
        }
    }
}
