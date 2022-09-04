using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Album", menuName = "Music/Album")]
public class AlbumData : Music
{
    [SerializeField]
    private ALBUM m_albumKind;

    public ALBUM ALBUM => m_albumKind;
}
