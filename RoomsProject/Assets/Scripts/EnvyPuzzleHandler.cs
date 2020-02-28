using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvyPuzzleHandler : MonoBehaviour
{
    private GameObject[] Notes;
    private int[] pattern;
    public Color PatternColor;
    [SerializeField] private int TotalSequenceCount;
    private int currentSequencePoint;
    private float timer;
    private bool RepeatPattern;
    // Start is called before the first frame update
    void Start()
    {
        Notes = new GameObject[this.transform.childCount];
        for (int i = 0; i < this.transform.childCount; i++)
        {
            Notes[i] = this.transform.GetChild(i).gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!RepeatPattern)
        {
            timer += Time.deltaTime;
            if (currentSequencePoint < TotalSequenceCount)
            {
                if (timer < 0.5f)
                {
                    pattern[currentSequencePoint] = Random.Range(1, 4);
                    Notes[pattern[currentSequencePoint]].gameObject.GetComponent<SpriteRenderer>().color = new Color(0,255,0);
                    if(currentSequencePoint > 0) 
                        Notes[pattern[currentSequencePoint-1]].gameObject.GetComponent<SpriteRenderer>().color = new Color(255,255,255);
                    currentSequencePoint++;
                }
            }
        }
    }
}
