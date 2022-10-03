using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIElementMusicSelect : MonoBehaviour
{
    [SerializeField] private GameObject stage;
    [SerializeField] private Transform stageBase;

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
    }


    void test()
    {

    }

    void makeAlbum1Stages()
    {
        var albumCircles = GlobalData.Instance.Album.AlbumCircles;
        var albumTitles = GlobalData.Instance.Album.AlbumTitles;

        int _albumIndex = 0;
        foreach (var obj in albumCircles)
        {
            var _album = GameObject.Instantiate(stage, stageBase);
            var albumInfo = _album.GetComponent<UIObjectAlbum>();

            if (albumInfo)
            {
                // null check
                if (albumCircles.Length == albumTitles.Length)
                {
                    albumInfo.name = $"Album_{_albumIndex}";
                    albumInfo.AlbumCircle = albumCircles[_albumIndex];
                    albumInfo.AlbumTitle = albumTitles[_albumIndex];
                    albumInfo.AlbumIndex = _albumIndex;
                    albumInfo.InitMyIndexPos();
                }
                else
                {
                    Debug.LogError("리소스가 빠져있습니다.");
                }
            }

            _albumIndex++;
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
                ChangeBackGroound();

                Debug.Log($"Selecte My Stage Index : {GlobalState.Instance.StageIndex}");
                SoundManager.Instance.TurnOnSelectedMusic();
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (0 < GlobalState.Instance.StageIndex)
            {
                GlobalState.Instance.StageIndex--;
                ChangeBackGroound(); 

                Debug.Log($"Selecte My Stage Index : {GlobalState.Instance.StageIndex}");
                SoundManager.Instance.TurnOnSelectedMusic();
            }
        }
    }

    void ChangeBackGroound()
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
