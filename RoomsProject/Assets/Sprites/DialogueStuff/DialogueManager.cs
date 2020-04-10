﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DialogueManager : MonoBehaviour
{
    [SerializeField] private Text textText;
    [SerializeField] private Text nameText;
    [SerializeField] private GameObject textBox;
    public TextAsset textFile;
    [SerializeField] private Image speaker;


    public string[] lines;
    public string[] textTextItems;
    public string[] nameTextItems;
    public Sprite[] speakerItems;
    private int speakerProgress;
    private int progress;
    public bool GameIsPaused = false;

    public bool StartDialogue = false;



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
        if (StartDialogue == true)
        {
            Pause();
            //RunDialogue();
            textBox.SetActive(true);
         
            if (Input.GetKeyDown(KeyCode.E))
            {
                progress += 2;
                speakerProgress++;
                SetText();
            }


            if (progress >= textTextItems.Length - 1)
            {
                textBox.SetActive(false);
                Resume();
                gameObject.SetActive(false);
            }
        }

        else 
        {
            textBox.SetActive(false);
            Resume();
        }
    }

    public void RunDialogue()
    {

        
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
