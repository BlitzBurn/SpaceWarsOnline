﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MoveMissile : MonoBehaviour
{
    private Transform direction = null;
    private Rigidbody missileRB;
    private Rigidbody rocketRB;    
    public float missileForce;

    void Start()
    {
        direction = gameObject.GetComponent<Transform>();

        missileRB = gameObject.GetComponent<Rigidbody>();

        rocketRB = gameObject.GetComponentInParent<Rigidbody>();

        //Debug.Log("Missile Start");
    }

   

    void Update()
    {
        if (this.direction != null)
        {
            missileRB.velocity =(direction.up * Time.deltaTime * missileForce);
        }
    }
}
