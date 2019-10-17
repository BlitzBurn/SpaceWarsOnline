using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


namespace SpaceWarsOnline
{
    public class PlayerScript : MonoBehaviourPun
    {

        [HideInInspector]
        public InputStr Input;
        public struct InputStr
        {

        }

        protected Rigidbody rocketRigidBody;
        protected Quaternion rocketRotation;

        private void Awake()
        {
            rocketRigidBody = GetComponent<Rigidbody>();

            if (!photonView.IsMine && GetComponent<RocketController>() != null && GetComponent<FireMissile>() !=null)
            {
                Destroy(GetComponent<FireMissile>());
                Destroy(GetComponent<RocketController>());
            }
        }

        public static void RefreshInstance(ref PlayerScript player, PlayerScript playerPrefab, GameObject spawnLocation)
        {
            Debug.Log("Spawned");
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
            Debug.Log("Instantiated "+playerPrefab.gameObject.name);
        }
    }
}
