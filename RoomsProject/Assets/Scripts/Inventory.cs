using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Inventory : MonoBehaviour
{
    public SingletonInventory inventory;
    internal void PopulateInventory(List<IInventoryItem> playerinventory)
    {
        inventory.PopulateInventory(playerinventory);
    }
    private void Start()
    {
        inventory = SingletonInventory.Inventory;
        Debug.Log("Item count " + inventory.items.Count);
    }

    public void AddItem(IInventoryItem item)
    {
        inventory.AddItem(item);
    }
    public bool CheckObject(string name)
    {
        return inventory.CheckObject(name);
    }
}
