using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MissileExplode : MonoBehaviour
{
    
    private string collisionIgnore;
    private Health health;

    
    
    public void SetCollisionIgnore(string originRocketName)
    {
        collisionIgnore = originRocketName;
        

    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collision.transform.name: " + collision.transform.name);
        Debug.Log("collisionIgnore: "+collisionIgnore);


        if (collision.transform.name != collisionIgnore)
        {
            Debug.Log("Damage taken " + collision.gameObject.name);
            health = collision.transform.GetComponent<Health>();
            health.TakeDamage(1);            
            DetonateMissile();
            
        }
        else if (collision.transform.name == collisionIgnore)
        {
            Debug.Log("Collision ignored: " + collisionIgnore);
            // Debug.Log("PewPaPew");
        }
    }

    
    public void DetonateMissile()
    {
        Destroy(gameObject);
        //Debug.Log("DetonateMissileCalled");
    }
    
}
