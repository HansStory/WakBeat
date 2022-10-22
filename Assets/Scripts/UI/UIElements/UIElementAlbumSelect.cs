using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIElementAlbumSelect : MonoBehaviour
{
    //public Image fadeImage = null;
    private float fadeTime = 0.5f;

    [SerializeField] private GameObject album;
    [SerializeField] private Transform albumBase;

    [SerializeField] private Image imageBackGround;

    [SerializeField] private GameObject albumInfo;
    [SerializeField] private Transform albumInfoBase;

    // Start is called before the first frame update
    private void OnEnable()
    {
        //StartCoroutine(StartAlbumSelectPanel());
        SoundManager.Instance.TurnOnGameBackGround();
    }
    void Start()
    {
        makeAlbums();
    }

    void makeAlbums()
    {
        var albumCircles = GlobalData.Instance.Album.AlbumCircles;
        var albumTitles = GlobalData.Instance.Album.AlbumTitles;

        int _albumIndex = 0;
        foreach (var obj in albumCircles)
        {
            var _album = GameObject.Instantiate(album, albumBase);
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

                    albumInfo.UIElementAlbumSelect = this;
                }
                else
                {
                    Debug.LogError("리소스가 빠져있습니다.");
                }
            }

            _albumIndex++;
        }
    }

    public void ShowAlbumInfo()
    {
        var _albumInfo = GameObject.Instantiate(albumInfo, albumInfoBase);
    }

    // Update is called once per frame
    void Update()
    {
        //SelectAlbum();
        OnClickEsc();
        InputExecute();
    }

    public void InputExecute()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (GlobalState.Instance.AlbumIndex <= GlobalData.Instance.Album.AlbumCircles.Length)
            {
                GlobalState.Instance.AlbumIndex++;
                if (GlobalState.Instance.AlbumIndex == GlobalData.Instance.Album.AlbumCircles.Length)
                {
                    GlobalState.Instance.AlbumIndex = 0;
                }
            }

            SoundManager.Instance.PlaySoundFX(2);
            ChangeBackGround(GlobalState.Instance.AlbumIndex);
            Debug.Log($"Current My Album : {GlobalState.Instance.AlbumIndex}");
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (GlobalState.Instance.AlbumIndex >= 0)
            {
                GlobalState.Instance.AlbumIndex--;                
                if (GlobalState.Instance.AlbumIndex < 0)
                {
                    GlobalState.Instance.AlbumIndex = GlobalData.Instance.Album.AlbumCircles.Length - 1;
                }
            }

            SoundManager.Instance.PlaySoundFX(2);
            ChangeBackGround(GlobalState.Instance.AlbumIndex);
            Debug.Log($"Current My Album : {GlobalState.Instance.AlbumIndex}");
        }
    }

    void ChangeBackGround(int currentAlbumIndex)
    {
        switch (GlobalState.Instance.AlbumIndex)
        {
            case (int)GlobalData.ALBUM.ISEDOL:
                break;
            case (int)GlobalData.ALBUM.CONTEST:
                break;
            case (int)GlobalData.ALBUM.GOMIX:
                break;
            case (int)GlobalData.ALBUM.WAKALOID:
                break;
        }

        if (imageBackGround != null)
        {
            imageBackGround.sprite = GlobalData.Instance.Album.AlbumBackgournds[currentAlbumIndex];
        }
    }

    int SFX_Move_02 = 3;
    //void SelectAlbum()
    //{
    //    if (Input.GetKeyDown(KeyCode.Return))
    //    {
    //        StartCoroutine(SelectAlbumProcedure());
    //        SoundManager.Instance.PlaySoundFX(SFX_Move_02);
    //    }
    //}

    void OnClickEsc()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UIManager.Instance.GoPanelMain();
        }
    }

    //IEnumerator StartAlbumSelectPanel()
    //{
    //    fadeImage.gameObject.SetActive(true);
    //    UIManager.Instance.FadeToWhite(fadeImage, fadeTime);
    //    yield return new WaitForSeconds(fadeTime);

    //    fadeImage.gameObject.SetActive(false);
    //}

    //IEnumerator SelectAlbumProcedure()
    //{
    //    fadeImage.gameObject.SetActive(true);
    //    UIManager.Instance.FadeToBlack(fadeImage, fadeTime);        
    //    yield return new WaitForSeconds(fadeTime);

    //    fadeImage.gameObject.SetActive(false);
    //    UIManager.Instance.GoPanelMusicSelect();

    //}
}
