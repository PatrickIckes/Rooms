using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEnemy : MonoBehaviour
{
    public Enemy enemy;

    internal void TakeDamage(int damage)
    {
        enemy.TakeDamage(damage);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.GetComponent<PlayerData>().Damage(1);
        }
    }
}
