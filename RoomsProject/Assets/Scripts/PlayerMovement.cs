using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 1f;
    public float jumpSpeed = 1f;
    public bool withinInteractable;
    public bool canJump;
    bool isColliding;
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
        isColliding = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        isColliding = false;
        Vector2 currentPos = rbody.position;
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        float jumpInput = Input.GetAxis("Jump");
        AnimatePlayer(horizontalInput,verticalInput);
        flipPlayer(horizontalInput);
        MovePlayer(horizontalInput, verticalInput, currentPos);
        if(canJump && jumpInput != 0)
        {
            
            PlayerJump(jumpInput,currentPos);
        }
    }
    public void PlayerJump(float jumpInput, Vector2 CurrentPos)
    {
        rbody.AddForce(new Vector2(0,Mathf.Abs(CurrentPos.y) * jumpInput*jumpSpeed),ForceMode2D.Impulse);
        Debug.Log("Jumped");
    }
    /// <summary>
    /// Moves the player
    /// </summary>
    /// <param name="horizontalInput"></param>
    /// <param name="verticalInput"></param>
    /// <param name="currentPos"></param>
    public void MovePlayer(float horizontalInput, float verticalInput, Vector2 currentPos)
    {
        Vector2 inputVector = new Vector2(horizontalInput, verticalInput);
        inputVector = Vector2.ClampMagnitude(inputVector, 1);
        Vector2 movement = inputVector * movementSpeed;
        Vector2 newPos = currentPos + movement * Time.fixedDeltaTime;
        rbody.MovePosition(newPos);
    }
    /// <summary>
    /// Animates the player
    /// </summary>
    /// <param name="horizontalInput"></param>
    /// <param name="verticalInput"></param>
    public void AnimatePlayer(float horizontalInput, float verticalInput)
    {
        if (horizontalInput != 0 || verticalInput != 0)
        {
            playerAnimator.SetBool("isWalking", true);
        }//Walking animation
        else
        {
            playerAnimator.SetBool("isWalking", false);
        }//Idle Anim if not moving

    }
    /// <summary>
    /// Flips the player sprite based on the horizontalInput
    /// </summary>
    /// <param name="horizontalInput">Gets what direction the player input</param>
    public void flipPlayer(float horizontalInput)
    {
        if (horizontalInput < 0)
        {
            this.GetComponent<SpriteRenderer>().flipX = false;
        }//Flips left
        else if (horizontalInput > 0)
        {
            this.GetComponent<SpriteRenderer>().flipX = true;
        }//flips right
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
        if (isColliding) return;//https://answers.unity.com/questions/738991/ontriggerenter-being-called-multiple-times-in-succ.html for some reason the collider is registering twice this is a solution i found
        isColliding = true;
        IInventoryItem item = collision.gameObject.GetComponent<IInventoryItem>();
        if (item != null)
        {
            if (item.IsQuestItem)
            {
                qm.CollectedQuestItem(item);
                if (qm.DoneWithQuest())
                {
                    SceneManager.LoadScene(((HallwayQuest)qm.CurrentQuest).NextScene, LoadSceneMode.Single);
                }
            }
            else
            {
                inventory.AddItem(item);
            }
        }

        if (collision.tag == "Platform")
        {
            if (withinInteractable)
            {
                rbody.gravityScale = 0;
                canJump = true;
            }
        }
        if (collision.tag == "Floor")
        {
            rbody.gravityScale = 0;
        }
        if (collision.name == "Door" || collision.name == "PlatformArea")
        {
            withinInteractable = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "Door" || collision.name == "PlatformArea")
        {
            Debug.Log("Out of interactable");
            withinInteractable = false;
        }
        if (collision.name == "PlatformArea")
        {
                Debug.Log("Out of platformarea");
                rbody.gravityScale = 0;
            canJump = false;
        }
        if (collision.tag == "Platform")
        {
            if (withinInteractable)
            {
                Debug.Log("Falling here");
                canJump = false;
                rbody.gravityScale = 1;
            }
            else
            {
                Debug.Log("Shouldn't be falling");
            }
        }

        if (collision.tag == "Floor")
        {
            Debug.Log("Tried to walk up the wall");
            rbody.gravityScale = 1;
        }
    }
}