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
        if (other.CompareTag("RocketTag") && GameManagerScript.gameIsInProgress)
        {
           // Debug.Log(GameManagerScript.gameIsInProgress);
            Rigidbody gravRB = other.GetComponent<Rigidbody>();
            PV.RPC("AttractGarbage", RpcTarget.AllViaServer, gravRB );
            //AttractGarbage(gravRB);
        }
    }

    [PunRPC]
    void AttractGarbage(Rigidbody garbageToAttract)
    {
        Vector3 direction = gravityRb.position - garbageToAttract.position;
        float distance = direction.magnitude;
        //Debug.Log("flkjhgdsfzökliojgvndlfoziskjngm");
        float forceMagnitude = GravityForce * (gravityRb.mass * garbageToAttract.mass) / Mathf.Pow(distance, 2);
        Vector3 force = direction.normalized * forceMagnitude;

        garbageToAttract.AddForce(force);
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting == true)
        {
            stream.SendNext(GravityForce);
            //stream.SendNext()
        }
        else
        {
            GravityForce = (float)stream.ReceiveNext();
        }
    }


}
