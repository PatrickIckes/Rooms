using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashHit : MonoBehaviour
{
    public GameObject TrashDrop;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Hazard" && PlatformManager.DropItem)
        {
            Instantiate(TrashDrop, this.transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
            PlatformManager.DropItem = false;
        }
    }
}
