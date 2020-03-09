using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMODLifetimeHandler : MonoBehaviour
{
    /*Manages the lifetime of FMOD Studio objects*/
    //has a camera gameobject
    GameObject mainCamera;

    private void Start()
    {
        //initialzie attributes
        mainCamera = GameObject.Find("Main Camera");
        //run FMODPersistence method
        FMODPersistence();
    }

    //Finds FMOD studio listener component and allows it to persist throughout scene changes
    private void FMODPersistence()
    {
        //makes sure that the mainCamera is NOT destroyed during scene loading/changes so that audio can continue playback without interruption
        DontDestroyOnLoad(mainCamera);
        //check to see if mainCamera already has an FMOD studio listener and add one if it does not
        if (!mainCamera.GetComponentInChildren<FMODUnity.StudioListener>())
        {
            mainCamera.AddComponent<FMODUnity.StudioListener>();
        }
    }
}
