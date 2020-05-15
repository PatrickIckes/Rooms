using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinStateManager : MonoBehaviour
{
    public Button MainMenuButton;

    // Start is called before the first frame update
    void Start()
    {
        MainMenuButton.onClick.AddListener(MainMenu);
    }

    void MainMenu()
    {
        //jpost Audio
        //PlayUIClickPlay();

        SceneManager.LoadScene((int)Scenes.MainMenu);
    }

    //jpost audio
    void PlayUIClickPlay()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/UI/sx_game_menu_ui_click_play", gameObject.transform.position);
    }
}
