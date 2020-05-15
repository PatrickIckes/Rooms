using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvyBossFight : MonoBehaviour
{
    private enum BossFightPhase { First, Second, Dead }

    private BossFightPhase phase;
    private float noteTimer;
    private float sandBagTimer;
    private float phase2AttackTimer;
    public float noteFireTime;
    public float sandbagFallTime;
    public float phase2AttackTime;
    public SandbagManager sandbagManager;
    public GameObject Note;
    public GameObject Minion;
    public GameObject Player;
    public Door EndRoom;
    private float minionSpawnTimer;
    public float minionSpawnTime;
    [SerializeField]
    private GameObject EnvyMask;
    private float phase1Timer;
    public float phase1Time;

    [SerializeField]
    private Inventory PlayerInventory;

    [SerializeField]
    private Transform phase1NoteSpawner, phase2MinionSpawner;
    private Transform noteSpawner;

    [SerializeField]
    private Animator SingAnimator;

    [SerializeField]
    private Enemy EnvysHealth;

    [SerializeField]
    private GameObject[] windows = new GameObject[3];

    [SerializeField]
    private GameObject envyPhase1,envyPhase2Sprite, envyPhase2Hitbox;

    private float envyPhase2SpriteXScale;

    bool endGame;
    bool fired;
    // Start is called before the first frame update
    void Start()
    {
        phase = BossFightPhase.First;
        noteSpawner = phase1NoteSpawner;

        envyPhase2Hitbox.SetActive(false);
        envyPhase2Sprite.SetActive(false);

        envyPhase2SpriteXScale = envyPhase2Sprite.transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        
        switch (phase) 
        {
            case BossFightPhase.First:
                FireNotes();
                DropSandbags();
                break;
            case BossFightPhase.Second:
                noteSpawner = phase2MinionSpawner;
                SpawnMinions();
                Phase2Attack();
                break;
            case BossFightPhase.Dead:

                break;
        }

        phase1Timer += Time.deltaTime;
        if (phase1Timer >= phase1Time && phase != BossFightPhase.Dead)
        {
            sandbagManager.DisableSandbags();
            StartCoroutine(StartPhaseTwo());
            phase = BossFightPhase.Second;
        }
        else if(phase == BossFightPhase.Dead && !endGame)
        {
            GameObject go = Instantiate(EnvyMask, new Vector3(), Quaternion.identity);
            PlayerInventory.AddItem(go.GetComponent<IInventoryItem>());
            EndRoom.OpenDoor(true);
            endGame = true;
        }
    }

    public IEnumerator StartPhaseTwo()
    {
        SingAnimator.SetTrigger("Phase2");
        yield return new WaitForSeconds(2f);
        envyPhase1.SetActive(false);
    }

    private void Phase2Attack()
    {
        if (envyPhase2Hitbox == null) phase = BossFightPhase.Dead;
        phase2AttackTimer += Time.deltaTime;
        if (phase2AttackTimer >= phase2AttackTime)
        {
            int window = Random.Range(0, 2);
            envyPhase2Sprite.transform.position = windows[window].transform.position; //random window

            if (window == 0)
            {
                envyPhase2Sprite.transform.localScale = new Vector3(envyPhase2SpriteXScale, envyPhase2Sprite.transform.localScale.y, envyPhase2Sprite.transform.localScale.z); 
            }
            else if (window == 1)
            {
                envyPhase2Sprite.transform.localScale = new Vector3(-envyPhase2SpriteXScale, envyPhase2Sprite.transform.localScale.y, envyPhase2Sprite.transform.localScale.z); 
            }

            StartCoroutine(Phase2AttackInstance());
            phase2AttackTimer = 0;
        }
    }

    private void FireNotes()
    {
        if (!fired)
        {
            noteTimer += Time.deltaTime;
            if (noteTimer >= noteFireTime)
            {
                StartCoroutine(FireNote());
            }
        }
    }
    
    public IEnumerator FireNote()
    {
        fired = true;
        SingAnimator.SetBool("Sing", true);
        yield return new WaitForSeconds(6f);
        GameObject temp = Instantiate(Note, noteSpawner.position, Quaternion.identity, this.transform);
        temp.GetComponent<Fired>().Direction = new Vector3(Player.transform.position.x, Player.transform.position.y, Player.transform.position.z);
        noteTimer = 0;
        SingAnimator.SetBool("Sing", false);
        fired = false;
    }

    //TODO: Edit once minion sprites exist
    private void SpawnMinions()
    {
        minionSpawnTimer += Time.deltaTime;
        if (minionSpawnTimer >= minionSpawnTime)
        {
            Debug.Log("Spawn Minion");
            GameObject temp = Instantiate(Minion, noteSpawner.position, Quaternion.identity, this.transform);
            minionSpawnTimer = 0;
        }
    }

    private void DropSandbags()
    {
        sandBagTimer += Time.deltaTime;
        if (sandBagTimer >= sandbagFallTime)
        {
            sandbagManager.fall = true;
            sandBagTimer = 0;
        }
    }

    private IEnumerator Phase2AttackInstance()
    {
        envyPhase2Sprite.SetActive(true);//start attack wind up animation
        yield return new WaitForSeconds(2f);
        envyPhase2Hitbox.SetActive(true);//attack and sit for a sec for the player to hit
        yield return new WaitForSeconds(1.7f);
        envyPhase2Hitbox.SetActive(false);//start moving back out window
        yield return new WaitForSeconds(2f);
        envyPhase2Sprite.SetActive(false);//disapear
    }
}
