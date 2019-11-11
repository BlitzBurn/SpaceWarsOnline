using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateImage : MonoBehaviour
{
    RectTransform rectTransform;
    public float rotationRate;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        
    }

    
    void Update()
    {
        rectTransform.Rotate(new Vector3(0, 0, rotationRate*Time.deltaTime));
    }
}
