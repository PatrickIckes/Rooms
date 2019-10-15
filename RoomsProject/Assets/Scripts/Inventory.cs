using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] int SLOTS = 5;

    private List<IInventoryItem> mItems = new List<IInventoryItem>();

    public event EventHandler<InventoryEventArgs> ItemAdded;

    public GameObject InventoryUI;
    public GameObject PrefabImage;
    public Vector3 CurrentLocation;//Where the current inventory Item was on the canvas
    private void Start()
    {
        CurrentLocation = InventoryUI.transform.position;
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

}
