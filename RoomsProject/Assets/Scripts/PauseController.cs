﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    private bool pause;
    [SerializeField]
    private Button resumeButton, menuButton;
    [SerializeField]
    private GameObject pausePanel, dialoguePanel, interactText, lifeBar;

    // Start is called before the first frame update
    void Start()
    {
        resumeButton.onClick.AddListener(Resume);
        menuButton.onClick.AddListener(MainMenu);
    }

    private void Update()
    {
        PauseGame();
    }

    void PauseGame()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !pause)
        {
           if(pausePanel!=null)pausePanel.SetActive(true);
           if(dialoguePanel!=null)dialoguePanel.SetActive(false);
           if(interactText!=null)interactText.SetActive(false);
           if(lifeBar!=null)lifeBar.SetActive(false);

            pause = true;
            Time.timeScale = 0;
        } else if(Input.GetKeyDown(KeyCode.Escape))
        {
            Resume();
        }
    }

    void Resume()
    {



        if(pausePanel!=null)pausePanel.SetActive(false);
        if(dialoguePanel!=null)dialoguePanel.SetActive(true);
        if(interactText!=null)interactText.SetActive(true);
        if(lifeBar != null) lifeBar.SetActive(true);
        pause = false;
        Time.timeScale = 1;

        //jpost Audio
        PlayUIClickBack();
    }

    void MainMenu()
    {
        SceneManager.LoadScene((int)Scenes.MainMenu);

        //jpost Audio
        PlayUIClickThrough();
    }

    //jpost audio
    void PlayUIClickThrough()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/UI/sx_game_menu_ui_click_through", gameObject.transform.position);
    }
    //jpost audio
    void PlayUIClickBack()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/UI/sx_game_menu_ui_click_back", gameObject.transform.position);
    }
    //jpost audio
    void PlayUIClickPlay()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/UI/sx_game_menu_ui_click_play", gameObject.transform.position);
    }
}
