using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipBobMainMenu : MonoBehaviour
{   
    public float rateOfBob;
    public float rotationVar;
    private float yPosition, xRotation;

    void Start()
    {
        yPosition = gameObject.transform.position.y;
        xRotation = gameObject.transform.rotation.x;
    }

    
    void Update()
    {       
        transform.position = new Vector3(transform.position.x, yPosition+ ( (float)Mathf.Sin(Time.time) *rateOfBob), transform.position.z);
        transform.rotation =  Quaternion.Euler(xRotation + ( (float)Mathf.Sin(Time.time) * rotationVar), (150f), transform.rotation.z);
    }
}
