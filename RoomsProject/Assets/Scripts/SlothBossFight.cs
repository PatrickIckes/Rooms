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
    private float timeBetweenFalls;
    public float timeBetweenFallsStageOne = 3;
    public float timeBetweenFallsStageTwo = 6;
    public float firstStageTime;
    internal bool RoomDone;
    private int timesThrough;
    private bool finished;

    void Start()
    {
        timeBetweenFalls = timeBetweenFallsStageOne;
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
            timeBetweenFalls = timeBetweenFallsStageTwo;
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
            int targetPlatform = Random.Range(0, 3);
            fallingtimer = 0;
            TriggerFall(targetPlatform);

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

    private void TriggerFall(int targetPlatform)
    {
        if (rounds == BossFight.First)
        {
            int i = 0;
            Lights[targetPlatform].GetComponent<SpriteRenderer>().color = Color.green;
            FMODUnity.RuntimeManager.PlayOneShot("event:/Environment/Lights/sx_game_env_spotlight_on", Lights[targetPlatform].GetComponent<Transform>().position);
            foreach (GameObject platform in Platforms)
            {
                if (platform != Platforms[targetPlatform])
                {
                    Vector3 p = platform.transform.position;
                    Instantiate(Debris, new Vector3(p.x, p.y + spawnHeight, p.z), Quaternion.identity);
                    Lights[i].GetComponent<SpriteRenderer>().color = Color.red;
                }
                i++;
            }
        }

        if (rounds == BossFight.Second)
        {
            int i = 0;
            FMODUnity.RuntimeManager.PlayOneShot("event:/Environment/Lights/sx_game_env_spotlight_on", Lights[targetPlatform].GetComponent<Transform>().position);
            foreach (GameObject platform in Platforms)
            {
                Lights[i].GetComponent<SpriteRenderer>().color = Color.green;
                if (platform == Platforms[targetPlatform])
                {
                    Vector3 p = platform.transform.position;
                    Instantiate(Debris, new Vector3(p.x, p.y + spawnHeight, p.z), Quaternion.identity);
                    Lights[i].GetComponent<SpriteRenderer>().color = Color.red;
                }
                i++;
            }
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
