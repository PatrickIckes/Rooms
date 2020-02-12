using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fired : MonoBehaviour
{
    public Transform Direction;
    public float bulletSpeed;
    // Update is called once per frame
    void Update()
    {
        Debug.Log($"position {Direction.position}");
        this.transform.position = Vector2.MoveTowards(transform.position,Direction.position,bulletSpeed);
    }
}
