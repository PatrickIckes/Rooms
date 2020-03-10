using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableTrashSFX : MonoBehaviour
{
    ///jpost Audio
    ///a class to handle some of the SFX involved with the throwable trash prefab
    ///

    //jpost Audio
    public void PlayThrowableTrashAppear()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Interactible/Trash/sx_game_int_slothfight_trashbag_appear", GetComponent<Transform>().position);
    }
}
