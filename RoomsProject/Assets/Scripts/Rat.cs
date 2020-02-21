using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rat : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField]
    private float speed = 1;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = rb.transform.right * speed * Time.deltaTime * (this.gameObject.transform.localScale.x / Mathf.Abs(this.gameObject.transform.localScale.x));
    }
}
