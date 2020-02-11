using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandbagManager : MonoBehaviour
{
    public Rigidbody2D[] SandBags;
    public bool fall;
    bool fallen;
    public float FallTime;//Time before the sandbags are retrieved
    // Start is called before the first frame update
    void Start()
    {
        foreach (Rigidbody2D sandbag in SandBags)
        {
            sandbag.bodyType = RigidbodyType2D.Kinematic;
        }
    }
    float timer;
    // Update is called once per frame
    void Update()
    {
        if(!fallen)
        {
            if(fall)
            {
                foreach(Rigidbody2D sandbag in SandBags)
                {
                    sandbag.bodyType = RigidbodyType2D.Dynamic;
                    sandbag.gravityScale = 1;
                    fallen = true;
                }
            }
        } else
        {
            if (timer > FallTime)
            {
                foreach (Rigidbody2D sandbag in SandBags)
                {
                    sandbag.gravityScale = -1;
                    timer = 0;
                    fallen = false;
                    fall = false;
                }
            }
            timer += Time.deltaTime;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "SandBag")
        {
            collision.attachedRigidbody.gravityScale = 1;
            collision.attachedRigidbody.bodyType = RigidbodyType2D.Kinematic;
        }
    }
}
