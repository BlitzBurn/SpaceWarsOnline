using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMissile : MonoBehaviour
{
    public Transform direction = null;
    public Rigidbody missileRB;
    public float missileForce;

    void Start()
    {
        missileRB = GetComponent<Rigidbody>();
    }

    // GetVelocity.rocketVelocity

    void Update()
    {
        if (this.direction != null )
        {
           // missileRB.AddForce(direction.up * (missileForce + GetVelocity.rocketSpeed) );

        }
    }
}
