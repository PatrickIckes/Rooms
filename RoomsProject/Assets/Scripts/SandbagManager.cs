using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandbagManager : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D[] SandBags;
    private List<Rigidbody2D> unfallenSandBags;

    [HideInInspector]
    public bool fall;
    private bool fallen = false;

    [Tooltip("Time before the sandbags are retrieved")]
    public float FallTime;

    public GameObject min, max;
    private float timer;
    private bool Stopped;

    [SerializeField]
    private float sandbagSpeed = 1;

    // Start is called before the first frame update
    void Start()
    {
        unfallenSandBags = new List<Rigidbody2D>();
        PopulateUnfallenSandbags(SandBags[Random.Range(0,SandBags.Length)]);

        foreach (Rigidbody2D sandbag in SandBags)
        {
            sandbag.bodyType = RigidbodyType2D.Kinematic;
        }
    }
    
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
                Rigidbody2D sandbag = unfallenSandBags[Random.Range(0, unfallenSandBags.Count)];
                unfallenSandBags.Clear();
                PopulateUnfallenSandbags(sandbag);
                sandbag.gravityScale = sandbagSpeed;
                sandbag.bodyType = RigidbodyType2D.Dynamic;
                fallen = true;
                Stopped = false;
            }
        } 
        else
        {
            if (timer > FallTime)
            {
                foreach (Rigidbody2D sandbag in SandBags)
                {
                    sandbag.gravityScale = -sandbagSpeed;
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

    private void PopulateUnfallenSandbags(Rigidbody2D fallenBag)
    {
        foreach(Rigidbody2D sandbag in SandBags)
        {
            if (sandbag != fallenBag)
            {
                unfallenSandBags.Add(sandbag);
            }
        }
    }
}
