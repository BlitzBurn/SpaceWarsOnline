using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LoginSceneSoundManager : MonoBehaviour /* IPointerEnterHandler, IPointerDownHandler*/
{
    public bool isSubmitName;

    [Header("Sound Clips")]
    public AudioClip buttonHighLightSound;
    public AudioClip buttonClickSound;

    [Header("AudioSources")]
    public AudioSource _ADS;

    public void OnButtonHighLight()
    {
        if(NetworkConnectionManager.hasEnteredName == true || isSubmitName == true)
        _ADS.PlayOneShot(buttonHighLightSound);
    }

    public void OnClickButton()
    {
        if (NetworkConnectionManager.hasEnteredName == true || isSubmitName == true)
            _ADS.PlayOneShot(buttonClickSound);
    }
   
}
