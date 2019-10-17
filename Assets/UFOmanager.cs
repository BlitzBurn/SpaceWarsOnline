using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOmanager : MonoBehaviour
{
    //Try instatiating this shit with different rotations

    public Transform startPosition, endPosition;
    //public GameObject 

    Rigidbody ufoRigidbody;
    public float movementSpeed = -18f;
   // public float frequency = 0.06f;
    public float magnitude = 5;

    private Vector3 ufoDirection;
    private Vector3 orthogonal;

    float angle = 0f;

    //Vector3 pos, localScale;

    void Start()
    {
          ufoRigidbody = gameObject.GetComponent<Rigidbody>();
        determineUfoDirection();
    }

    private void determineUfoDirection()
    {
        ufoDirection = (startPosition.position - endPosition.position).normalized;
        orthogonal = new Vector3(-ufoDirection.z, 0 , ufoDirection.x);
    }


    
    void Update()
    {
        angle += Time.deltaTime;

        ufoRigidbody.velocity = ufoDirection * movementSpeed + orthogonal * magnitude * Mathf.Sin(magnitude*angle);
        //float angleBullShit = Mathf.Cos(92.0f * Mathf.PI * frequency * angle);
       /// transform.position = startPosition.position + (endPosition.position-startPosition.position) * 0.5f * (1-angleBullShit);
    }
}
