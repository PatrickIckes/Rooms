using System.Collections;
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
        if (progress > 1 && progress+1 < textTextItems.Length)
        {
            textText.text = textTextItems[progress + 1]; //sets text for each keypress
            nameText.text = textTextItems[progress];
            speaker.sprite = speakerItems[speakerProgress];
        }
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
            RunDialogue();
     
    }

    public void RunDialogue()
    {
        textBox.SetActive(true);
        Pause();
        //KEYPRESS TO INCREMENT PROGRESS NOT WORKING
        if (Input.GetButtonDown("Interact"))
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
        if (Input.GetButtonDown("Interact"))
        {
            progress += 2;
            speakerProgress++;
            SetText();
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
