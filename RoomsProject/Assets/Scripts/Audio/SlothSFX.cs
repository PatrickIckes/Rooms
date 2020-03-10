using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlothSFX : MonoBehaviour
{
    ///jpost Audio
    ///a class to handle Sloth's sfx

    //jpost Audio
    public void PlaySlothBreathInhale()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/NPC/Sloth/sx_game_npc_sloth_breathing_inhale", GetComponent<Transform>().position);
    }
    //jpost Audio
    public void PlaySlothBreathExhale()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/NPC/Sloth/sx_game_npc_sloth_breathing_exhale", GetComponent<Transform>().position);
    }
}
