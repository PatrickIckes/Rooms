using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMODLifetimeHandler : MonoBehaviour
{
    /*Manages the lifetime of FMOD Studio objects*/

    private void Start()
    {
        FMODPersistence();
    }

    //Finds FMOD studio listener component and allows it to persist throughout scene changes
    private void FMODPersistence()
    {
        DontDestroyOnLoad(GameObject.Find("Main Camera"));
    }
}
