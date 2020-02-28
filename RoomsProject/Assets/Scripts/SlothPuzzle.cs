using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlothPuzzle : MonoBehaviour
{
    [SerializeField] private GameObject[] layerItems;
    public int layerIndex;


    void Start()
    {
        layerIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Increment()
    {
        //var layerRender = gameObject.GetComponent<Renderer>(); 
        //layerRender.material.color = Color.Lerp(Color.white, Color.green, 0.0f); //unhighlights layer before changing

        layerIndex++; // changes selected layer based on list on Puzzle Items

        if (layerIndex == 3) //resets layer index so repeats
            layerIndex = 0;
       
        //layerRender.material.color = Color.Lerp(Color.white, Color.green, 0.5f); //highlights current layer
        
    }

    public void RotateLayer()
    {
        layerItems[layerIndex].transform.Rotate(0, 0, -120);
    }
    
}
