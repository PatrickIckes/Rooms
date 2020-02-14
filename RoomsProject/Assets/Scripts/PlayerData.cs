using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerData : MonoBehaviour
{
    public GameObject gameManager;

    internal PlayerAttributes pa;
    private int startinghealth;
    public int health;
    private int previoushealth = 0;
    PlayerMovement playerMovement;
    public static PlayerData SavePoint;
    public void ResetPlayer()
    {
        pa.health = startinghealth;
        pa.Collectables = new List<GameObject>();
        
    }

    // Start is called before the first frame update
    void Start()
    {

        startinghealth = 3;
        playerMovement = GetComponent<PlayerMovement>();
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Hazard")
        {
            Damage(1);
            Destroy(collision.gameObject);
        }
    }
}
