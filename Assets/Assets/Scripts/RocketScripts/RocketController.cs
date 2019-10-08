using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour
{
    //public Transform direction = null;

    private Transform rocketDirection;
    private Rigidbody rocketRB;

    public float rocketForce;
   

    void Start()
    {
        rocketRB = gameObject.GetComponent<Rigidbody>();
        rocketDirection = gameObject.GetComponent<Transform>();
        //rocketDirection = null;
    }

   
    void FixedUpdate()
    {
        if (this.rocketDirection != null && Input.GetKey(KeyCode.Space))
        {
            rocketRB.AddForce(rocketDirection.right*rocketForce);
        }

    }

}
