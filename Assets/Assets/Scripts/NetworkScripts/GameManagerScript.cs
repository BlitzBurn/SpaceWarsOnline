using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using SpaceWarsOnline;
using Photon.Realtime;
//using Photon.Pun.IPunObservable;

namespace SpaceWarsOnline
{
    public class GameManagerScript : MonoBehaviourPunCallbacks, IPunObservable
    {

        [Header("Spawn Players")]
        public PlayerScript playerPrefab;
        public List<GameObject> spawnLocation;
        // public GameObject spawnLocation1, spawnLocation2, spawnLocation3, spawnLocation4;

        protected int numberOfPlayers;


        [HideInInspector]
        public PlayerScript localPlayer;

        private void Awake()
        {
            if (!PhotonNetwork.IsConnected)
            {
                SceneManager.LoadScene("Login");
                return;
            }
        }

        private void Start()
        {
            numberOfPlayers = PhotonNetwork.CountOfPlayers;
            PlayerScript.RefreshInstance(ref localPlayer, playerPrefab, spawnLocation[numberOfPlayers]);
           
        }    

        public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
        {
            numberOfPlayers = PhotonNetwork.CountOfPlayers;
            base.OnPlayerEnteredRoom(newPlayer);
            Debug.Log(numberOfPlayers);
            PlayerScript.RefreshInstance(ref localPlayer, playerPrefab, spawnLocation[numberOfPlayers]);
        }

        private void Update()
        {
            //
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(numberOfPlayers);

            }
            else if (stream.IsReading)
            {
                numberOfPlayers = (int)stream.ReceiveNext();
            }
        }

    }
}

