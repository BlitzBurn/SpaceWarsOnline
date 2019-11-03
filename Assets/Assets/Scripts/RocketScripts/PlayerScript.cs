using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


namespace SpaceWarsOnline
{
    public class PlayerScript : MonoBehaviourPun, IPunObservable
    {
        private string newName;

        private float instantiatedPlayerID;

        private Vector3 startPosition;

        private PhotonView PV;
        public static PlayerScript playerScriptPrefab;

        

        protected Rigidbody rocketRigidBody;
        protected Quaternion rocketRotation;

        private bool hasRestared;

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
            hasRestared = false;
            startPosition = gameObject.transform.position;
            Debug.Log(startPosition);

        }

        void Update()
        {
            if (GameManagerScript.gameHasEnded && !hasRestared)
            {
                Debug.Log("Restart PlayerScript");
                hasRestared = true;
                RestartGame();
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

        
        public void RestartGame()
        {
            if(photonView.IsMine)
            gameObject.transform.position = startPosition;
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting == true)
            {
                stream.SendNext(newName);
                stream.SendNext(hasRestared);
                stream.SendNext(startPosition);
                stream.SendNext(GameManagerScript.gameHasEnded);
            }
            else
            {
                newName = (string)stream.ReceiveNext();
                hasRestared = (bool)stream.ReceiveNext();
                startPosition = (Vector3)stream.ReceiveNext();
                GameManagerScript.gameHasEnded = (bool)stream.ReceiveNext();
            }
        }
    }
}
