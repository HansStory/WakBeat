using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO : 나중에 Dictionary로 수정할 것.
public class UserData
{
    [SerializeField]
    private MusicData[] m_firstStageArr = new MusicData[(int)STAGEMUSIC.FIRST];
    [SerializeField]
    private MusicData[] m_secondStageArr = new MusicData[(int)STAGEMUSIC.SECOND];
    [SerializeField]
    private MusicData[] m_thirdStageArr = new MusicData[(int)STAGEMUSIC.THIRD];

    public UserData()
    {
        for(int i = 0; i < m_firstStageArr.Length; i++)
        {
            m_firstStageArr[i] = new MusicData();
        }

        for (int i = 0; i < m_secondStageArr.Length; i++)
        {
            m_secondStageArr[i] = new MusicData();
        }

        for (int i = 0; i < m_thirdStageArr.Length; i++)
        {
            m_thirdStageArr[i] = new MusicData();
        }
    }
}

[Serializable]
public class MusicData
{
    [SerializeField]
    private float m_progress;
    [SerializeField]
    private bool m_clear;
    [SerializeField]
    private string m_rank;

    public MusicData()
    {
        m_progress = 0f;
        m_clear = false;
        m_rank = string.Empty;
    }
}
