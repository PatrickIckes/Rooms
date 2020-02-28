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
    [SerializeField] private Image speaker;


    public string[] lines;
    public string[] textTextItems;
    public string[] nameTextItems;
    public Sprite[] speakerItems;
    private int speakerProgress;
    private int progress;
    public bool GameIsPaused = false;




    void SetText()
    {
        textText.text = textTextItems[progress + 1]; //sets text for each keypress
        nameText.text = textTextItems[progress];
        speaker.sprite = speakerItems[speakerProgress];
    }

    void Start()
    {
        progress = 0;
        char[] separators = { '\n', ':' };
        textTextItems = (textFile.text.Split(separators));
        SetText();
    }

    // Update is called once per frame
    void Update()
    {


        RunDialogue();
        
       
    }

    public void RunDialogue()
    {
        

        if (progress >= textTextItems.Length - 1)
        {
            textBox.SetActive(false);
            Resume();
        }
        else
        {
            
            textBox.SetActive(true); 
            if (Input.GetKeyDown(KeyCode.F))
            {
                progress += 2;
                speakerProgress++;
                SetText();
            }
        }
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    
}
