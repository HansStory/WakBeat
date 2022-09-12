public class Config : MonoBehaviourSingleton<Config>
{
    //------------------------- URL ---------------------------------
    //WakZoo
    public string WakZoo { get; internal set; }
    //BackGroundMusic
    public string BGM_01 { get; internal set; }
    public string BGM_02 { get; internal set; }
    public string BGM_03 { get; internal set; }

    //Music
    public string Rewind { get; internal set; }
    public string WinterSpring { get; internal set; }

    public string NobleLick { get; internal set; }
    public string Wakaloid { get; internal set; }
    public string WakGoodAroma100 { get; internal set; }
    public string AvantGarde { get; internal set; }

    public string YouHi { get; internal set; }
    public string Gotterfly { get; internal set; }
    public string KingADance { get; internal set; }
    public string IPad { get; internal set; }
    public string LikeDogRevival { get; internal set; }

    public string GalUseGirl { get; internal set; }
    public string BangOff { get; internal set; }
    public string AfterTwist { get; internal set; }
    public string Waklio { get; internal set; }
    private void Awake()
    {
        //URL
        WakZoo = string.Empty;

        BGM_01 = string.Empty;
        BGM_02 = string.Empty;
        BGM_03 = string.Empty;

        Rewind = string.Empty;
        WinterSpring = string.Empty;

        NobleLick = string.Empty;
        Wakaloid = string.Empty;
        WakGoodAroma100 = string.Empty;
        AvantGarde = string.Empty;

        YouHi = string.Empty;
        Gotterfly = string.Empty;
        KingADance = string.Empty;
        IPad = string.Empty;
        LikeDogRevival = string.Empty;

        GalUseGirl = string.Empty;
        BangOff = string.Empty;
        AfterTwist = string.Empty;
        Waklio = string.Empty;

        LoadConfig();
    }

    void LoadConfig()
    {
        IniFile iniFile = new IniFile();
        iniFile.Load("config.ini");

        WakZoo = iniFile["URL"]["WakZoo"].ToString().Trim();

        BGM_01 = iniFile["URL"]["BGM_01"].ToString().Trim();
        BGM_02 = iniFile["URL"]["BGM_02"].ToString().Trim();
        BGM_03 = iniFile["URL"]["BGM_03"].ToString().Trim();

        Rewind = iniFile["URL"]["Rewind"].ToString().Trim();
        WinterSpring = iniFile["URL"]["WinterSpring"].ToString().Trim();

        NobleLick = iniFile["URL"]["NobleLick"].ToString().Trim();
        Wakaloid = iniFile["URL"]["Wakaloid"].ToString().Trim();
        WakGoodAroma100 = iniFile["URL"]["WakGoodAroma100"].ToString().Trim();
        AvantGarde = iniFile["URL"]["AvantGarde"].ToString().Trim();

        YouHi = iniFile["URL"]["YouHi"].ToString().Trim();
        Gotterfly = iniFile["URL"]["Gotterfly"].ToString().Trim();
        KingADance = iniFile["URL"]["KingADance"].ToString().Trim();
        IPad = iniFile["URL"]["IPad"].ToString().Trim();
        LikeDogRevival = iniFile["URL"]["LikeDogRevival"].ToString().Trim();

        GalUseGirl = iniFile["URL"]["GalUseGirl"].ToString().Trim();
        BangOff = iniFile["URL"]["BangOff"].ToString().Trim();
        AfterTwist = iniFile["URL"]["AfterTwist"].ToString().Trim();
        Waklio = iniFile["URL"]["Waklio"].ToString().Trim();
    }

}
