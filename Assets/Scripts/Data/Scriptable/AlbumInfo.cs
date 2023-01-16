using UnityEngine;

[CreateAssetMenu(fileName = "SetupAlbumInfo", menuName = "ScriptableObjects/SetupAlbumInfo", order = 2)]
public class AlbumInfo : ScriptableObject
{
    [Header("[ Album List (Prefabs) ]")]
    public GameObject[] AlbumLists;

    [Space(10)]
    [Header("[ Album BackGround Images ]")]
    public Sprite[] AlbumBackgournds;

    [Space(10)]
    [Header("[ Album Circle Images ]")]
    public Sprite[] AlbumCircles;

    [Space(10)]
    [Header("[ Album Title Images ]")]
    public Sprite[] AlbumTitles;

    [Space(10)]
    [Header("[ Stage Icon Images ]")]
    public Sprite[] StageIcons;

    [Space(10)]
    [Header("[ First Album Music Information ]")]
    public Sprite[] FirstAlbumMusicBackground;
    public Sprite[] FirstAlbumMusicCircle;
    public Sprite[] FirstAlbumMusicLevel;

    [Space(10)]
    [Header("[ Second Album Music Information ]")]
    public Sprite[] SecondAlbumMusicBackground;
    public Sprite[] SecondAlbumMusicCircle;
    public Sprite[] SecondAlbumMusicLevel;

    [Space(10)]
    [Header("[ Third Album Music Information ]")]
    public Sprite[] ThirdAlbumMusicBackground;
    public Sprite[] ThirdAlbumMusicCircle;
    public Sprite[] ThirdAlbumMusicLevel;

    [Space(10)]
    [Header("[ Forth Album Music Information ]")]
    public Sprite[] ForthAlbumMusicBackground;
    public Sprite[] ForthAlbumMusicCircle;
    public Sprite[] ForthAlbumMusicLevel;

    [Space(10)]
    [Header("[ Fifth Album Music Information ]")]
    public Sprite[] FifthAlbumMusicBackground;
    public Sprite[] FifthAlbumMusicCircle;
    public Sprite[] FifthAlbumMusicLevel;

    [Space(10)]
    public Sprite[] StageCount1;
    public Sprite[] StageCount2;
    public Sprite[] StageCount3;
    public Sprite[] StageCount4;
    public Sprite[] StageCount5;


    [Space(10)]
    [Header("[ Album Infomation Image ]")]
    public Sprite[] AlbumInfomationImage;

    [Space(10)]
    [Header("[ Music Banner Image ]")]
    public Sprite[] Album1MusicBannerImage;
    public Sprite[] Album2MusicBannerImage;
    public Sprite[] Album3MusicBannerImage;
    public Sprite[] Album4MusicBannerImage;
    public Sprite[] Album5MusicBannerImage;
}
