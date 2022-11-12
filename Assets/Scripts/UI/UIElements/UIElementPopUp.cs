using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIElementPopUp : MonoBehaviour
{
    public GameObject Background;
    public GameObject AlbumInfoPopUp;
    public Button PopUpClose;

    // ÆË¾÷ ¼¼ÆÃ
    public void SetPopUp(string Division, int Index)
    {
        // ¾Ù¹ü Á¤º¸ Ãâ·Â ÆË¾÷
        if (Division.Equals("Album"))
        {
            Background.SetActive(true);
            AlbumInfoPopUp.SetActive(true);
            PopUpClose.onClick.AddListener(() => SetClose("Album"));

            for (int i = 0; i < GlobalData.Instance.Album.AlbumCircles.Length; i++)
            {
                if (Index.Equals(i))
                {
                    AlbumInfoPopUp.transform.Find("PopUpWindow").GetComponent<Image>().sprite = GlobalData.Instance.Album.AlbumInfomationImage[Index];
                }
            }
        }
    }

    public void SetClose(string Division)
    {
        Background.SetActive(false);

        if(Division.Equals("Album"))
        {
            AlbumInfoPopUp.SetActive(false);
        }
    }
}
