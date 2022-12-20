using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using DG.Tweening.Plugins;

public class DataManager : MonoBehaviourSingleton<DataManager>
{
    // 글로벌 데이터 설정 (불변)
    // 파일 관련 데이터
    // 파일 경로
    static string _path;
    // 파일 명
    static string _fileName = "save";
    // 파일 여부
    static Boolean _fileYn = false;
    // 상점 관련 데이터
    // 스킨 개수
    static int _skinCount = 4;
    // 스킬 개수
    static int _skillCount = 5;
    // 스킨 해금 조건 배열
    static int[] _skinUnLockCondition = { 1, 2, 3, 5, 7 };
    // 스킬 해금 조건 배열
    static int[] _skillUnLockCondition = { 1, 2, 3, 5, 7 };
    // 앨범 별 스테이지 수
    static int _album1StageCount = 2;
    static int _album2StageCount = 4;
    static int _album3StageCount = 5;
    static int _album4StageCount = 4;
    // 상점 강제 해금 사용 여부 > True : 강제 해금 사용 / False : 강제 해금 미사용
    static Boolean _shopCompulsionActive = true;
    // 인게임 관련 데이터
    // 배경 프로세스 진행 여부 > True : 진행 / False : 미진행(팝업 창 용)
    static Boolean _backgroundProcActive = true;

    // 글로벌 데이터 설정 (가변)
    // 설정 관련 데이터
    // 배경음 볼륨
    static float? _BGMValue;
    // 환경음 볼륨
    static float? _SFXValue;
    // 키 구분 값 > Integration : 통합, Separation : 분리
    static string _keyDivision;
    // 키 구분 > 분리 > 안쪽 이동 키 배열
    static string[] _innerOperationKey = new string[4];
    // 키 구분 > 분리 > 바깥 이동 키 배열
    static string[] _outerOperationKey = new string[4];
    // 상점 관련 데이터
    // 스킨 해금 여부 > Y : 해금, N : 미해금
    static string[] _skinUnLockYn;
    // 스킨 사용 여부 > Y : 사용, N : 미사용
    static string[] _skinUsingYn;
    // 스킬 해금 여부 > Y : 해금, N : 미해금
    static string[] _skillUnLockYn;
    // 스킬 사용 여부 > Y : 사용, N : 미사용
    static string[] _skillUsingYn;
    // 인게임 관련 데이터
    // 스테이지 클리어 수
    static int? _clearStageCount;
    // 앨범 별 스테이지 클리어 여부
    static string[] _album1ClearYn;
    static string[] _album2ClearYn;
    static string[] _album3ClearYn;
    static string[] _album4ClearYn;
    // 앨범 별 스테이지 진행률
    static int[] _album1StageProgressLine;
    static int[] _album2StageProgressLine;
    static int[] _album3StageProgressLine;
    static int[] _album4StageProgressLine;

