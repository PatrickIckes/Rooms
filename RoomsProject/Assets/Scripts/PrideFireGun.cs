using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrideFireGun : MonoBehaviour
{
    [SerializeField]
    private GameObject Player;
    [SerializeField]
    private GameObject Bullet;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    internal void Fire()
    {
        Instantiate(Bullet, this.transform.position, Quaternion.identity).GetComponent<Fired>().Direction = new Vector3(Player.transform.position.x,Player.transform.position.y);
    }
}
