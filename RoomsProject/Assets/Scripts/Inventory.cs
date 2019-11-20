using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Inventory : MonoBehaviour
{
    [SerializeField] int SLOTS = 5;

    private List<IInventoryItem> mItems = new List<IInventoryItem>();
    public List<IInventoryItem> items {get {return new List<IInventoryItem>(mItems);}}
    public event EventHandler<InventoryEventArgs> ItemAdded;

    public GameObject InventoryUI;
    public GameObject PrefabImage;
    public Vector3 CurrentLocation;//Where the current inventory Item was on the canvas
    internal void PopulateInventory(List<IInventoryItem> playerinventory)
    {
        mItems.AddRange(playerinventory);
    }
    private void Start()
    {
        if (InventoryUI != null)
        {
            CurrentLocation = InventoryUI.transform.position;
        }
    }

    public void AddItem(IInventoryItem item)
    {
        if(mItems.Count < SLOTS)
        {
            Collider2D collider = (item as MonoBehaviour).GetComponent<Collider2D>();
            if(collider.enabled)
            {
                collider.enabled = false;

                mItems.Add(item);

                item.OnPickup();

                GameObject temp = Instantiate(PrefabImage, new Vector3(CurrentLocation.x + PrefabImage.transform.localScale.x,CurrentLocation.y,CurrentLocation.z), Quaternion.identity);
                temp.transform.SetParent(InventoryUI.transform);
                temp.GetComponent<Image>().sprite = item.Image; 

                if (ItemAdded != null)
                {
                    ItemAdded(this, new InventoryEventArgs(item));
                }
            }
        }
    }
    public bool CheckObject(string name)
    {
        return CheckExistence(name);
    }
    private bool CheckExistence(string name)
    {
        foreach (IInventoryItem item in mItems)
        {
            if (name == "key")
            {
                if (item.GetType() == typeof(Key))
                {
                    return true;
                }
                else
                {
                    //This should not be a thing that happens.
                    Debug.Log("Wrong Implementation Exception");
                    return false;
                }
            }
            else
            {
                Debug.Log("Not looking for a key");
            }
        }
        return false;
    }
}
