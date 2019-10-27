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
        Debug.Log("Start1");
        if (!photonView.IsMine)
        {
            Debug.Log("Start2");
            
        }
        else if (photonView.IsMine)
        {
            Debug.Log("Start3");
            GetComponent<PhotonView>().RPC("ChangeName", RpcTarget.MasterClient);
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
