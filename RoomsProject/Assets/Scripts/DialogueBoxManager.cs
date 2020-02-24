using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueBoxManager : MonoBehaviour
{
    public GameObject textBox;
    public Image image;
    public Text theText;
    public Text name;
    public Sprite[] Character;
    

    public TextAsset textFile;
    public string[] textLines;
    public string[] currentSpeaker;

    public int currentLine;
    public int endAtLine;




    //add pause from inventory


    // Start is called before the first frame update
    void Start()
    {
        //imports txt file into Dialogue Manager
                endAtLine = textLines.Length;
        if (textFile != null)
        {
            for (int i = 1; i < endAtLine; i++)
            {
                string[] parts = (textFile.text.Split(':'));
                currentSpeaker[i] = parts[0];
                textLines[i] = parts[1];

            }
            //splitter into name and dialogue
        }

        if (endAtLine == 0)
        {
            endAtLine = textLines.Length - 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
       RunDialogue();
        
        
    }

    void RunDialogue()
    {
        Time.timeScale = 0.0f;
        theText.text = textLines[currentLine]; //Dialogue Text
        name.text = textLines[currentLine]; // name text
        //image.sprite = ImageSelector(name.text); 
        //start of dialogue,f to continue
        if (Input.GetKeyDown(KeyCode.F))
        {
            currentLine += 1;

        }

        if (currentLine == endAtLine)
        {
            textBox.SetActive(false);
            Time.timeScale = 1.0f;
        }
    }

    
}
