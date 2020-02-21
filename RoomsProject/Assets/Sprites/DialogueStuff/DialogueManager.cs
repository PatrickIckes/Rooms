using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private Text textText;
    [SerializeField] private Text nameText;
    [SerializeField] private GameObject textBox;
    [SerializeField] private TextAsset textFile;

    public string[] lines;
    public string[] textTextItems;
    public string[] nameTextItems;
    private int endLine;
    private int progress; 

    void SetText()
    {
        //string[] parts = lines[progress].Split(':');
        //textText.text = lines[progress];
        //nameText.text = parts[1];

        textText.text = textTextItems[progress]; //sets text for each keypress
        //nameText.text = nameTextItems[progress];


    }
    void Start()
    {
        progress = 0;
        textTextItems = (textFile.text.Split('\n'));



        SetText();
        


    }

    // Update is called once per frame
    void Update()
    {
        

        RunDialogue();   
       
    }

    void RunDialogue()
    {
        

        if (progress >= textTextItems.Length - 1)
        {
            textBox.SetActive(false);
        }
        else
        {
            textBox.SetActive(true); 
            if (Input.GetKeyDown(KeyCode.F))
            {
                progress++;
                SetText();
            }
        }
    }
}
