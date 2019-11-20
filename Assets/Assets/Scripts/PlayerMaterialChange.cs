using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerMaterialChange : MonoBehaviourPun, IPunObservable
{
    [Header("Material")]
    public List<Material> RocketColors;
    Renderer rocketRenderer;
    private PhotonView _photonView;
    private bool hasChangedMat = false;

    private int shipColour;

    void Start()
    {        
        hasChangedMat = false;
        _photonView = GetComponent<PhotonView>();
        

        

            rocketRenderer = GetComponent<Renderer>();
            rocketRenderer.enabled = true;
    }

    private void Update()
    {
        shipColour = PhotonNetwork.LocalPlayer.ActorNumber;
        _photonView.RPC("ChangeRocketMaterial", RpcTarget.All);
    }


    [PunRPC]
    public void ChangeRocketMaterial()
    {
        Debug.Log("Changed mats");
        rocketRenderer.material = RocketColors[shipColour];//.sharedMaterial = RocketColors[PhotonNetwork.PlayerList.Length];
        hasChangedMat = true;
    }


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting == true)
        {
            stream.SendNext(hasChangedMat);
            stream.SendNext(shipColour);
        }
        else
        {
            hasChangedMat = (bool)stream.ReceiveNext();
            shipColour = (int)stream.ReceiveNext();
        }
    }
}


