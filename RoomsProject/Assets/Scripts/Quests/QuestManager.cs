using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestManager
{
    Inventory QuestInventory;
    Quest quest;
    public Quest CurrentQuest { get { return quest; } }
    // Start is called before the first frame update
    public QuestManager()
    {
        quest = new HallwayQuest();
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
