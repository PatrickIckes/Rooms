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
    [SerializeField]
    Inventory playerinventory;
    Animator animation;
    bool attack;
    private void Start()
    {
        animation = GetComponent<Animator>();
    }
    void Update()
    {

        if (playerinventory.CheckObject("Sword"))
        {
            if (timeBtwAttack <= 0)
            {
                //Then you can attack
                if (Input.GetKey(KeyCode.Mouse0))
                {
                    StartCoroutine(Attack());
                    timeBtwAttack = 1;
                }
            }
            else
            {
                timeBtwAttack -= Time.deltaTime;
            }
        }
    }
    IEnumerator Attack()
    {
        attack = true;
        animation.SetBool("isAttacking", true);
        yield return new WaitForSeconds(.5f);
        //Player attacking animation here
        Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(attackPos.position, new Vector2(attackRangeX, attackRangeY), 0, whatIsEnemies);
        for (int i = 0; i < enemiesToDamage.Length; i++)
        {
            if (enemiesToDamage[i].tag == "Note")
                Destroy(enemiesToDamage[i].gameObject);
            else if (enemiesToDamage[i].tag == "Trap")
            {
                enemiesToDamage[i].GetComponent<Trap>().Break();
                Destroy(enemiesToDamage[i].gameObject);
            }
            else if (enemiesToDamage[i].tag == "Boss")
            {
                if (enemiesToDamage[i].GetComponent<DamageEnemy>() != null)
                {
                    enemiesToDamage[i].GetComponent<DamageEnemy>().TakeDamage(damage);
                }
                else
                {
                    enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(damage);
                }
            }
            else
            {
                if (enemiesToDamage[i].GetComponent<DamageEnemy>() != null)
                {
                    enemiesToDamage[i].GetComponent<DamageEnemy>().TakeDamage(damage);
                }
                else
                {
                    enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(damage);
                }
            }
        }
        timeBtwAttack = startTimeBtwAttack;
        animation.SetBool("isAttacking", false);
        attack = false;
    }
    void OnDrawGizmosSelected()
    {
        if (!attack)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(attackPos.position, new Vector3(attackRangeX, attackRangeY, 1));
        } else
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(attackPos.position, new Vector3(attackRangeX, attackRangeY, 1));
        }
    }

}
