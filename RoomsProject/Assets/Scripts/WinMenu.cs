using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinMenu : MonoBehaviour
{
    public Button ReplayButton;

    void Start()
    {
        ReplayButton.onClick.AddListener(Replay);
    }

    void Replay()
    {
        SceneManager.LoadScene(0);
    }
}