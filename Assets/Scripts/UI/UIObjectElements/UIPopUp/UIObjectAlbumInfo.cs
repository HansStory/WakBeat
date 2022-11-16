using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIObjectAlbumInfo : MonoBehaviour
{
    public Sprite _PopUpAlbumInfoImage;
    [SerializeField] private Image PopUpAlbumInfoImage;
    public Sprite ContentImage
    {
        get { return _PopUpAlbumInfoImage; }
        set
        {
            _PopUpAlbumInfoImage = value;
            PopUpAlbumInfoImage.sprite = _PopUpAlbumInfoImage;
            PopUpAlbumInfoImage.SetNativeSize();
        }
    }
}
