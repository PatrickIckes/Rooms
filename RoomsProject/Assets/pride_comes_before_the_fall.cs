using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pride_comes_before_the_fall : MonoBehaviour
{
    public bool fall;
    [SerializeField]
    private float fallTime;
    private float timer;
    private Vector2 StartPoint;
    Rigidbody2D rigidbody;
    private void Start()
    {
        fall = false;
        rigidbody = this.GetComponent<Rigidbody2D>();
        rigidbody.gravityScale = 0;
        StartPoint = this.transform.position;
    }
    private void Update()
    {
        if (fall)
        {
            if (timer == 0) rigidbody.gravityScale = 1;
            timer += Time.deltaTime;
            if (timer > fallTime)
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
    }
}
