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
    public Text InfoText;
    // Start is called before the first frame update
    void Start()
    {
        stairway.GetComponent<SpriteRenderer>().sortingLayerName = "Stairs";
        InfoText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F)&& withinStairway)
        {
            Debug.Log(ExitPoint.name);
            player.transform.position = new Vector3(ExitPoint.transform.position.x, ExitPoint.transform.position.y);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        withinStairway = true;
        stairway.GetComponent<SpriteRenderer>().sortingLayerName = "Player";
        InfoText.enabled = true;
        InfoText.text = "Press F to use the stairs";
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        withinStairway = false;
        stairway.GetComponent<SpriteRenderer>().sortingLayerName = "Stairs";
        InfoText.enabled = false;
    }
}
