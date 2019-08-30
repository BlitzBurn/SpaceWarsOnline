using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveShipByBorders : MonoBehaviour
{
    private Rigidbody shipRB;

    public float leftXborder;
    public float rightXborder;
    public float upperYborder;
    public float bottomYborder;

    void Start()
    {
        shipRB = GetComponent<Rigidbody>();
    }

    void Update()
    {

        if (transform.position.x <= leftXborder)
        {
            transform.position = new Vector3(rightXborder - 1, transform.position.y, transform.position.z);
        }
        else if (shipRB.position.x >= rightXborder)
        {
            transform.position = new Vector3(leftXborder + 1, transform.position.y, transform.position.z);
        }
        else if (shipRB.position.z >= upperYborder)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, bottomYborder + 1);
        }
        else if (shipRB.position.z <= bottomYborder)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, upperYborder - 1);
        }

    }
}
