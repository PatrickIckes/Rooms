using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryMenu
    : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;

    void Update()
    {
        ItemCheck();

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (GameIsPaused)          
                Resume();
            else
                Pause();
        }  
    }

   public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    //add item check to disable mask buttons when not have yet

    public void ItemCheck()
    {

    }

    
}
