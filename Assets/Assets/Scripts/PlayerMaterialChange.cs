using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerMaterialChange : MonoBehaviourPunCallbacks, IPunObservable
{
    [Header("Material")]
    public List<Material> RocketColors;
    Renderer rocketRenderer;
    private PhotonView _photonView;
    private bool hasChangedMat;

    [Header("Materials")]
    public Material redMaterial;
    public Material greenMaterial;
    public Material purpleMaterial;
    public Material blueMaterial;

    private int shipColour;
    
    
    void Start()
    {        
        hasChangedMat = false;
    }

    private void Update()
    {
        if (!photonView.IsMine && !hasChangedMat)
        {
            _photonView = GetComponent<PhotonView>();
            shipColour = PhotonNetwork.LocalPlayer.ActorNumber;
            _photonView.RPC("ChangeRocketMaterial", RpcTarget.AllBuffered, shipColour);
        }
    }


    [PunRPC]
    public void ChangeRocketMaterial(int _shipColour)
    {
        if (_shipColour == 1 )
        {
            Debug.Log(redMaterial.name);
            rocketRenderer = GetComponent<Renderer>();
            rocketRenderer.enabled = true;
            rocketRenderer.material = redMaterial;
            hasChangedMat = true;
        }
        else if (_shipColour == 2 )
        {
            Debug.Log(greenMaterial.name);
            rocketRenderer = GetComponent<Renderer>();
            rocketRenderer.enabled = true;
            rocketRenderer.material = greenMaterial;
            hasChangedMat = true;
        }
        else if (_shipColour == 3 )
        {
            Debug.Log(purpleMaterial.name);
            rocketRenderer = GetComponent<Renderer>();
            rocketRenderer.enabled = true;
            rocketRenderer.material = purpleMaterial;
            hasChangedMat = true;
        }
        else if (_shipColour == 4 )
        {
            Debug.Log(blueMaterial.name);
            rocketRenderer = GetComponent<Renderer>();
            rocketRenderer.enabled = true;
            rocketRenderer.material = blueMaterial;
            hasChangedMat = true;
        }

       
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


