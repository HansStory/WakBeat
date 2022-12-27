public class Config : MonoBehaviourSingleton<Config>
{
    //------------------------- URL ---------------------------------
    //WakZoo
    public string WakZoo { get; internal set; }

    //Origin URL
    public string Origin_Rewind { get; internal set; }
    public string Origin_WinterSpring { get; internal set; }

    public string Origin_NobleLick { get; internal set; }
    public string Origin_Wakaloid { get; internal set; }
    public string Origin_WakGoodAroma100 { get; internal set; }
    public string Origin_AvantGarde { get; internal set; }

    public string Origin_YouHi { get; internal set; }
    public string Origin_Gotterfly { get; internal set; }
    public string Origin_KingADance { get; internal set; }
    public string Origin_IPad { get; internal set; }
    public string Origin_ReviveLikeADog { get; internal set; }

    public string Origin_GalUseGirl { get; internal set; }
    public string Origin_BangOff { get; internal set; }
    public string Origin_TwistedLove { get; internal set; }
    public string Origin_Waklio { get; internal set; }

    //ReMix URL
    //Music       
    public string ReMix_Rewind { get; internal set; }
    public string ReMix_WinterSpring { get; internal set; }
                  
    public string ReMix_NobleLick { get; internal set; }
    public string ReMix_Wakaloid { get; internal set; }
    public string ReMix_WakGoodAroma100 { get; internal set; }
    public string ReMix_AvantGarde { get; internal set; }

    public string ReMix_YouHi { get; internal set; }
    public string ReMix_Gotterfly { get; internal set; }
    public string ReMix_KingADance { get; internal set; }
    public string ReMix_IPad { get; internal set; }
    public string ReMix_ReviveLikeADog { get; internal set; }
                  
    public string ReMix_GalUseGirl { get; internal set; }
    public string ReMix_BangOff { get; internal set; }
    public string ReMix_TwistedLove { get; internal set; }
    public string ReMix_Waklio { get; internal set; }

    //BackGroundMusic
    public string BGM_01 { get; internal set; }
    public string BGM_02 { get; internal set; }
    public string BGM_03 { get; internal set; }

    //------------------------- SYSTEM ------------------------------
    public string DEVKEY { get; internal set; }
    public string GAMEMODE { get; internal set; }
    public string AUTOMODE { get; internal set; }

    //------------------------- Shop > Video ------------------------
    public string CREDIT { get; internal set; }


    private bool _isDevMode = false;
    public bool DevMode
    {
        get
        {
            if (string.IsNullOrEmpty(DEVKEY))
            {
                _isDevMode = false;
            }
            else
            {
                _isDevMode = DEVKEY.Equals("1");
            }

            return _isDevMode;
        }
    }

    private bool _isGameMode = false;
    public bool GameMode
    {
        get
        {
            if (string.IsNullOrEmpty(GAMEMODE))
            {
                _isGameMode = false;
            }
            else
            {
                _isGameMode = GAMEMODE.Equals("1");
            }

            return _isGameMode;
        }
    }

    private bool _isAutoMode = false;
    public bool AutoMode
    {
        get
        {
            if (string.IsNullOrEmpty(AUTOMODE))
            {
                _isAutoMode = false;
            }
            else
            {
                _isAutoMode = AUTOMODE.Equals("1");
            }

            return _isAutoMode;
        }
    }

    private void Awake()
    {
        // URL
        WakZoo = string.Empty;

        // Origin URL
        Origin_Rewind = string.Empty;
        Origin_WinterSpring = string.Empty;

        Origin_NobleLick = string.Empty;
        Origin_Wakaloid = string.Empty;
        Origin_WakGoodAroma100 = string.Empty;
        Origin_AvantGarde = string.Empty;

        Origin_YouHi = string.Empty;
        Origin_Gotterfly = string.Empty;
        Origin_KingADance = string.Empty;
        Origin_IPad = string.Empty;
        Origin_ReviveLikeADog = string.Empty;

        Origin_GalUseGirl = string.Empty;
        Origin_BangOff = string.Empty;
        Origin_TwistedLove = string.Empty;
        Origin_Waklio = string.Empty;

        // Remix URL
        ReMix_Rewind = string.Empty;
        ReMix_WinterSpring = string.Empty;

        ReMix_NobleLick = string.Empty;
        ReMix_Wakaloid = string.Empty;
        ReMix_WakGoodAroma100 = string.Empty;
        ReMix_AvantGarde = string.Empty;

        ReMix_YouHi = string.Empty;
        ReMix_Gotterfly = string.Empty;
        ReMix_KingADance = string.Empty;
        ReMix_IPad = string.Empty;
        ReMix_ReviveLikeADog = string.Empty;

        ReMix_GalUseGirl = string.Empty;
        ReMix_BangOff = string.Empty;
        ReMix_TwistedLove = string.Empty;
        ReMix_Waklio = string.Empty;

        //BGM
        BGM_01 = string.Empty;
        BGM_02 = string.Empty;
        BGM_03 = string.Empty;

        // SYSTEM
        DEVKEY = string.Empty;
        GAMEMODE = string.Empty;
        AUTOMODE = string.Empty;

        //------------------------- Shop > Video ------------------------
        CREDIT = string.Empty;

        LoadConfig();
    }

    void LoadConfig()
    {
        IniFile iniFile = new IniFile();
        iniFile.Load("config.ini");

        //-------------------- URL --------------------------------

        WakZoo = iniFile["URL"]["WakZoo"].ToString().Trim();

        // Origin URL
        Origin_Rewind = iniFile["URL"]["Origin_Rewind"].ToString().Trim();
        Origin_WinterSpring = iniFile["URL"]["Origin_WinterSpring"].ToString().Trim();

        Origin_NobleLick = iniFile["URL"]["Origin_NobleLick"].ToString().Trim();
        Origin_Wakaloid = iniFile["URL"]["Origin_Wakaloid"].ToString().Trim();
        Origin_WakGoodAroma100 = iniFile["URL"]["Origin_WakGoodAroma100"].ToString().Trim();
        Origin_AvantGarde = iniFile["URL"]["Origin_AvantGarde"].ToString().Trim();

        Origin_YouHi = iniFile["URL"]["Origin_YouHi"].ToString().Trim();
        Origin_Gotterfly = iniFile["URL"]["Origin_Gotterfly"].ToString().Trim();
        Origin_KingADance = iniFile["URL"]["Origin_KingADance"].ToString().Trim();
        Origin_IPad = iniFile["URL"]["Origin_IPad"].ToString().Trim();
        Origin_ReviveLikeADog = iniFile["URL"]["Origin_ReviveLikeADog"].ToString().Trim();

        Origin_GalUseGirl = iniFile["URL"]["Origin_GalUseGirl"].ToString().Trim();
        Origin_BangOff = iniFile["URL"]["Origin_BangOff"].ToString().Trim();
        Origin_TwistedLove = iniFile["URL"]["Origin_TwistedLove"].ToString().Trim();
        Origin_Waklio = iniFile["URL"]["Origin_Waklio"].ToString().Trim();

        // Remix URL
        ReMix_Rewind = iniFile["URL"]["ReMix_Rewind"].ToString().Trim();
        ReMix_WinterSpring = iniFile["URL"]["ReMix_WinterSpring"].ToString().Trim();

        ReMix_NobleLick = iniFile["URL"]["ReMix_NobleLick"].ToString().Trim();
        ReMix_Wakaloid = iniFile["URL"]["ReMix_Wakaloid"].ToString().Trim();
        ReMix_WakGoodAroma100 = iniFile["URL"]["ReMix_WakGoodAroma100"].ToString().Trim();
        ReMix_AvantGarde = iniFile["URL"]["ReMix_AvantGarde"].ToString().Trim();

        ReMix_YouHi = iniFile["URL"]["ReMix_YouHi"].ToString().Trim();
        ReMix_Gotterfly = iniFile["URL"]["ReMix_Gotterfly"].ToString().Trim();
        ReMix_KingADance = iniFile["URL"]["ReMix_KingADance"].ToString().Trim();
        ReMix_IPad = iniFile["URL"]["ReMix_IPad"].ToString().Trim();
        ReMix_ReviveLikeADog = iniFile["URL"]["ReMix_ReviveLikeADog"].ToString().Trim();

        ReMix_GalUseGirl = iniFile["URL"]["ReMix_GalUseGirl"].ToString().Trim();
        ReMix_BangOff = iniFile["URL"]["ReMix_BangOff"].ToString().Trim();
        ReMix_TwistedLove = iniFile["URL"]["ReMix_TwistedLove"].ToString().Trim();
        ReMix_Waklio = iniFile["URL"]["ReMix_Waklio"].ToString().Trim();

        // BGM
        BGM_01 = iniFile["URL"]["BGM_01"].ToString().Trim();
        BGM_02 = iniFile["URL"]["BGM_02"].ToString().Trim();
        BGM_03 = iniFile["URL"]["BGM_03"].ToString().Trim();


        //-------------------- SYSTEM --------------------------------

        DEVKEY = iniFile["SYSTEM"]["DEVKEY"].ToString().Trim();
        GAMEMODE = iniFile["SYSTEM"]["GAMEMODE"].ToString().Trim();
        AUTOMODE = iniFile["SYSTEM"]["AUTOMODE"].ToString().Trim();

        //------------------------- Shop > Video ------------------------
        CREDIT = iniFile["URL"]["CREDIT"].ToString().Trim();
    }
}
