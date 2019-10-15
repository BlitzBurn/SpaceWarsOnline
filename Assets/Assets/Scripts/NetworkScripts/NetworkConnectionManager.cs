using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class NetworkConnectionManager : MonoBehaviourPunCallbacks
{
    public Button BtnConnectMaster;
    public Button BtnCreateRoom;
    public Button BtnConnectRoom;

    protected bool TriesToConnectToMaster;
    protected bool TriesToConnectToRoom;

    private float time;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        TriesToConnectToMaster = false;
        TriesToConnectToRoom = false;
    }

    void Update()
    {
        if (BtnConnectMaster != null)
        {
            BtnConnectMaster.gameObject.SetActive(!PhotonNetwork.IsConnected && !TriesToConnectToMaster);
        }

        if (BtnConnectRoom != null)
        {
            BtnConnectRoom.gameObject.SetActive(PhotonNetwork.IsConnected && !TriesToConnectToMaster && !TriesToConnectToRoom);
        }
    }

    public void OnClickConnectToMaster()
    {
        PhotonNetwork.OfflineMode = false;
        //PhotonNetwork.NickName = "PlayerNickname";
        //PhotonNetwork.AutomaticallySyncScene = true;

        TriesToConnectToMaster = true;

        PhotonNetwork.ConnectUsingSettings();

    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);
        TriesToConnectToMaster = false;
        TriesToConnectToRoom = false;

        Debug.Log(cause);
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        TriesToConnectToMaster = false;
        Debug.Log("Connected to master");
    }

    public void OnClickConnectToRoom()
    {
        if (!PhotonNetwork.IsConnected)
        {
            return;
        }
            
       TriesToConnectToRoom = true;
        //PhotonNetwork.CreateRoom("TestRoom1");
        PhotonNetwork.JoinRoom("room1");

        //PhotonNetwork.JoinRandomRoom();        
    }

    public void OnClick_CreateRoom()
    {
        if (!PhotonNetwork.IsConnected)
        {
            return;
        }

        RoomOptions roomOptions = new RoomOptions();
        //roomOptions.IsVisible = true;
        roomOptions.MaxPlayers = 4;

        PhotonNetwork.CreateRoom("room1", roomOptions, TypedLobby.Default);


    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();

        TriesToConnectToRoom = false;

        Debug.Log("Master: "+PhotonNetwork.IsMasterClient + " | Players In Room: " + PhotonNetwork.CurrentRoom.PlayerCount + " | RoomName: " + PhotonNetwork.CurrentRoom.Name + " Region: " + PhotonNetwork.CloudRegion);
        SceneManager.LoadScene("GamePlayScene");
        /*
        if (PhotonNetwork.IsMasterClient && SceneManager.GetActiveScene().name != "GamePlayScene")
        {
            SceneManager.LoadScene("GamePlayScene");//Change This later!!!!
        }*/
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);

        PhotonNetwork.CreateRoom(null, new RoomOptions {MaxPlayers=20 });
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
        Debug.Log("Room creation failed");
        
        TriesToConnectToRoom = false;
    }

    public override void OnCreatedRoom()
    {
        //MasterManager.DebugConsole.Add
        base.OnCreatedRoom();
        Debug.Log("Created room successfully: "+this);
    }
}
