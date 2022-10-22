using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIObjectAlbumInfo : MonoBehaviour
{
    [SerializeField] private Image albumImage;
    [SerializeField] private TMP_Text textHeader;
    [SerializeField] private TMP_Text textContents;
    private string headerText;
    private string contentsText;

    void Start()
    {
        InitAlbumInfo();
    }

    void InitAlbumInfo()
    {
        albumImage.sprite = GlobalData.Instance.Album.AlbumCircles[GlobalState.Instance.AlbumIndex];
        // To do : 
        //textHeader.text = headerText;
        //textContents.text = contentsText;
    }

    public void OnClickDestroyAlbumInfo()
    {
        Destroy(this.gameObject);
    }
}
