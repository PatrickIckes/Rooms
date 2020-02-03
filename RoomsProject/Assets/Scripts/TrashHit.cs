using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashHit : MonoBehaviour
{
    public GameObject ThrowableObject;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Instantiate(ThrowableObject, collision.transform.position, Quaternion.identity);
    }
}
