using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidMovement : MonoBehaviour
{
    public float movementSpeed;
    public float rotationSpeed;

    private Rigidbody asteroidRB;


    void Start()
    {
        asteroidRB = gameObject.GetComponent<Rigidbody>();
    }

   
    void FixedUpdate()
    {
        asteroidRB.AddForce(new Vector3(1,0,0)* movementSpeed);

        gameObject.transform.Rotate(Vector3.up * rotationSpeed);
    }
}
