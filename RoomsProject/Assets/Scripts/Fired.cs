﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fired : MonoBehaviour
{
    public Vector3 Direction;
    public float bulletSpeed;
    public int damage = 1;
    [SerializeField]
    private float LifeTime;
    private float timer;
    [SerializeField]
    private bool HurtPlayer;
    [SerializeField]
    private bool DestroyOnHit;
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= LifeTime)
        {
            Destroy(this.gameObject);
        }

        this.transform.position = Vector2.MoveTowards(transform.position,Direction,bulletSpeed*Time.smoothDeltaTime);
        if (this.transform.position == Direction && DestroyOnHit)
        {
            Destroy(gameObject);
        } else if(this.transform.position == Direction)
        {
            this.tag = "Trap";
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" &&  HurtPlayer)
        {
            collision.GetComponent<PlayerData>().Damage(damage);
            Destroy(gameObject);
            Debug.Log("Hurt player");
        }
        if(collision.gameObject.tag == "Boss" && collision.name == "Envy")
        {
            collision.GetComponent<BossHealth>().Damage(damage);
            Destroy(gameObject);
            Debug.Log("Hurt Boss");
        }
        if(collision.gameObject.tag == "Platform")
        {
            Destroy(gameObject);
        }
    }
}
