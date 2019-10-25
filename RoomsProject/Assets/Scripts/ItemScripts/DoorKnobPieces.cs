using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorKnobPieces : MonoBehaviour, IInventoryItem
{

    public bool IsQuestItem { get { return true; } }


    public string Name
    {
        get
        {
            return "Doorknob Pieces";
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
        Destroy(gameObject);
    }

}
