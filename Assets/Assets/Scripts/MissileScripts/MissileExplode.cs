using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Photon.Pun;

public class MissileExplode : MonoBehaviourPun
{

    private string collisionIgnore;
    private Health health;

    [Header("Explosion")]
    public GameObject explosionPrefab;

    [Header("Missile Sound stuff")]
    public AudioClip missileThrust;
    public AudioClip explosionSound;
    private AudioSource ad;

    private void Start()
    {
        ad = GetComponent<AudioSource>();
        //ad.loop = true;
        ad.clip = missileThrust;
        ad.Play();
    }

    public void SetCollisionIgnore(string originRocketName)
    {
        collisionIgnore = originRocketName;


    }

    

    private void OnCollisionEnter(Collision collision)
    {
        PhotonNetwork.Instantiate("Explosion", gameObject.transform.position, Quaternion.identity, 0);
        ad.Stop();
       
        Debug.Log("Kaboom");
        //PhotonNetwork.Destroy(gameObject);
        Destroy(gameObject);
        //health = collision.transform.GetComponent<Health>();
       // health.TakeDamage(1);

        //Debug.Log("collision.transform.name: " + collision.transform.name);
        //Debug.Log("collisionIgnore: "+collisionIgnore);
        /*
        if (collision.transform.name != collisionIgnore)
        {
            Debug.Log("Damage taken " + collision.gameObject.name);
            health = collision.transform.GetComponent<Health>();
            health.TakeDamage(1);            
            DetonateMissile();
            
        }
        else if (collision.transform.name == collisionIgnore)
        {
            //Debug.Log("Collision ignored: " + collisionIgnore);
            // Debug.Log("PewPaPew");
        }
        */
    }


    public void DetonateMissile()
    {
        Destroy(gameObject);
        //Debug.Log("DetonateMissileCalled");
    }

}
