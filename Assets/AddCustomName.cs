using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class AddCustomName : MonoBehaviourPun
{

    private string newName;

    public Text nametag;

    [PunRPC]
    void Start()
    {
        Debug.Log("PhotonNetwork.Nickname"+PhotonNetwork.NickName);
        Debug.Log("LocalPlayer.Nickname: "+PhotonNetwork.LocalPlayer.NickName);

        Debug.Log("Start1");
        if (!photonView.IsMine)
        {
            Debug.Log("Start2");
            
        }
        else if (photonView.IsMine)
        {
            Debug.Log("Start3");
           // GetComponent<PhotonView>().RPC("ChangeName", RpcTarget.MasterClient);
        }
    }

    [PunRPC]
    private void ChangeName()
    {
        Debug.Log("[Change Name Called]");
        newName = (string)PhotonNetwork.LocalPlayer.CustomProperties["CustomPlayerName"];
        Debug.Log("PhotonNetwork Name:"+PhotonNetwork.LocalPlayer.CustomProperties["CustomPlayerName"]);   
        gameObject.name = newName;

        PhotonNetwork.LocalPlayer.NickName = newName;
        Debug.Log("Nickname: "+PhotonNetwork.LocalPlayer.NickName);
    }


    //AidenStudiosTutorialHere

    void Awake()
    {
        //photonView.RPC("rpcShet", RpcTarget.All, PhotonNetwork.NickName);
    }

    [PunRPC]
    public void updateName(string name)
    {
        nametag.text = name;
    }
}
