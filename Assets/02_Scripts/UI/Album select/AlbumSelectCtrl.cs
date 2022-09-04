using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AlbumSelectCtrl : MonoBehaviour
{
    private RectTransform m_rect;
    private AlbumSelector m_albumSelector;
    private BackgroundChanger m_backgroundChanger;

    //TODO : 앨범 데이터베이스 구성
    [SerializeField, Header("Albums")]
    private AlbumData[] m_albumDatas;

    private int m_albumIndex = 0;

    public RectTransform Rect => m_rect;

    public ALBUM Album => (ALBUM)m_albumIndex;
    public int AlbumIndex
    {
        get => m_albumIndex;
        private set
        {
            value = MyUtil.RepeatIndex(value, m_albumDatas.Length);
            m_albumIndex = value;
        }
    }

    private void Awake()
    {
        m_rect = GetComponent<RectTransform>();
        m_albumSelector = GetComponentInChildren<AlbumSelector>();
        m_albumSelector.Init();
        m_albumSelector.SetAlbumSprites(m_albumDatas[0], m_albumDatas[1], m_albumDatas[m_albumDatas.Length - 1]);

        m_backgroundChanger = GetComponentInChildren<BackgroundChanger>();
        m_backgroundChanger.Init(m_albumDatas[0].BackGround);
    }

    public void InputExecute()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            AlbumIndex++;
            m_albumSelector.MoveAlbum(INCREASE.INCREASE);
            m_albumSelector.SetAlbumSprites(INCREASE.INCREASE, m_albumDatas[MyUtil.RepeatIndex(AlbumIndex + 1, m_albumDatas.Length)]);
            m_backgroundChanger.ChangeBackground(m_albumDatas[AlbumIndex].BackGround);
        }
        else if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            AlbumIndex--;
            m_albumSelector.MoveAlbum(INCREASE.DECREASE);
            m_albumSelector.SetAlbumSprites(INCREASE.DECREASE, m_albumDatas[MyUtil.RepeatIndex(AlbumIndex - 1, m_albumDatas.Length)]);
            m_backgroundChanger.ChangeBackground(m_albumDatas[AlbumIndex].BackGround);
        }
    }

    public void StartOpenAnim()
    {
        m_albumSelector.StartOpenAnim();
    }

    public void StartChangeWindow(Action action)
    {
        m_albumSelector.SelectAlbum(action);
    }
}
