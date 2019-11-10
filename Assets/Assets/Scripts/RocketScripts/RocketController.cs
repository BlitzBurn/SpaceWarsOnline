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
    private PlayerScript _playerScript;


    void Start()
    {
        _playerScript = GetComponent<PlayerScript>();
        rocketRB = gameObject.GetComponent<Rigidbody>();
        rocketDirection = gameObject.GetComponent<Transform>();
        gameObject.name = PhotonNetwork.NickName;
        
    }


    void FixedUpdate()
    {
        if (GameManagerScript.gameIsInProgress && _playerScript.playerIsAlive)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                MoveForward();
            }
        }

        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
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
