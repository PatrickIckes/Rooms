using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlatformManager : MonoBehaviour
{
    enum BossFight { First, Second, Dead }
    BossFight rounds;
    public GameObject[] Platforms;
    public GameObject[] Lights;
    double timer;
    double fallingtimer;
    public GameObject Debris;
    public MyGameManager gm;
    public GameObject Sloth;
    public float spawnHeight;
    PlayerData player_script;
    public float Fallspeed;
    public float FallspeedIncrementer;
    public float fallingtime;
    int lastval;
    internal bool RoomDone;
    // Start is called before the first frame update
    void Start()
    {
        rounds = BossFight.First;
        player_script = gm.player.GetComponent<PlayerData>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (rounds)
        {
            case BossFight.First:
                FirstStage(timer);
                break;            
            case BossFight.Second:
                SecondStage(timer);
                break;            
            case BossFight.Dead:
                Dead();
                break;
        }
        timer += Time.deltaTime;
    }

    private void FirstStage(double timer)
    {
        if (timer < fallingtime)
        {
            fallingtimer += Time.deltaTime;
            if (fallingtimer > Fallspeed)
            {
                fallingtimer = 0;
                TriggerFall();
            }
        } else
        {
            timer = 0;
            rounds = BossFight.Second;
        }
    }

    private void SecondStage(double timer)
    {
        if (timer < fallingtime)
        {
            fallingtimer += Time.deltaTime;

            if (fallingtimer > Fallspeed)
            {
                fallingtimer = 0;
                TriggerFall();
            }
        }
        else
        {
            timer = 0;
            rounds = BossFight.Dead;
        }
    }

    private void Dead()
    {
        Debug.Log("Boss is dead");
    }


    private void FixedUpdate()
    {
    }

    private void TriggerFall()
    {
        int safe = UnityEngine.Random.Range(0, Platforms.Length);
        Lights[safe].GetComponent<SpriteRenderer>().color = Color.green;
        int i = 0;
        foreach (GameObject platform in Platforms)
        {

            if (platform != Platforms[safe])
            {
                Vector3 p = platform.transform.position;
                GameObject temp = Instantiate(Debris, new Vector3(p.x, p.y + spawnHeight, p.z), Quaternion.identity);
                Lights[i].GetComponent<SpriteRenderer>().color = Color.magenta;
            }
            i++;
        }
    }
}
