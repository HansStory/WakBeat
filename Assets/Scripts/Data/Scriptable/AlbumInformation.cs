using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SetupAlbumInfo", menuName = "ScriptableObjects/SetupAlbumInfo", order = 2)]
public class AlbumInformation : ScriptableObject
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
    [Header("[ First Album Music List ]")]
    public GameObject[] FirstAlbumMusicLists;

    [Space(10)]
    [Header("[ Second Album Music List ]")]
    public GameObject[] SecondAlbumMusicLists;

    [Space(10)]
    [Header("[ Third Album Music List ]")]
    public GameObject[] ThirdAlbumMusicLists;

    [Space(10)]
    [Header("[ Forth Album Music List ]")]
    public GameObject[] ForthAlbumMusicLists;
}
