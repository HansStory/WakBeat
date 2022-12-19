using UnityEngine;
using UnityEngine.UI;

public class UIObjectAlbumInfo : MonoBehaviour
{
    //public Sprite _PopUpAlbumInfoImage;
    [SerializeField] private Image _contentImage;
    public Image ContentImage
    {
        get { return _contentImage; }
        set
        {
            _contentImage = value;
            ContentImage = _contentImage;
            ContentImage.SetNativeSize();
        }
    }

    //public void OnClickExitPopUp()
    //{
    //    SoundManager.Instance.PlaySoundFX((int)GlobalData.SFX.SettingIn);
    //    Destroy(this);
    //}
}
