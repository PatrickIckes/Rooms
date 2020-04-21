using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleHandler : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject PuzzleUI;
    bool InPuzzle;
    bool PuzzleActive;
    // Start is called before the first frame update
    void Start()
    {
        PuzzleUI.SetActive(false);
        InPuzzle = false;
        PuzzleActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (InPuzzle)
        {
            if(Input.GetButtonDown("Interact"))
            {
                if (!PuzzleActive)
                {
                    PuzzleUI.SetActive(true);
                    Player.GetComponent<PlayerMovement>().canMove = false;
                } else {
                    PuzzleUI.SetActive(false);
                    Player.GetComponent<PlayerMovement>().canMove = true;
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player") InPuzzle = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player") InPuzzle = false;
    }
}
