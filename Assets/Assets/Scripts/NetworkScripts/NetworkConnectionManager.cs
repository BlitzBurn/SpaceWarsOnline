using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;
using UnityEngine.SceneManagement;



public class NetworkConnectionManager : MonoBehaviourPunCallbacks
{
    
    private Text _userNameText;

    [Header("Buttons")]
    public Button BtnConnectMaster;
    public Button BtnCreateRoom;
    public Button BtnConnectRoom;
    public Button submitUserName;
    public Button submitRoomName;


    [Header("Username")]
    public InputField userNameInput;
    private string userName;

    [Header("Enter Room")]
    private string roomName;
    public InputField roomNameInput;


    protected bool TriesToConnectToMaster;
    protected bool TriesToConnectToRoom;

    private float time;

    void Start()
    {
        roomName = "room1";
        //userNameInput.onValueChanged.AddListener(delegate { OnTextInput(); });
        DontDestroyOnLoad(gameObject);
        TriesToConnectToMaster = false;
        TriesToConnectToRoom = false;
        OnClickConnectToMaster();
    }

    public void RoomNameInput_OnClick()
    {
        roomName = roomNameInput.text.ToString();
    }

    public void UserNameInput_OnClick()
    {
        ExitGames.Client.Photon.Hashtable _customName = PhotonNetwork.LocalPlayer.CustomProperties;

        userName = userNameInput.text.ToString();

        PhotonNetwork.NickName = userName;
        PhotonNetwork.LocalPlayer.NickName = userName;
       
     

        _customName["CustomPlayerName"] = userName;
        PhotonNetwork.LocalPlayer.CustomProperties = _customName;

        string SavedUsername = (string)PhotonNetwork.LocalPlayer.CustomProperties["CustomPlayerName"];

       

        //PhotonNetwork.AuthValues = new AuthenticationValues(SavedUsername);
        //PhotonNetwork.LocalPlayer.UserId = SavedUsername;

        // Debug.Log(PhotonNetwork.LocalPlayer.UserId);        
    }



    void Update()
    {
        if (BtnConnectMaster != null)
        {
            BtnConnectMaster.gameObject.SetActive(!PhotonNetwork.IsConnected && !TriesToConnectToMaster);
        }

        if (BtnConnectRoom != null && BtnCreateRoom != null)
        {
            BtnConnectRoom.gameObject.SetActive(PhotonNetwork.IsConnected && !TriesToConnectToMaster && !TriesToConnectToRoom);
            BtnCreateRoom.gameObject.SetActive(PhotonNetwork.IsConnected && !TriesToConnectToMaster && !TriesToConnectToRoom);
        }

        if (TriesToConnectToRoom)
        {
            //Debug.Log("TriesToConnectToRoom");
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
        Debug.Log("Connect to do room");
       TriesToConnectToRoom = true;
        //PhotonNetwork.CreateRoom("TestRoom1");
        PhotonNetwork.JoinRoom(roomName);

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

        PhotonNetwork.CreateRoom(roomName, roomOptions, TypedLobby.Default);


    }

    public override void OnJoinedRoom()
    {
        //Debug.Log("OnJoinedRoomCalled");
        base.OnJoinedRoom();

        TriesToConnectToRoom = false;

        Debug.Log("Master: "+PhotonNetwork.IsMasterClient + " | Players In Room: " + PhotonNetwork.CurrentRoom.PlayerCount + " | RoomName: " + PhotonNetwork.CurrentRoom.Name + " Region: " + PhotonNetwork.CloudRegion);
        SceneManager.LoadScene("GamePlayScene");
        
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
