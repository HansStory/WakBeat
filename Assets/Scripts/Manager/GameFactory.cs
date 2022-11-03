using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFactory : MonoBehaviourSingleton<GameFactory>
{
    [SerializeField] private GameObject[] album1GameList;
    [SerializeField] private GameObject[] album2GameList;
    [SerializeField] private GameObject[] album3GameList;
    [SerializeField] private GameObject[] album4GameList;

    [SerializeField] private Transform creatGameBase;

    private GameObject game;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    public void CreateGame()
    {
        if (game == null)
        {
            game = GameObject.Instantiate(album1GameList[1].gameObject, creatGameBase);
        }
    }

    public void DistroyGame()
    {
        if (game != null)
        {
            Destroy(game);
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
