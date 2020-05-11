using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    [SerializeField]
    private Button resumeButton, menuButton;
    [SerializeField]
    private GameObject pausePanel, dialoguePanel, interactText;

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
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            pausePanel.SetActive(true);
            dialoguePanel.SetActive(false);
            interactText.SetActive(false);

            Time.timeScale = 0;
        }
    }

    void Resume()
    {
        //jpost Audio
        PlayUIClickBack();

        pausePanel.SetActive(false);
        dialoguePanel.SetActive(true);
        interactText.SetActive(true);

        Time.timeScale = 1;
    }

    void MainMenu()
    {
        //jpost Audio
        PlayUIClickThrough();
        SceneManager.LoadScene((int)Scenes.MainMenu);
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
