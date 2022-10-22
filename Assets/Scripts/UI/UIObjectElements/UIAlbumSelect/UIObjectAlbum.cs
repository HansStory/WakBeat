using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIObjectAlbum : MonoBehaviour
{
    public UIElementAlbumSelect UIElementAlbumSelect { get; set; }

    [SerializeField] private Image albumTitle; 
    private Sprite _albumTitle;
    public Sprite AlbumTitle
    {
        get { return _albumTitle; }
        set
        {
            _albumTitle = value;
            albumTitle.sprite = _albumTitle;
        }
    }

    [SerializeField] private Image albumCircle;
    private Sprite _albumCircle;
    public Sprite AlbumCircle
    {
        get { return _albumCircle; }
        set
        {
            _albumCircle = value;
            albumCircle.sprite = _albumCircle;
        }
    }

    private int _albumIndex = 0;
    public int AlbumIndex
    {
        get { return _albumIndex; }
        set
        {
            _albumIndex = value;
        }
    }

    public void InputExecute()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            ShowMyTitle();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            ShowMyTitle();
        }
    }

    void SetMyAlbumProcedure()
    {
        albumTitle.gameObject.SetActive(GlobalState.Instance.AlbumIndex == AlbumIndex);
    }

    void ShowMyTitle()
    {
        albumTitle.gameObject.SetActive(GlobalState.Instance.AlbumIndex == AlbumIndex);
    }

    private Vector2 upPos = new Vector2(-340f, 420f);
    private Vector2 centerPos = new Vector2(0f, -16f);
    private Vector2 downPos = new Vector2(-340f, -430f);

    private Vector3 centerSize = Vector3.one;
    private Vector3 smallSize = new Vector3(0.6f, 0.6f, 0.6f);

    [SerializeField] private float _slideDuration = 2f;
    [SerializeField] private RectTransform myRectTransform;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    void Init()
    {
        ShowMyTitle();
    }

    public void InitMyIndexPos()
    {
        switch (AlbumIndex)
        {
            case (int)GlobalData.ALBUM.ISEDOL:
                break;
            case (int)GlobalData.ALBUM.CONTEST:
                myRectTransform.anchoredPosition = downPos;
                myRectTransform.localScale = smallSize;
                break;
            case (int)GlobalData.ALBUM.GOMIX:
                myRectTransform.anchoredPosition = downPos + downPos;
                myRectTransform.localScale = smallSize;
                break;
            case (int)GlobalData.ALBUM.WAKALOID:
                myRectTransform.anchoredPosition = upPos;
                myRectTransform.localScale = smallSize;
                break;
        }
    }

    public void OnClickInfoButton()
    {
        UIElementAlbumSelect.ShowAlbumInfo();
        Debug.Log($"Click Info Button. My Album Index = {_albumIndex}");
    }

    // Update is called once per frame
    void Update()
    {
        InputTest();
        InputExecute();
    }

    void InputTest()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            myRectTransform.DOAnchorPos(upPos, _slideDuration, false);
            myRectTransform.DOScale(smallSize, _slideDuration);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            myRectTransform.DOAnchorPos(centerPos, _slideDuration, false);
            myRectTransform.DOScale(centerSize, _slideDuration);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            myRectTransform.DOAnchorPos(downPos, _slideDuration, false);
            myRectTransform.DOScale(smallSize, _slideDuration);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {

        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {

        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {

        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {

        }
    }
}
