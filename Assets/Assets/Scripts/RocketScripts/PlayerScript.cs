﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


namespace SpaceWarsOnline
{
    public class PlayerScript : MonoBehaviourPun
    {
        private string newName;

        protected Rigidbody rocketRigidBody;
        protected Quaternion rocketRotation;

        private void Awake()
        {
            rocketRigidBody = GetComponent<Rigidbody>();
           
            if (!photonView.IsMine && GetComponent<RocketController>() != null && GetComponent<FireMissile>() !=null )
            {
                Debug.Log("Start called");
                Destroy(GetComponent<FireMissile>());
                Destroy(GetComponent<RocketController>());

                
               
            }
                     
        }

        [PunRPC]
        void Start()
        {

            if (photonView.IsMine)
            {
                GetComponent<PhotonView>().RPC("ChangeName", RpcTarget.All);               

            }
             else if (!photonView.IsMine)
             {
                 
             }
            //Changename();
        }

        

        [PunRPC]
        private void ChangeName()
        {
            Debug.Log("[Change Name Called]");
            newName = (string)PhotonNetwork.LocalPlayer.CustomProperties["CustomPlayerName"];
            Debug.Log("PhotonNetwork Name:"+PhotonNetwork.LocalPlayer.CustomProperties["CustomPlayerName"]);   
            gameObject.name = newName;
            Debug.Log("newName "+newName);
        }

        public static void RefreshInstance(ref PlayerScript player, PlayerScript playerPrefab, GameObject spawnLocation)
        {
            var position = Vector3.zero;
            var spawnPosition = spawnLocation.transform.position;
            var rotation = Quaternion.identity;
            if (player != null)
            {
                spawnPosition = player.transform.position;
                rotation = player.transform.rotation;
                PhotonNetwork.Destroy(player.gameObject);
            }

            player = PhotonNetwork.Instantiate(playerPrefab.gameObject.name, spawnPosition, rotation).GetComponent<PlayerScript>();
           // Debug.Log("Instantiated "+playerPrefab.gameObject.name);
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting == true)
            {
                stream.SendNext(newName);
                stream.SendNext(gameObject.name);
            }
            else
            {
                newName = (string)stream.ReceiveNext();
                gameObject.name = (string)stream.ReceiveNext();
            }
        }
    }
}
