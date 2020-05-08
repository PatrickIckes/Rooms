using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    void Resume()
    {
        //jpost Audio
        PlayUIClickBack();

        pausePanel.SetActive(false);
        dialoguePanel.SetActive(true);
        interactText.SetActive(true);
    }

    void MainMenu()
    {
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
