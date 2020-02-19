using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueBoxManager : MonoBehaviour
{
    public GameObject textBox;

    public Text theText;

    public TextAsset textFile;
    public string[] textLines;

    public int currentLine;
    public int endAtLine;




    //add pause from inventory


    // Start is called before the first frame update
    void Start()
    {
        //imports txt file into Dialogue Manager
        if (textFile != null)
        {
            textLines = (textFile.text.Split('\n'));
        }

        if (endAtLine == 0)
        {
            endAtLine = textLines.Length - 1;
        }
    }

    // Update is called once per frame
    void Update()
    {


        theText.text = textLines[currentLine];
        //start of dialogue,f to continue
        if (Input.GetKeyDown(KeyCode.F))
        {
            currentLine += 1;

        }

        if (currentLine == endAtLine)
        {
            textBox.SetActive(false);
        }
    }
}
