using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIElementAlbumSelect : MonoBehaviour
{
    public List<GameObject> AlbumList = new List<GameObject>();

    // 쌍방 참조... 나중에 해결책 찾기...
    public List<UIObjectAlbum> UIObjectAlbums = new List<UIObjectAlbum>();

    [SerializeField] private GameObject album;
    [SerializeField] private Transform albumBase;

    [SerializeField] 
    private GameObject uiObjectAlbumTutorial;
    private Image _imageTutorial = null;

    [SerializeField] private Image imageBackGround;

    private Vector2 _centerPos = new Vector2(12f, -16f);
    private Vector2 _downPos = new Vector2(-333f, -430f);
    private Vector2 _outDownPos = new Vector2(-700f, -750f);
    private Vector2 _upPos = new Vector2(-333f, 415f);
    private Vector2 _outUpPos = new Vector2(-700f, 750f);

    private Vector3 _centerSize = new Vector3(1f, 1f, 1f);
    private Vector3 _smallSize = new Vector3(0.6f, 0.6f, 0.6f);

    public int UpIndex;
    public int DownIndex;

    private int _outUpIndex;
    private int _outDownIndex;

    private float _titleTweenDuration = 0.4f;

    private void OnEnable()
    {

    }

    void Start()
    {
        Init();
    }

    private void Init()
    {
        MakeAlbums();

        CalculateAlbumBoundary();

        UIManager.Instance.MakeTutorial(uiObjectAlbumTutorial, this.transform, _imageTutorial, 0.4f, 2f, 2f);
    }

    void MakeAlbums()
    {
        var albumCircles = GlobalData.Instance.Album.AlbumCircles;
        var albumTitles = GlobalData.Instance.Album.AlbumTitles;

        int _albumIndex = 0;
        foreach (var obj in albumCircles)
        {
            var _album = Instantiate(album, albumBase);
            var albumInfo = _album.GetComponent<UIObjectAlbum>();

            AlbumList.Add(_album);

            if (albumInfo)
            {
                // null check
                if (albumCircles.Length == albumTitles.Length)
                {
                    UIObjectAlbums.Add(albumInfo);

                    albumInfo.name = $"Album_{_albumIndex}";
                    albumInfo.AlbumCircle.sprite = albumCircles[_albumIndex];
                    albumInfo.AlbumTitle.sprite = albumTitles[_albumIndex];
                    albumInfo.AlbumIndex = _albumIndex;
                    albumInfo.UIElementAlbumSelect = this;

                    InitAlbumsPos(_albumIndex, albumInfo.MyRectTransform, albumInfo.AlbumCircle, albumInfo.CurveAlbumCircle);
                }
                else
                {
                    Debug.LogError("리소스가 빠져있습니다.");
                }
            }

            _albumIndex++;
        }
    }

    void InitAlbumsPos(int albumIndex, RectTransform rectTransform, Image albumCircle, AnimCurve animCurve)
    {
        switch (albumIndex)
        {
            case (int)GlobalData.ALBUM.ISEDOL:
                InitAlbumPos(rectTransform, _centerPos, _centerSize);
                InitAlbumTween(albumCircle, animCurve, 1f, 0.5f);
                break;
            case (int)GlobalData.ALBUM.CONTEST:
                InitAlbumPos(rectTransform, _downPos, _smallSize);
                InitAlbumTween(albumCircle, animCurve, 1f, 0.6f);
                break;
            case (int)GlobalData.ALBUM.GOMIX:
                InitAlbumPos(rectTransform, _outDownPos, _smallSize);
                InitAlbumTween(albumCircle, animCurve, 1f, 0.8f);
                break;
            case (int)GlobalData.ALBUM.WAKALOID:
                InitAlbumPos(rectTransform, _outUpPos, _smallSize);
                InitAlbumTween(albumCircle, animCurve, 1f, 0.7f);
                break;
            case (int)GlobalData.ALBUM.CONTEST2:
                InitAlbumPos(rectTransform, _upPos, _smallSize);
                InitAlbumTween(albumCircle, animCurve, 1f, 0.7f);
                break;
        }
    }

    void InitAlbumPos(RectTransform rectTransform, Vector2 pos, Vector3 size)
    {
        rectTransform.anchoredPosition = pos;
        rectTransform.localScale = size;
    }

    void InitAlbumTween(Image albumCircle, AnimCurve animCurve, float duration, float delay)
    {
        albumCircle.rectTransform.localScale = Vector3.one * 0.9f;
        albumCircle.rectTransform.DOScale(Vector3.one, duration).SetEase(animCurve.Curve).SetDelay(delay);
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
                InputDown();
                InputUp();

                if (GlobalState.Instance.DevMode)
                {
                    InputReturn();
                    InputEscape();
                }
            }
        }
    }

    void InputDown()
    {
        if (Input.GetKey(KeyCode.DownArrow))
        {
            InputDownFunction();
        }
    }

    public void InputDownFunction()
    {
        if (!IsAlbumMove)
        {
            ChangeAlbumIndexInputDown();
            CalculateAlbumBoundary();

            MoveUpAlbums();
            ShowAlbumTitle();

            SoundManager.Instance.PlaySoundFX((int)GlobalData.SFX.AlbumMove);
            Debug.Log($"Current My Album : {GlobalState.Instance.AlbumIndex}");
        }
    }

    void InputUp()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            InputUpFunction();
        }
    }

    public void InputUpFunction()
    {
        if (!IsAlbumMove)
        {
            ChangeAlbumIndexInputUp();
            CalculateAlbumBoundary();

            MoveDownAlbums();
            ShowAlbumTitle();

            SoundManager.Instance.PlaySoundFX((int)GlobalData.SFX.AlbumMove);

            //ChangeBackGround(GlobalState.Instance.AlbumIndex);

            Debug.Log($"Current My Album : {GlobalState.Instance.AlbumIndex}");
        }
    }

    void ShowAlbumTitle()
    {
        var state = GlobalState.Instance;

        foreach (var album in UIObjectAlbums)
        {
            album.AlbumTitle.gameObject.SetActive(state.AlbumIndex == album.AlbumIndex);
        }

        UIObjectAlbums[state.AlbumIndex].ShowMyTitle(_titleTweenDuration, 0.1f);
    }

    void InputReturn()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            var albumIndex = GlobalState.Instance.AlbumIndex;

            if (!UIObjectAlbums[albumIndex].IsTitleMove)
            {
                UIObjectAlbums[albumIndex].OnClickAlbum();
            }
        }
    }

    void InputEscape()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UIManager.Instance.GoPanelMain();
        }
    }

    void ChangeAlbumIndexInputDown()
    {
        GlobalState.Instance.AlbumIndex++;

        if (GlobalState.Instance.AlbumIndex == GlobalData.Instance.Album.AlbumCircles.Length)
        {
            GlobalState.Instance.AlbumIndex = 0;
        }
    }

    void ChangeAlbumIndexInputUp()
    {
        GlobalState.Instance.AlbumIndex--;

        if (GlobalState.Instance.AlbumIndex < 0)
        {
            GlobalState.Instance.AlbumIndex = GlobalData.Instance.Album.AlbumCircles.Length - 1;
        }
    }

    void CalculateAlbumBoundary()
    {
        UpIndex = GlobalState.Instance.AlbumIndex - 1;
        _outUpIndex = GlobalState.Instance.AlbumIndex - 2;

        DownIndex = GlobalState.Instance.AlbumIndex + 1;
        _outDownIndex = GlobalState.Instance.AlbumIndex + 2;

        if (UpIndex < 0)
        {
            UpIndex = GlobalData.Instance.Album.AlbumCircles.Length - 1;
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

        if (DownIndex > GlobalData.Instance.Album.AlbumCircles.Length - 1)
        {
            DownIndex = 0;
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

    #region Move Album Function
    public Tween AlbumMoveTween;
    public bool IsAlbumMove = false;
    private float _albumMoveDuration = 0.3f;

    void MoveUpAlbums()
    {
        MoveAlbums(UpIndex, _upPos, DownIndex, _outDownPos, _downPos, _outUpIndex, _outUpPos);
    }

    void MoveDownAlbums()
    {
        MoveAlbums(DownIndex, _downPos, UpIndex, _outUpPos, _upPos, _outDownIndex, _outDownPos);
    }

    void MoveAlbums(int centerToSideIndex, Vector2 centerToSidePos, int outToSideIndex, Vector2 outPos, Vector2 outToSidePos, int sideToOutIndex, Vector2 sideToOutPos)
    {
        GlobalState.Instance.IsTweening = true;
        IsAlbumMove = true;

        MoveAlbumSideToCenter(GlobalState.Instance.AlbumIndex, _centerPos);
        MoveAlbumCenterToSide(centerToSideIndex, centerToSidePos);
        MoveAlbumOutToSide(outToSideIndex, outPos, outToSidePos);
        MoveAlbumSideToOut(sideToOutIndex, sideToOutPos);  
        
        AlbumMoveTween.OnComplete(() => OnCompleteAlbumTween());
    }

    void MoveAlbumSideToCenter(int centerIndex, Vector2 targetPos)
    {
        AlbumMoveTween = AlbumList[centerIndex].transform.DOLocalMove(targetPos, _albumMoveDuration).SetAutoKill();
        AlbumList[centerIndex].transform.DOScale(_centerSize, _albumMoveDuration);
    }

    void MoveAlbumOutToSide(int inIndex, Vector2 startPos, Vector2 targetPos)
    {
        AlbumList[inIndex].transform.localPosition = startPos;
        AlbumList[inIndex].transform.DOLocalMove(targetPos, _albumMoveDuration);
        AlbumList[inIndex].transform.DOScale(_smallSize, _albumMoveDuration);
    }

    void MoveAlbumCenterToSide(int sideIndex, Vector2 targetPos)
    {
        AlbumList[sideIndex].transform.DOLocalMove(targetPos, _albumMoveDuration);
        AlbumList[sideIndex].transform.DOScale(_smallSize, _albumMoveDuration);
    }

    void MoveAlbumSideToOut(int outIndex, Vector2 targetPos)
    {
        AlbumList[outIndex].transform.DOLocalMove(targetPos, _albumMoveDuration);
    }

    void OnCompleteAlbumTween()
    {
        GlobalState.Instance.IsTweening = false;
        IsAlbumMove = false;
    }
    #endregion

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

    // 팀장님의 부탁으로 BackGround는 변하지 않는걸로 수정
    //void ChangeBackGround(int currentAlbumIndex)
    //{
    //    switch (GlobalState.Instance.AlbumIndex)
    //    {
    //        case (int)GlobalData.ALBUM.ISEDOL:
    //            break;
    //        case (int)GlobalData.ALBUM.CONTEST:
    //            break;
    //        case (int)GlobalData.ALBUM.GOMIX:
    //            break;
    //        case (int)GlobalData.ALBUM.WAKALOID:
    //            break;
    //    }

    //    if (imageBackGround != null)
    //    {
    //        imageBackGround.sprite = GlobalData.Instance.Album.AlbumBackgournds[currentAlbumIndex];
    //    }
    //}

    public void SelectAlbum()
    {
        ShowHideAlbumList(1.5f);
    }

    public Tween ShowHideAlbum;
    public Sequence ShowHideSequence;
    private bool isShowAlbum = true;
    Vector3 _hideTartgetVector = new Vector3(-1000f, 0f, 0f);

    public void ShowHideAlbumList(float delay)
    {
        if (isShowAlbum)
        {
            HideAlbumList(delay);
        }
        else
        {
            ShowAlbumList(delay);
        }

        isShowAlbum = !isShowAlbum;
    }

    void ShowAlbumList(float delay)
    {
        albumBase.transform.localPosition = _hideTartgetVector;
        ShowHideAlbum = albumBase.DOLocalMove(Vector3.zero, 1f);
        ShowHideAlbum.SetDelay(delay);
    }

    void HideAlbumList(float delay)
    {
        ShowHideSequence = DOTween.Sequence().SetAutoKill().OnStart(() =>
        {
            GlobalState.Instance.IsTweening = true;
            albumBase.transform.localPosition = Vector3.zero;
            ShowHideAlbum = albumBase.DOLocalMove(_hideTartgetVector, 0.5f);
            ShowHideAlbum.SetDelay(delay);//.OnComplete(() => { GlobalState.Instance.IsTweening = false; });
        }).InsertCallback(0.5f, () => UIManager.Instance.GoPanelMusicSelect()); ;
      
    }
}

