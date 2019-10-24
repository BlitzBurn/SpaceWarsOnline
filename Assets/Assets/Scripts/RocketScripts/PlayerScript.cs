using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


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
            Changename();

            if (!photonView.IsMine && GetComponent<RocketController>() != null && GetComponent<FireMissile>() !=null )
            {
                Debug.Log("Start called");
                Destroy(GetComponent<FireMissile>());
                Destroy(GetComponent<RocketController>());
                
                // Destroy(GetComponent<Health>());
            }

            
        }


        private void Changename()
        {
            newName = (string)PhotonNetwork.LocalPlayer.CustomProperties["CustomPlayerName"];
            //newName = PlayerPrefs.GetString("PlayerName");
            //newName = PhotonNetwork.NickName;

            gameObject.name = newName;
            Debug.Log(newName);
        }

        public static void RefreshInstance(ref PlayerScript player, PlayerScript playerPrefab, GameObject spawnLocation)
        {
            Debug.Log("Spawned");
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
            Debug.Log("Instantiated "+playerPrefab.gameObject.name);
        }

        void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting == true)
            {
                stream.SendNext(newName);
            }
            else
            {
                newName = (string)stream.ReceiveNext();
            }
        }
    }
}
