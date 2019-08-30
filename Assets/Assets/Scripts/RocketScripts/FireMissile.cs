using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireMissile : MonoBehaviour
{
    public GameObject Missile;
    public GameObject Rocket;    
    public Transform MissileSpawnPoint;    

    public float MissileCooldown;
    private float time;
    
    void Update()
    {
        time += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Mouse1) && time >= MissileCooldown)
        {
            Instantiate(Missile, MissileSpawnPoint.position, MissileSpawnPoint.rotation);
            time = 0;
        }


    }

}
