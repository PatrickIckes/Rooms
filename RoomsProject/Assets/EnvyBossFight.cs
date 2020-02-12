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
        sandBagTimer = noteFireTimer += Time.deltaTime;
        if(noteFireTimer >= FireTime)
        {
            GameObject temp = Instantiate(Note, this.transform);
            temp.transform.LookAt(Player.transform);
            temp.GetComponent<Fired>().Direction = Player.transform;
            noteFireTimer = 0;
        }
        if(sandBagTimer >= FallTime)
        {
            manager.fall = true;
            sandBagTimer = 0;
        }
    }
}
