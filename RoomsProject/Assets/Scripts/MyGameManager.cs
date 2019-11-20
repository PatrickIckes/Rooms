using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class MyGameManager : MonoBehaviour
{
    public GameObject[] Doors;//Keeps a list of the Dorrs(This honestly may not be needed and will probably be removed)
    public Vector2[] Boundaries;//Boundaries(Will be used to make sure everything is properly deleted and things are kept within.
    public GameObject player;//Used to keep track of the player
    public bool GameInProgress;//Used to see if the game is still in progress
    public bool restart;
    public LevelManager levelManager;
    public List<Quest> AllQuests;
    int PresentLevel;
    // Start is called before the first frame update
    void Start()
    {
        AllQuests = new List<Quest>();
        AllQuests.Add(new HallwayQuest());
        AllQuests.Add(new BeatSloth());//Solve adding quests later
        player.GetComponent<PlayerMovement>().gameManager = this;
        GameInProgress = true;
    }

    public void LoadGamesLevel(string levelname)
    {
        if (restart)
        {
            GameInProgress = true;
            restart = false;
            SceneManager.LoadScene("HubWorld", LoadSceneMode.Single);
        }
        else
        {
            DontDestroyOnLoad(player);
            SceneManager.LoadScene("Scene 2", LoadSceneMode.Single);
        }
        player.GetComponentInChildren<QuestManager>().CurrentQuest = levelManager.levelQuest;
    }

    //Gets in the cause of the game being over and prints it to the console.
    public void GameOver(string Text)
    {
        Debug.ClearDeveloperConsole();
        Debug.Log(Text);
        LoadLastSave();
    }
    // Update is called once per frame
    void Update()
    {
        if(!GameInProgress || Input.GetKeyDown(KeyCode.R))
        {
            restart = true;
            LoadGamesLevel("");
        }
        if (Input.GetKeyDown(KeyCode.Slash))//to restart
        {
            Debug.Log("Test");
            SaveLevel();
        }
        if (Input.GetKeyDown(KeyCode.Backslash))//to restart
        {
            LoadLastSave();
        }
    }
    internal void SaveLevel()
    {
        BinaryFormatter bf = new BinaryFormatter();
        PlayerAttributes data = player.GetComponent<PlayerData>().pa;
        PlayerData.SavePoint = player.GetComponent<PlayerData>();
        //data.inventory = player.GetComponent<PlayerMovement>().inventory.items;
        data.SetPlayerPosition(player.transform.position);
        data.CurrentScene = SceneManager.GetActiveScene().buildIndex;
        string path = Application.persistentDataPath + "/playerInfo.dat";
        if (!File.Exists(path))
        {
            using (FileStream fs = File.Create(Application.persistentDataPath + "/playerInfo.dat")) { }
        }
        using (FileStream fs = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open))
        {
            bf.Serialize(fs, data);
        }
        Debug.Log($"Saved at {Application.persistentDataPath}/playerInfo.dat");
    }
    PlayerAttributes LoadData()
    {
        BinaryFormatter bf = new BinaryFormatter();
        PlayerAttributes data = player.GetComponent<PlayerData>().pa;
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            using (FileStream fs = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open))
            {
               data = (PlayerAttributes)bf.Deserialize(fs);
            }
        }
        return data;

    }
    void LoadLastSave()
    {
        PlayerAttributes data = LoadData();
        player.transform.position = data.GetPlayerPosition();
        SceneManager.LoadScene(data.CurrentScene);
        player.GetComponent<PlayerMovement>().inventory.PopulateInventory(data.inventory);
    }
    internal void LoadInventory()
    { 
        //PlayerAttributes data = LoadData();
        //if (data.inventory != null)
        //{
        //    player.GetComponent<PlayerMovement>().inventory.PopulateInventory(data.inventory);
        //}
    }
}
