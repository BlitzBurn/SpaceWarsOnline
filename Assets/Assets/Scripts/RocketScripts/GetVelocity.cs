using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetVelocity : MonoBehaviour
{
    public  Rigidbody rocketRigidbody;
    public static Vector3 rocketVelocity;
    public static float rocketSpeed;

    void Start()
    {
        Rigidbody rocketRigidbody = GetComponent<Rigidbody>();
    }

   
    void Update()
    {
        Vector3 rocketVelocity = rocketRigidbody.velocity;
       // Debug.Log(rocketVelocity);
       // rocketSpeed = rocketVelocity.sqrMagnitude;

    }
}
