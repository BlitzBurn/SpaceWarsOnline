using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class AddCustomName : MonoBehaviourPun
{

    private string newName;

    [PunRPC]
    void Start()
    {

        if (photonView.IsMine)
        {
            //GetComponent<PhotonView>().RPC("ChangeName", RpcTarget.AllBuffered);
        }
        else if (!photonView.IsMine)
        {

        }
    }

    [PunRPC]
    private void ChangeName()
    {
        Debug.Log("[Change Name Called]");
        newName = (string)PhotonNetwork.LocalPlayer.CustomProperties["CustomPlayerName"];
        Debug.Log("PhotonNetwork Name:"+PhotonNetwork.LocalPlayer.CustomProperties["CustomPlayerName"]);   
        gameObject.name = newName;
        
    }
        
}
