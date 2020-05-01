using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndSlothRoom : MonoBehaviour
{
    public GameObject Spawner;
    public GameObject Door; // TODO Find a better way to handle this - preferably a way to get it to fall
    public GameObject Sword;
    public GameObject QuestReward;
    Quest PlayerQuest;
    public GameObject Player;
    [SerializeField]
    Animator slothRoomAnim;
    Animator dooranimator;
    // Start is called before the first frame update
    void Start()
    {
        dooranimator = Door.GetComponentInChildren<Animator>();
        Door.gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerQuest == null)
        {
            PlayerQuest = Player.GetComponentInChildren<QuestManager>().CurrentQuest;
        }
        else if (SingletonInventory.Inventory.CheckObject("Sword"))
        {
            slothRoomAnim.SetTrigger("SwordAcquired");
            dooranimator.SetBool("Fall", true);
        }
    }
    public void RoomOver()
    {

        slothRoomAnim.SetTrigger("SlothKilled");
        StartCoroutine(SpitOutSword());
        Instantiate(Sword, Spawner.transform.position, Quaternion.identity);
        GameObject temp = Instantiate(QuestReward, Spawner.transform.position, Quaternion.identity);
        PlayerQuest.CollectedItem(temp.GetComponentInChildren<IInventoryItem>());
    }

    IEnumerator SpitOutSword()
    {
        yield return 2.12;
    }
}
