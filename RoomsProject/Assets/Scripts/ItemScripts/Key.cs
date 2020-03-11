using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour, IInventoryItem
{
    public bool IsQuestItem
    {
        get
        {
            return false;
        }
    }
    public string Name
    {
        get
        {
            return gameObject.name;
        }
    }

    public Sprite _Image = null;

    public Sprite Image
    {
        get
        {
            return _Image;
        }
    }

    public void OnPickup()
    {
        // Add logic what happens when key is picked up by player for example unlock door
        
        gameObject.SetActive(false);
       

        //jpost Audio
        PlayKeyPickup();
        
    }

    //jpost Audio
    public void PlayKeyPickup()
    {
        //play the FMOD event for door locked
        FMODUnity.RuntimeManager.PlayOneShot("event:/Interactible/Keys/sx_game_int_key_pickup", GetComponent<Transform>().position);
    }

}