    public static string GetUserData()
    {
        var globalUserData = GlobalState.Instance.UserData.data;

        JsonUserData userData = new JsonUserData();

        var settingData = userData.data.settingData;
        var shopData = userData.data.shopData;
        var gameData = userData.data.gameData;

        // 게임 정보 관련 데이터
        userData.data.version = GlobalData.Instance.Information.Version;
        userData.data.totalPlayTime = (userData.data.totalPlayTime + GlobalState.Instance.PlayTime).ToString();
        userData.data.firstPlayTime = _fileYn ? globalUserData.firstPlayTime : DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss:tt"));
        userData.data.lastPlayTime = DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss:tt"));

        // 최초 혹은 파일이 없을 때 글로벌 데이터 처리
        if (!_fileYn)
        {
            // 설정 관련 데이터
            settingData.BGMValue = DataManager.dataBGMValue ?? 0.5f;
            settingData.SFXValue = DataManager.dataSFXValue ?? 0.5f;
            settingData.keyDivision = DataManager.dataKeyDivision ?? "Integration";

            // 상점 관련 데이터
            if (null == DataManager.dataSkinUnLockYn || DataManager.dataSkinUnLockYn.Length <= 0 
                || (Array.IndexOf(DataManager.dataSkinUnLockYn, "N") < 0 && Array.IndexOf(DataManager.dataSkinUnLockYn, "Y") < 0)
                || null == DataManager.dataSkinUsingYn || DataManager.dataSkinUsingYn.Length <= 0 
                || (Array.IndexOf(DataManager.dataSkinUsingYn, "N") < 0 && Array.IndexOf(DataManager.dataSkinUnLockYn, "Y") < 0))
            {
                DataManager.dataSkinUnLockYn = new string[DataManager.dataSkinCount];
                DataManager.dataSkinUsingYn = new string[DataManager.dataSkinCount];

                for (int i = 0; i < DataManager.dataSkinCount; i++)
                {
                    if(i == 0)
                    {
                        DataManager.dataSkinUnLockYn[i] = "Y";
                        DataManager.dataSkinUsingYn[i] = "Y";
                    }
                    else
                    {
                        DataManager.dataSkinUnLockYn[i] = "N";
                        DataManager.dataSkinUsingYn[i] = "N";
                    }
                }
            }
            if(null == DataManager.dataSkillUnLockYn || DataManager.dataSkillUnLockYn.Length <= 0 
                || (Array.IndexOf(DataManager.dataSkillUnLockYn, "N") < 0 && Array.IndexOf(DataManager.dataSkillUnLockYn, "Y") < 0)
                || null == DataManager.dataSkillUsingYn || DataManager.dataSkillUsingYn.Length <= 0 
                || (Array.IndexOf(DataManager.dataSkillUsingYn, "N") < 0 && Array.IndexOf(DataManager.dataSkillUsingYn, "Y") < 0))
            {
                DataManager.dataSkillUnLockYn = new string[DataManager.dataSkillCount];
                DataManager.dataSkillUsingYn = new string[DataManager.dataSkillCount];

                for (int i = 0; i < dataSkillCount; i++)
                {
                    DataManager.dataSkillUnLockYn[i] = "N";
                    DataManager.dataSkillUsingYn[i] = "N";
                }
            }
            shopData.skinUnLockYn = DataManager.dataSkinUnLockYn;
            shopData.skinUsingYn = DataManager.dataSkinUsingYn;
            shopData.skillUnLockYn = DataManager.dataSkillUnLockYn;
            shopData.skillUsingYn = DataManager.dataSkillUsingYn;
        }
        else
        {
            // 설정 관련 데이터
            settingData.BGMValue = (float)DataManager.dataBGMValue;
            settingData.SFXValue = (float)DataManager.dataSFXValue;
            settingData.keyDivision = DataManager.dataKeyDivision;

            // 상점 관련 데이터
            shopData.skinUnLockYn = DataManager.dataSkinUnLockYn;
            shopData.skinUsingYn = DataManager.dataSkinUsingYn;
            shopData.skillUnLockYn = DataManager.dataSkillUnLockYn;
            shopData.skillUsingYn = DataManager.dataSkillUsingYn;
        }

        // 설정 관련 데이터
        settingData.innerOperationKey = DataManager.dataInnerOperationKey;
        settingData.outerOperationKey = DataManager.dataOuterOperationKey;

        // 인게임 관련 데이터
        gameData.clearStageCount = (int)DataManager.dataClearStageCount;
        gameData.album1ClearYn = DataManager.dataAlbum1ClearYn;
        gameData.album2ClearYn = DataManager.dataAlbum2ClearYn;
        gameData.album3ClearYn = DataManager.dataAlbum3ClearYn;
        gameData.album4ClearYn = DataManager.dataAlbum4ClearYn;
        gameData.album1StageProgressLine = DataManager.dataAlbum1StageProgressLine;
        gameData.album2StageProgressLine = DataManager.dataAlbum2StageProgressLine;
        gameData.album3StageProgressLine = DataManager.dataAlbum3StageProgressLine;
        gameData.album4StageProgressLine = DataManager.dataAlbum4StageProgressLine;

        return userData.ToJson();
    }

