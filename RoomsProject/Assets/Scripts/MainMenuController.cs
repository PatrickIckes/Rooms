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
        SceneManager.LoadScene(0);
    }

    void Story()
    {
        MainMenuCanvas.SetActive(false);
        StoryCanvas.SetActive(true);
    }

    void Credits()
    {
        MainMenuCanvas.SetActive(false);
        CreditsCanvas.SetActive(true);
    }

    void CreditsBack()
    {
        CreditsCanvas.SetActive(false);
        MainMenuCanvas.SetActive(true);
    }

    void Settings()
    {
        MainMenuCanvas.SetActive(false);
        SettingsCanvas.SetActive(true);
    }

    void SettingsBack()
    {
        SettingsCanvas.SetActive(false);
        MainMenuCanvas.SetActive(true);
    }
}
