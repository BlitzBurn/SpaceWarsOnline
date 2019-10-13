using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Health : MonoBehaviour
{

    public int AmountOfHealth;
    public bool isNonDestructible = false;

    void Update()
    {
        

        if (AmountOfHealth == 0 && isNonDestructible==false)
        {
            Destroy(gameObject);
            Debug.Log("Ded");
        }

    }

   
    public void TakeDamage(int damageTaken)
    {
        if (!isNonDestructible)
        {
            AmountOfHealth = AmountOfHealth - damageTaken;
        }
        else if (isNonDestructible)
        {
            //This crap is here to prevent nullreference exceptions when shooting garbage that cant be destroyed
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "MissileTag")
        {
           // Debug.Log("Missile hit");

            //MissileExplode explodeScript = collision.gameObject.GetComponent<MissileExplode>();
            //explodeScript.DetonateMissile();
        }        
    }
}
