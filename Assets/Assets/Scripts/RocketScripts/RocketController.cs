using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour
{
    public Transform direction = null;

    
    public Rigidbody rocketRB;

    public float rocketForce;
   

    void Start()
    {
        rocketRB = GetComponent<Rigidbody>();
    }

   
    void FixedUpdate()
    {
        if (this.direction != null && Input.GetKey(KeyCode.Space))
        {
            rocketRB.AddForce(direction.right*rocketForce);
        }

    }

}
