using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine.UI;
using SpaceWarsOnline;
using Photon.Realtime;
//using Photon.Pun.IPunObservable;

namespace SpaceWarsOnline
{
    public class GameManagerScript : MonoBehaviourPunCallbacks, IPunObservable
    {
        [Header("Lobby Stuff")]
        List<GameObject> players = new List<GameObject>();
        public Text playersInLobby;
        private PhotonView PV;

        [Header("Spawn Players")]
        public PlayerScript playerPrefab;
        public List<GameObject> spawnLocation;
        // public GameObject spawnLocation1, spawnLocation2, spawnLocation3, spawnLocation4;

        protected int numberOfPlayers;

        private bool gameHasStarted;
        

        [HideInInspector]
        public PlayerScript localPlayer;

        private void Awake()
        {
            PV = GetComponent<PhotonView>();
            gameHasStarted = false;

            if (!PhotonNetwork.IsConnected)
            {
                SceneManager.LoadScene("Login");
                return;
            }
        }

        private void startGame()
        {

        }

        private void Start()
        {
            // numberOfPlayers = PhotonNetwork.CountOfPlayers;
            numberOfPlayers += 1;
            PlayerScript.RefreshInstance(ref localPlayer, playerPrefab, spawnLocation[numberOfPlayers]);
            Debug.Log(gameHasStarted);
           
        }    

        public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
        {
            numberOfPlayers += 1;
            Debug.Log("Player Entered room "+numberOfPlayers);
            base.OnPlayerEnteredRoom(newPlayer);
            
            PlayerScript.RefreshInstance(ref localPlayer, playerPrefab, spawnLocation[numberOfPlayers]);

        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            numberOfPlayers -= 1;
            base.OnPlayerLeftRoom(otherPlayer);
        }

        void FixedUpdate()
        {
            if (players.Count < PhotonNetwork.CountOfPlayers)
            {
                PV.RPC("LobbyPlayerCount", RpcTarget.AllBuffered, gameHasStarted);
                //Debug.Log(PhotonNetwork.CountOfPlayers);
                Debug.Log(numberOfPlayers);
            }

            if (!gameHasStarted)
            {
                
            }
        }

        [PunRPC]
        private void LobbyPlayerCount(bool gameStarted)
        {
            
                foreach (GameObject rocket in GameObject.FindGameObjectsWithTag("RocketTag"))
                {
                    players.Add(rocket);
                    //rocket.SetActive(false);
                Debug.Log("Added Rocket To List");
                }
            playersInLobby.text = numberOfPlayers.ToString();
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(numberOfPlayers);
                stream.SendNext(gameHasStarted);
                stream.SendNext(playersInLobby);
            }
            else if (stream.IsReading)
            {
                gameHasStarted = (bool)stream.ReceiveNext();
                numberOfPlayers = (int)stream.ReceiveNext();
                playersInLobby = (Text)stream.ReceiveNext();
            }
        }

    }
}

