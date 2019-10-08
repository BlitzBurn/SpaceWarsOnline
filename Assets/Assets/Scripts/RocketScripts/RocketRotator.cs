using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketRotator : MonoBehaviour
{

    public float rotationSpeed;

    void FixedUpdate()
    {

        if (Input.GetKey(KeyCode.A))
        {
           gameObject.transform.Rotate(Vector3.down * rotationSpeed);
        }

        else if (Input.GetKey(KeyCode.D))
        {
            gameObject.transform.Rotate(Vector3.up * rotationSpeed);
        }
    }
}
