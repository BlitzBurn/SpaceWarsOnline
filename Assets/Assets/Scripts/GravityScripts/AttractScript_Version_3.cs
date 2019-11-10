using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class AttractScript_Version_3 : MonoBehaviour
{
    private Rigidbody gravityRb;

    public float GravityForce;

    private PhotonView PV;

    private void Start()
    {
        PV = GetComponent<PhotonView>();
        gravityRb = GetComponent<Rigidbody>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("SunTag"))
        {
            Rigidbody gravRB = other.GetComponent<Rigidbody>();
            //PV.RPC("AttractGarbage", RpcTarget.All, gravRB );
            AttractGarbage(gravRB);
        }
    }

    [PunRPC]
    void AttractGarbage(Rigidbody garbageToAttract)
    {
        Vector3 direction = gravityRb.position - garbageToAttract.position;
        float distance = direction.magnitude;

        float forceMagnitude = GravityForce * (gravityRb.mass * garbageToAttract.mass) / Mathf.Pow(distance, 2);
        Vector3 force = direction.normalized * forceMagnitude;

        garbageToAttract.AddForce(force);
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting == true)
        {
            stream.SendNext(GravityForce);
        }
        else
        {
            GravityForce = (float)stream.ReceiveNext();
        }
    }


}
