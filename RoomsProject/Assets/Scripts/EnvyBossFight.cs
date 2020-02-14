using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvyBossFight : MonoBehaviour
{
    float noteFireTimer;
    float sandBagTimer;
    public float FireTime;
    public float FallTime;
    public SandbagManager manager;
    public GameObject Note;
    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        noteFireTimer += Time.deltaTime;
        sandBagTimer += Time.deltaTime;
        if(noteFireTimer >= FireTime)
        {
            GameObject temp = Instantiate(Note, new Vector3(this.transform.position.x-0.75f,0,0),Quaternion.identity,this.transform);
            temp.GetComponent<Fired>().Direction = new Vector3(Player.transform.position.x, Player.transform.position.y, Player.transform.position.z);
            noteFireTimer = 0;
        }
        if(sandBagTimer >= FallTime)
        {
            manager.fall = true;
            sandBagTimer = 0;
        }
    }
}
