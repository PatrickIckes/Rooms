using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    float basespeed;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerData>().Damage(1);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.transform.position = new Vector3(this.transform.position.x,this.transform.position.y+0.25f);
            
            basespeed = collision.gameObject.GetComponent<PlayerMovement>().movementSpeed;
            collision.gameObject.GetComponent<PlayerMovement>().movementSpeed = 0;
        }
        else
        {
            gameObject.GetComponent<PlayerMovement>().movementSpeed = basespeed;
        }
    }
}
