﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class SingletonInventory
    {
        private static SingletonInventory inventory;

        public static SingletonInventory Inventory
        {
            get
            {
                if (inventory == null)
                {
                    inventory = new SingletonInventory();
                }
                return inventory;
            }
        }

        [SerializeField] int SLOTS = 5;

        private List<IInventoryItem> mItems = new List<IInventoryItem>();
        public List<IInventoryItem> items { get { return new List<IInventoryItem>(mItems); } }
        public event EventHandler<InventoryEventArgs> ItemAdded;

        public GameObject InventoryUI;
        public GameObject PrefabImage;
        public Vector3 CurrentLocation;//Where the current inventory Item was on the canvas
        internal void PopulateInventory(List<IInventoryItem> playerinventory)
        {
            mItems.AddRange(playerinventory);
        }

        private SingletonInventory()
        {

        }

        public void AddItem(IInventoryItem item)
        {
            if (mItems.Count < SLOTS)
            {
                mItems.Add(item);

                item.OnPickup();

                if (ItemAdded != null)
                {
                    ItemAdded(this, new InventoryEventArgs(item));
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
                if (item.GetType().Name != "null")
                {
                    if (name == item.Name || item.GetType().Name == name)
                    {
                        if (item.GetType() == typeof(Key))
                        {
                            return true;
                        }
                        else
                        {
                            return true;
                        }
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
}
