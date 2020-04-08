using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deer : MonoBehaviour
{
    [SerializeField]
    internal GameObject Target;
    [SerializeField]
    private float speed;
    Vector2 DeerTarget;
    // Start is called before the first frame update
    void Start()
    {
        DeerTarget = new Vector3(Target.transform.position.x, this.transform.position.y, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.x >= DeerTarget.x) this.GetComponent<SpriteRenderer>().flipX = true;
        if (this.transform.position.x < DeerTarget.x) this.GetComponent<SpriteRenderer>().flipX = false;
        this.transform.position = Vector3.MoveTowards(this.transform.position, DeerTarget, Time.smoothDeltaTime * speed);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"Collided with {collision.gameObject.name}");
        if (collision.gameObject.tag == "Boss")
        {
            collision.GetComponent<pride_comes_before_the_fall>().fall = true;
            Destroy(this.gameObject);
        }
        else if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerData>().Damage(1);
            Destroy(this.gameObject);
        }
    }
}
