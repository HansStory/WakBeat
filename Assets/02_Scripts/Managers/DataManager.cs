using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    private const string KEY_SFX = "SoundFX";
    private const string KEY_MUSIC = "Music";
    private const string KEY_KEYTYPE = "KeyType";
    private const string KEY_USER = "UserData";

    #region DATA
    private UserData m_userData = null;
    #endregion

    [SerializeField]
    private Sprite[] m_backgroundSpriteArr;
    [SerializeField]
    private Sprite[] m_circleSpriteArr;
    private int m_backgroundIndex = 0;
    public int BackgroundIndex
    {
        get => m_backgroundIndex;
        //Temp
        set { m_backgroundIndex = Mathf.Clamp(value, 0, m_backgroundSpriteArr.Length - 1); }
    }

    public float SoundFX
    {
        get => PlayerPrefs.GetFloat(KEY_SFX, 1f);
        set
        {
            value = Mathf.Clamp01(value);
            PlayerPrefs.SetFloat(KEY_SFX, value);
        }
    }

    public float Music
    {
        get => PlayerPrefs.GetFloat(KEY_MUSIC, 1f);
        set
        {
            value = Mathf.Clamp01(value);
            PlayerPrefs.SetFloat(KEY_MUSIC, value);
        }
    }

    public KEYTYPE KeyType
    {
        get => (KEYTYPE)PlayerPrefs.GetInt(KEY_KEYTYPE, (int)KEYTYPE.COMBINE);
        set
        {
            value = (KEYTYPE)Mathf.Clamp((int)value, 0, (int)KEYTYPE.END - 1);
            PlayerPrefs.SetInt(KEY_KEYTYPE, (int)value);
        }
    }

    protected override void Init()
    {
        if (!PlayerPrefs.HasKey(KEY_USER))
        {
            CreateData();
        }
        else
        {
            Debug.LogWarning("Laod");
            LoadData();
        }
    }

    private void CreateData()
    {
        m_userData = new UserData();
        PlayerPrefs.SetString(KEY_USER, JsonUtility.ToJson(m_userData));
    }

    private void LoadData()
    {
        string json = PlayerPrefs.GetString(KEY_USER);
        m_userData = JsonUtility.FromJson<UserData>(json);
    }

    public Sprite GetBackgroundSprite()
    {
        return m_backgroundSpriteArr[m_backgroundIndex];
    }

    public Sprite GetCircleSprite(CHANGESIDE cs)
    {
        int index = BackgroundIndex + (int)cs;
        index = MyUtil.RepeatIndex(index, m_circleSpriteArr.Length);
        return m_circleSpriteArr[index];
    }

    public HIDE GetHideSide()
    {
        if(BackgroundIndex == m_backgroundSpriteArr.Length - 1)
        {
            return HIDE.RIGHT;
        }
        else if(BackgroundIndex == 0)
        {
            return HIDE.LEFT;
        }
        else
        {
            return HIDE.NONE;
        }
    }
}
