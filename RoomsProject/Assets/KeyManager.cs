using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyManager : MonoBehaviour
{
    [SerializeField]
    Inventory playerInventory;
    [SerializeField]
    List<GameObject> keys;
    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject key in keys)
        {
            key.SetActive(false);
        }
        if (playerInventory.CheckObject("EnvyMask") && !playerInventory.CheckObject("PrideKey")) keys[2].SetActive(true);
        else if (playerInventory.CheckObject("SlothReward") && !playerInventory.CheckObject("EnvyKey")) keys[1].SetActive(true);
        else if(!playerInventory.CheckObject("SlothKey")) keys[0].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
