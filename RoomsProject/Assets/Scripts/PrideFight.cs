using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrideFight : MonoBehaviour
{
    private float FireAtPlayerTimer;
    private float SummonDeerTimer;
    [SerializeField]
    private float FireCooldown;
    [SerializeField]
    private float SummonCooldown;
    [SerializeField]
    private GameObject Deer;
    [SerializeField]
    private GameObject Pride;
    [SerializeField]
    private GameObject[] SpawnPoints;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Pride != null)
        {
            FireUpdate();
            SummonUpdate();
        }
    }

    private void SummonUpdate()
    {
        SummonDeerTimer += Time.deltaTime;
        if (SummonDeerTimer >= SummonCooldown)
        {
            foreach(GameObject spawn in SpawnPoints)
            {
                Instantiate(Deer, spawn.transform.position, Quaternion.identity).GetComponent<Deer>().Target = Pride;
                SummonDeerTimer = 0;
            }
        }
    }

    private void FireUpdate()
    {
        FireAtPlayerTimer += Time.deltaTime;
        if (FireAtPlayerTimer >= FireCooldown)
        {
            Pride.GetComponent<PrideFireGun>().Fire();
            FireAtPlayerTimer = 0;
        }
    }
}
