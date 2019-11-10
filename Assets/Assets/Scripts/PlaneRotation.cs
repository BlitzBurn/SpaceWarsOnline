using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneRotation : MonoBehaviour
{
    private float rotationAngle;
    public float rotationSpeed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //rotationAngle += Time.deltaTime ;

        transform.rotation *= Quaternion.Euler(0, Time.deltaTime*rotationSpeed, 0  ) ;
    }
}
