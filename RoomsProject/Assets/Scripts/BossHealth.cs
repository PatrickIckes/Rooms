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

    [SerializeField]
    private bool destroyWhenDead = true;

    [HideInInspector]
    public bool isDead = false;

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
        if (slothSFX != null)
        {
            slothSFX.PlaySlothTakeDamage();
            slothSFX.PlaySlothHit();
        }
        if (health <= 0)
        {
            if (slothSFX != null)
            {
                //jpost Audio
                slothSFX.PlaySlothDie();
            }

            isDead = true;

            if (destroyWhenDead)
            {
                Destroy(gameObject);
            }
        }
    }
}
