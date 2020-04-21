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
        GameObject tempItem = Instantiate(((MonoBehaviour)item).gameObject, new Vector3(), Quaternion.identity);
        DontDestroyOnLoad(tempItem);
        tempItem.SetActive(false);
        tempItem.name = tempItem.name.Substring(0, tempItem.name.Length - 7);
        inventory.AddItem(tempItem.GetComponent<IInventoryItem>());

    }
    public bool CheckObject(string name)
    {
        if (inventory == null) inventory = SingletonInventory.Inventory;
        return inventory.CheckObject(name);
    }
}
