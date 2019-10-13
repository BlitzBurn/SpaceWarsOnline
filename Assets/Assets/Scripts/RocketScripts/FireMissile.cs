using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FireMissile :MonoBehaviour
{
    public GameObject Missile;   
    public Transform MissileSpawnPoint;
    public MissileExplode missileExplode;

   
    public float MissileCooldown;    
    private float time;   
    private string rocketName;
    
    void Start()
    {
        rocketName = gameObject.transform.name;
        //Debug.Log("And Da daddy is: "+rocketName);
    }

    void Update()
    {
        
        time += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Mouse1) && time >= MissileCooldown)
        {
            FireMissileMethod();
        }
    }

    
    private void FireMissileMethod()
    {
        var instantiatedMissile = Instantiate(Missile, MissileSpawnPoint.position, MissileSpawnPoint.rotation);           
        missileExplode = instantiatedMissile.GetComponent<MissileExplode>();

        missileExplode.SetCollisionIgnore(rocketName);

        time = 0;
    }
}
