using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class AsteroidMovement : MonoBehaviourPunCallbacks
{
    public float movementSpeed;
    public float circleWidth;
    public float circleHeight;
    public float angle;


    public float rotationSpeed;

    private Rigidbody asteroidRB;


    void Start()
    {
        asteroidRB = gameObject.GetComponent<Rigidbody>();

        movementSpeed = 1;
        circleWidth = 10;
        circleHeight = 10;
    }

    void Update()
    {
        angle += Time.deltaTime*movementSpeed;

        float x = Mathf.Cos(angle)*circleWidth;
        float z = Mathf.Sin(angle) * circleHeight;
        float y = gameObject.transform.position.y;

        transform.position= new Vector3(x, y, z);
    }

    void FixedUpdate()
    {
        //asteroidRB.AddForce(new Vector3(1,0,0)* movementSpeed);
        //gameObject.transform.Rotate(Vector3.up * rotationSpeed);
    }
}
