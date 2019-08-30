using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileExplode : MonoBehaviour
{

    public GameObject Missile;

    public void DetonateMissile()
    {
        Destroy(gameObject);
        Debug.Log("DetonateMissileCalled");
    }
    
}
