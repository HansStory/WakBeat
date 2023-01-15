using UnityEngine;

public class AppManager : MonoBehaviourSingleton<AppManager>
{
    private void Awake()
    {
        //Screen.SetResolution(1920, 1080, true);

        //Screen.SetResolution(Screen.width, (Screen.width * 9) / 16, true);
        SetResolution();
    }

    public void SetResolution()
    {
        int setWidth = 1920; 
        int setHeight = 1080;

#if UNITY_ANDROID
        int deviceWidth = Screen.width;   // 기기 너비 저장
        int deviceHeight = Screen.height; // 기기 높이 저장

        Screen.SetResolution(setWidth, (int)(((float)deviceHeight / deviceWidth) * setWidth), true); // SetResolution 함수 제대로 사용하기

        if ((float)setWidth / setHeight < (float)deviceWidth / deviceHeight) // 기기의 해상도 비가 더 큰 경우
        {
            float newWidth = ((float)setWidth / setHeight) / ((float)deviceWidth / deviceHeight); // 새로운 너비
            Camera.main.rect = new Rect((1f - newWidth) / 2f, 0f, newWidth, 1f); // 새로운 Rect 적용
        }
        else // 게임의 해상도 비가 더 큰 경우
        {
            float newHeight = ((float)deviceWidth / deviceHeight) / ((float)setWidth / setHeight); // 새로운 높이
            Camera.main.rect = new Rect(0f, (1f - newHeight) / 2f, 1f, newHeight); // 새로운 Rect 적용
        }
#else
        Screen.SetResolution(setWidth, setHeight, true);
        Screen.SetResolution(Screen.width, (Screen.width * 9) / 16, true);
#endif
    }

    void Start()
    {
        Application.targetFrameRate = 144;
    }


    void Update()
    {
        if (GlobalState.Instance.DevMode)
        {
            ShowFrame();
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

    // On GUI Frame Check
    private float deltaTime = 0.0f;
    private bool isShow = false;
    protected virtual void ShowFrame()
    {
        if (isShow)
        {
            deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        }

        if (Input.GetKeyDown(KeyCode.F1))
        {
            isShow = !isShow;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Application.targetFrameRate = 30;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Application.targetFrameRate = 60;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Application.targetFrameRate = 144;
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Application.targetFrameRate = -1;
        }
    }

    [SerializeField, Range(1, 100)] private int size = 80;
    [SerializeField] private Color color = Color.green;
    private void OnGUI()
    {
        if (isShow)
        {
            GUIStyle style = new GUIStyle();

            Rect rect = new Rect(100, 150, Screen.width, Screen.height);
            style.alignment = TextAnchor.UpperLeft;
            style.fontSize = size;
            style.normal.textColor = color;

            float ms = deltaTime * 1000f;
            float fps = 1.0f / deltaTime;
            string text = string.Format("{0:0.} FPS ({1:0.0} ms)", fps, ms);

            GUI.Label(rect, text, style);
        }
    }

}
