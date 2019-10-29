using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMaterialChange : MonoBehaviourPun
{
    [Header("Material")]
    public List<Material> RocketColors;
    Renderer rocketRenderer;
    private PhotonView _photonView;
    private bool hasChangedMat=false;

    void Start()
    {
        Debug.Log("Mat change start");
        _photonView = GetComponent<PhotonView>();
        _photonView.RPC("ChangeRocketMaterial", RpcTarget.All);
    }


    [PunRPC]
    public void ChangeRocketMaterial()
    {
        if (!hasChangedMat && photonView.IsMine) {
            Debug.Log("Change mat method;");
            rocketRenderer = GetComponent<Renderer>();
            rocketRenderer.enabled = true;
            
            rocketRenderer.material = RocketColors[PhotonNetwork.PlayerList.Length];//.sharedMaterial = RocketColors[PhotonNetwork.PlayerList.Length];
            hasChangedMat = true;
        }
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
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
