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
        public Text timeToGameStartText;
        public float countDownTime;
        private float timer;
        public Text playersInLobby;
        public GameObject canvasStartGame;
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

        private void Start()
        {
            timer =0;

            // numberOfPlayers = PhotonNetwork.CountOfPlayers;
            PhotonNetwork.SendRate = 20;
            PhotonNetwork.SerializationRate = 10;
            Debug.Log("Start Called: Game Manager");
            //numberOfPlayers += 1;
            PlayerScript.RefreshInstance(ref localPlayer, playerPrefab, spawnLocation[PhotonNetwork.PlayerList.Length-1/*numberOfPlayers*/]);

            
            PV.RPC("GameStartSequence", RpcTarget.All, gameHasStarted);

        }    

       

        public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
        {
          
            Debug.Log("Player Entered room ");
            base.OnPlayerEnteredRoom(newPlayer);
            
            PlayerScript.RefreshInstance(ref localPlayer, playerPrefab, spawnLocation[PhotonNetwork.PlayerList.Length]);
            PV.RPC("GameStartSequence", RpcTarget.All, gameHasStarted);
        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            numberOfPlayers -= 1;
            base.OnPlayerLeftRoom(otherPlayer);
        }

        void Update()
        {
            //Debug.Log(players.Count);
            //PV.RPC("LobbyPlayerCount", RpcTarget.AllBuffered, gameHasStarted);
            if (countDownTime >= 0 && !gameHasStarted)
            {
                timer = Time.deltaTime;
                countDownTime -= timer;
                // Debug.Log("Countdown time: " + countDownTime + " | Timer: " + timer + " | GameHasStarted: " + gameHasStarted);
            }
            else if (countDownTime<= 0&& !gameHasStarted)
            {
                gameHasStarted = true;
                PV.RPC("GameStartSequence", RpcTarget.All, gameHasStarted);
            }

            timeToGameStartText.text = Mathf.Ceil(countDownTime).ToString();
            playersInLobby.text = PhotonNetwork.PlayerList.Length.ToString();
           
        }

        [PunRPC]
        public void AddPlayersToList(GameObject playerReference)
        {
            Debug.Log("Added " + playerReference+" to list");
            players.Add(playerReference);
        }

        [PunRPC]
        private void GameStartSequence(bool hasGameStarted)
        {
           /* if (!hasGameStarted)
            {
                Debug.Log("GameStartSequence" + hasGameStarted);
                foreach (GameObject rocket in GameObject.FindGameObjectsWithTag("RocketTag"))
                {
                    players.Add(rocket);
                }
            }*/
            if (hasGameStarted)
            {
                Debug.Log("GameStartSequence"+hasGameStarted);
                canvasStartGame.SetActive(false);
                foreach(GameObject rocket in players)
                {
                    Debug.Log("foreach");
                    rocket.SetActive(true);
                }
            }

           
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {                
                stream.SendNext(gameHasStarted);
                stream.SendNext(numberOfPlayers);
                stream.SendNext(countDownTime);
                stream.SendNext(timer);
                
            }
            else if (stream.IsReading)
            {
                gameHasStarted = (bool)stream.ReceiveNext();
                numberOfPlayers = (int)stream.ReceiveNext();
                countDownTime = (float)stream.ReceiveNext();
                timer = (float)stream.ReceiveNext();
            }
        }

    }
}

