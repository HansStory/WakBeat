public class GlobalData : MonoBehaviourSingleton<GlobalData>
{
    public ScriptInformation Information;
    public AlbumInfo Album;
    public BGMInfo BGM;

    public enum ALBUM
    {
        ISEDOL,
        CONTEST,
        GOMIX,
        WAKALOID,
    }

    public enum STAGE
    {
        STAGE1,
        STAGE2,
        STAGE3,
        STAGE4,
        STAGE5,
    }
}
