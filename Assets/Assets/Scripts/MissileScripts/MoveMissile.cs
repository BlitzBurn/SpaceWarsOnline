using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMissile : MonoBehaviour
{
    public Transform direction = null;
    private Rigidbody missileRB;
    private Rigidbody rocketRB;
    public float missileForce;

    void Start()
    {
        missileRB = gameObject.GetComponent<Rigidbody>();

        rocketRB = gameObject.GetComponentInParent<Rigidbody>();
    }

    // GetVelocity.rocketVelocity

    void Update()
    {
        Debug.Log(rocketRB.velocity);
        if (this.direction != null )
        {
            //  missileRB.AddForce(direction.up * (missileForce + GetVelocity.rocketSpeed) );

            //missileRB.AddForce(direction.up*Time.deltaTime*missileForce);

            missileRB.velocity =(direction.up * Time.deltaTime * missileForce);


        }
    }
}
