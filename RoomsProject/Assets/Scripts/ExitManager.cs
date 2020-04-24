using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> Rewards;
    [SerializeField]
    private Inventory player;
    [SerializeField]
    Door door;
    private bool ended;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }
    public void EndRoom()
    {
        if (!ended)
        {
            foreach (GameObject reward in Rewards)
            {
                IInventoryItem item = reward.GetComponent<IInventoryItem>();
                if (item is IInventoryItem)//since I still don't know how to get null this is my work around
                {
                    ended = true;
                    player.AddItem(item);
                    door.OpenDoor(true);
                }
            }
        }
    }
}
