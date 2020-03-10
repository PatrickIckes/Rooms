using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public float maxHealth;
    [HideInInspector]
    public float health;
    //jpost Audio
    SlothSFX slothSFX;

    private void Start()
    {
        health = maxHealth;
        //jpost Audio
        slothSFX = GetComponent<SlothSFX>();
    }

    internal void Damage(int damage)
    {
        health -= damage;
        //jpost Audio
        slothSFX.PlaySlothTakeDamage();
        if (health <= 0)
        {
            //jpost Audio
            slothSFX.PlaySlothDie();

            Destroy(gameObject);
        }
    }
}
