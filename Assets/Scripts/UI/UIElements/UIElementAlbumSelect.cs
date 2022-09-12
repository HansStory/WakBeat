using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIElementAlbumSelect : MonoBehaviour
{
    public Image fadeImage = null;
    private float fadeTime = 0.5f;
    private float fadeHoldTime = 1f;

    // Start is called before the first frame update
    private void OnEnable()
    {
        StartCoroutine(StartAlbumSelectPanel());
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SelectAlbum();
        OnClickEsc();
    }

    void SelectAlbum()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            StartCoroutine(SelectAlbumProcedure());
        }
    }

    void OnClickEsc()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UIManager.Instance.GoPanelMain();
        }
    }

    IEnumerator StartAlbumSelectPanel()
    {
        fadeImage.gameObject.SetActive(true);
        UIManager.Instance.FadeToWhite(fadeImage, fadeTime);
        yield return new WaitForSeconds(fadeTime);

        fadeImage.gameObject.SetActive(false);
    }

    IEnumerator SelectAlbumProcedure()
    {
        fadeImage.gameObject.SetActive(true);
        UIManager.Instance.FadeToBlack(fadeImage, fadeTime);        
        yield return new WaitForSeconds(fadeTime);

        fadeImage.gameObject.SetActive(false);
        UIManager.Instance.GoPanelMusicSelect();

    }
}
