using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public Quest levelQuest;
    public bool GravityEnabled;//Probably a bad practice but to work simply needed a public setter
    private void Awake()
    {
        
    }
    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)//TODO make this more dynamic for the quest system
        {
            levelQuest = new HallwayQuest();
        }
    }
}