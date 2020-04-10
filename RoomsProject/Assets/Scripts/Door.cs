using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Door : MonoBehaviour
{
    /// <summary>
    /// Animtor variables for scene transitions.
    /// </summary>
    public Animator transition;
    public float transitionTime;

    public bool withinInteractable;
    public GameObject Key;
    public Scenes scene;
    public MyGameManager gameManager;
    public Inventory inventory;
    public Text QuestCollectionText;
    /// <summary>
    /// Figures out the situation and if the requirements are met it saves the game and goes to the next position
    /// </summary>
    public void OpenDoor()
    {
        //I think I left everything here for audio....Sorry if I accidentally deleted something
        //jpost Audio
        //PlayDoorOpen();
        //jpost Audio test delaying loading next scene to allow door open sfx to play properly
        //Invoke("LoadSceneSlothHallway", 1.01f);
        //jpost Audio test delaying loading next scene to allow door open sfx to play properly
        //Invoke("LoadSceneSlothHallway", 1.01f);
        if (Input.GetButtonDown("Interact") && withinInteractable)
        {
           if (Key != null && inventory.CheckObject(Key.name))
            {
                //jpost Audio
                PlayDoorOpen();

                LoadScene();
            }
            else if (Key == null)
            {
                LoadScene();
            }
            else
            {
                if (QuestCollectionText != null)
                {
                    QuestCollectionText.text = "You cannot unlock the door, it appears you need something to open it.";

                    //jpost Audio
                    PlayDoorLocked();
                }
            }
        }
    }

    private void LoadScene()
    {
        gameManager.SaveLevel();
        SceneManager.LoadScene((int)scene);
    }
    private void Update()
    {
        OpenDoor();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerMovement>().withinInteractable = true;
            withinInteractable = true;
            //QuestCollectionText.enabled = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerMovement>().withinInteractable = false;
            withinInteractable = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerMovement>().withinInteractable = true;
            withinInteractable = true;
            QuestCollectionText.enabled = true;
        }
    }

    public void PlayDoorLocked()
    {
        //play the FMOD event for door locked
        FMODUnity.RuntimeManager.PlayOneShot("event:/Interactible/Doors/sx_game_int_door_locked", GetComponent<Transform>().position);
    }

    public void PlayDoorOpen()
    {
        //play the FMOD event for door locked
        FMODUnity.RuntimeManager.PlayOneShot("event:/Interactible/Doors/sx_game_int_door_open", GetComponent<Transform>().position);
    }

    IEnumerator LoadLevel(int scene)
    {
        transition.SetTrigger("Start");
        

    }
}