using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SlothBossFight : MonoBehaviour
{
    public GameObject ThrowableObject;
    public GameObject player;
    public int ThrowTime;
    public int BossFightTime;
    float throwTimer;
    internal bool startFight;
    float timer;
    public GameObject QuestReward;
    Quest PlayerQuest;
    public PlatformManager platformManager;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerQuest == null)
        {
            PlayerQuest = player.GetComponentInChildren<QuestManager>().CurrentQuest;
        }

        if (startFight)
        {
            timer += Time.deltaTime;
            throwTimer += Time.deltaTime;
            if (throwTimer >= ThrowTime)
            {
                Instantiate(ThrowableObject,
                    new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z),
                    Quaternion.identity).GetComponent<Rigidbody2D>()
                    .AddForce(new Vector2((player.transform.position.x - this.transform.position.x)*1.5f,
                    player.transform.position.y - this.transform.position.y),ForceMode2D.Impulse);
                Debug.Log("Thrown");
                throwTimer = 0;
            }
            if(timer >= BossFightTime)
            {
                startFight = false;
                GameObject temp= Instantiate(QuestReward);
                PlayerQuest.CollectedItem(temp.GetComponentInChildren<IInventoryItem>());
                platformManager.RoomDone = PlayerQuest.CheckQuestDone();
                Destroy(this.gameObject);
                Destroy(temp);
            }
        }
    }
}
