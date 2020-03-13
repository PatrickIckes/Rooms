using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryMenu
    : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;

    private void Start()
    {
        Time.timeScale = 1.0f;
    }
    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (GameIsPaused)
            {

                Resume();
                Time.timeScale = 0.0f;
            }
            else
            {
                Pause();
            }
        }
        //Debug.Log(Time.timeScale);

    }

   public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1.0f;
        GameIsPaused = false;
        Debug.Log("time scale is 1");

    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0.0f;
        GameIsPaused = true;
        Debug.Log("time scale is 0");        
        PlayerMovement playerMovement = GetComponent<PlayerMovement>();
        

    }

    //add item check to disable mask buttons when not have yet

    public void ItemCheck()
    {

    }

    
}
