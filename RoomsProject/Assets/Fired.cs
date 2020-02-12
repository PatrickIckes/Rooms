using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fired : MonoBehaviour
{
    public Vector3 Direction;
    public float bulletSpeed;
    // Update is called once per frame
    void Update()
    {
        Debug.Log($"position {Direction}");
        this.transform.position = Vector2.MoveTowards(transform.position,Direction,bulletSpeed*Time.smoothDeltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}
