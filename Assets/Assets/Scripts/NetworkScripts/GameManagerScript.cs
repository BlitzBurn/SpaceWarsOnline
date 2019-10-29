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

        public static int numberOfPlayers=0;

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
            PhotonNetwork.SendRate = 20;
            PhotonNetwork.SerializationRate = 10;
            Debug.Log("Start Called: Game Manager");
            //numberOfPlayers += 1;
            PlayerScript.RefreshInstance(ref localPlayer, playerPrefab, spawnLocation[numberOfPlayers]);
            Debug.Log(gameHasStarted);
           
        }    

       

        public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
        {
            //numberOfPlayers += 1;
            Debug.Log("Player Entered room "+numberOfPlayers);
            base.OnPlayerEnteredRoom(newPlayer);
            
            PlayerScript.RefreshInstance(ref localPlayer, playerPrefab, spawnLocation[PhotonNetwork.PlayerList.Length]);

        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            numberOfPlayers -= 1;
            base.OnPlayerLeftRoom(otherPlayer);
        }

        void FixedUpdate()
        {
            // PV.RPC("LobbyPlayerCount", RpcTarget.AllBuffered, gameHasStarted);
            // Debug.Log(numberOfPlayers);

            /*
            Debug.Log("CountOfPlayers: "+PhotonNetwork.CountOfPlayers);
            Debug.Log("PlayerList: "+PhotonNetwork.PlayerList.Length);
            Debug.Log("PlayersOnMaster"+PhotonNetwork.CountOfPlayersOnMaster);
            Debug.Log("PlayersInRooms"+PhotonNetwork.CountOfPlayersInRooms);*/

            // playersInLobby.text = numberOfPlayers.ToString();
            playersInLobby.text = PhotonNetwork.PlayerList.Length.ToString();
            if (players.Count < PhotonNetwork.CountOfPlayers)
            {
                
                //
               // Debug.Log(numberOfPlayers);
            }

            if (!gameHasStarted)
            {
                
            }
        }

        [PunRPC]
        private void LobbyPlayerCount(bool gameStarted)
        {
            /*
                foreach (GameObject rocket in GameObject.FindGameObjectsWithTag("RocketTag"))
                {
                    players.Add(rocket);
                    //rocket.SetActive(false);
                Debug.Log("Added Rocket To List");
                }*/
            
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {                
                stream.SendNext(gameHasStarted);
                stream.SendNext(numberOfPlayers);
                //stream.SendNext(playersInLobby);
            }
            else if (stream.IsReading)
            {
                gameHasStarted = (bool)stream.ReceiveNext();
                numberOfPlayers = (int)stream.ReceiveNext();
                //playersInLobby = (Text)stream.ReceiveNext();
            }
        }

    }
}

