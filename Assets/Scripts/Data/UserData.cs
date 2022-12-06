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
    // 게임 관련 글로벌 데이터
    // 게임 버전
    public string version = string.Empty;
    // 총 플레이 타임
    public string totalPlayTime = string.Empty;
    // 첫 플레이 타임
    public string firstPlayTime = string.Empty;
    // 마지막 플레이 타임
    public string lastPlayTime = string.Empty;

    // 설정 관련 글로벌 데이터
    public SettingData settingData = new SettingData();

    // 상점 관련 글로벌 데이터
    public ShopData shopData = new ShopData();

    // 게임 관련 글로벌 데이터
    public GameData gameData = new GameData();
}

[Serializable]
// 설정 관련 글로벌 데이터
public class SettingData : DataBase
{
    // 배경음 볼륨
    public float BGMValue = new float();
    // 환경음 볼륨
    public float SFXValue = new float();
    // 키 설정 구분 > Integration : 통합, Separation : 분리
    public string keyDivision = string.Empty;
    // 키 설정 구분 > 분리 > 안쪽 이동 키 배열
    public string[] innerOperationKey = new string[4];
    // 키 설정 구분 > 분리 > 바깥 이동 키 배열
    public string[] outerOperationKey = new string[4];
}

[Serializable]
// 상점 관련 글로벌 데이터
public class ShopData : DataBase
{
    // 스킨 해금 여부 배열
    public string[] skinUnLockYn = new string[DataManager.dataSkinCount];
    // 스킨 사용 여부 배열
    public string[] skinUsingYn = new string[DataManager.dataSkinCount];
    // 스킬 해금 여부 배열
    public string[] skillUnLockYn = new string[DataManager.dataSkillCount];
    // 스킬 사용 여부 배열
    public string[] skillUsingYn = new string[DataManager.dataSkillCount];
}

[Serializable]
// 게임 관련 글로벌 데이터
public class GameData : DataBase
{
    // 스테이지 클리어 수
    public int clearStageCount = new int();
    // 앨범 별 스테이지 클리어 여부 배열
    public string[] album1ClearYn = new string[DataManager.dataAlbum1StageCount];
    public string[] album2ClearYn = new string[DataManager.dataAlbum2StageCount];
    public string[] album3ClearYn = new string[DataManager.dataAlbum3StageCount];
    public string[] album4ClearYn = new string[DataManager.dataAlbum4StageCount];
    // 앨범 별 스테이지 진행률 배열
    public int[] album1ProgressRate = new int[DataManager.dataAlbum1StageCount];
    public int[] album2ProgressRate = new int[DataManager.dataAlbum2StageCount];
    public int[] album3ProgressRate = new int[DataManager.dataAlbum3StageCount];
    public int[] album4ProgressRate = new int[DataManager.dataAlbum4StageCount];
    // 앨범 별 스테이지 클리어 시 데스 수 배열
    public int[] album1DeathCount = new int[DataManager.dataAlbum1StageCount];
    public int[] album2DeathCount = new int[DataManager.dataAlbum2StageCount];
    public int[] album3DeathCount = new int[DataManager.dataAlbum3StageCount];
    public int[] album4DeathCount = new int[DataManager.dataAlbum4StageCount];
    // 앨범 별 스테이지 클리어 시 사용 아이템 목록 배열
    public string[] ablum1UsingItem = new string[DataManager.dataAlbum1StageCount];
    public string[] ablum2UsingItem = new string[DataManager.dataAlbum2StageCount];
    public string[] ablum3UsingItem = new string[DataManager.dataAlbum3StageCount];
    public string[] ablum4UsingItem = new string[DataManager.dataAlbum4StageCount];
}

[Serializable]
public class JsonUserData : DataBase
{
    public UserData data = new UserData();
}

