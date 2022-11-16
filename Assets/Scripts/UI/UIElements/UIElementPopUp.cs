using DG.Tweening;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIElementPopUp : MonoBehaviour
{
    public GameObject Background;
    [SerializeField] private GameObject AlbumInfoPrefab;
    [SerializeField] private Transform AlbumInfoPanel;
    [SerializeField] private GameObject MusicInfoPrefab;
    [SerializeField] private Transform MusicInfoPanel;

    // 팝업 세팅 > 앨범 정보
    public void SetPopUpAlbumInfo()
    {
        var _PopUp = (GameObject)Instantiate(AlbumInfoPrefab, AlbumInfoPanel);
        var PopUpInfo = _PopUp.GetComponent<UIObjectAlbumInfo>();
        int AlbumIndex = (int)GlobalState.Instance.AlbumIndex;

        if (PopUpInfo)
        {
            PopUpInfo.ContentImage = GlobalData.Instance.Album.AlbumInfomationImage[AlbumIndex];
            // Close 버튼 이벤트
            _PopUp.transform.Find("ButtonClose").GetComponent<Button>().onClick.AddListener(() => SetButtonEvent("Album", _PopUp));

            _PopUp.SetActive(true);
            Background.SetActive(true);
        }
    }

    // 팝업 세팅 > 음악 정보
    public void SetPopUpMusicInfo()
    {
        var _PopUp = (GameObject)Instantiate(MusicInfoPrefab, MusicInfoPanel);
        var PopUpInfo = _PopUp.GetComponent<UIObjectMusicInfo>();
        float _duration = 1.5f;

        if (PopUpInfo)
        {
            // Music Cover
            // Music Cover Title
            // Music Cover Background
            // Music Title
            // Music Time

            _PopUp.SetActive(true);

            // 팝업 Moving 처리
            this.transform.position = new Vector3(0, -3);
            this.transform.DOLocalMoveY(10, _duration).SetEase(Ease.Linear).OnComplete(() => {
                this.transform.DOLocalMoveY(-400, _duration).SetEase(Ease.Linear).SetDelay(2).OnComplete(() => { SetButtonEvent("Music", _PopUp); });
            });
        }
    }

    // 팝업 닫기 및 인스턴스 오브젝트 디스트로이
    public void SetButtonEvent(string Division, GameObject Obj)
    {
        if(Division.Equals("Album"))
        {
            Background.SetActive(false);
            Destroy(Obj);
        } 
        else if(Division.Equals("Music"))
        {
            Destroy(Obj);
        }
    }
}
