using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndRoom : MonoBehaviour
{
    public GameObject Spawner;
    public GameObject Door; // TODO Find a better way to handle this - preferably a way to get it to fall
    public GameObject Sword;
    public GameObject QuestReward;
    Quest PlayerQuest;
    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {

        Door.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerQuest == null)
        {
            PlayerQuest = Player.GetComponentInChildren<QuestManager>().CurrentQuest;
        }
    }
    public void RoomOver()
    {
        Door.SetActive(true);
        Instantiate(Sword, Spawner.transform);
        GameObject temp = Instantiate(QuestReward);
        PlayerQuest.CollectedItem(temp.GetComponentInChildren<IInventoryItem>());
    }
}
