using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public GameObject gameManager;
    internal PlayerAttributes pa;
    private int startinghealth;
    public int health;
    private int previoushealth = 0;
    public void ResetPlayer()
    {
        pa.health = startinghealth;
        pa.Collectables = new List<GameObject>();
    }

    // Start is called before the first frame update
    void Start()
    {
        pa = new PlayerAttributes();
        startinghealth = 3;
    }

    // Update is called once per frame
    void Update()
    {

        if (previoushealth != pa.health)
        {
            previoushealth = health = pa.health;
            Debug.Log(health);
        }
    }
}
