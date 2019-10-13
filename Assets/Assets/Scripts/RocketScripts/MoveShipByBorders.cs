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

    public static float leftXborderstatic;
    public static float rightXborderstatic;
    public static float upperYborderstatic;
    public static float bottomYborderstatic;

    void Start()
    {
        shipRB = GetComponent<Rigidbody>();

        leftXborderstatic = leftXborder;
        rightXborderstatic = rightXborder;
        upperYborderstatic = upperYborder;
        bottomYborderstatic = bottomYborder;
    }

    void Update()
    {

        if (transform.position.x <= leftXborderstatic)
        {
            transform.position = new Vector3(rightXborderstatic - 1, transform.position.y, transform.position.z);
        }
        else if (shipRB.position.x >= rightXborderstatic)
        {
            transform.position = new Vector3(leftXborderstatic + 1, transform.position.y, transform.position.z);
        }
        else if (shipRB.position.z >= upperYborderstatic)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, bottomYborderstatic + 1);
        }
        else if (shipRB.position.z <= bottomYborderstatic)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, upperYborderstatic - 1);
        }

    }
}
