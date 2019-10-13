using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DestroyMissileAfterLifeTime : MonoBehaviour
{
   
    public float missileLifetime;

    void Start()
    {
        Destroy(gameObject, missileLifetime);
    }

}
