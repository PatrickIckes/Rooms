using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thrown : MonoBehaviour
{
    bool thrown;
    public GameObject Enemy;
    //Throw Calculations
    public float power = 10;
    Vector2 StartPosition;
    float Angle;
    public Rigidbody2D rigidBody;
    public int damage
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = this.GetComponent<Rigidbody2D>();
        rigidBody.bodyType = RigidbodyType2D.Kinematic;
    }

    // Update is called once per frame
    void Update()
    {
        if(thrown)
        {
            Move();
        }
    }

    public void Move()//TODO More natural projectile movement
    {
        Vector2 force = new Vector2(power * Mathf.Cos((Angle * Mathf.PI) / 180),0);
        rigidBody.velocity = new Vector2(force.x*0.75f,1);
    }

    public void Throw()
    {
        Angle = Vector2.Angle(Input.mousePosition, this.transform.position);
        thrown = true;
        StartPosition = this.transform.position;
        rigidBody.bodyType = RigidbodyType2D.Dynamic;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == Enemy.name)
        {
            collision.gameObject.GetComponent<Health>().Damage(damage);
            Destroy(this.gameObject);
        }
    }
}
