using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyManager : MonoBehaviour
{
    [SerializeField]
    GameObject stairs;
    [SerializeField]
    GameObject Garbagebags;
    [SerializeField]
    Inventory playerInventory;
    [SerializeField]
    List<GameObject> keys;
    [SerializeField]
    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject key in keys)
        {
            key.SetActive(false);
        }
        Garbagebags.SetActive(true);
        stairs.SetActive(false);
        if ((playerInventory.CheckObject("EnvyMask") || playerInventory.CheckObject("EnvyMask")) && (!playerInventory.CheckObject("PrideKey") || !playerInventory.CheckObject("PrideKey(Clone)")))
        {
            Garbagebags.SetActive(false);
            stairs.SetActive(true);
            keys[2].SetActive(true);
        }
        else if (playerInventory.CheckObject("SlothReward") && (!playerInventory.CheckObject("EnvyKey") || !playerInventory.CheckObject("EnvyKey(Clone)")))
            keys[1].SetActive(true);
        else if (!playerInventory.CheckObject("SlothKey") || !playerInventory.CheckObject("SlothKey(Clone)"))
            keys[0].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
