using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundaries : MonoBehaviour
{
    //This is temporary make a new one to fix later
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(this.gameObject.transform.position.y < -2 || this.gameObject.transform.position.x < -2 || this.gameObject.transform.position.x > 7)
        {
            Destroy(this.gameObject);
        }
    }
}
