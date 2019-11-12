using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateImage : MonoBehaviour
{
    Transform rectTransform;
    public float rotationRate;

    void Start()
    {
        rectTransform = GetComponent<Transform>();
        
    }

    
    void Update()
    {
        rectTransform.Rotate(new Vector3(0,  rotationRate * Time.deltaTime,  0 ));
    }
}
