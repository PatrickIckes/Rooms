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
    //jpost Audio
    public void PlaySlothLaugh()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/NPC/Sloth/sx_game_npc_sloth_laugh", GetComponent<Transform>().position);
    }
    //jpost Audio
    public void PlaySlothRummageUnderBelly()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/NPC/Sloth/sx_game_npc_sloth_rummage_under_belly", GetComponent<Transform>().position);
    }
    //jpost Audio
    public void PlaySlothThrowTrash()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/NPC/Sloth/sx_game_npc_sloth_throw_trash", GetComponent<Transform>().position);
    }
}
