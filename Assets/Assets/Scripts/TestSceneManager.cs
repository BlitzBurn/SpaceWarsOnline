using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TestSceneManager : MonoBehaviourPun
{
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.OfflineMode = true;
        GameManagerScript.gameIsInProgress = true;
        Debug.Log(GameManagerScript.preparingToStart + " " + GameManagerScript.gameIsInProgress + " " + GameManagerScript.gameHasEnded);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
