using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine.UI;
//using SpaceWarsOnline;
using Photon.Realtime;
//using Photon.Pun.IPunObservable;


public class GameManagerScript : MonoBehaviourPunCallbacks, IPunObservable
{
    [Header("Lobby Stuff")]
    List<GameObject> playersList = new List<GameObject>();

    public Text timeToGameStartText;
    public float countDownTime;
    private float timer;
    private float countDownTimerReset;
    public Text playersInLobby;
    public GameObject canvasStartGame;
    public GameObject canvasEndGame;
    private PhotonView PV;
    public static int livingPlayers = 0;

    [Header("Booltexts")]
    public Text preptostText;
    public Text gahaenText;
    public Text gaiprogText;

    [Header("Spawn Players")]
    public PlayerScript playerPrefab;
    public List<GameObject> spawnLocation;
    public List<GameObject> playerLocationGameStart;

    [Header("Restart Crap")]
    public float restartTime;
    public Text restartText;
    private float resTime;
    private float restartResetTimer;

    public static int numberOfPlayers = 0;

    [Header("Gamemanager Booleans")]
    public static bool preparingToStart;
    public static bool gameHasEnded;
    public static bool gameIsInProgress;
    public bool showGameManagerBools;

    private int LoopVariable = 0;

    [HideInInspector]
    public PlayerScript localPlayer;

    [Header("Music Clips")]
    public AudioClip battleTheme;
    public AudioClip setupTheme;
    public AudioClip gameOverTheme;

    [Header("Audio Sources")]
    private AudioSource musicAD;

    private void Awake()
    {
        PV = GetComponent<PhotonView>();

        if (!PhotonNetwork.IsConnected)
        {
            SceneManager.LoadScene("Login");
            return;
        }

        musicAD = GetComponent<AudioSource>();
        musicAD.loop = true;
        musicAD.clip = setupTheme;
        musicAD.Play();
    }

    private void Start()
    {
        preparingToStart = true;
        gameIsInProgress = false;
        gameHasEnded = false;

        canvasStartGame.SetActive(true);
        canvasEndGame.SetActive(false);

        timer = 0;
        countDownTimerReset = countDownTime;

        resTime = 0;
        restartResetTimer = restartTime;

        

        //PhotonNetwork.SendRate = 20;
        //PhotonNetwork.SerializationRate = 10;

        PlayerScript.RefreshInstance(ref localPlayer, playerPrefab, spawnLocation[PhotonNetwork.PlayerList.Length - 1]);
    }

    public void RestartGame()
    {
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            // PhotonNetwork.Destroy(playersList[i].gameObject);

            //playerList[i].seyActive

            playersList[i].SetActive(false);
            Debug.Log("SetActive");
        }

        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
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

        


        if (countDownTime >= 0 && preparingToStart && PhotonNetwork.PlayerList.Length >= 2)
        {
            timer = Time.deltaTime;
            countDownTime -= timer;
        }
        else if (countDownTime <= 0 && preparingToStart)
        {
            PV.RPC("GameStartSequence", RpcTarget.All, preparingToStart);
        }

        if (gameIsInProgress && livingPlayers == 1)
        {
            gameHasEnded = true;
            gameIsInProgress = false;
        }

        
        //4>3
        if (gameHasEnded && restartTime >= 0)
        {
            canvasEndGame.SetActive(true);
            restartTime = restartTime - Time.deltaTime;
            Debug.Log("Game has ended"+restartTime);
        }
        else if(gameHasEnded && restartTime <= 0)
        {
            livingPlayers = PhotonNetwork.PlayerList.Length;
            
            gameHasEnded = false;
            gameIsInProgress = false;
            preparingToStart = true;

            restartTime = restartResetTimer;
            //resTime = 0;
            timer = 0;
            countDownTime = countDownTimerReset;

            canvasEndGame.SetActive(false);
            canvasStartGame.SetActive(true);
        }

        restartText.text = Mathf.Ceil(restartTime).ToString();
        timeToGameStartText.text = Mathf.Ceil(countDownTime).ToString();
        playersInLobby.text = PhotonNetwork.PlayerList.Length.ToString();

        if (showGameManagerBools)
        {
            preptostText.text = "PreparingToStart: " + preparingToStart.ToString();
            gahaenText.text = "GameHasEnded: " + gameHasEnded.ToString();
            gaiprogText.text = "GameIsInProgress: " + gameIsInProgress.ToString();
        }
        else if (!showGameManagerBools)
        {
            preptostText.gameObject.SetActive(false);
            gahaenText.gameObject.SetActive(false);
            gaiprogText.gameObject.SetActive(false);
        }
    }

    [PunRPC]
    public void GameRestart()
    {
        canvasStartGame.SetActive(true);
        countDownTime = countDownTimerReset;
    }



    [PunRPC]
    public void GameStartSequence(bool IfgameHasStarted)
    {

        canvasStartGame.SetActive(false);
        preparingToStart = false;
        gameIsInProgress = true;
        Debug.Log("GameStartSequence, gameHasStarted:" + preparingToStart + ", IfgameHasStarted: " + IfgameHasStarted);

        musicAD.clip = battleTheme;
        musicAD.Play();

        playersList.Clear();
        Debug.Log(playersList.Count);

        foreach (GameObject rocket in GameObject.FindGameObjectsWithTag("RocketTag"))
        {
            //rocket.transform.position = playerLocationGameStart[i].transform.position;
            playersList.Add(rocket);
            //Debug.Log("Added crap to da big list");
            //Debug.Log(playersList.Count);

        }

        livingPlayers = playersList.Count;
        Debug.Log(livingPlayers);

    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(preparingToStart);
            stream.SendNext(numberOfPlayers);
            stream.SendNext(countDownTime);
            stream.SendNext(timer);
            stream.SendNext(LoopVariable);
            stream.SendNext(gameHasEnded);
            stream.SendNext(countDownTimerReset);
            stream.SendNext(livingPlayers);
            stream.SendNext(gameIsInProgress);
            stream.SendNext(resTime);
            stream.SendNext(restartTime);
            stream.SendNext(restartResetTimer);
        }
        else if (stream.IsReading)
        {
            preparingToStart = (bool)stream.ReceiveNext();
            numberOfPlayers = (int)stream.ReceiveNext();
            countDownTime = (float)stream.ReceiveNext();
            timer = (float)stream.ReceiveNext();
            LoopVariable = (int)stream.ReceiveNext();
            gameHasEnded = (bool)stream.ReceiveNext();
            countDownTimerReset = (float)stream.ReceiveNext();
            livingPlayers = (int)stream.ReceiveNext();
            gameIsInProgress = (bool)stream.ReceiveNext();
            resTime = (float)stream.ReceiveNext();
            restartTime = (float)stream.ReceiveNext();
            restartResetTimer = (float)stream.ReceiveNext();
        }
    }

}


