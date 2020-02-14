using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fired : MonoBehaviour
{
    public Vector3 Direction;
    public float bulletSpeed;
    public int damage = 1;
    // Update is called once per frame
    void Update()
    {
        Debug.Log($"position {Direction}");
        this.transform.position = Vector2.MoveTowards(transform.position,Direction,bulletSpeed*Time.smoothDeltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);
        if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerData>().Damage(damage);
            Destroy(gameObject);
            Debug.Log("Hurt player");
        } else if(collision.name == "EnvyManager")
        {
            collision.GetComponent<Enemy>().TakeDamage(damage);
            Destroy(gameObject);
            Debug.Log("Hurt Boss");
        }
    }
}
