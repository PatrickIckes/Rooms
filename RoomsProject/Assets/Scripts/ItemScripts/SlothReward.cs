using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.ItemScripts
{
    class SlothReward : MonoBehaviour, IInventoryItem
    {
        public bool IsQuestItem { get { return true; } }


        public string Name
        {
            get
            {
                return "Sloth Reward";
            }
        }

        public Sprite _Image = null;

        public Sprite Image
        {
            get
            {
                return _Image;
            }
        }

        public void OnPickup()
        {
            // Add logic what happens when key is picked up by player for example unlock door
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
