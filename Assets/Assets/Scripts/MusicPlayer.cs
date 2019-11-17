using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [Header("AudioStuff")]
    public AudioClip sound;
    private AudioSource ADS;
    public bool isLogin;

    void Start()
    {
        ADS = GetComponent<AudioSource>();

        if (isLogin)
        {
            ADS.loop = true;
        }
        else
        {
            ADS.loop = false;
        }

        ADS.clip = sound;
        ADS.Play();
    }
}
