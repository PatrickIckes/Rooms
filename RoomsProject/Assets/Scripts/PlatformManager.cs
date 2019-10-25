using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlatformManager : MonoBehaviour
{
    public GameObject[] Platforms;
    double timer;
    double fallingtimer;
    public GameObject Debris;
    public MyGameManager gm;
    public GameObject Sloth;
    PlayerData player_script;
    // Start is called before the first frame update
    void Start()
    {
        player_script = gm.player.GetComponent<PlayerData>();
    }
    
    // Update is called once per frame
    void Update()
    {
    }
    private void FixedUpdate()
    {
        if (gm.GameInProgress)
        {
            fallingtimer += Time.deltaTime;
            timer += Time.deltaTime;
            if (fallingtimer > 4)
            {
                fallingtimer = 0;
                TriggerFall();
            }
            if (timer > 20)
            {
            }
        }
    }
    private void TriggerFall()
    {
        int safe = Random.Range(0, Platforms.Length);
        foreach(GameObject platform in Platforms)
        {
            if (platform != Platforms[safe])
            {
                Vector3 p = platform.transform.position;
                Instantiate(Debris, new Vector3(p.x,p.y+15,p.z),Quaternion.identity);
                
            }
        }
    }
}
