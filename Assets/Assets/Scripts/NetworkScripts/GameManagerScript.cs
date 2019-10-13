using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using SpaceWarsOnline;
using Photon.Realtime;

namespace SpaceWarsOnline
{
    public class GameManagerScript : MonoBehaviourPunCallbacks
    {

        [Header("Game Manager")]
        public PlayerScript playerPrefab;

        [HideInInspector]
        public PlayerScript localPlayer;

        private void Awake()
        {
            if (!PhotonNetwork.IsConnected)
            {
                SceneManager.LoadScene("Login");
                return;
            }
        }

        private void Start()
        {
            PlayerScript.RefreshInstance(ref localPlayer, playerPrefab);
           
        }

        public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
        {
            base.OnPlayerEnteredRoom(newPlayer);
            PlayerScript.RefreshInstance(ref localPlayer, playerPrefab);
        }
    }
}

