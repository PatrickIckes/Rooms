﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deer : MonoBehaviour
{
    [SerializeField]
    internal GameObject Target;
    [SerializeField]
    private float speed;
    Vector2 DeerTarget;
    // Start is called before the first frame update
    void Start()
    {
        DeerTarget = new Vector3(Target.transform.position.x, this.transform.position.y, 0);
        PlayDeerCharge();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.x >= DeerTarget.x) this.GetComponent<SpriteRenderer>().flipX = true;
        if (this.transform.position.x < DeerTarget.x) this.GetComponent<SpriteRenderer>().flipX = false;
        this.transform.position = Vector3.MoveTowards(this.transform.position, DeerTarget, Time.smoothDeltaTime * speed);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Boss")
        {
            collision.GetComponent<pride_comes_before_the_fall>().Hit();
            PlayDeerAntlerHit();
            Destroy(this.gameObject);
        }
        else if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerData>().Damage(1);
            PlayDeerAntlerHit();
            Destroy(this.gameObject);
        }
    }

    //jpost Audio
    //a method to play the deer charge sfx
    public void PlayDeerCharge()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/NPC/Deer Men/sx_npc_deer_men_charge", GetComponent<Transform>().position);
    }
    //a method to play the deer charge sfx
    public void PlayDeerAntlerHit()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/NPC/Deer Men/sx_npc_deer_men_antler_hit", GetComponent<Transform>().position);
    }
}
