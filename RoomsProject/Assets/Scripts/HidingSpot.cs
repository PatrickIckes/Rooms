using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HidingSpot : MonoBehaviour
{
    public Text Notification;
    // Start is called before the first frame update
    void Start()
    {
        Notification.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Notification.enabled = true;
        if(collision.gameObject.GetComponent<PlayerMovement>() != null)
        {
            collision.gameObject.GetComponent<PlayerMovement>().canCheckForObject = true;
        }
        Notification.text = "Press F to check for objects";

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Notification.enabled = false;
        if (collision.gameObject.GetComponent<PlayerMovement>() != null)
        {
            collision.gameObject.GetComponent<PlayerMovement>().canCheckForObject = false;
        }
    }
}
