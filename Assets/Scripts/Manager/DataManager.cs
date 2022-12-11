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
    static int[] _album1ProgressRate;
    static int[] _album2ProgressRate;
    static int[] _album3ProgressRate;
    static int[] _album4ProgressRate;
    // 스테이지 별 클리어 시 데스 수 
    static int[] _album1DeathCount;
    static int[] _album2DeathCount;
    static int[] _album3DeathCount;
    static int[] _album4DeathCount;
    // 스테이지 별 클리어 시 사용 아이템 배열
    static string[] _ablum1UsingItem;
    static string[] _ablum2UsingItem;
    static string[] _ablum3UsingItem;
    static string[] _ablum4UsingItem;

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
            settingData.BGMValue = _BGMValue ?? 0.5f;
            settingData.SFXValue = _SFXValue ?? 0.5f;
            settingData.keyDivision = _keyDivision ?? "Integration";

            // 상점 관련 데이터
            if (null == _skinUnLockYn || _skinUnLockYn.Length <= 0 || (Array.IndexOf(_skinUnLockYn, "N") < 0 && Array.IndexOf(_skinUnLockYn, "Y") < 0)
                || null == _skinUsingYn || _skinUsingYn.Length <= 0 || (Array.IndexOf(_skinUsingYn, "N") < 0 && Array.IndexOf(_skinUnLockYn, "Y") < 0))
            {
                _skinUnLockYn = new string[_skinCount];
                _skinUsingYn = new string[_skinCount];

                for (int i = 0; i < _skinCount; i++)
                {
                    if(i == 0)
                    {
                        _skinUnLockYn[i] = "Y";
                        _skinUsingYn[i] = "Y";
                    }
                    else
                    {
                        _skinUnLockYn[i] = "N";
                        _skinUsingYn[i] = "N";
                    }
                }
            }
            if(null == _skillUnLockYn || _skillUnLockYn.Length <= 0 || (Array.IndexOf(_skillUnLockYn, "N") < 0 && Array.IndexOf(_skillUnLockYn, "Y") < 0)
                || null == _skillUsingYn || _skillUsingYn.Length <= 0 || (Array.IndexOf(_skillUsingYn, "N") < 0 && Array.IndexOf(_skillUsingYn, "Y") < 0))
            {
                _skillUnLockYn = new string[_skillCount];
                _skillUsingYn = new string[_skillCount];

                for (int i = 0; i < _skillCount; i++)
                {
                    _skillUnLockYn[i] = "N";
                    _skillUsingYn[i] = "N";
                }
            }
            shopData.skinUnLockYn = _skinUnLockYn;
            shopData.skinUsingYn = _skinUsingYn;
            shopData.skillUnLockYn = _skillUnLockYn;
            shopData.skillUsingYn = _skillUsingYn;
        }
        else
        {
            // 설정 관련 데이터
            settingData.BGMValue = _BGMValue ?? globalUserData.settingData.BGMValue;
            settingData.SFXValue = _SFXValue ?? globalUserData.settingData.SFXValue;
            settingData.keyDivision = _keyDivision ?? globalUserData.settingData.keyDivision;

            // 상점 관련 데이터
            shopData.skinUnLockYn = _skinUnLockYn ?? globalUserData.shopData.skinUnLockYn;
            shopData.skinUsingYn = _skinUsingYn ?? globalUserData.shopData.skinUsingYn;
            shopData.skillUnLockYn = _skillUnLockYn ?? globalUserData.shopData.skillUnLockYn;
            shopData.skillUsingYn = _skillUsingYn ?? globalUserData.shopData.skillUsingYn;
        }

        // 설정 관련 데이터
        settingData.innerOperationKey = _innerOperationKey ?? globalUserData.settingData.innerOperationKey;
        settingData.outerOperationKey = _outerOperationKey ?? globalUserData.settingData.outerOperationKey;

        // 인게임 관련 데이터
        gameData.clearStageCount = _clearStageCount ?? globalUserData.gameData.clearStageCount;
        gameData.album1ClearYn = _album1ClearYn ?? globalUserData.gameData.album1ClearYn;
        gameData.album2ClearYn = _album2ClearYn ?? globalUserData.gameData.album2ClearYn;
        gameData.album3ClearYn = _album3ClearYn ?? globalUserData.gameData.album3ClearYn;
        gameData.album4ClearYn = _album4ClearYn ?? globalUserData.gameData.album4ClearYn;
        gameData.album1ProgressRate = _album1ProgressRate ?? globalUserData.gameData.album1ProgressRate;
        gameData.album2ProgressRate = _album2ProgressRate ?? globalUserData.gameData.album2ProgressRate;
        gameData.album3ProgressRate = _album3ProgressRate ?? globalUserData.gameData.album3ProgressRate;
        gameData.album4ProgressRate = _album4ProgressRate ?? globalUserData.gameData.album4ProgressRate;
        gameData.album1DeathCount = _album1DeathCount ?? globalUserData.gameData.album1DeathCount;
        gameData.album2DeathCount = _album2DeathCount ?? globalUserData.gameData.album2DeathCount;
        gameData.album3DeathCount = _album3DeathCount ?? globalUserData.gameData.album3DeathCount;
        gameData.album4DeathCount = _album4DeathCount ?? globalUserData.gameData.album4DeathCount;
        gameData.ablum1UsingItem = _ablum1UsingItem ?? globalUserData.gameData.ablum1UsingItem;
        gameData.ablum2UsingItem = _ablum2UsingItem ?? globalUserData.gameData.ablum2UsingItem;
        gameData.ablum3UsingItem = _ablum3UsingItem ?? globalUserData.gameData.ablum3UsingItem;
        gameData.ablum4UsingItem = _ablum4UsingItem ?? globalUserData.gameData.ablum4UsingItem;

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
            // 파일에서 데이터 불러옴
            var jsonUserData = File.ReadAllText(_path + _fileName);
            GlobalState.Instance.UserData = JsonUtility.FromJson<JsonUserData>(jsonUserData);

            // 글로벌 값 호출 즉시 파일에 저장 된 배경음/환경음 볼륨으로 제어
            SoundManager.Instance.CtrlBGMVolume(userData.settingData.BGMValue);
            SoundManager.Instance.CtrlSFXVolume(userData.settingData.SFXValue);

            // 파일 여부 확인
            _fileYn = true;
            Debug.Log($"Load User Data : {jsonUserData}");
        } 
        else
        {
            // 파일 여부 확인
            _fileYn = false;

            // 파일 없을 시 기본 값 세팅
            SoundManager.Instance.CtrlBGMVolume(0.5f);
            SoundManager.Instance.CtrlSFXVolume(0.5f);

            _skinUnLockYn = new string[_skinCount];
            _skinUsingYn = new string[_skinCount];
            _skillUnLockYn = new string[_skillCount];
            _skillUsingYn = new string[_skillCount];

            for (int i = 0; i < _skinCount; i++)
            {
                if (i == 0)
                {
                    _skinUnLockYn[i] = "Y";
                    _skinUsingYn[i] = "Y";
                }
                else
                {
                    _skinUnLockYn[i] = "N";
                    _skinUsingYn[i] = "N";
                }
            }

            for (int i = 0; i < _skillCount; i++)
            {
                _skillUnLockYn[i] = "N";
                _skillUsingYn[i] = "N";
            }

            userData.settingData.BGMValue = 0.5f;
            userData.settingData.SFXValue = 0.5f;
            userData.settingData.keyDivision = "Integration";

            userData.shopData.skinUnLockYn = _skinUnLockYn;
            userData.shopData.skinUsingYn = _skinUsingYn;
            userData.shopData.skillUnLockYn = _skillUnLockYn;
            userData.shopData.skillUsingYn = _skillUsingYn;
        }
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
    public static int[] dataAlbum1ProgressRate
    {
        get { return _album1ProgressRate; }
        set { _album1ProgressRate = value; }
    }

    // 앨범 2의 스테이지 별 진행률
    public static int[] dataAlbum2ProgressRate
    {
        get { return _album2ProgressRate; }
        set { _album2ProgressRate = value; }
    }

    // 앨범 3의 스테이지 별 진행률
    public static int[] dataAlbum3ProgressRate
    {
        get { return _album3ProgressRate; }
        set { _album3ProgressRate = value; }
    }

    // 앨범 4의 스테이지 별 진행률
    public static int[] dataAlbum4ProgressRate
    {
        get { return _album4ProgressRate; }
        set { _album4ProgressRate = value; }
    }

    // 스테이지 1의 클리어 시 데스 카운트
    public static int[] dataAlbum1DeathCount
    {
        get { return _album1DeathCount; }
        set { _album1DeathCount = value; }
    }

    // 스테이지 2의 클리어 시 데스 카운트
    public static int[] dataAlbum2DeathCount
    {
        get { return _album2DeathCount; }
        set { _album2DeathCount = value; }
    }

    // 스테이지 3의 클리어 시 데스 카운트
    public static int[] dataAlbum3DeathCount
    {
        get { return _album3DeathCount; }
        set { _album3DeathCount = value; }
    }

    // 스테이지 4의 클리어 시 데스 카운트
    public static int[] dataAlbum4DeathCount
    {
        get { return _album4DeathCount; }
        set { _album4DeathCount = value; }
    }

    // 스테이지 1의 클리어 시 사용 아이템 배열
    public static string[] dataAblum1UsingItem
    {
        get { return _ablum1UsingItem; }
        set { _ablum1UsingItem = value; }
    }

    // 스테이지 2의 클리어 시 사용 아이템 배열
    public static string[] dataAblum2UsingItem
    {
        get { return _ablum2UsingItem; }
        set { _ablum2UsingItem = value; }
    }

    // 스테이지 3의 클리어 시 사용 아이템 배열
    public static string[] dataAblum3UsingItem
    {
        get { return _ablum3UsingItem; }
        set { _ablum3UsingItem = value; }
    }

    // 스테이지 4의 클리어 시 사용 아이템 배열
    public static string[] dataAblum4UsingItem
    {
        get { return _ablum4UsingItem; }
        set { _ablum4UsingItem = value; }
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
