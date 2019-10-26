using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RocketController : MonoBehaviourPun
{
    
    [Header("Player")]
    private Transform rocketDirection;
    private Rigidbody rocketRB;    
    public float rocketForce;    
    public float rotationSpeed;

    private string newName;

    void Start()
    {
        
        rocketRB = gameObject.GetComponent<Rigidbody>();
        rocketDirection = gameObject.GetComponent<Transform>();        
    }


    void FixedUpdate()
    {    
        if (Input.GetKey(KeyCode.Space))
        {
            Debug.Log("[Change Name Called]");
            newName = (string)PhotonNetwork.LocalPlayer.CustomProperties["CustomPlayerName"];
            Debug.Log("PhotonNetwork Name:" + PhotonNetwork.LocalPlayer.CustomProperties["CustomPlayerName"]);
            gameObject.name = newName;
            Debug.Log("newName " + newName);
            MoveForward();
        }

        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else if (Input.GetKey(KeyCode.D))
        {;
            RotateRight();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    private void MoveForward()
    {
        rocketRB.AddForce(rocketDirection.right * rocketForce);
    }

    private void RotateRight()
    {
        gameObject.transform.Rotate(Vector3.up * rotationSpeed);
    }
    
    private void RotateLeft()
    {
        gameObject.transform.Rotate(Vector3.down * rotationSpeed);
    }

   
}
