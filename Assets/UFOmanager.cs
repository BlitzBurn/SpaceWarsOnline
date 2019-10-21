using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class UFOmanager : MonoBehaviourPun
{
    //Try instatiating this shit with different rotations

    
    
    [Header("UFO movement")]
    public Transform startPosition, endPosition;
    Rigidbody ufoRigidbody;
    public GameObject UfoLaserBlast;
    public float movementSpeed = -18f;
    public float frequency = 0.06f;
    public float magnitude = 5;
    public float rotationSpeed;

    [Header("Laser")]
    public List<GameObject> laserPointList = new List<GameObject>();
    public float laserForce;

    private Vector3 ufoDirection;
    private Vector3 orthogonal;
    private float ufoAngle;

    float angle = 0f;

    //Vector3 pos, localScale;

    void Start()
    {
        transform.position= startPosition.position;
        ufoRigidbody = gameObject.GetComponent<Rigidbody>();
        DetermineUfoDirection();

        foreach (GameObject ufoLaserFiringPoint in GameObject.FindGameObjectsWithTag("UFOLaserTag"))
        {
            laserPointList.Add(ufoLaserFiringPoint);
        }

    }

    private void DetermineUfoDirection()
    {
        ufoDirection = (startPosition.position - endPosition.position).normalized;
        orthogonal = new Vector3(-ufoDirection.z, 0 , ufoDirection.x);
    }

    private IEnumerator FireLasers()
    {
        Rigidbody laserRB;
        for (int i = 0; i < laserPointList.Count; i++)
        {
           var InstantiatedLaserBlast = Instantiate(UfoLaserBlast, laserPointList[i].transform.position, laserPointList[i].transform.rotation);

            laserRB = InstantiatedLaserBlast.GetComponent<Rigidbody>();
           // laserRB.AddForce(laserPointList[i].transform.rotation* laserForce);
        }

        yield return null;
    }


    
    void Update()
    {
        ufoAngle += Time.deltaTime * rotationSpeed;

        if (Input.GetKeyDown(KeyCode.Tab))
            StartCoroutine(FireLasers());

        gameObject.transform.Rotate(0, ufoAngle, 0);

        angle += Time.deltaTime;

        ufoRigidbody.velocity = ufoDirection * movementSpeed + orthogonal * magnitude * Mathf.Sin(frequency * angle);
        //float angleBullShit = Mathf.Cos(92.0f * Mathf.PI * frequency * angle);
       /// transform.position = startPosition.position + (endPosition.position-startPosition.position) * 0.5f * (1-angleBullShit);
    }
}
