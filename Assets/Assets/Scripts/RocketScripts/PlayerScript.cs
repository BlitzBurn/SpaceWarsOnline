using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;



public class PlayerScript : MonoBehaviourPun, IPunObservable
{
    private string newName;

    private float instantiatedPlayerID;

    private Vector3 startPosition;
    private Vector3 deathPosition;

    private PhotonView PV;
    public static PlayerScript playerScriptPrefab;

    protected Rigidbody rocketRigidBody;
    protected Quaternion rocketRotation;

    public bool playerIsAlive;

    private void Awake()
    {
        rocketRigidBody = GetComponent<Rigidbody>();

        PV = GetComponent<PhotonView>();
        if (!base.photonView.IsMine && GetComponent<RocketController>() != null && GetComponent<FireMissile>() != null && GetComponent<PlayerMaterialChange>() != null)
        {
            Debug.Log("Start called Player Scripts");
            Destroy(GetComponent<FireMissile>());
            Destroy(GetComponent<RocketController>());
            Destroy(GetComponent<PlayerMaterialChange>());
        }
    }

    private void Start()
    {
        playerIsAlive = true;
        startPosition = gameObject.transform.position;
        deathPosition = new Vector3(gameObject.transform.position.x, -40f, gameObject.transform.position.z);

        Debug.Log(startPosition);
    }

    void Update()
    {
        if (GameManagerScript.gameHasEnded)
        {
            Debug.Log("Restart PlayerScript");
            playerIsAlive = true;
            //RestartGame();
            PV.RPC("RestartGame", RpcTarget.AllBuffered);
        }

        if (!playerIsAlive)
        {
            gameObject.transform.position = deathPosition;
        }

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
    }

    [PunRPC]
    public void RestartGame()
    {
        gameObject.transform.position = startPosition;
        // if (photonView.IsMine)
        {

        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting == true)
        {
            stream.SendNext(newName);
            stream.SendNext(playerIsAlive);
            stream.SendNext(startPosition);
            stream.SendNext(deathPosition);
            stream.SendNext(GameManagerScript.gameHasEnded);
        }
        else
        {
            newName = (string)stream.ReceiveNext();
            playerIsAlive = (bool)stream.ReceiveNext();
            startPosition = (Vector3)stream.ReceiveNext();
            deathPosition = (Vector3)stream.ReceiveNext();
            GameManagerScript.gameHasEnded = (bool)stream.ReceiveNext();
        }
    }
}

