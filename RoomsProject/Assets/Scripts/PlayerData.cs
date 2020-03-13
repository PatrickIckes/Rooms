using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerData : MonoBehaviour
{
    public enum PlayerHurtAmount { unhurt, twoHealth, oneHealth, dead }

    [HideInInspector]
    public PlayerHurtAmount playerHurtAmount;

    public GameObject gameManager;

    internal PlayerAttributes pa;
    [SerializeField]
    private int startinghealth = 20;

    //[HideInInspector]
    public int health;

    private int previoushealth = 0;
    public static PlayerData SavePoint;

    [SerializeField]
    private Animator playerAnimator;

    public void ResetPlayer()
    {
        pa.health = startinghealth;
        pa.Collectables = new List<GameObject>();
    }

    // Start is called before the first frame update
    void Start()
    {
        pa = new PlayerAttributes(startinghealth,SceneManager.GetActiveScene().buildIndex,new List<GameObject>(), this.transform.position);
        playerHurtAmount = PlayerHurtAmount.unhurt;
    }

    // Update is called once per frame
    void Update()
    {
        if (previoushealth != pa.health)
        {
            previoushealth = health = pa.health;
        }

        CheckHealthLevelEnum();
    }

    private void CheckHealthLevelEnum()
    {
        switch (health)
        {
            case 3:
                playerHurtAmount = PlayerHurtAmount.unhurt;
                break;
            case 2:
                playerHurtAmount = PlayerHurtAmount.twoHealth;
                break;
            case 1:
                playerHurtAmount = PlayerHurtAmount.oneHealth;
                break;
        }
        if (pa.health <= 0)
        {
            playerHurtAmount = PlayerHurtAmount.dead;
            StartCoroutine(Death());
        }
    }

    internal void Damage(int damage)
    {
        pa.health -= damage;
        FMODUnity.RuntimeManager.PlayOneShot("event:/Player/Vocalizations/sx_game_plr_voc_grunt", GetComponent<Transform>().position);
    }

    private IEnumerator Death()
    {
        playerAnimator.Play("Walking");

        yield return new WaitForSeconds(2f);

        ResetPlayer();
        gameManager.GetComponent<MyGameManager>().GameInProgress = false;
}
