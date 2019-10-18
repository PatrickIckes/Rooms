using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 1f;
    public float jumpSpeed = 1f;
    public bool withinInteractable;
    //IsometricCharacterRenderer isoRenderer;

    Rigidbody2D rbody;
    Collider2D colliderTrigger;
    Animator playerAnimator;
    QuestManager qm;
    //Used to track when items are picked up
    public Inventory inventory;
    
    private void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
        colliderTrigger = GetComponent<Collider2D>();
        playerAnimator = GetComponent<Animator>();
        qm = new QuestManager();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 currentPos = rbody.position;
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        if (horizontalInput != 0 || verticalInput != 0)
        {
            playerAnimator.SetBool("isWalking", true);
        }//Walking animation
        else
        {
            playerAnimator.SetBool("isWalking", false);
        }//Idle Anim if not moving
        if (horizontalInput < 0)
        {
            this.GetComponent<SpriteRenderer>().flipX = false;
        }//Flips left
        else if (horizontalInput > 0)
        {
            this.GetComponent<SpriteRenderer>().flipX = true;
        }//flips right
        float jumpInput = Input.GetAxis("Jump");
        Vector2 inputVector = new Vector2(horizontalInput, verticalInput);
        inputVector = Vector2.ClampMagnitude(inputVector, 1);
        Vector2 movement = inputVector * movementSpeed;
        Vector2 newPos = currentPos + movement * Time.fixedDeltaTime;
        rbody.MovePosition(newPos);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && withinInteractable && inventory.CheckObject("key"))
        {
            SceneManager.LoadScene(1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IInventoryItem item = collision.gameObject.GetComponent<IInventoryItem>();
        if (item != null)
        {
            if (item.IsQuestItem)
            {
                qm.CollectedQuestItem(item);
                if(qm.DoneWithQuest())
                {
                    SceneManager.LoadScene(((HallwayQuest)qm.CurrentQuest).NextScene, LoadSceneMode.Single);
                }
            }
            else
            {
                inventory.AddItem(item);
            }
        }

        if (collision.name == "Door")
        {
            withinInteractable = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "Door")
        {
            withinInteractable = false;
        }
    }
}