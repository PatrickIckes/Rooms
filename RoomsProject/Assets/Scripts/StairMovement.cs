using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StairMovement : MonoBehaviour
{
    public GameObject stairway;
    public GameObject player;
    public GameObject ExitPoint;
    bool withinStairway;
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

        if(Input.GetButtonDown("Interact")&& withinStairway)
        {
            if (stairway.activeInHierarchy)
            {
                stairway.SetActive(false);
            }
            else
            {
                stairway.SetActive(true);
            }
            player.transform.position = new Vector3(ExitPoint.transform.position.x, ExitPoint.transform.position.y);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        withinStairway = true;
        Notification.SetActive(true);
        NotificationText.enabled = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        withinStairway = false;
        Notification.SetActive(false);
        NotificationText.enabled = false;
    }
}
