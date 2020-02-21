using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SlothBossFight : MonoBehaviour
{
    enum BossFight { First, Second, Dead }

    BossFight rounds;
    public GameObject[] Platforms;
    public GameObject[] Lights;
    public GameObject[] FallingPlanks;
    double timer;
    double fallingtimer;
    private GameObject Debris;
    public GameObject FirstStageDebris;
    public GameObject SecondStageDebris;
    public MyGameManager gm;
    public GameObject Sloth;
    public GameObject EndManager; // Object with the end script on it
    public static bool DropItem;
    public float spawnHeight;
    public GameObject TrashDrop;
    public float timeBetweenFalls;
    public float firstStageTime;
    internal bool RoomDone;
    private int timesThrough;
    private bool finished;

    void Start()
    {
        timer = 0;
        rounds = BossFight.First;
        timesThrough = 0;
        Debris = FirstStageDebris;
        finished = false;
    }

    // Update is called once per frame
    void Update()
    {
        StageManager();
    }

    private void StageManager()
    {
        timer += Time.deltaTime;

        if (rounds == BossFight.First || rounds == BossFight.Second)
        {
            FallTrigger();
        }

        //first stage done
        if (timer >= firstStageTime && rounds == BossFight.First)
        {
            PlanksFall();
            Debris = SecondStageDebris;
            rounds = BossFight.Second;
        }

        if (Sloth == null)
        {
            rounds = BossFight.Dead;
        }

        if (rounds == BossFight.Dead && !finished)
        {
            Dead();
            finished = true;
            ClearLights();
        }
    }

    private void FallTrigger()
    {
        fallingtimer += Time.deltaTime;

        if (fallingtimer > timeBetweenFalls)
        {
            int safe = Random.Range(0, 3);
            fallingtimer = 0;
            IndicateSafe(safe);
            TriggerFall(safe);

            if (rounds == BossFight.Second)
            {
                timesThrough++;

                //throwable spawning
                if (timesThrough == 3)
                {
                    int rand = Random.Range(0, 3);
                    timesThrough = 0;
                    Instantiate(TrashDrop, Platforms[rand].transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
                }
            }
        }
    }

    private void Dead()
    {
        EndManager.GetComponent<EndRoom>().RoomOver();
    }

    private void PlanksFall()
    {
        foreach (GameObject plank in FallingPlanks)
        {
            plank.GetComponent<BoxCollider2D>().isTrigger = true;
            plank.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }
    }

    private void IndicateSafe(int safe)
    {
        int i = 0;
        Lights[safe].GetComponent<SpriteRenderer>().color = Color.green;
        //jpost Audio
        //play the FMOD event for door locked
        FMODUnity.RuntimeManager.PlayOneShot("event:/Environment/Lights/sx_game_env_spotlight_on", Lights[safe].GetComponent<Transform>().position);
        foreach (GameObject platform in Platforms)
        {
            if (platform != Platforms[safe])
            {
                Lights[i].GetComponent<SpriteRenderer>().color = Color.magenta;
            }
            i++;
        }
    }
    private void TriggerFall(int safe)
    {
        int i = 0;
        foreach (GameObject platform in Platforms)
        {
            if (platform != Platforms[safe])
            {
                Vector3 p = platform.transform.position;
                GameObject temp = Instantiate(Debris, new Vector3(p.x, p.y + spawnHeight, p.z), Quaternion.identity);
            }
            i++;
        }
    }

    private void ClearLights()
    {
        int i = 0;
        foreach (GameObject platform in Platforms)
        {
            Lights[i].GetComponent<SpriteRenderer>().color = new Color(185, 183, 183);
            i++;
        }
    }

    //jpost Audio
    public void PlaySpotlightTurnOn()
    {

    }
}