    public static void SaveUserData()
    {
        // Json 데이터 가져옴
        var userData = GetUserData();

        // 파일에 데이터 작성하여 저장
        File.WriteAllText(_path + _fileName, userData);
    }

    public void LoadUserData()
    {
        var userData = GlobalState.Instance.UserData.data;

        if (File.Exists(_path + _fileName))
        {
            // 파일 여부 확인
            _fileYn = true;

            // 파일에서 데이터 불러옴
            var jsonUserData = File.ReadAllText(_path + _fileName);
            GlobalState.Instance.UserData = JsonUtility.FromJson<JsonUserData>(jsonUserData);

            Debug.Log($"Load User Data : {jsonUserData}");

            // DataManager에 파일에서 가져온 데이터 넣기
            // 설정 데이터 세팅
            dataBGMValue = GlobalState.Instance.UserData.data.settingData.BGMValue;
            dataSFXValue = GlobalState.Instance.UserData.data.settingData.SFXValue;
            dataKeyDivision = GlobalState.Instance.UserData.data.settingData.keyDivision;
            dataInnerOperationKey = GlobalState.Instance.UserData.data.settingData.innerOperationKey;
            dataOuterOperationKey = GlobalState.Instance.UserData.data.settingData.outerOperationKey;

            // 상점 데이터 세팅
            dataSkinUnLockYn = GlobalState.Instance.UserData.data.shopData.skillUnLockYn;
            dataSkinUsingYn = GlobalState.Instance.UserData.data.shopData.skinUsingYn;
            dataSkillUnLockYn = GlobalState.Instance.UserData.data.shopData.skillUnLockYn;
            dataSkillUsingYn = GlobalState.Instance.UserData.data.shopData.skillUsingYn;

            // 인게임 관련 데이터
            dataClearStageCount = GlobalState.Instance.UserData.data.gameData.clearStageCount;
            dataAlbum1ClearYn = GlobalState.Instance.UserData.data.gameData.album1ClearYn;
            dataAlbum2ClearYn = GlobalState.Instance.UserData.data.gameData.album2ClearYn;
            dataAlbum3ClearYn = GlobalState.Instance.UserData.data.gameData.album3ClearYn;
            dataAlbum4ClearYn = GlobalState.Instance.UserData.data.gameData.album4ClearYn;
            dataAlbum1StageProgressLine = GlobalState.Instance.UserData.data.gameData.album1StageProgressLine;
            dataAlbum2StageProgressLine = GlobalState.Instance.UserData.data.gameData.album2StageProgressLine;
            dataAlbum3StageProgressLine = GlobalState.Instance.UserData.data.gameData.album3StageProgressLine;
            dataAlbum4StageProgressLine = GlobalState.Instance.UserData.data.gameData.album4StageProgressLine;
        } 
        else
        {
            // 파일 여부 확인
            _fileYn = false;

            // 파일 없을 시 기본 값 세팅
            dataBGMValue = 0.5f;
            dataSFXValue = 0.5f;
            dataKeyDivision = "Integration";
            dataInnerOperationKey = new string[4];
            dataOuterOperationKey = new string[4];

            dataSkinUnLockYn = new string[dataSkinCount];
            dataSkinUsingYn = new string[dataSkinCount];
            dataSkillUnLockYn = new string[dataSkillCount];
            dataSkillUsingYn = new string[dataSkillCount];

            dataClearStageCount = 0;
            dataAlbum1ClearYn = new string[dataAlbum1StageCount];
            dataAlbum2ClearYn = new string[dataAlbum2StageCount];
            dataAlbum3ClearYn = new string[dataAlbum3StageCount];
            dataAlbum4ClearYn = new string[dataAlbum4StageCount];
            dataAlbum1StageProgressLine = new int[dataAlbum1StageCount];
            dataAlbum2StageProgressLine = new int[dataAlbum2StageCount];
            dataAlbum3StageProgressLine = new int[dataAlbum3StageCount];
            dataAlbum4StageProgressLine = new int[dataAlbum4StageCount];

            for (int i = 0; i < dataSkinCount; i++)
            {
                if (i == 0)
                {
                    dataSkinUnLockYn[i] = "Y";
                    dataSkinUsingYn[i] = "Y";
                }
                else
                {
                    dataSkinUnLockYn[i] = "N";
                    dataSkinUsingYn[i] = "N";
                }
            }

            for (int i = 0; i < dataSkillCount; i++)
            {
                dataSkillUnLockYn[i] = "N";
                dataSkillUsingYn[i] = "N";
            }

            userData.settingData.BGMValue = (float)dataBGMValue;
            userData.settingData.SFXValue = (float)dataSFXValue;
            userData.settingData.keyDivision = dataKeyDivision;

            userData.shopData.skinUnLockYn = dataSkinUnLockYn;
            userData.shopData.skinUsingYn = dataSkinUsingYn;
            userData.shopData.skillUnLockYn = dataSkillUnLockYn;
            userData.shopData.skillUsingYn = dataSkillUsingYn;

            for (int i = 0; i < dataAlbum1StageCount; i++)
            {
                dataAlbum1ClearYn[i] = "N";
                dataAlbum1StageProgressLine[i] = 0;
            }
            for (int i = 0; i < dataAlbum2StageCount; i++)
            {
                dataAlbum2ClearYn[i] = "N";
                dataAlbum2StageProgressLine[i] = 0;
            }
            for (int i = 0; i < dataAlbum3StageCount; i++)
            {
                dataAlbum3ClearYn[i] = "N";
                dataAlbum3StageProgressLine[i] = 0;
            }
            for (int i = 0; i < dataAlbum4StageCount; i++)
            {
                dataAlbum4ClearYn[i] = "N";
                dataAlbum4StageProgressLine[i] = 0;
            }

            userData.gameData.clearStageCount = (int)dataClearStageCount;
            userData.gameData.album1ClearYn = dataAlbum1ClearYn;
            userData.gameData.album2ClearYn = dataAlbum2ClearYn;
            userData.gameData.album3ClearYn = dataAlbum3ClearYn;
            userData.gameData.album4ClearYn = dataAlbum4ClearYn;
            userData.gameData.album1StageProgressLine = dataAlbum1StageProgressLine;
            userData.gameData.album2StageProgressLine = dataAlbum2StageProgressLine;
            userData.gameData.album3StageProgressLine = dataAlbum3StageProgressLine;
            userData.gameData.album4StageProgressLine = dataAlbum4StageProgressLine;
        }

        // 글로벌 값 호출 즉시 파일에 저장 된 배경음/환경음 볼륨으로 제어
        SoundManager.Instance.CtrlBGMVolume((float)dataBGMValue);
        SoundManager.Instance.CtrlSFXVolume((float)dataSFXValue);
    }

