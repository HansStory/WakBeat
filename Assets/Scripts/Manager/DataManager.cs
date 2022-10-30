using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using DG.Tweening.Plugins.Core.PathCore;
//using UnityEditorInternal;
using static UnityEngine.Rendering.DebugUI;
using UnityEditorInternal;

public class DataManager : MonoBehaviourSingleton<DataManager>
{
    // 글로벌 데이터 세팅
    static Boolean _FileYn;
    static int _ClearStageCount;

    // 설정 화면 변수
    static string[] _InnerOperationKey;
    static string[] _OuterOperationKey;
    static string _KeyDivision;
    static float? _BGMValue;
    static float? _SFXValue;
    // 상점 화면 변수
    static int _SkinCount = 4;
    static int _SkillCount = 5;
    static string[] _SkinUnLockYn;
    static string[] _SkinUsingYn;
    static int[] _SkinUnLockCondition;
    static string[] _SkillUnLockYn;
    static string[] _SkillUsingYn;
    static int[] _SkillUnLockCondition;
    static Boolean _ShopCompulsionActive;
    static Boolean _BackgroundProcActive;

    public static string GetUserData()
    {
        JsonUserData userData = new JsonUserData();

        userData.data.fileName = _fileName;
        userData.data.version = GlobalData.Instance.Information.Version;

        userData.data.playTime = GlobalState.Instance.PlayTime.ToString();
        userData.data.dateTime = DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss:tt"));
        userData.data.coin = 0;

        userData.data.ClearStageCount = 0;
        userData.data.statusAlbum_01 = 0;
        userData.data.statusAlbum_02 = 0;
        userData.data.statusAlbum_03 = 0;
        userData.data.statusAlbum_04 = 0;

        // 설정 화면 글로벌 데이터 세팅
        userData.data.BGMValue = _BGMValue ?? GlobalState.Instance.UserData.data.BGMValue;
        userData.data.SFXValue = _SFXValue ?? GlobalState.Instance.UserData.data.SFXValue;
        userData.data.KeyDivision = _KeyDivision ?? (GlobalState.Instance.UserData.data.KeyDivision ?? "Integration");
        userData.data.InnerOperationKey = _InnerOperationKey ?? GlobalState.Instance.UserData.data.InnerOperationKey;
        userData.data.OuterOperationKey = _OuterOperationKey ?? GlobalState.Instance.UserData.data.OuterOperationKey;

        // 상점 화면 글로벌 데이터 세팅
        userData.data.SkinCount = _SkinCount;
        userData.data.SkillCount = _SkillCount;
        userData.data.SkinUnLockYn = _SkinUnLockYn ?? GlobalState.Instance.UserData.data.SkinUnLockYn;
        userData.data.SkinUsingYn = _SkinUsingYn ?? GlobalState.Instance.UserData.data.SkinUsingYn;
        userData.data.SkillUnLockYn = _SkillUnLockYn ?? GlobalState.Instance.UserData.data.SkillUnLockYn;
        userData.data.SkillUsingYn = _SkillUsingYn ?? GlobalState.Instance.UserData.data.SkillUsingYn;

        return userData.ToJson();
    }

    static string path;
    static string _fileName = "save";

    public static void SaveUserData()
    {
        // Json 데이터 가져옴
        var userData = GetUserData();

        // 파일에 데이터 작성하여 저장
        File.WriteAllText(path + _fileName, userData);
    }

    public void LoadUserData()
    {
        if (File.Exists(path + _fileName))
        {
            // 파일에서 데이터 불러옴
            var userData = File.ReadAllText(path + _fileName);
            GlobalState.Instance.UserData = JsonUtility.FromJson<JsonUserData>(userData);

            // 글로벌 값 호출 즉시 파일에 저장 된 배경음/환경음 볼륨으로 제어
            SoundManager.Instance.CtrlBGMVolume(GlobalState.Instance.UserData.data.BGMValue);
            SoundManager.Instance.CtrlSFXVolume(GlobalState.Instance.UserData.data.SFXValue);

            // 파일 여부 확인
            _FileYn = true;
            GlobalState.Instance.UserData.data.FileYn = _FileYn;

            Debug.Log($"Load User Data : {userData}");
        } 
        else
        {
            // 파일 여부 확인
            _FileYn = false;
            GlobalState.Instance.UserData.data.FileYn = _FileYn;

            // 파일 없을 시
            SoundManager.Instance.CtrlBGMVolume(0.5f);
            SoundManager.Instance.CtrlSFXVolume(0.5f);
        }
    }

    private void Awake()
    {
        path = Application.dataPath + "/StreamingAssets/";
        LoadUserData();
    }

    // 파일 조회 여부
    public static Boolean SetFileYn
    {
        get { return _FileYn; }
        set { _FileYn = value; }
    }

    // 클리어 스테이지 수
    public static int SetClearStageCount
    {
        get { return _ClearStageCount; }
        set { _ClearStageCount = value; }
    }

    // 설정 > 키 설정 구분 값 세팅
    public static string SetKeyDivision
    {
        set { _KeyDivision = value; }
    }

    // 설정 > 배경음 크기 값 세팅
    public static float SetBGMValue
    {
        set { _BGMValue = value; }
    }

    // 설정 > 환경음 크기 값 세팅
    public static float SetSFXValue
    {
        set { _SFXValue = value; }
    }

    // 설정 > 키 설정 > 분리 > 안쪽 이동 값 배열 세팅
    public static string[] SetInnerOperationKey
    {
        set { _InnerOperationKey = value; }
    }

    // 설정 > 키 설정 > 분리 > 바깥 이동 값 배열 세팅
    public static string[] SetOuterOperationKey
    {
        set { _OuterOperationKey = value; }
    }

    // 상점 스킨 개수
    public static int SetSkinCount
    {
        get { return _SkinCount; }
        set { _SkinCount = value;  }
    }

    // 상점 스킬 개수
    public static int SetSkillCount
    {
        get { return _SkillCount; }
        set { _SkillCount = value;  }
    }

    // 상점 스킬 사용 여부
    public static string[] SetSkillUsingYn
    {
        set { _SkillUsingYn = value; }
    }

    // 상점 스킨 해금 여부
    public static string[] SetSkinUnLockYn
    {
        set { _SkinUnLockYn = value; }
    }

    // 상점 스킨 사용 여부
    public static string[] SetSkinUsingYn
    {
        set { _SkinUsingYn = value; }
    }

    // 상점 스킬 해금 요건
    public static int[] SetSkinUnLockCondition
    {
        set { _SkinUnLockCondition = value; }
    }

    // 상점 스킬 해금 여부
    public static string[] SetSkillUnLockYn
    {
        set { _SkillUnLockYn = value; }
    }

    // 상점 스킬 사용 여부
    public static Boolean SetShopCompulsionActive
    {
        get { return _ShopCompulsionActive; }
        set { _ShopCompulsionActive = value; }
    }

    // 상점 스킬 해금 요건
    public static int[] SetSkillUnLockCondition
    {
        set { _SkillUnLockCondition = value; }
    }

    // 상점 스킬 사용 여부
    public static Boolean SetBackgroundProcActive
    {
        get { return _BackgroundProcActive; }
        set { _BackgroundProcActive = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log($"JsonData = {DataManager.Instance.GetUserData()}");
        //SaveUserData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
