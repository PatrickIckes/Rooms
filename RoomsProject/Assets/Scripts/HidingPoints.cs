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
        //ObjectsToHide = HidingSpots.Count;//Shouldn't be hard coded but out of time
        //for(int i = 0; i < ObjectsToHide-1;i++)
        //{
        //    int num = Random.Range(0, ObjectsToHide-1);
        //    while (HidingSpots[num] != null)
        //    {
        //        num = NewRan();
        //    }
        //    HidingSpots[num] = Instantiate(hiddenObject, this.HidingSpots[num].transform);
        //    HidingSpots[num].transform.localPosition = new Vector3(0, 0, 0);
        //}
    }

    private int NewRan()
    {
        return Random.Range(0, ObjectsToHide-1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
