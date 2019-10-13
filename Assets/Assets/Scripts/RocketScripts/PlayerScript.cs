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

            if (!photonView.IsMine && GetComponent<RocketController>() != null)
            {
                Destroy(GetComponent<RocketController>());
            }
        }

        public static void RefreshInstance(ref PlayerScript player, PlayerScript playerPrefab)
        {
            var position = Vector3.zero;
            var rotation = Quaternion.identity;
            if (player != null)
            {
                position = player.transform.position;
                rotation = player.transform.rotation;
                PhotonNetwork.Destroy(player.gameObject);
            }

            player = PhotonNetwork.Instantiate(playerPrefab.gameObject.name, position, rotation).GetComponent<PlayerScript>();
            Debug.Log("Instantiated "+playerPrefab.gameObject.name);
        }
    }
}
