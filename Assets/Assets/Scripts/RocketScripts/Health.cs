using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    public static float HealtPoints;
    public int AmountOfHealth;

    

    void Start()
    {
        
    }

  
    void Update()
    {

        if (AmountOfHealth == 0)
        {
            Destroy(gameObject);
            Debug.Log("Ded");
        }

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "MissileTag")
        {
            Debug.Log("Missile hit");

            MissileExplode explodeScript = collision.gameObject.GetComponent<MissileExplode>();
            explodeScript.DetonateMissile();

            AmountOfHealth = AmountOfHealth - 1;
        }
        
    }


     

}
