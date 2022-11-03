using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIElementAlbumSelect : MonoBehaviour
{
    public List<GameObject> AlbumList;

    [SerializeField] private GameObject album;
    [SerializeField] private Transform albumBase;

    [SerializeField] private Image imageBackGround;

    [SerializeField] private GameObject albumInfo;
    [SerializeField] private Transform albumInfoBase;

    private Vector2 centerPos = new Vector2(12f, -16f);
    private Vector2 downPos = new Vector2(-333f, -430f);
    private Vector2 outDownPos = new Vector2(-700f, -750f);
    private Vector2 upPos = new Vector2(-333f, 415f);
    private Vector2 outUpPos = new Vector2(-700f, 750f);

    private Vector3 centerSize = new Vector3(1f, 1f, 1f);
    private Vector3 smallSize = new Vector3(0.6f, 0.6f, 0.6f);

    private int _upIndex;
    private int _outUpIndex;
    private int _downIndex;
    private int _outDownIndex;

    private void OnEnable()
    {

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

            AlbumList.Add(_album);

            if (albumInfo)
            {
                // null check
                if (albumCircles.Length == albumTitles.Length)
                {
                    albumInfo.name = $"Album_{_albumIndex}";
                    albumInfo.AlbumCircle.sprite = albumCircles[_albumIndex];
                    albumInfo.AlbumTitle.sprite = albumTitles[_albumIndex];
                    albumInfo.AlbumIndex = _albumIndex;
                    albumInfo.UIElementAlbumSelect = this;

                    InitAlbumPos(_albumIndex, albumInfo.MyRectTransform, albumInfo.AlbumCircle, albumInfo.CurveAlbumCircle);
                }
                else
                {
                    Debug.LogError("리소스가 빠져있습니다.");
                }
            }

            _albumIndex++;
        }
    }

    void InitAlbumPos(int albumIndex, RectTransform rectTransform, Image albumCircle, AnimCurve animCurve)
    {
        switch (albumIndex)
        {
            case (int)GlobalData.ALBUM.ISEDOL:
                rectTransform.anchoredPosition = centerPos;
                rectTransform.localScale = centerSize;
                InitAlbumTween(albumCircle, animCurve, 1f, 0.5f);
                break;
            case (int)GlobalData.ALBUM.CONTEST:
                rectTransform.anchoredPosition = downPos;
                rectTransform.localScale = smallSize;
                InitAlbumTween(albumCircle, animCurve, 1f, 0.6f);
                break;
            case (int)GlobalData.ALBUM.GOMIX:
                rectTransform.anchoredPosition = outDownPos;
                rectTransform.localScale = smallSize;
                InitAlbumTween(albumCircle, animCurve, 1f, 0.8f);
                break;
            case (int)GlobalData.ALBUM.WAKALOID:
                rectTransform.anchoredPosition = upPos;
                rectTransform.localScale = smallSize;
                InitAlbumTween(albumCircle, animCurve, 1f, 0.7f);
                break;
        }
    }

    void InitAlbumTween(Image albumCircle, AnimCurve animCurve, float duration, float delay)
    {
        albumCircle.rectTransform.localScale = Vector3.one * 0.9f;
        albumCircle.rectTransform.DOScale(Vector3.one, duration).SetEase(animCurve.Curve).SetDelay(delay);
    }

    public void ShowAlbumInfo()
    {
        var _albumInfo = GameObject.Instantiate(albumInfo, albumInfoBase);
    }


    void Update()
    {
        OnClickEsc();
        InputExecute();
    }

    public void InputExecute()
    {
        if (GlobalState.Instance.UserData.data.BackgroundProcActive)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                InputDown();
            }

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                InputUp();
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                //UIManager.Instance.GoPanelMusicSelect();
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                //UIManager.Instance.GoPanelMain();
            }
        }
    }

    void InputDown()
    {
        if (!isAlbumMove)
        {
            InputDownChangeAlbumIndex();
            SoundManager.Instance.PlaySoundFX((int)GlobalData.SFX.AlbumMove);

            CalculateAlbumBoundary();
            MoveUpAlbums();

            // 팀장님의 부탁으로 BackGround는 변하지 않는걸로 수정
            //ChangeBackGround(GlobalState.Instance.AlbumIndex);

            Debug.Log($"Current My Album : {GlobalState.Instance.AlbumIndex}");
        }
    }

    void InputUp()
    {
        if (!isAlbumMove)
        {
            InputUpChangeAlbumIndex();
            SoundManager.Instance.PlaySoundFX((int)GlobalData.SFX.AlbumMove);

            CalculateAlbumBoundary();
            MoveDownAlbums();

            // 팀장님의 부탁으로 BackGround는 변하지 않는걸로 수정
            //ChangeBackGround(GlobalState.Instance.AlbumIndex);

            Debug.Log($"Current My Album : {GlobalState.Instance.AlbumIndex}");
        }
    }

    void InputDownChangeAlbumIndex()
    {
        GlobalState.Instance.AlbumIndex++;
        if (GlobalState.Instance.AlbumIndex == GlobalData.Instance.Album.AlbumCircles.Length)
        {
            GlobalState.Instance.AlbumIndex = 0;
        }
    }

    void InputUpChangeAlbumIndex()
    {
        GlobalState.Instance.AlbumIndex--;

        if (GlobalState.Instance.AlbumIndex < 0)
        {
            GlobalState.Instance.AlbumIndex = GlobalData.Instance.Album.AlbumCircles.Length - 1;
        }
    }

    void CalculateAlbumBoundary()
    {
        _upIndex = GlobalState.Instance.AlbumIndex - 1;
        _outUpIndex = GlobalState.Instance.AlbumIndex - 2;

        _downIndex = GlobalState.Instance.AlbumIndex + 1;
        _outDownIndex = GlobalState.Instance.AlbumIndex + 2;

        if (_upIndex < 0)
        {
            _upIndex = GlobalData.Instance.Album.AlbumCircles.Length - 1;
        }

        if (_outUpIndex < 0)
        {
            if (GlobalState.Instance.AlbumIndex == 0)
            {
                _outUpIndex = GlobalData.Instance.Album.AlbumCircles.Length - 2;
            }
            else
            {
                _outUpIndex = GlobalData.Instance.Album.AlbumCircles.Length - 1;
            }
        }

        if (_downIndex > GlobalData.Instance.Album.AlbumCircles.Length - 1)
        {
            _downIndex = 0;
        }

        if (_outDownIndex > GlobalData.Instance.Album.AlbumCircles.Length - 1)
        {
            if (GlobalState.Instance.AlbumIndex == GlobalData.Instance.Album.AlbumCircles.Length - 1)
            {
                _outDownIndex = 1;
            }
            else
            {
                _outDownIndex = 0;
            }
        }
    }

    public Tween AlbumMoveTween;
    public bool isAlbumMove = false;
    private float AlbumMoveSpeed = 0.3f;
    void MoveUpAlbums()
    {       
        if (!isAlbumMove)
        {
            AlbumList[GlobalState.Instance.AlbumIndex].transform.DOLocalMove(centerPos, AlbumMoveSpeed);
            AlbumList[GlobalState.Instance.AlbumIndex].transform.DOScale(centerSize, AlbumMoveSpeed);

            AlbumList[_upIndex].transform.DOLocalMove(upPos, AlbumMoveSpeed);
            AlbumList[_upIndex].transform.DOScale(smallSize, AlbumMoveSpeed);

            AlbumList[_downIndex].transform.localPosition = outDownPos;
            AlbumList[_downIndex].transform.DOLocalMove(downPos, AlbumMoveSpeed);
            AlbumList[_downIndex].transform.DOScale(smallSize, AlbumMoveSpeed);

            AlbumMoveTween = AlbumList[_outUpIndex].transform.DOLocalMove(outUpPos, AlbumMoveSpeed);

            isAlbumMove = true;
        }

        //AlbumMoveTween.OnComplete(() => { isAlbumMove = false; });
    }

    void MoveDownAlbums()
    {
        if (!isAlbumMove)
        {
            AlbumList[GlobalState.Instance.AlbumIndex].transform.DOLocalMove(centerPos, AlbumMoveSpeed);
            AlbumList[GlobalState.Instance.AlbumIndex].transform.DOScale(centerSize, AlbumMoveSpeed);

            AlbumList[_upIndex].transform.localPosition = outUpPos;
            AlbumList[_upIndex].transform.DOLocalMove(upPos, AlbumMoveSpeed);
            AlbumList[_upIndex].transform.DOScale(smallSize, AlbumMoveSpeed);

            AlbumList[_downIndex].transform.DOLocalMove(downPos, AlbumMoveSpeed);
            AlbumList[_downIndex].transform.DOScale(smallSize, AlbumMoveSpeed);

            AlbumMoveTween = AlbumList[_outUpIndex].transform.DOLocalMove(outDownPos, AlbumMoveSpeed);

            isAlbumMove = true;
        }

        //AlbumMoveTween.OnComplete(() => { isAlbumMove = false; });
    }


    // TO DO : 앨범 증가시 불필요한 앨범은 SetAcitive 할것
    //void SetActiveAlbum()
    //{
    //    isAlbumMove = false;

    //    for (int i = 0; i < GlobalData.Instance.Album.AlbumCircles.Length; i++)
    //    {
    //        if (i == _upIndex || i == GlobalState.Instance.AlbumIndex || i == _downIndex)
    //        {
    //            Albums[i].SetActive(true);
    //        }
    //        else
    //        {
    //            Albums[i].SetActive(false);
    //        }
    //    }
    //}

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


    //int SFX_Move_02 = 3;
    public void SelectAlbum()
    {
        ShowHideAlbumList(1.5f);
        //ShowHideAlbum.OnComplete(() => { UIManager.Instance.WantShowPanel((int)GlobalData.UIMODE.SELECT_MUSIC); });
    }

    public Tween ShowHideAlbum;
    public Sequence ShowHideSequence;
    private bool isShowAlbum = true;
    Vector3 hideTartgetVector = new Vector3(-1000f, 0f, 0f);
    public void ShowHideAlbumList(float delay)
    {

        if (isShowAlbum)
        {
            HideAlbumList(delay);
            ShowHideSequence.InsertCallback(0.5f, () => UIManager.Instance.GoPanelMusicSelect());
            //ShowHideAlbum.OnComplete(() => { UIManager.Instance.GoPanelMusicSelect(); });
            //UIManager.Instance.WantShowPanel((int)GlobalData.UIMODE.SELECT_MUSIC);
        }
        else
        {
            albumBase.transform.localPosition = hideTartgetVector;
            ShowHideAlbum = albumBase.DOLocalMove(Vector3.zero, 1f);
            ShowHideAlbum.SetDelay(delay);
        }

        isShowAlbum = !isShowAlbum;
    }

    void HideAlbumList(float delay)
    {
        ShowHideSequence = DOTween.Sequence().SetAutoKill(false).OnStart(() =>
        {
            albumBase.transform.localPosition = Vector3.zero;
            ShowHideAlbum = albumBase.DOLocalMove(hideTartgetVector, 0.5f);
            ShowHideAlbum.SetDelay(delay);
        });
    }

    void OnClickEsc()
    {
        {
            if (GlobalState.Instance.UserData.data.BackgroundProcActive)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    UIManager.Instance.GoPanelMain();
                }
            }
        }
    }
}

