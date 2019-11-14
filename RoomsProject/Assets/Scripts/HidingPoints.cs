using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HidingPoints : MonoBehaviour
{
    public List<GameObject> HidingSpots;
    public GameObject hiddenObject;
    int ObjectsToHide;

    // Start is called before the first frame update
    void Start()
    {
        if (HidingSpots != null)
        {
            int notChosenNum = Random.Range(0, HidingSpots.Count - 1);

            foreach(GameObject spot in HidingSpots)
            {
                if(HidingSpots.IndexOf(spot) != notChosenNum)
                {
                    Instantiate(hiddenObject, spot.transform).GetComponent<SpriteRenderer>().enabled = false;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
