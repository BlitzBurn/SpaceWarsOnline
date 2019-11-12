using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Health : MonoBehaviourPun/*, Photon.Pun.IPunObservable*/
{
    

    public int AmountOfHealth;
    private int maxHealth;
    public bool isNonDestructible = false;
    private float time;
    private float invisTime = (1.4f);
    private PhotonView PV;
    private PlayerScript _playerScript;

    [Header("Explosion")]
    public GameObject explosionprefab;
    //public GameObject explosionLocation;

    private void Start()
    {
        time = 0;
        maxHealth = AmountOfHealth;
        PV = GetComponent<PhotonView>();
        _playerScript = GetComponent<PlayerScript>();
    }

    void Update()
    {
        time += Time.deltaTime;

        if (AmountOfHealth <= 0 && isNonDestructible == false)
        {
            PhotonNetwork.Instantiate(explosionprefab.gameObject.name, gameObject.transform.position, Quaternion.identity);
            GameManagerScript.livingPlayers -= 1;
            Debug.Log(GameManagerScript.livingPlayers);
            Debug.Log("Ded");            
            AmountOfHealth = maxHealth;
            _playerScript.playerIsAlive = false;
            Debug.Log(gameObject.name + "||" + _playerScript.playerIsAlive);
        }

    }

    [PunRPC]
    public void TakeDamage(int damageTaken)
    {
        if (!isNonDestructible && time >= invisTime)
        {

            AmountOfHealth = AmountOfHealth - damageTaken;
            time = 0;
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
            PV.RPC("TakeDamage", RpcTarget.All, 1);
           
        }
        else if (collision.collider.tag == "SunTag")
        {
            PV.RPC("TakeDamage", RpcTarget.All, 15);
        }
    }
    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting == true)
        {
            stream.SendNext(AmountOfHealth);
            stream.SendNext(maxHealth);
            stream.SendNext(time);
            stream.SendNext(invisTime);
            stream.SendNext(GameManagerScript.livingPlayers);
        }
        else
        {
            AmountOfHealth = (int)stream.ReceiveNext();
            maxHealth = (int)stream.ReceiveNext();
            time = (float)stream.ReceiveNext();
            invisTime = (float)stream.ReceiveNext();
            GameManagerScript.livingPlayers = (int)stream.ReceiveNext();
        }
    }

}
