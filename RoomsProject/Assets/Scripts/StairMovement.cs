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
    
    // Start is called before the first frame update
    void Start()
    {
        stairway.GetComponent<SpriteRenderer>().sortingLayerName = "Stairs";
        Notification.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.F)&& withinStairway)
        {
            if (stairway.activeInHierarchy)
            {
                stairway.SetActive(false);
            }
            else
            {
                stairway.SetActive(true);
            }
            Debug.Log(ExitPoint.name);
            player.transform.position = new Vector3(ExitPoint.transform.position.x, ExitPoint.transform.position.y);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        withinStairway = true;
        stairway.GetComponent<SpriteRenderer>().sortingLayerName = "Player";
        Notification.SetActive(true);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        withinStairway = false;
        Notification.SetActive(false);
    }
}
