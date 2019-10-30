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
    private bool hasChangedMat=false;

    void Start()
    {
        
        _photonView = GetComponent<PhotonView>();
        _photonView.RPC("ChangeRocketMaterial", RpcTarget.All);
    }


    [PunRPC]
    public void ChangeRocketMaterial()
    {
        if (!hasChangedMat) {

            rocketRenderer = GetComponent<Renderer>();
            rocketRenderer.enabled = true;
            
            rocketRenderer.material = RocketColors[PhotonNetwork.PlayerList.Length-1];//.sharedMaterial = RocketColors[PhotonNetwork.PlayerList.Length];
            hasChangedMat = true;
        }
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
