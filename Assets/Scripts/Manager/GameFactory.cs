using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFactory : MonoBehaviourSingleton<GameFactory>
{
    [SerializeField] private GameObject[] album1StageList;
    [SerializeField] private GameObject[] album2StageList;
    [SerializeField] private GameObject[] album3StageList;
    [SerializeField] private GameObject[] album4StageList;

    [SerializeField] private Transform creatGameBase;

    private GameObject stage = null;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    public void CreateStage()
    {
        switch (GlobalState.Instance.AlbumIndex)
        {
            case (int)GlobalData.ALBUM.ISEDOL:
                stage = GameObject.Instantiate(album1StageList[GlobalState.Instance.StageIndex], creatGameBase);
                break;
            case (int)GlobalData.ALBUM.CONTEST:
                stage = GameObject.Instantiate(album2StageList[GlobalState.Instance.StageIndex], creatGameBase);
                break;
            case (int)GlobalData.ALBUM.GOMIX:
                stage = GameObject.Instantiate(album3StageList[GlobalState.Instance.StageIndex], creatGameBase);
                break;
            case (int)GlobalData.ALBUM.WAKALOID:
                stage = GameObject.Instantiate(album4StageList[GlobalState.Instance.StageIndex], creatGameBase);
                break;
        }
    }

    public void DistroyStage()
    {
        if (stage != null)
        {
            SoundManager.Instance.ForceAudioStop();
            Destroy(stage);
        }
    }

    private void OnEnable()
    {

    }

    private void OnDisable()
    {

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
