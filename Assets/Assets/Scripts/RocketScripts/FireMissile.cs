using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class FireMissile :MonoBehaviourPun
{
    public GameObject Missile;   
    public Transform MissileSpawnPoint;
    public MissileExplode missileExplode;

    private PhotonView pv;

    public float MissileCooldown;    
    private float time;   
    private string rocketName;

    private PlayerScript _playerScript;


    void Start()
    {
        rocketName = gameObject.transform.name;

        _playerScript = GetComponent<PlayerScript>();
    }

    void Update()
    {
        time += Time.deltaTime;
        if (GameManagerScript.gameIsInProgress && _playerScript.playerIsAlive)
        {
            if (Input.GetKeyDown(KeyCode.Mouse1) && time >= MissileCooldown)
            {
                FireMissileMethod();

            }
        }
    }

    
    private void FireMissileMethod()
    {
        var instantiatedMissile = PhotonNetwork.Instantiate(Missile.gameObject.name, MissileSpawnPoint.position, MissileSpawnPoint.rotation);           
        missileExplode = instantiatedMissile.GetComponent<MissileExplode>();

        //missileExplode.SetCollisionIgnore(rocketName);

        time = 0;
    }
}
