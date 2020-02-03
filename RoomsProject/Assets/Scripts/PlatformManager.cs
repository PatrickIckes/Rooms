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
    public GameObject[] FallingConnector;
    double timer;
    double fallingtimer;
    public GameObject Debris;
    public MyGameManager gm;
    public GameObject Sloth;
    public float spawnHeight;
    PlayerData player_script;
    public float Fallspeed;
    public float fallingtime;
    int lastval;
    internal bool RoomDone;
    public int[] Sequence = new int[12] { 1,3,2,1,3,2,1,3,2,3,1,3 }; // Maybe Randomly implement this
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        rounds = BossFight.First;
        player_script = gm.player.GetComponent<PlayerData>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (rounds)
        {
            case BossFight.First:
                FirstStage();
                break;
            case BossFight.Second:
                SecondStage();
                break;
            case BossFight.Dead:
                Dead();
                break;
        }

    }
    int i = 0;
    private void FirstStage()
    {   
        if (timer < fallingtime)
        {
            int safe = Sequence[i] - 1;
            fallingtimer += Time.deltaTime;
            timer += Time.deltaTime;
            if (fallingtimer > Fallspeed)
            {
                fallingtimer = 0;
                IndicateSafe(safe);
                TriggerFall(safe);
                i++;
            }
        }
        else
        {
            i = 0;
            timer = 0;
            ClearLights();
            rounds = BossFight.Second;
        }
    }
    int timesThrough;
    private void SecondStage()
    {
        if (Sloth != null)
        {
            int safe = Sequence[i]-1;
            fallingtimer += Time.deltaTime;

            if(timesThrough == 3)
            {

            } 
            if (fallingtimer > Fallspeed)
            {
                fallingtimer = 0;
                PlanksFall();
                TriggerFall(safe);
                i++;
                timesThrough++;
            }
        }
        else
        {
            i = 0;
            rounds = BossFight.Dead;
        }
    }

    private void Dead()
    {
        Debug.Log("Boss is dead");
    }

    private void PlanksFall()
    {
        foreach(GameObject plank in FallingConnector)
        {
            plank.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }
    }

    private void ClearLights()
    {
        int i = 0;
        foreach (GameObject platform in Platforms)
        {
            Lights[i].GetComponent<SpriteRenderer>().color = new Color(185,183,183);
            i++;
        }
    }

    private void IndicateSafe(int safe)
    {

        int i = 0;
        Lights[safe].GetComponent<SpriteRenderer>().color = Color.green;
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
}
