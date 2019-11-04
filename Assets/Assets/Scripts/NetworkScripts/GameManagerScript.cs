using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine.UI;
using SpaceWarsOnline;
using Photon.Realtime;
//using Photon.Pun.IPunObservable;


public class GameManagerScript : MonoBehaviourPunCallbacks, IPunObservable
{
    [Header("Lobby Stuff")]
    List<GameObject> playersList = new List<GameObject>();

    public Text timeToGameStartText;
    public float countDownTime;
    private float timer;
    public Text playersInLobby;
    public GameObject canvasStartGame;
    private PhotonView PV;
    public static int livingPlayers=0;

    [Header("Spawn Players")]
    public PlayerScript playerPrefab;
    public List<GameObject> spawnLocation;
    public List<GameObject> playerLocationGameStart;

    public static int numberOfPlayers = 0;
    public static bool gameHasStarted;
    public static bool gameHasEnded;

    private int LoopVariable = 0;

    [HideInInspector]
    public PlayerScript localPlayer;

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
        gameHasStarted = false;
        gameHasEnded = false;

        if (!PhotonNetwork.IsConnected)
        {
            SceneManager.LoadScene("Login");
            return;
        }
    }

    private void Start()
    {
        timer = 0;

        // numberOfPlayers = PhotonNetwork.CountOfPlayers;
        PhotonNetwork.SendRate = 20;
        PhotonNetwork.SerializationRate = 10;
        //numberOfPlayers += 1;
        PlayerScript.RefreshInstance(ref localPlayer, playerPrefab, spawnLocation[PhotonNetwork.PlayerList.Length - 1]);

        // PV.RPC("GameStartSequence", RpcTarget.All);
    }

    public void RestartGame()
    {
        for(int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            // PhotonNetwork.Destroy(playersList[i].gameObject);

            //playerList[i].seyActive

            playersList[i].SetActive(false);
            Debug.Log("SetActive");
        }

        for (int i=0; i<PhotonNetwork.PlayerList.Length; i++) {
            PlayerScript.RefreshInstance(ref localPlayer, playerPrefab, spawnLocation[i]);
            Debug.Log("Refresh instance");
        }
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {

        Debug.Log("Player Entered room ");
        base.OnPlayerEnteredRoom(newPlayer);

        PlayerScript.RefreshInstance(ref localPlayer, playerPrefab, spawnLocation[PhotonNetwork.PlayerList.Length]);
        //PV.RPC("GameStartSequence", RpcTarget.All);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {

        base.OnPlayerLeftRoom(otherPlayer);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {            
            Debug.Log("Restart Game");
            gameHasEnded = true;
            Debug.Log(gameHasEnded);            
        }

        if (countDownTime >= 0 && !gameHasStarted)
        {
            timer = Time.deltaTime;
            countDownTime -= timer;
        }
        else if (countDownTime <= 0 && !gameHasStarted)
        {

            canvasStartGame.SetActive(false);
            gameHasStarted = true;

            PV.RPC("GameStartSequence", RpcTarget.All, gameHasStarted);
        }

        if(gameHasStarted && livingPlayers == 1)
        {
            gameHasEnded = true;
            gameHasStarted = false;
        }
        timeToGameStartText.text = Mathf.Ceil(countDownTime).ToString();
        playersInLobby.text = PhotonNetwork.PlayerList.Length.ToString();

    }


    [PunRPC]
    public void GameStartSequence(bool IfgameHasStarted)
    {

        Debug.Log("GameStartSequence, gameHasStarted:" + gameHasStarted + ", IfgameHasStarted: " + IfgameHasStarted);



        playersList.Clear();
        Debug.Log(playersList.Count);

        foreach (GameObject rocket in GameObject.FindGameObjectsWithTag("RocketTag"))
        {
            //rocket.transform.position = playerLocationGameStart[i].transform.position;
            playersList.Add(rocket);
            Debug.Log("Added crap to da big list");
            Debug.Log(playersList.Count);
            livingPlayers = playersList.Count;
        }
        Debug.Log(livingPlayers);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(gameHasStarted);
            stream.SendNext(numberOfPlayers);
            stream.SendNext(countDownTime);
            stream.SendNext(timer);
            stream.SendNext(LoopVariable);
            stream.SendNext(gameHasEnded);
        }
        else if (stream.IsReading)
        {
            gameHasStarted = (bool)stream.ReceiveNext();
            numberOfPlayers = (int)stream.ReceiveNext();
            countDownTime = (float)stream.ReceiveNext();
            timer = (float)stream.ReceiveNext();
            LoopVariable = (int)stream.ReceiveNext();
            gameHasEnded = (bool)stream.ReceiveNext();

        }
    }

}


