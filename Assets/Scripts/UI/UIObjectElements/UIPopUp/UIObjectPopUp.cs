using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIObjectPopUp : MonoBehaviour
{
    public Sprite _PopUpContentImage;
    [SerializeField] private Image PopUpContentImage;
    public Sprite ContentImage
    {
        get { return _PopUpContentImage; }
        set
        {
            _PopUpContentImage = value;
            PopUpContentImage.sprite = _PopUpContentImage;
            PopUpContentImage.SetNativeSize();
        }
    }
}
