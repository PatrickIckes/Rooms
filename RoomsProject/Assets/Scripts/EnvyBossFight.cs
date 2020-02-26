using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvyBossFight : MonoBehaviour
{
    private float noteTimer;
    private float sandBagTimer;
    public float noteFireTime;
    public float sandbagFallTime;
    public SandbagManager sandbagManager;
    public GameObject Note;
    public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FireNotes();
        DropSandbags();
    }

    private void FireNotes()
    {
        noteTimer += Time.deltaTime;
        if (noteTimer >= noteFireTime)
        {
            GameObject temp = Instantiate(Note, new Vector3(this.transform.position.x - 0.75f, 0, 0), Quaternion.identity, this.transform);
            temp.GetComponent<Fired>().Direction = new Vector3(Player.transform.position.x, Player.transform.position.y, Player.transform.position.z);
            noteTimer = 0;
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
}
