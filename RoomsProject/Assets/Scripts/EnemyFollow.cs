using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public float speed;

    public float stoppingDistance;

    public float jumpAttackDistance;

    private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, target.position) > stoppingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            if(Vector2.Distance(transform.position, target.position) > stoppingDistance - jumpAttackDistance)
            {
                Invoke("jumpAttack", 2);
            }
        }
        //else if(Vector2.Distance(transform.position, target.position) <= stoppingDistance)
        //{
        //    Invoke("jumpAttack", 3);
        //}
    }

    void jumpAttack()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, (speed * 2) * Time.deltaTime);
        //Add Attack damage from Player Data Script on entering the player's collision box
        //Invoke("delayMovement", 2);
    }
    void delayMovement()
    {
        //transform.position = Vector2.MoveTowards(-transform.position, -target.position, speed * Time.deltaTime);
        transform.position = Vector2.MoveTowards(transform.position, target.position, (speed * -2) * Time.deltaTime);
        // Very Buggy, needs a better way to stop the enemy from moving for a good while
    }
}
