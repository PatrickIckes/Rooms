using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HidingSpot : MonoBehaviour
{
    public GameObject Notification;
    public Text NotificationText;
    // Start is called before the first frame update
    void Start()
    {
        Notification.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Notification.SetActive(true);
        NotificationText.enabled = true;
        if (collision.gameObject.GetComponent<PlayerMovement>() != null)
        {
            collision.gameObject.GetComponent<PlayerMovement>().canCheckForObject = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Notification.SetActive(false);
        NotificationText.enabled = false;
        if (collision.gameObject.GetComponent<PlayerMovement>() != null)
        {
            collision.gameObject.GetComponent<PlayerMovement>().canCheckForObject = false;
        }
    }
}
