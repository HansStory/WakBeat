using UnityEngine;

public class AppManager : MonoBehaviourSingleton<AppManager>
{
    private void Awake()
    {
        Screen.SetResolution(1920, 1080, true);

        Screen.SetResolution(Screen.width, (Screen.width * 9) / 16, true);
    }

    void Start()
    {

    }


    void Update()
    {

    }

    public void Quit()
    {
        Application.Quit();
    }

}
