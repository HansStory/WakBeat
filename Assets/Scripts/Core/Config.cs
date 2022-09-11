public class Config : MonoBehaviourSingleton<Config>
{
    public string ExamString { get; internal set; }
    public int ExamInt { get; internal set; }
    public bool ExamBool { get; internal set; } = true;

    private void Awake()
    {
        ExamString = string.Empty;
        ExamInt = 0;
        ExamBool = true;

        LoadConfig();
    }

    void LoadConfig()
    {
        IniFile iniFile = new IniFile();
        iniFile.Load("config.ini");

        ExamString = iniFile["TEST"]["ExamString"].ToString().Trim();
        ExamInt = iniFile["TEST"]["ExamInt"].ToInt();
        ExamBool = iniFile["TEST"]["ExamBool"].ToBool();

    }

}
