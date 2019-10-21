using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MoveMissile : MonoBehaviourPun
{
    private Transform direction = null;
    private Rigidbody missileRB;
    private Rigidbody rocketRB;    
    public float missileForce;
    private float velocity;

    void Start()
    {
        direction = gameObject.GetComponent<Transform>();

        missileRB = gameObject.GetComponent<Rigidbody>();
       


        rocketRB = gameObject.GetComponentInParent<Rigidbody>();

        //Debug.Log("Missile Start");
    }

    private void FixedUpdate()
    {
        missileRB.AddForce(direction.up*missileForce);
    }

    void Update()
    {
        velocity =  Time.deltaTime * missileForce;
        //transform.position += new Vector3(0, 0, 1);

        //missileRB = gameObject.GetComponent().Velocity= new ;

        if (this.direction != null)
        {
            //missileRB.velocity =(direction.up * velocity);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
       
        if (stream.IsWriting)
        {
            stream.SendNext(velocity);

        }
        else if (stream.IsReading)
        {
             velocity= (float)stream.ReceiveNext();
        }
    }
}
