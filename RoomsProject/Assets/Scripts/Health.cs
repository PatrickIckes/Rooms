﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    int health = 3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal void Damage(int damageValue)
    {
        this.health -= damageValue;
        if (this.health < 0) Destroy(gameObject);
    }
}
