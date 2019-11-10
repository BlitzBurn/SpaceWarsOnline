using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class AttractScript_Version_2 : MonoBehaviourPunCallbacks
{
    [Range(0.1f, 1.5f)]
    public float attractionForce;
    private float distance;
    
    //public float attractionIncrease;

    //public Text attractionText;
    //public float attractionUI;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("SunTag") && GameManagerScript.gameIsInProgress)
        {
            distance = Vector3.Distance(other.transform.position, transform.position);

           // attractionUI = (attractionForce * Time.deltaTime) ;

            transform.position = Vector3.MoveTowards(transform.position, other.transform.position, (attractionForce * Time.deltaTime) );
        }
    }

    private void Update()
    {
       // attractionText.text = attractionUI.ToString();
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting == true)
        {
            stream.SendNext(attractionForce);
            stream.SendNext(distance);
        }
        else
        {
            attractionForce = (float)stream.ReceiveNext();
            distance = (float)stream.ReceiveNext();
        }
    }



}
