using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class UFOmanager : MonoBehaviourPun
{
    //Try instatiating this shit with different rotations


     [SerializeField]
    public List<GameObject> startPositions = new List<GameObject>();
    public List<Transform> EndPositions = new List<Transform>();

    Rigidbody ufoRigidbody;
    public GameObject UfoLaserBlast;
    public float movementSpeed = -18f;
    public float frequency = 0.06f;
    public float magnitude = 5;
    public float rotationSpeed;
    public static bool MoveUFO;
    private int ufoInt;

    [Header("Laser")]
    public List<GameObject> laserPointList = new List<GameObject>();
    public float laserForce;

    private Vector3 ufoDirection;
    private Vector3 orthogonal;
    private float ufoAngle;

    float angle = 0f;

    private PhotonView PV;

    [Header("FireLaserShit")]
    public float ufoFireRate;
    private float time;
    //Vector3 pos, localScale;

    void Start()
    {
        PV = GetComponent<PhotonView>();
        MoveUFO = false;
        transform.position = startPositions[1].transform.position;
        ufoRigidbody = gameObject.GetComponent<Rigidbody>();
        DetermineUfoDirection();

        foreach (GameObject ufoLaserFiringPoint in GameObject.FindGameObjectsWithTag("UFOLaserTag"))
        {
            laserPointList.Add(ufoLaserFiringPoint);
        }

    }

    [PunRPC]
    private void DetermineUfoDirection()
    {
        ufoDirection = (startPositions[1].transform.position - EndPositions[1].transform.position).normalized;
        orthogonal = new Vector3(-ufoDirection.z, 0, ufoDirection.x);
    }

    [PunRPC]
    private void FireLasers()
    {
        Rigidbody laserRB;
        for (int i = 0; i < laserPointList.Count; i++)
        {
            var InstantiatedLaserBlast = Instantiate(UfoLaserBlast, laserPointList[i].transform.position, laserPointList[i].transform.rotation);

            laserRB = InstantiatedLaserBlast.GetComponent<Rigidbody>();
        }

    }
    void Update()
    {
        if (MoveUFO)
        {
            ufoAngle += Time.deltaTime * rotationSpeed;

            gameObject.transform.Rotate(0, ufoAngle, 0);

            angle += Time.deltaTime;

            ufoRigidbody.velocity = ufoDirection * movementSpeed + orthogonal * magnitude * Mathf.Sin(frequency * angle);
        }

        time += Time.deltaTime;

        if (time >= ufoFireRate)
        {
            PV.RPC("FireLasers", RpcTarget.All);
            time = 0;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("UFOendPosition"))
        {
            transform.position = startPositions[1].transform.position;
            MoveUFO = false;
            movementSpeed = movementSpeed * (-1);
        }
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting == true)
        {
            stream.SendNext(MoveUFO);
            stream.SendNext(ufoFireRate);
            stream.SendNext(time);
        }
        else
        {
            MoveUFO = (bool)stream.ReceiveNext();
            ufoFireRate = (float)stream.ReceiveNext();
            time = (float)stream.ReceiveNext();
        }
    }




}
