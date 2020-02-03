using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public GameObject Player;
    bool Connected;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!Connected) {
        //this.transform.position = Player.transform.position;
        //this.transform.parent = Player.transform;
            if (collision.gameObject.name == Player.name)
            {
                Player.GetComponent<PlayerMovement>().ThrowableObject = this.gameObject;
            }
        }
    }
}
