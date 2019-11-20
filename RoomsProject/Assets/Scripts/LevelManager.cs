using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public Quest levelQuest;
    public bool GravityEnabled;//Probably a bad practice but to work simply needed a public setter
    List<Quest> possibleQuests;
    private void Awake()
    {
    }
    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == HallwayQuest.CurrentScene)
        {
            levelQuest = new HallwayQuest();
        }
        else if (SceneManager.GetActiveScene().buildIndex == BeatSloth.CurrentScene)
        {
            levelQuest = new BeatSloth();
        }
    }
}