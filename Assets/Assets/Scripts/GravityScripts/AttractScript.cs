using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Photon.Pun;

public class AttractScript : MonoBehaviourPun
{
    public Rigidbody gravityRb;


    //AttractScript[] attractors = FindObjectsOfType<AttractScript>();

    //const float G = 667.4f;
    public float G;

    public static List<AttractScript> attractList;

    private void Start()
    {
        Debug.Log("Start called");
        gravityRb = GetComponent<Rigidbody>();

        if (attractList == null)
        {
            Debug.Log("attract list");
            attractList = new List<AttractScript>();
            Debug.Log("Added meself to da attracklist" + attractList.Count);
        }

        attractList.Add(this);
    }

    private void FixedUpdate()
    {
        foreach (AttractScript attractor in attractList)
        {
            if (attractor != this)
            {
                AttractGarbage(attractor);
            }
        }
    }

    private void OnEnable()
    {

        //Debug.Log("AttractScript: " + gameObject.name + " " + attractList.Count);
    }

    [PunRPC]
    void AttractGarbage(AttractScript garbageToAttract)
    {
        Rigidbody rigidbodyTobringOverHere = garbageToAttract.gravityRb;

        Vector3 direction = gravityRb.position - rigidbodyTobringOverHere.position;
        float distance = direction.magnitude;

        float forceMagnitude = G * (gravityRb.mass * rigidbodyTobringOverHere.mass) / Mathf.Pow(distance, 2);
        Vector3 force = direction.normalized * forceMagnitude;

        rigidbodyTobringOverHere.AddForce(force);
    }

}
