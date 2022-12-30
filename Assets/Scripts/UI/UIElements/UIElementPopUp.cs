using DG.Tweening;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UIElementPopUp : MonoBehaviour
{
    public GameObject Background;
    [SerializeField] private GameObject _UIElementPopUp;
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
            PopUpInfo.ContentImage.sprite = GlobalData.Instance.Album.AlbumInfomationImage[AlbumIndex];
            // Close 버튼 이벤트
            _PopUp.transform.Find("ButtonClose").GetComponent<Button>().onClick.AddListener(() => SetButtonEvent("Album", _PopUp));

            _UIElementPopUp.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
            _UIElementPopUp.gameObject.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
            _UIElementPopUp.gameObject.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);

            _PopUp.SetActive(true);
            Background.SetActive(true);
        }
    }

    [SerializeField] private Sprite musicBanner;
    // 팝업 세팅 > 음악 정보
    public void SetPopUpMusicInfo()
    {
        var _PopUp = (GameObject)Instantiate(MusicInfoPrefab, MusicInfoPanel);
        var PopUpInfo = _PopUp.GetComponent<UIObjectMusicInfo>();
        int StageIndex = (int)GlobalState.Instance.StageIndex;
        int AlbumIndex = (int)GlobalState.Instance.AlbumIndex;
        float _duration = 1.5f;

        switch(AlbumIndex)
        {
            case 0: musicBanner = GlobalData.Instance.Album.Album1MusicBannerImage[StageIndex]; break;
            case 1: musicBanner = GlobalData.Instance.Album.Album2MusicBannerImage[StageIndex]; break;
            case 2: musicBanner = GlobalData.Instance.Album.Album3MusicBannerImage[StageIndex]; break;
            case 3: musicBanner = GlobalData.Instance.Album.Album4MusicBannerImage[StageIndex]; break;
            case 4: musicBanner = GlobalData.Instance.Album.Album5MusicBannerImage[StageIndex]; break;
        }

        if (PopUpInfo)
        {
            // Music Banner
            PopUpInfo.transform.Find("PopUpWindow").GetComponent<Image>().sprite = musicBanner;
            PopUpInfo.transform.Find("PopUpWindow").GetComponent<Image>().SetNativeSize();

            _PopUp.SetActive(true);

            // 팝업 Moving 처리
            this.transform.position = new Vector3(0, -3);
            this.transform.DOLocalMoveY(10, _duration).SetEase(Ease.Linear).SetAutoKill().OnComplete(() => 
            {
                this.transform.DOLocalMoveY(-400, _duration).SetEase(Ease.Linear).SetAutoKill().
                SetDelay(2).OnComplete(() => { SetButtonEvent("Music", _PopUp); });
            });
        }
    }

    // 팝업 닫기 및 인스턴스 오브젝트 디스트로이
    public void SetButtonEvent(string Division, GameObject Obj)
    {
        if(Division.Equals("Album"))
        {
            Background.SetActive(false);
            SoundManager.Instance.PlaySoundFX((int)GlobalData.SFX.SettingIn);
            Destroy(Obj);
        } 
        else if(Division.Equals("Music"))
        {
            Destroy(Obj);
        }
    }
}
