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

    void Start()
    {        
        hasChangedMat = false;
        _photonView = GetComponent<PhotonView>();
        _photonView.RPC("ChangeRocketMaterial", RpcTarget.All);

        

            rocketRenderer = GetComponent<Renderer>();
            rocketRenderer.enabled = true;
    }

    private void Update()
    {
        if (hasChangedMat == false)
        {
            Debug.Log("Changed mats");
            rocketRenderer.material = RocketColors[PhotonNetwork.LocalPlayer.ActorNumber];//.sharedMaterial = RocketColors[PhotonNetwork.PlayerList.Length];
            hasChangedMat = true;
        }
    }


    [PunRPC]
    public void ChangeRocketMaterial()
    {
       
    }


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting == true)
        {
            stream.SendNext(hasChangedMat);
        }
        else
        {
            hasChangedMat = (bool)stream.ReceiveNext();
        }
    }
}


