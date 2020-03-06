using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public GameObject Player;
    bool Connected;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(Player.gameObject.GetComponent<PlayerMovement>().ThrowableObject);
        if (Player.gameObject.GetComponent<PlayerMovement>().ThrowableObject == null)
        {
            if (collision.gameObject.name == Player.name)
            {
                collision.gameObject.GetComponent<PlayerMovement>().ThrowableObject = this.gameObject;
                Connected = true;
            }
        }
    }
}
