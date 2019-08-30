using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketRotator : MonoBehaviour
{

    public GameObject ship;

    public float rotationSpeed;

    void FixedUpdate()
    {

        if (Input.GetKey(KeyCode.D))
        {
            ship.transform.Rotate(Vector3.down * rotationSpeed);
        }

        else if (Input.GetKey(KeyCode.A))
        {
            ship.transform.Rotate(Vector3.up * rotationSpeed);
        }
    }
}
