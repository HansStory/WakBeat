public class GlobalData : MonoBehaviourSingleton<GlobalData>
{
    public ScriptInformation Information;
    public AlbumInfo Album;
    public AnimCurve AnimCurve;

    public enum STAGE
    {
        STAGE1,
        STAGE2,
        STAGE3,
        STAGE4,
        STAGE5,
    }

    public enum ALBUM
    {
        ISEDOL,
        CONTEST,
        GOMIX,
        WAKALOID,
    }

    public enum UIMODE
    {
        INTRO,
        MAIN,
        SELECT_ALBUM,
        SELECT_MUSIC,
        GAME,
        RESULT,
    }
}
