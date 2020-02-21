using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRats : MonoBehaviour
{
    [SerializeField]
    private GameObject ratPrefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            Instantiate(ratPrefab, new Vector3(transform.position.x, transform.position.y - 0.8f, transform.position.z), transform.rotation);
            GameObject tempRat = Instantiate(ratPrefab, new Vector3(transform.position.x, transform.position.y - 0.8f, transform.position.z), transform.rotation);
            tempRat.transform.localScale = new Vector3(tempRat.transform.localScale.x * -1, tempRat.transform.localScale.y, tempRat.transform.localScale.z);
        }
    }
}
