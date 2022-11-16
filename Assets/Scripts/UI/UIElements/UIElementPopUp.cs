using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIElementPopUp : MonoBehaviour
{
    public GameObject Background;
    public GameObject MySelfPopUp;
    [SerializeField] private GameObject AlbumInfoPrefab;
    [SerializeField] private Transform AlbumInfoPanel;

    // 팝업 세팅
    public void SetPopUp()
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

    public void SetButtonEvent(string Division, GameObject Obj)
    {
        Background.SetActive(false);

        if(Division.Equals("Album"))
        {
            Destroy(Obj);
        }
    }
}
