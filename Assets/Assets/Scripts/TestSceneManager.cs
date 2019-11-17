using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TestSceneManager : MonoBehaviourPun
{
    public GameObject player;
    public GameObject obsPlayer;
    public bool useObsPlayer;

    void Start()
    {
        PhotonNetwork.OfflineMode = true;
        GameManagerScript.gameIsInProgress = true;
        Debug.Log(GameManagerScript.preparingToStart + " " + GameManagerScript.gameIsInProgress + " " + GameManagerScript.gameHasEnded);
        if (useObsPlayer)
        {
            Instantiate(obsPlayer, gameObject.transform.position, gameObject.transform.rotation);
        }
        else
        {
            Instantiate(player, gameObject.transform.position, gameObject.transform.rotation);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
