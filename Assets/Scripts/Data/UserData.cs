using System;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;

public interface IUserData
{
    string ToJson(bool intented);
    void UpdateFromJson(string jsonData);
}

[Serializable]
public class DataBase : IUserData
{
    public virtual string ToJson(bool intented = false)
    {
        return JsonUtility.ToJson(this);
    }

    public virtual void UpdateFromJson(string jsonData)
    {
        JsonUtility.FromJsonOverwrite(jsonData, this);
    }
}

// ------------------------------------------------------------------------
[Serializable]
public enum BallType
{
    BlackBall,
    BlueBall,
    PeridotGreenBall,
    OrangeBall,
}


[Serializable]
public class UserData
{
    public string fileName;
    public string version;
    public string playTime;
    public string dateTime;
    public int coin;
    //public string currentBall;
    //public string[] InnerOperationKey;
    //public string[] OuterOperationKey;
    public int clearStageCount;
    public int statusAlbum_01;
    public int statusAlbum_02;
    public int statusAlbum_03;
    public int statusAlbum_04;

    // 설정 관련 글로벌 데이터
    // 배경음 크기
    public float BGMValue;
    // 효과음 크기
    public float SFXValue;
    // 키 설정 구분 > Integration : 통합, Separation : 분리
    public string KeyDivision;
    // 키 설정 구분 > 분리 > 안쪽 이동 
    public string[] InnerOperationKey = new string[4];
    // 키 설정 구분 > 분리 > 바깥 이동
    public string[] OuterOperationKey = new string[4];

    // 상점 관련 글로벌 데이터
    // 스킨 개수
    public int SkinCount;
    // 스킬 개수
    public int SkillCount;

    // 스킨 관련 데이터
    // 현재 볼 종류 > Black, Blue, Green, Orange
    public string currentBall;
}

[Serializable]
public class JsonUserData : DataBase
{
    public UserData data = new UserData();
}

