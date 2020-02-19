using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DialogueImporter : MonoBehaviour
{
    [SerializeField] private bool trigger = false;
    [SerializeField] private string[] textLines;
    [SerializeField] private TextAsset TextFile;

    void Start()
    {
        if(TextFile != null)
        {
            textLines = (TextFile.text.Split('\n'));
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

}
