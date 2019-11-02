using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Health : MonoBehaviourPun/*, Photon.Pun.IPunObservable*/
{

    public int AmountOfHealth;
    public bool isNonDestructible = false;


    void Update()
    {
        if (AmountOfHealth <= 0 && isNonDestructible==false)
        {
            GameManagerScript.livingPlayers = -1;
            Debug.Log(GameManagerScript.livingPlayers);
            Debug.Log("Ded");
            PhotonNetwork.Destroy(gameObject);
            
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
    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)    
    {
        if (stream.IsWriting == true)
        {
            stream.SendNext(AmountOfHealth);
           
        }
        else
        {
             AmountOfHealth= (int)stream.ReceiveNext();
            
        }
    }

}
