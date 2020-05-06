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

    private float minionSpawnTimer;
    public float minionSpawnTime;

    private float phase1Timer;
    public float phase1Time;

    [SerializeField]
    private Transform phase1NoteSpawner, phase2MinionSpawner;
    private Transform noteSpawner;

    [SerializeField]
    private BossHealth EnvysHealth;

    [SerializeField]
    private GameObject[] windows = new GameObject[3];

    [SerializeField]
    private GameObject envyPhase2Sprite, envyPhase2Hitbox;

    // Start is called before the first frame update
    void Start()
    {
        phase = BossFightPhase.First;
        noteSpawner = phase1NoteSpawner;

        envyPhase2Hitbox.SetActive(false);
        envyPhase2Sprite.SetActive(false);
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
            phase = BossFightPhase.Second;
        }
    }

    private void Phase2Attack()
    {
        phase2AttackTimer += Time.deltaTime;
        if (phase2AttackTimer >= phase2AttackTime)
        {
            envyPhase2Sprite.transform.position = windows[Random.Range(0, 2)].transform.position; //random window

            StartCoroutine(Phase2AttackInstance());
            phase2AttackTimer = 0;
        }
    }

    private void FireNotes()
    {
        noteTimer += Time.deltaTime;
        if (noteTimer >= noteFireTime)
        {
            Debug.Log("Fire Note");
            GameObject temp = Instantiate(Note, noteSpawner.position, Quaternion.identity, this.transform);
            temp.GetComponent<Fired>().Direction = new Vector3(Player.transform.position.x, Player.transform.position.y, Player.transform.position.z);
            noteTimer = 0;
        }
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