    private void Awake()
    {
        _path = Application.dataPath + "/StreamingAssets/";
        LoadUserData();
    }

    // 파일 존재 여부
    public static Boolean dataFileYn
    {
        get { return _fileYn; }
        set { _fileYn = value; }
    }

    // 상점 강제 해금 사용 여부
    public static Boolean dataShopCompulsionActive
    {
        get { return _shopCompulsionActive; }
        set { _shopCompulsionActive = value; }
    }

    // 배경 프로세스 진행 가능 여부
    public static Boolean dataBackgroundProcActive
    {
        get { return _backgroundProcActive; }
        set { _backgroundProcActive = value; }
    }

    // 클리어 스테이지 수
    public static int? dataClearStageCount
    {
        get { return _clearStageCount; }
        set { _clearStageCount = value; }
    }

    // 설정 > 키 설정 구분 값 세팅
    public static string dataKeyDivision
    {
        get { return _keyDivision; }
        set { _keyDivision = value; }
    }

    // 설정 > 배경음 크기 값 세팅
    public static float? dataBGMValue
    {
        get { return _BGMValue; }
        set { _BGMValue = value; }
    }

    // 설정 > 환경음 크기 값 세팅
    public static float? dataSFXValue
    {
        get { return _SFXValue; }
        set { _SFXValue = value; }
    }

