using UnityEngine;

[CreateAssetMenu(fileName = "SetupResultInfo", menuName = "ScriptableObjects/SetupResultInfo", order = 5)]
public class ResultInfo : ScriptableObject
{
    [Header("[ First Album Thumnail ]")]
    public Sprite[] FirstAlbumThumnails;
    [Header("[ First Album Title ]")]
    public Sprite[] FirstAlbumTitles;

    [Space(10)]
    [Header("[ Second Album Thumnail ]")]
    public Sprite[] SecondAlbumThumnails;
    [Header("[ Second Album Title ]")]
    public Sprite[] SecondAlbumTitles;

    [Space(10)]
    [Header("[ Third Album Thumnail ]")]
    public Sprite[] ThirdAlbumThumnails;
    [Header("[ Third Album Title ]")]
    public Sprite[] ThirdAlbumTitles;

    [Space(10)]
    [Header("[ Fourth Album Thumnail ]")]
    public Sprite[] FourthAlbumThumnails;
    [Header("[ Fourth Album Title ]")]
    public Sprite[] FourthAlbumTitles;

    [Space(10)]
    public Sprite StarOn;
    public Sprite StarOff;
}
