using UnityEngine;

public class AppManager : MonoBehaviourSingleton<AppManager>
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    Quit();
        //}
    }

    public void Quit()
    {
        Application.Quit();
    }

}
