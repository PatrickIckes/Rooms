using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fired : MonoBehaviour
{
    public Vector3 Direction;
    public float bulletSpeed;
    public int damage = 1;
    [SerializeField]
    private float LifeTime;
    private float timer;
    [SerializeField]
    private bool HurtPlayer;
    [SerializeField]
    private bool DestroyOnHit;
    float angle;
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= LifeTime)
        {
            Destroy(this.gameObject);
        }
        if (angle == 0)
        {
            angle = Vector2.Angle(this.transform.position, Direction);
        }
        else
        {
            this.transform.position = Vector2.MoveTowards(transform.position, Direction, bulletSpeed * Time.smoothDeltaTime);
            if (this.transform.position == Direction && DestroyOnHit)
            {
                float x = angle / 180;
                Direction += new Vector3(-x, -1f, 0);
            }
            else if (this.transform.position == Direction)
            {
                this.tag = "Trap";
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" &&  HurtPlayer)
        {
            collision.GetComponent<PlayerData>().Damage(damage);
            Destroy(gameObject);
            Debug.Log("Hurt player");
        }
        if(collision.gameObject.tag == "Boss" && collision.name == "Envy")
        {
            collision.GetComponent<BossHealth>().Damage(damage);
            Destroy(gameObject);
            Debug.Log("Hurt Boss");
        }
        if(collision.gameObject.tag == "Platform")
        {
            Destroy(gameObject);
        }
    }
}
