using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class UFOsummon : MonoBehaviourPun, IPunObservable
{
    private bool UFOhasActivated;
    private int totalAmountOfHealtReference = GameManagerScript.totalAmountOfHealth;

    void Start()
    {
        UFOhasActivated = false;   
    }

    void Update()
    {
        if (GameManagerScript.gameIsInProgress && GameManagerScript.totalAmountOfHealth == totalAmountOfHealtReference-3)
        {
            UFOmanager.MoveUFO = true;

        }
        else if (GameManagerScript.gameIsInProgress && GameManagerScript.totalAmountOfHealth == totalAmountOfHealtReference - 5)
        {
            UFOmanager.MoveUFO = true;
        }
        else if (GameManagerScript.gameIsInProgress && GameManagerScript.totalAmountOfHealth == totalAmountOfHealtReference - 7)
        {
            UFOmanager.MoveUFO = true;

        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting == true)
        {
            stream.SendNext(UFOhasActivated);
            stream.SendNext(totalAmountOfHealtReference);
        }
        else
        {
            UFOhasActivated = (bool)stream.ReceiveNext();
            totalAmountOfHealtReference = (int)stream.ReceiveNext();
        }
    }



}
