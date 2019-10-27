﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class AddCustomName : MonoBehaviourPun
{

    private string newName;

    public Text nametag;

    List<GameObject> players = new List<GameObject>();

    [PunRPC]
    void Start()
    {
        
        foreach(GameObject rocket in GameObject.FindGameObjectsWithTag("RocketTag"))
        {
            players.Add(rocket);
            rocket.name = PhotonNetwork.NickName;

        }

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
        Debug.Log("ChangeName");
        foreach(Player player in PhotonNetwork.PlayerList)
        {
            
        }

        for (int i = 0; i < PhotonNetwork.CountOfPlayers; i++)
        {
            if (PhotonNetwork.LocalPlayer == PhotonNetwork.PlayerList[i])
            {
                
            }
        }
        /*
        Debug.Log("[Change Name Called]");
        newName = (string)PhotonNetwork.LocalPlayer.CustomProperties["CustomPlayerName"];
        Debug.Log("PhotonNetwork Name:"+PhotonNetwork.LocalPlayer.CustomProperties["CustomPlayerName"]);   
        gameObject.name = newName;*/

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
