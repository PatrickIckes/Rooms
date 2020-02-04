using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    internal bool canCheckForObject;
    //IsometricCharacterRenderer isoRenderer;
    Rigidbody2D rbody;
    Collider2D colliderTrigger;
    Animator playerAnimator;
    QuestManager qm;
    //Used to track when items are picked up
    public Inventory inventory;
    public MyGameManager gameManager;
    public Text QuestCollectionText;
    public GameObject InteractionIndicator;
    public GameObject ThrowableObject;
    List<GameObject> CollidingObjects;
    public float PlayerGravityScale; //Only set at start
    //Called upon initialization of the object.
    private void Awake()
    {
        CollidingObjects = new List<GameObject>();
        qm = GetComponentInChildren<QuestManager>();
        rbody = GetComponent<Rigidbody2D>();
        colliderTrigger = GetComponent<Collider2D>();
        playerAnimator = GetComponent<Animator>();
        isColliding = false;
        InteractionIndicator.SetActive(false);
        if (QuestCollectionText != null)
        {
            QuestCollectionText.enabled = false;
        }
    }
    //Called on the frame the script is enabled right before update is called the first time
    private void Start()
    {
        //gameManager.LoadInventory();
        GravityEnabled = gameManager.levelManager.GravityEnabled;
        if(GravityEnabled)
        {
            rbody.gravityScale = PlayerGravityScale;
        } else
        {
            rbody.gravityScale = 0;
        }
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
        if (ThrowableObject != null) 
            ThrowableObject.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 0.2f, 0);
    }


    public virtual void PlayerThrow()
    {
        if(Input.GetButtonDown("Fire1") && ThrowableObject != null)
        {
            ThrowableObject.GetComponent<Thrown>().Throw();
            //Calculate Angle between two vectors
            ThrowableObject = null;

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

    /// <summary>
    /// Called every frame
    /// </summary>
    private void Update()
    {
        if(GetComponentInChildren<QuestManager>().CurrentQuest == null)//Instantiates the current quest because in start is doesn't work properly
        {
            GetComponentInChildren<QuestManager>().CurrentQuest = qm.CurrentQuest = gameManager.levelManager.levelQuest;
            Start();
        }
        if(transform.position.y < -10)
        {
            gameManager.GameInProgress = false;
        }
        QuestItemCollection();
        OpenDoor();
        PlayerThrow();
    }

    public void QuestItemCollection()
    {

        if (Input.GetKeyDown(KeyCode.F) && canCheckForObject) 
        {
            List <GameObject> RemoveObjects = new List<GameObject>();
            foreach (GameObject collidingObject in CollidingObjects)
            {
                if(collidingObject.GetComponentInChildren<DoorKnobPieces>() != null)
                {
                    qm.CollectedQuestItem(collidingObject.GetComponentInChildren<DoorKnobPieces>().gameObject.GetComponent<IInventoryItem>());
                    RemoveObjects.Add(collidingObject.GetComponentInChildren<DoorKnobPieces>().gameObject);

                }
            }
            for (int i = 0; i < RemoveObjects.Count; i++)
            {
                Destroy(RemoveObjects[i]);
            }
        }
    }

    /// <summary>
    /// Figures out the situation and if the requirements are met it saves the game and goes to the next position
    /// </summary>
    public void OpenDoor()
    {
        if (Input.GetKeyDown(KeyCode.F) && withinInteractable)
        {
            if (inventory.CheckObject("key"))
            {
                gameManager.SaveLevel();
                SceneManager.LoadScene((int)Scenes.SlothHallway);
            } else
            {
                if (QuestCollectionText != null)
                {
                    QuestCollectionText.text = "You cannot unlock the door, it appears you need something to open it.";
                } 
            }
        }
        if (Input.GetKeyDown(KeyCode.F) && withinInteractable && qm.CurrentQuest != null) 
        {
            if (qm.CurrentQuest.GetType() == typeof(HallwayQuest) && qm.DoneWithQuest())
            {
                gameManager.SaveLevel();
                SceneManager.LoadScene(((HallwayQuest)qm.CurrentQuest).NextScene);
            }
            if (qm.CurrentQuest.GetType() == typeof(BeatSloth) && qm.DoneWithQuest())
            {
                gameManager.SaveLevel();
                SceneManager.LoadScene(((BeatSloth)qm.CurrentQuest).NextScene);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Stairway movement handled in stairmovement script
        CollidingObjects.Add(collision.gameObject);
        //if (isColliding) return;//https://answers.unity.com/questions/738991/ontriggerenter-being-called-multiple-times-in-succ.html for some reason the collider is registering twice this is a solution i found
        isColliding = true;
        if(GravityEnabled && collision.tag == "Platform")
        {
            canJump = true;
        }
        //Collision for HallwayQuest managed in hidingpoints
        IInventoryItem item = collision.gameObject.GetComponent<IInventoryItem>();
        if (item != null)
        {
            QuestCollectionText.enabled = true;
            InteractionIndicator.SetActive(true);
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
            QuestCollectionText.enabled = true;
            withinInteractable = true;
            InteractionIndicator.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        InteractionIndicator.SetActive(false);
        QuestCollectionText.enabled = false;
        if (QuestCollectionText != null)
        {
            QuestCollectionText.text = "Press F to interact.";
        }
        CollidingObjects.Remove(collision.gameObject);
        if (collision.name == "Door")
        {
            Debug.Log("Out of interactable");
            withinInteractable = false;
        }
    }
}