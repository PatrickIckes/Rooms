using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField]
    private int damage = 3;
    [SerializeField]
    private bool getsDestroyedOnContact = true;

    private bool hasDamaged = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && !hasDamaged)
        {
            hasDamaged = true;
            collision.GetComponent<PlayerData>().Damage(damage);

            if (getsDestroyedOnContact)
            {
                Destroy(this.gameObject);
            }
        } 

        if(collision.tag == "Enemy")
        {

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            hasDamaged = false;
        }
    }
}
