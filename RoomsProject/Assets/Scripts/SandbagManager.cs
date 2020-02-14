using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandbagManager : MonoBehaviour
{
    public Rigidbody2D[] SandBags;
    public bool fall;
    bool fallen;
    public float FallTime;//Time before the sandbags are retrieved
    public GameObject min, max;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Rigidbody2D sandbag in SandBags)
        {
            sandbag.bodyType = RigidbodyType2D.Kinematic;
        }
    }
    float timer;
    bool Stopped;
    // Update is called once per frame
    void Update()
    {
        if (!Stopped) {
            foreach (Rigidbody2D rb in SandBags)
            {
                if (rb.transform.position.y >= max.transform.position.y)
                {
                    rb.bodyType = RigidbodyType2D.Static;
                    rb.transform.position = new Vector3(rb.transform.position.x, max.transform.position.y, 0);
                    Stopped = true;
                } 
                else if(rb.transform.position.y <= min.transform.position.y)
                {
                    rb.bodyType = RigidbodyType2D.Static;
                    rb.transform.position = new Vector3(rb.transform.position.x, min.transform.position.y, 0);
                    Stopped = true;
                }
            }  
        }
        if(!fallen)
        {
            if(fall)
            {
                foreach(Rigidbody2D sandbag in SandBags)
                {
                    sandbag.gravityScale = 1;
                    sandbag.bodyType = RigidbodyType2D.Dynamic;
                    fallen = true;
                    Stopped = false;
                }
            }
        } else
        {
            if (timer > FallTime)
            {
                foreach (Rigidbody2D sandbag in SandBags)
                {

                    sandbag.gravityScale = -1;
                    sandbag.bodyType = RigidbodyType2D.Dynamic;
                    timer = 0;
                    fallen = false;
                    fall = false;
                    Stopped = false;
                }
            }
            timer += Time.deltaTime;
        }
    }
}
