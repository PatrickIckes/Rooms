using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public Button StartGameButton;
    public Button PlayGameButton;
    public Button SettingsButton;
    public Button CreditsButton;
    public Button CreditsBackButton;
    public Button SettingsBackButton;
    public GameObject MainMenuCanvas;
    public GameObject StoryCanvas;
    public GameObject SettingsCanvas;
    public GameObject CreditsCanvas;

    // Start is called before the first frame update
    void Start()
    {
        StartGameButton.onClick.AddListener(Story);
        PlayGameButton.onClick.AddListener(StartGame);
        SettingsButton.onClick.AddListener(Settings);
        CreditsButton.onClick.AddListener(Credits);
        CreditsBackButton.onClick.AddListener(CreditsBack);
        SettingsBackButton.onClick.AddListener(SettingsBack);
    }

    void StartGame()
    {
        //jpost Audio
        PlayUIClickPlay();

        SceneManager.LoadScene((int)Scenes.Tutorial);
    }

    void Story()
    {
        //jpost Audio
        PlayUIClickThrough();

        MainMenuCanvas.SetActive(false);
        StoryCanvas.SetActive(true);
    }

    void Credits()
    {
        //jpost Audio
        PlayUIClickThrough();

        MainMenuCanvas.SetActive(false);
        CreditsCanvas.SetActive(true);
    }

    void CreditsBack()
    {
        //jpost Audio
        PlayUIClickBack();

        CreditsCanvas.SetActive(false);
        MainMenuCanvas.SetActive(true);
    }

    void Settings()
    {
        //jpost Audio
        PlayUIClickThrough();

        MainMenuCanvas.SetActive(false);
        SettingsCanvas.SetActive(true);
    }

    void SettingsBack()
    {
        //jpost Audio
        PlayUIClickBack();

        SettingsCanvas.SetActive(false);
        MainMenuCanvas.SetActive(true);
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
