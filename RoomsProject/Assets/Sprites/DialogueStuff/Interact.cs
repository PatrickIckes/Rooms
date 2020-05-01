using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Interact : MonoBehaviour
{
    public GameObject Notification;
    public Text NotificationText;
    public bool inCollider;
    private DialogueManager dm;
    // Start is called before the first frame update
    void Start()
    {
        inCollider = false;
        dm = GetComponent<DialogueManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(inCollider && Input.GetKeyDown(KeyCode.E))
        {
            dm.SetText();
            dm.StartDialogue = true;
            this.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Notification.SetActive(true);
            NotificationText.enabled = true;
            inCollider = true;
        }
    }


}
