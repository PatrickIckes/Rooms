﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrideFight : MonoBehaviour
{
    private float FireAtPlayerTimer;
    private float TrapAtPlayerTimer;
    private float SummonDeerTimer;
    [SerializeField]
    private float FireCooldown;  
    [SerializeField]
    private float TrapCooldown;
    [SerializeField]
    private float SummonCooldown;
    [SerializeField]
    private GameObject Deer;
    [SerializeField]
    private GameObject Trap;
    [SerializeField]
    private GameObject Pride;
    [SerializeField]
    private GameObject PridePivot;
    [SerializeField]
    private GameObject Player;
    [SerializeField]
    private GameObject[] SpawnPoints;
    [SerializeField]
    private ExitManager exit;
    float lastAngle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Pride != null && Player.GetComponent<PlayerData>().playerHurtAmount != PlayerData.PlayerHurtAmount.dead)
        {
            FireUpdate();
            SummonUpdate();
            ThrowTraps();
        } 
        else if(Pride == null)
        {
            exit.EndRoom();
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
        if (FireAtPlayerTimer >= FireCooldown && !Pride.GetComponentInParent<pride_comes_before_the_fall>().fall)
        {
            float angle = Vector3.Angle(PridePivot.transform.position, Player.transform.position);
            angle = 140-angle;
            float rotation = angle - lastAngle;
            PridePivot.transform.Rotate(new Vector3(0, 0, 1),rotation);
            Pride.GetComponent<PrideFireGun>().Fire();
            FireAtPlayerTimer = 0;
            print($"angle:{angle} last angle{lastAngle} rotation:{rotation}");
            lastAngle = angle;
            
        }
    }
    private void ThrowTraps()
    {
        TrapAtPlayerTimer += Time.deltaTime;
        if (TrapAtPlayerTimer >= TrapCooldown && !Pride.GetComponentInParent<pride_comes_before_the_fall>().fall)
        {
            GameObject tempTrap = Instantiate(Trap, Pride.transform.position,Quaternion.identity);
            tempTrap.GetComponent<Fired>().bulletSpeed = 7;
            tempTrap.GetComponent<Fired>().Direction = Player.transform.position - new Vector3(0, 0.1f);
            TrapAtPlayerTimer = 0;
        }
    }    
}
