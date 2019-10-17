using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOmanager : MonoBehaviour
{
    //Try instatiating this shit with different rotations

    public Vector3 startPosition, endPosition;

    float movementSpeed = 0.05f;
    float frequency = 1;
    float magnitude = 5;

    float angle = 0f;

    //Vector3 pos, localScale;

    void Start()
    {
        
    }

    
    void Update()
    {
        angle += Time.deltaTime * movementSpeed;
        float angleBullShit = Mathf.Cos(2.0f * Mathf.PI * frequency * angle);
        transform.position = startPosition + (endPosition-startPosition) * 0.5f * (1-angleBullShit);
    }
}
