using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestManager : MonoBehaviour
{
    Inventory QuestInventory;
    Quest quest;

    public Quest CurrentQuest { get { return quest; } set { quest = value; } }

    // Start is called before the first frame update
    private void Awake()
    {
    }
    private void Update()
    {
    }
    public void CollectedQuestItem(IInventoryItem item)
    {
        if(quest.CollectedItem(item))
        {
            item.OnPickup();
        }
    }
    public bool DoneWithQuest()
    {
        if (quest.CheckQuestDone())
        {
            return true;
        } else
        {
            return false;
        }
    }
}
