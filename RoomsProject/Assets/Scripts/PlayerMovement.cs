using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 1f;
    public float jumpSpeed = 1f;
    public float DistanceToGround;
    public bool withinInteractable;
    //When in grounded areas
    public bool canJump;
    public bool GravityEnabled;
    public bool Grounded;
    bool isColliding;
    //IsometricCharacterRenderer isoRenderer;
    Rigidbody2D rbody;
    Collider2D colliderTrigger;
    Animator playerAnimator;
    QuestManager qm;
    //Used to track when items are picked up
    public Inventory inventory;
    internal MyGameManager gameManager;
    internal bool canCheckForObject;
    List<GameObject> CollidingObjects;
    public float GravityScale; //Only set at start
    private void Awake()
    {
        CollidingObjects = new List<GameObject>();
        qm = GetComponentInChildren<QuestManager>();
        rbody = GetComponent<Rigidbody2D>();
        colliderTrigger = GetComponent<Collider2D>();
        playerAnimator = GetComponent<Animator>();
        isColliding = false;
    }
    private void Start()
    {
        gameManager = FindObjectOfType<MyGameManager>();
        GravityEnabled = gameManager.levelManager.GravityEnabled;
        if(GravityEnabled)
        {
            rbody.gravityScale = GravityScale;
        } else
        {
            rbody.gravityScale = 0;
        }
        Debug.Log(DistanceToGround);
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
    public virtual void PlayerJump(float jumpInput, Vector2 CurrentPos)
    {
        rbody.AddForce(new Vector2(0, jumpInput*jumpSpeed),ForceMode2D.Impulse);
        canJump = false;
    }
    /// <summary>
    /// Moves the player
    /// </summary>
    /// <param name="horizontalInput"></param>
    /// <param name="verticalInput"></param>
    /// <param name="currentPos"></param>
    public virtual void MovePlayer(float horizontalInput, float verticalInput, Vector2 currentPos)
    {

        if (GravityEnabled)
        {
            verticalInput = 0;
        }
        Vector2 inputVector = new Vector2(horizontalInput, verticalInput);
        inputVector = Vector2.ClampMagnitude(inputVector, 1);
        Vector2 movement = inputVector * movementSpeed;
        Vector2 newPos = currentPos + movement * Time.fixedDeltaTime;
        if (!GravityEnabled)
        {
            rbody.MovePosition(newPos);
        } else
        {
            this.transform.position = newPos;
        }
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
    public virtual void flipPlayer(float horizontalInput)
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
        if(GetComponentInChildren<QuestManager>().CurrentQuest == null)//Instantiates the current quest because in start is doesn't work properly
        {
            GetComponentInChildren<QuestManager>().CurrentQuest = gameManager.levelManager.levelQuest;
        }
        QuestItemCollection();
        OpenDoor();
    }

    public void QuestItemCollection()
    {
        if (Input.GetKeyDown(KeyCode.F) && canCheckForObject) 
        {
            foreach(GameObject collidingObject in CollidingObjects)
            {
                if(collidingObject.GetComponentInChildren<DoorKnobPieces>() != null)
                {
                    qm.CollectedQuestItem(collidingObject.GetComponentInChildren<DoorKnobPieces>().gameObject.GetComponent<IInventoryItem>());
                    Destroy(collidingObject.GetComponentInChildren<DoorKnobPieces>().gameObject);
                }
            }
        }
    }

    public void OpenDoor()
    {
        if (Input.GetKeyDown(KeyCode.F) && withinInteractable && inventory.CheckObject("key"))
        {
            SceneManager.LoadScene(1);
        }
        if (Input.GetKeyDown(KeyCode.F) && withinInteractable && qm.DoneWithQuest()) // Change later so the scenes aren't hardCoded
        {
            if (qm.CurrentQuest.GetType() == typeof(HallwayQuest))
            {
                SceneManager.LoadScene(((HallwayQuest)qm.CurrentQuest).NextScene);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CollidingObjects.Add(collision.gameObject);
        //if (isColliding) return;//https://answers.unity.com/questions/738991/ontriggerenter-being-called-multiple-times-in-succ.html for some reason the collider is registering twice this is a solution i found
        isColliding = true;
        if(GravityEnabled && collision.tag == "Platform")
        {
            canJump = true;
        }
        IInventoryItem item = collision.gameObject.GetComponent<IInventoryItem>();
        if (item != null)
        {
            if (item.IsQuestItem)
            {
                qm.CollectedQuestItem(item);
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
        CollidingObjects.Remove(collision.gameObject);
        if (collision.name == "Door")
        {
            Debug.Log("Out of interactable");
            withinInteractable = false;
        }
    }
}