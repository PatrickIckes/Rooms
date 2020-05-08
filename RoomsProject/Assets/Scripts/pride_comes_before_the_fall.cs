using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pride_comes_before_the_fall : MonoBehaviour
{
    public bool fall { get; private set; }
    [SerializeField]
    private float fallTime;
    private float timer;
    private Vector2 StartPoint;
    Rigidbody2D rigidbody;
    private float health;
    private float fallCount;
    [SerializeField]
    private int HitsToFall;
    private void Start()
    {
        fall = false;
        rigidbody = this.GetComponent<Rigidbody2D>();
        rigidbody.gravityScale = 0;
        StartPoint = this.transform.position;
    }
    public void Hit()
    {
        fallCount++;
        if(fallCount >= HitsToFall)
        {
            fall = true;
            fallCount = 0;
            Invoke("PlayPrideThud", 0.5f);
        }
    }
    private void Update()
    {
        if (fall)
        {
            if (timer == 0)
            {
                health = this.GetComponent<Enemy>().enemyHealth;
                rigidbody.gravityScale = 1;
            }
            timer += Time.deltaTime;
            if (timer > fallTime || health > this.GetComponent<Enemy>().enemyHealth)
            {
                timer = 0;
                fall = false;
                ReturnToPlatform();
            }
        }
    }

    private void ReturnToPlatform()
    {
        rigidbody.gravityScale = 0;
        this.gameObject.transform.position = StartPoint;
        PlayPrideJump();
    }

    //jpost Audio
    //a method to play the pride jump sfx
    public void PlayPrideJump()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/NPC/Pride/sx_npc_pride_jump", GetComponent<Transform>().position);
    }
    //a method to play the pride thud sfx
    public void PlayPrideThud()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/NPC/Pride/sx_npc_pride_thud", GetComponent<Transform>().position);
    }
}
