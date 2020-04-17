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
    Vector3 angle;
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= LifeTime)
        {
            Destroy(this.gameObject);
        }
        if (angle.x == 0)
        {
            this.angle = new Vector3(1, (Direction.y - this.transform.position.y) / (Direction.x - this.transform.position.x));
            float x = Direction.x;
            float y = Direction.y;
            Direction = new Vector3(x, y);
        }
        else
        {
            this.transform.position = Vector3.MoveTowards(transform.position, Direction, bulletSpeed * Time.smoothDeltaTime);
            if (this.transform.position == Direction && DestroyOnHit)
            {
                Direction -= angle;
            }
            else if (this.transform.position == Direction)
            {
                if (!(this.transform.position.y <= -4.1))
                {
                    Direction -= angle;
                } else
                {
                    this.tag = "Trap";
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" &&  HurtPlayer)
        {
            collision.GetComponent<PlayerData>().Damage(damage);
            Destroy(gameObject);
        }
        if(collision.gameObject.tag == "Boss" && collision.name == "Envy")
        {
            collision.GetComponent<BossHealth>().Damage(damage);
            Destroy(gameObject);
        }
        if(collision.gameObject.tag == "Platform")
        {
            Destroy(gameObject);
        }
    }
}
