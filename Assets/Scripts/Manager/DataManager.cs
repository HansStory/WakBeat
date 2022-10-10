using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using DG.Tweening.Plugins.Core.PathCore;

public class DataManager : MonoBehaviourSingleton<DataManager>
{
    // To do : 안쪽 바깥쪽 키세팅 저장 방법 고민
    string[] innerOeprationKey;
    string[] outerOperationKey;

    public string GetUserData()
    {
        JsonUserData userData = new JsonUserData();

        userData.data.fileName = _fileName;
        userData.data.version = GlobalData.Instance.Information.Version;

        userData.data.playTime = GlobalState.Instance.PlayTime.ToString();
        userData.data.dateTime = DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss:tt"));
        userData.data.coin = 0;
        // To do : 안쪽 바깥쪽 키세팅 저장 방법 고민
        userData.data.InnerOperationKey = innerOeprationKey;
        userData.data.InnerOperationKey = outerOperationKey;

        userData.data.clearStageCount = 0;
        userData.data.statusAlbum_01 = 0;
        userData.data.statusAlbum_02 = 0;
        userData.data.statusAlbum_03 = 0;
        userData.data.statusAlbum_04 = 0;

        return userData.ToJson();
    }


    string path;
    string _fileName = "save";

    public void SaveUserData()
    {
        var userData = GetUserData();

        File.WriteAllText(path + _fileName, userData);
    }

    public void LoadUserData()
    {
        if (File.ReadAllText(path + _fileName) != null)
        {
            var userData = File.ReadAllText(path + _fileName);
            GlobalState.Instance.UserData = JsonUtility.FromJson<JsonUserData>(userData);

            Debug.Log($"Load User Data : {userData}");
        }
    }

    private void Awake()
    {
        path = Application.dataPath + "/StreamingAssets/";
        LoadUserData();
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
