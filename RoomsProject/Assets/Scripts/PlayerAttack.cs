using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private float timeBtwAttack;
    public float startTimeBtwAttack;

    public Transform attackPos;
    public LayerMask whatIsEnemies;
    public float attackRangeX;
    public float attackRangeY;
    public int damage;
    int FacingDirection;
    void Update()
    {
        if(timeBtwAttack <= 0)
        {
            //Then you can attack
            if(Input.GetKey(KeyCode.F))
            {
                if (!this.GetComponent<SpriteRenderer>().flipX) FacingDirection = -1;
                else FacingDirection = 1;
                //Player attacking animation here
                Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(attackPos.position, new Vector2(attackRangeX, attackRangeY), 0, whatIsEnemies);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    if (enemiesToDamage[i].tag == "Note")
                        enemiesToDamage[i].GetComponent<Fired>().Direction = GameObject.FindGameObjectWithTag("Boss").transform.position;
                    else
                    {
                        enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(damage);
                    }
                    Debug.Log(enemiesToDamage[i].name);
                }
            }
            timeBtwAttack = startTimeBtwAttack;
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackPos.position, new Vector3(attackRangeX, attackRangeY, 1));
    }

}
