using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class QuestRequirement
{
    string name;

    string itemType;

    int numberOfItems;

    int totalItemsNeeded;
    public string Name { get { return name; } }
    public string ItemType { get { return itemType; } }
    public int NumberOfItems { get {return numberOfItems; } }

    public QuestRequirement(string name, string itemtype, int totalItemsNeeded)
    {
        this.name = name;
        this.itemType = itemtype;
        numberOfItems = 0;
        this.totalItemsNeeded = totalItemsNeeded;
    }
    
    public int CollectedItem() {
        numberOfItems++;
        return numberOfItems;
    }
    public bool CompletedQuest()
    {
        if (numberOfItems >= totalItemsNeeded)
        {
            
            return true;
        }
        return false;
    }
    public string QuestString()
    {
        return $"{Name} {ItemType}\t{NumberOfItems}/{totalItemsNeeded}\n";
    }

}
