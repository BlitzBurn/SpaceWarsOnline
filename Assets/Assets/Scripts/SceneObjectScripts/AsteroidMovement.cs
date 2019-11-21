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
    private float angle;


    public float rotationSpeed;



    private Rigidbody asteroidRB;


    void Start()
    {
        asteroidRB = gameObject.GetComponent<Rigidbody>();

        //movementSpeed = 1;
        //circleWidth = 10;
        //circleHeight = 10;
    }

    void Update()
    {
        angle += Time.deltaTime*movementSpeed;
        
        float x = Mathf.Cos(angle)*circleWidth;
        float z = Mathf.Sin(angle) * circleHeight;
        float y = gameObject.transform.position.y;

        transform.position= new Vector3(x, y, z);

       // transform.RotateAround(Vector3.zero, Vector3.up, 50*Time.deltaTime);
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting == true)
        {
            stream.SendNext(movementSpeed);
            stream.SendNext(circleHeight);
            stream.SendNext(circleWidth);
            stream.SendNext(angle);
            stream.SendNext(rotationSpeed);
        }
        else
        {
            movementSpeed = (float)stream.ReceiveNext();
            circleHeight = (float)stream.ReceiveNext();
            circleWidth = (float)stream.ReceiveNext();
            angle = (float)stream.ReceiveNext();
            rotationSpeed = (float)stream.ReceiveNext();
        }
    }




    void FixedUpdate()
    {
        //asteroidRB.AddForce(new Vector3(1,0,0)* movementSpeed);
        //gameObject.transform.Rotate(Vector3.up * rotationSpeed);
    }
}
