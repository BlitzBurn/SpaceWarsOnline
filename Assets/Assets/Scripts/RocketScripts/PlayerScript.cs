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
    public bool isInTestScene;

    public GameObject rocketThrust;

   public static void ResetPlayerPosition(GameObject playerReference, GameObject newPosition)
    {
        playerReference.transform.position = newPosition.transform.position;
    }

    private void Awake()
    {
        rocketRigidBody = GetComponent<Rigidbody>();

        rocketThrust.SetActive(false);

        PV = GetComponent<PhotonView>();
        if (!base.photonView.IsMine && GetComponent<RocketController>() != null && GetComponent<FireMissile>() != null/* && GetComponent<PlayerMaterialChange>() != null) */&& GetComponent<Health>() != null )
        {
            if (!isInTestScene)
            {
                Debug.Log("Components destroyed");
                Destroy(GetComponent<FireMissile>());
                Destroy(GetComponent<RocketController>());
                //Destroy(GetComponent<PlayerMaterialChange>());
                //Destroy(GetComponent<Health>());
                //Destroy(GetComponent<AttractScript>());
            }
        }
    }

    private void Start()
    {
        playerIsAlive = true;
        startPosition = gameObject.transform.position;
        deathPosition = new Vector3(gameObject.transform.position.x, -40f, gameObject.transform.position.z);


    }

    void Update()
    {
        if (!playerIsAlive)
        {
            gameObject.transform.position = deathPosition;

        }

        if (GameManagerScript.preparingToStart)
        {
            //Debug.Log("Restart PlayerScript");
            playerIsAlive = true;
            //RestartGame();
            PV.RPC("RestartGame", RpcTarget.AllBuffered);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rocketThrust.SetActive(true);
        }
        else if(Input.GetKeyUp(KeyCode.Space))
        {
            rocketThrust.SetActive(false);
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

