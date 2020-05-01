using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roach : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField]
    private float speed = 1;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        PlayRoachSpawn();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = rb.transform.right * speed * Time.deltaTime;
    }

    //jpost audio
    private void PlayRoachSpawn()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/NPC/Rats/sx_game_npc_rat_spawn", GetComponent<Transform>().position);//needs to change to roach
    }
}
