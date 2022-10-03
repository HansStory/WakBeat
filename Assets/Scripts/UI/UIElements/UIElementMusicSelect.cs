using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIElementMusicSelect : MonoBehaviour
{
    [SerializeField] private GameObject stage;
    [SerializeField] private Transform stageBase;

    [SerializeField] private Image stageText;
    [SerializeField] private Image backGround;

    // Start is called before the first frame update
    void Start()
    {
        GlobalState.Instance.StageIndex = 0;
        SoundManager.Instance.TurnOffGameBackground();
        SoundManager.Instance.TurnOnSelectedMusic();
    }
    private void OnEnable()
    {
        GlobalState.Instance.StageIndex = 0;
        SoundManager.Instance.TurnOnSelectedMusic();
        MakeAlbumStage();
        ChangeBackGround();
    }

    private void OnDisable()
    {
        DestroyAlbumStage();
    }

    void MakeAlbumStage()
    {
        switch (GlobalState.Instance.AlbumIndex)
        {
            case (int)GlobalData.ALBUM.ISEDOL:
                makeStages(GlobalData.Instance.Album.FirstAlbumMusicCircle, GlobalData.Instance.Album.FirstAlbumMusicLevel);
                break;
            case (int)GlobalData.ALBUM.CONTEST:
                makeStages(GlobalData.Instance.Album.SecondAlbumMusicCircle, GlobalData.Instance.Album.SecondAlbumMusicLevel);
                break;
            case (int)GlobalData.ALBUM.GOMIX:
                makeStages(GlobalData.Instance.Album.ThirdAlbumMusicCircle, GlobalData.Instance.Album.ThirdAlbumMusicLevel);
                break;
            case (int)GlobalData.ALBUM.WAKALOID:
                makeStages(GlobalData.Instance.Album.ForthAlbumMusicCircle, GlobalData.Instance.Album.ForthAlbumMusicLevel);
                break;
        }
    }

    void DestroyAlbumStage()
    {
        Transform[] childList = stageBase.gameObject.GetComponentsInChildren<Transform>();
        if (childList != null)
        {
            for (int i = 1; i < childList.Length; i++)
            {
                if (childList[i] != transform)
                {
                    Destroy(childList[i].gameObject);
                }
            }
        }

    }

    void makeStages(Sprite[] _stageCircles, Sprite[] _stageLevel)
    {
        var stageCircles = _stageCircles;
        var stageLevel = _stageLevel;

        int _stageIndex = 0;
        foreach (var stages in stageCircles)
        {
            var _stage = GameObject.Instantiate(stage, stageBase);
            var stageInfo = _stage.GetComponent<UIObjectStage>();

            if (stageInfo)
            {
                // null check
                if (stageCircles.Length == stageLevel.Length)
                {
                    stageInfo.name = $"Album_{_stageIndex}";
                    stageInfo.StageThumnail = stageCircles[_stageIndex];
                    stageInfo.StageLevel = stageLevel[_stageIndex];
                    stageInfo.StageIndex = _stageIndex;
                }
                else
                {
                    Debug.LogError("리소스가 빠져있습니다.");
                }
            }

            _stageIndex++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        SelectMusic();
        OnClickEsc();
        InputExecute();
    }

    public void InputExecute()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (GlobalState.Instance.StageIndex < SoundManager.Instance.selectedAlbumMusicLength)
            {
                GlobalState.Instance.StageIndex++;
                ChangeBackGround();

                Debug.Log($"Selecte My Stage Index : {GlobalState.Instance.StageIndex}");
                SoundManager.Instance.TurnOnSelectedMusic();
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (0 < GlobalState.Instance.StageIndex)
            {
                GlobalState.Instance.StageIndex--;
                ChangeBackGround(); 

                Debug.Log($"Selecte My Stage Index : {GlobalState.Instance.StageIndex}");
                SoundManager.Instance.TurnOnSelectedMusic();
            }
        }
    }

    void ChangeBackGround()
    {
        switch (GlobalState.Instance.AlbumIndex)
        {
            case (int)GlobalData.ALBUM.ISEDOL:
                backGround.sprite = GlobalData.Instance.Album.FirstAlbumMusicBackground[GlobalState.Instance.StageIndex];
                break;
            case (int)GlobalData.ALBUM.CONTEST:
                backGround.sprite = GlobalData.Instance.Album.SecondAlbumMusicBackground[GlobalState.Instance.StageIndex];
                break;
            case (int)GlobalData.ALBUM.GOMIX:
                backGround.sprite = GlobalData.Instance.Album.ThirdAlbumMusicBackground[GlobalState.Instance.StageIndex];
                break;
            case (int)GlobalData.ALBUM.WAKALOID:
                backGround.sprite = GlobalData.Instance.Album.ForthAlbumMusicBackground[GlobalState.Instance.StageIndex];
                break;
        }

        // stage Text 변경 시켜주는곳
        stageText.sprite = GlobalData.Instance.Album.StageIcons[GlobalState.Instance.StageIndex];
    }

    void SelectMusic()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            UIManager.Instance.GoPanelGamePlay();
        }
    }

    void OnClickEsc()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UIManager.Instance.GoPanelAlbumSelect();
            SoundManager.Instance.ForceAudioStop();
        }
    }
}
