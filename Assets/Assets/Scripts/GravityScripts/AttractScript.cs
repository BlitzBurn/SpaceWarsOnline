using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AttractScript : MonoBehaviour
{
    public Rigidbody gravityRb;

    private void FixedUpdate()
    {
        AttractScript[] attractors = FindObjectsOfType<AttractScript>();
        foreach(AttractScript attractorc in attractors)
        {
            if (attractorc != this)
            { 
             Attract(attractorc);
            }
        }
    }

    void Attract(AttractScript garbageToAttract)
    {
        Rigidbody rigidbodyTobringOverHere = garbageToAttract.gravityRb;

        Vector3 direction = gravityRb.position - rigidbodyTobringOverHere.position;
        float distance = direction.magnitude;

        float forceMagnitude = (gravityRb.mass * rigidbodyTobringOverHere.mass) / Mathf.Pow(distance, 2);
        Vector3 force = direction.normalized * forceMagnitude;

        rigidbodyTobringOverHere.AddForce(force);
    }

}