    // 설정 > 키 설정 > 분리 > 안쪽 이동 값 배열 세팅
    public static string[] dataInnerOperationKey
    {
        get { return _innerOperationKey; }
        set { _innerOperationKey = value; }
    }

    // 설정 > 키 설정 > 분리 > 바깥 이동 값 배열 세팅
    public static string[] dataOuterOperationKey
    {
        get { return _outerOperationKey; }
        set { _outerOperationKey = value; }
    }

    // 상점 스킨 개수
    public static int dataSkinCount
    {
        get { return _skinCount; }
        set { _skinCount = value;  }
    }

    // 상점 스킬 개수
    public static int dataSkillCount
    {
        get { return _skillCount; }
        set { _skillCount = value;  }
    }

    // 상점 스킬 사용 여부
    public static string[] dataSkillUsingYn
    {
        get { return _skillUsingYn; }
        set { _skillUsingYn = value; }
    }

    // 상점 스킨 해금 여부
    public static string[] dataSkinUnLockYn
    {
        get { return _skinUnLockYn; }
        set { _skinUnLockYn = value; }
    }

    // 상점 스킨 사용 여부
    public static string[] dataSkinUsingYn
    {
        get { return _skinUsingYn; }
        set { _skinUsingYn = value; }
    }

    // 상점 스킬 해금 요건
    public static int[] dataSkinUnLockCondition
    {
        get { return _skinUnLockCondition; }
        set { _skinUnLockCondition = value; }
    }

    // 상점 스킬 해금 여부
    public static string[]dataSkillUnLockYn
    {
        get { return _skillUnLockYn; }
        set { _skillUnLockYn = value; }
    }

    // 상점 스킬 해금 요건
    public static int[] dataSkillUnLockCondition
    {
        get { return _skillUnLockCondition; }
        set { _skillUnLockCondition = value; }
    }

    // 앨범 1 스테이지 개수
    public static int dataAlbum1StageCount
    {
        get { return _album1StageCount; }
        set { _album1StageCount = value; }
    }

    // 앨범 2 스테이지 개수
    public static int dataAlbum2StageCount
    {
        get { return _album2StageCount; }
        set { _album2StageCount = value; }
    }

    // 앨범 3 스테이지 개수
    public static int dataAlbum3StageCount
    {
        get { return _album3StageCount; }
        set { _album3StageCount = value; }
    }

    // 앨범 4 스테이지 개수
    public static int dataAlbum4StageCount
    {
        get { return _album4StageCount; }
        set { _album4StageCount = value; }
    }

    // 앨범 1의 스테이지 별 클리어 여부
    public static string[] dataAlbum1ClearYn
    {
        get { return _album1ClearYn; }
        set { _album1ClearYn = value; }
    }

    // 앨범 2의 스테이지 별 클리어 여부
    public static string[] dataAlbum2ClearYn
    {
        get { return _album2ClearYn; }
        set { _album2ClearYn = value; }
    }

    // 앨범 3의 스테이지 별 클리어 여부
    public static string[] dataAlbum3ClearYn
    {
        get { return _album3ClearYn; }
        set { _album3ClearYn = value; }
    }

    // 앨범 4의 스테이지 별 클리어 여부
    public static string[] dataAlbum4ClearYn
    {
        get { return _album4ClearYn; }
        set { _album4ClearYn = value; }
    }

    // 앨범 1의 스테이지 별 진행률
    public static int[] dataAlbum1StageProgressLine
    {
        get { return _album1StageProgressLine; }
        set { _album1StageProgressLine = value; }
    }

    // 앨범 2의 스테이지 별 진행률
    public static int[] dataAlbum2StageProgressLine
    {
        get { return _album2StageProgressLine; }
        set { _album2StageProgressLine = value; }
    }

    // 앨범 3의 스테이지 별 진행률
    public static int[] dataAlbum3StageProgressLine
    {
        get { return _album3StageProgressLine; }
        set { _album3StageProgressLine = value; }
    }

    // 앨범 4의 스테이지 별 진행률
    public static int[] dataAlbum4StageProgressLine
    {
        get { return _album4StageProgressLine; }
        set { _album4StageProgressLine = value; }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
