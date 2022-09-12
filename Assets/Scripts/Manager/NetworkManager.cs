using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkManager : MonoBehaviourSingleton<NetworkManager>
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    IEnumerator UnityWebRequestGet()
    {
        yield return null;
    }
}
