using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public Quest levelQuest;
    public bool GravityEnabled;//Probably a bad practice but to work simply needed a public setter
    List<Quest> possibleQuests;
    public Text questText; 
    private void Awake()
    {
    }
    private void Start()
    {
        if (questText != null)
        {
            if (SceneManager.GetActiveScene().buildIndex == HallwayQuest.CurrentScene)
            {
                levelQuest = new HallwayQuest();
            }
            else if (SceneManager.GetActiveScene().buildIndex == BeatSloth.CurrentScene)
            {
                levelQuest = new BeatSloth();

            } else
            {
                levelQuest = new Quest();
            }
        }
    }
    string pastText;
    private void Update()
    {
        if(levelQuest!=null && levelQuest.GetType() != typeof(Quest) && levelQuest.DisplayQuest() != pastText)
        {
            questText.text = levelQuest.DisplayQuest();
            pastText = questText.text;
        }
    }
}