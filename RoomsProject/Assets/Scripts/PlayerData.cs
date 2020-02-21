using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerData : MonoBehaviour
{
    public GameObject gameManager;

    internal PlayerAttributes pa;
    [SerializeField]
    private int startinghealth = 20;
    [HideInInspector]
    public int health;
    private int previoushealth = 0;
    public static PlayerData SavePoint;
    public void ResetPlayer()
    {
        pa.health = startinghealth;
        pa.Collectables = new List<GameObject>();
    }

    // Start is called before the first frame update
    void Start()
    {
        pa = new PlayerAttributes(startinghealth,SceneManager.GetActiveScene().buildIndex,new List<GameObject>(), this.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (previoushealth != pa.health)
        {
            previoushealth = health = pa.health;
        }
    }
    internal void Damage(int damage)
    {
        pa.health -= damage;
        if (pa.health <= 0)
        {
            ResetPlayer();
            gameManager.GetComponent<MyGameManager>().GameInProgress = false;
        }
    }
}
