using System.Collections.Generic;
using UnityEngine;

public class Quest
{
    protected List<QuestRequirement> qr;
    private bool IsDone;
    public int NextScene;
    public Quest()
    {
        NextScene = 0;
        qr = new List<QuestRequirement>();
    }
    public virtual bool CollectedItem(IInventoryItem item) 
    {
        foreach(var questrequirement in qr)
        {
            if(questrequirement.GetType() == typeof(QuestRequirement))
            {
                if (questrequirement.ItemType == item.Name)
                {
                    questrequirement.CollectedItem();
                    return true;
                }
            }
        }
        Debug.Log("Didn't Find item");
        return false;
    }
    string QuestString;
    public string DisplayQuest()
    {
        QuestString = "";
        foreach(QuestRequirement requirement in qr)
        {
            QuestString += requirement.QuestString()+"\n";
        }
        return QuestString;
    }

    public bool CheckQuestDone()
    {
        IsDone = true;
        foreach (var questrequirement in qr)
        {
            if(!questrequirement.CompletedQuest())
            {
                IsDone = false;
                break;
            }
        }
        return IsDone;
    }
}