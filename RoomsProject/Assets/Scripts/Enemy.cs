﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int enemyHealth;
    public GameObject bloodEffect;
    void Start()
    {
        //Add animation
    }

    void FixedUpdate()
    {
        if(enemyHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        if (bloodEffect != null)
        {
            //Play hurt sound of enemy here
            Instantiate(bloodEffect, transform.position, Quaternion.identity);
        }
        enemyHealth -= damage;
        //Debug.Log("Damage TAKEN!");
    }
}
