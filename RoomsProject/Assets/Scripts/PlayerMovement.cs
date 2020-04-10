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
    public bool Grounded;
    public bool GravityEnabled;
    public bool Falling;
    public bool isJumping;
    internal bool canCheckForObject;
    internal bool canMove;
    //IsometricCharacterRenderer isoRenderer;
    Rigidbody2D rbody;
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
    private PlayerData playerData;

    //Called upon initialization of the object.
    private void Awake()
    {
        canMove = true;
        CollidingObjects = new List<GameObject>();
        qm = GetComponentInChildren<QuestManager>();
        rbody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        InteractionIndicator.SetActive(false);
        if (QuestCollectionText != null)
        {
            QuestCollectionText.enabled = false;
        }
        playerData = GetComponent<PlayerData>();
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
        bool test = true;
        if (canMove && playerData.playerHurtAmount != PlayerData.PlayerHurtAmount.dead)
        {
            Vector2 currentPos = rbody.position;
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            float jumpInput = Input.GetAxis("Jump");
            flipPlayer(horizontalInput);
            MovePlayer(horizontalInput, verticalInput, currentPos);
            
            if (Grounded && jumpInput != 0)
            {
                PlayerJump(jumpInput);
                
            }
            AnimatePlayer(horizontalInput, verticalInput);
        }
    }
    public void CheckFallingOrJumping()
    {
        if (!Grounded && this.rbody.velocity.y <= 0)
        {
            Falling = true;
        }
        else
        {
            Falling = false;
        }
        
        if (!Grounded && this.rbody.velocity.y > 0)
        {
            isJumping = true;
        }
        else
        {
            isJumping = false;
        }

        if (!GravityEnabled)
        {
            Falling = false;
            isJumping = false;
        }

        playerAnimator.SetBool("isFalling", Falling);
        playerAnimator.SetBool("isJumping", isJumping);
    }

    public virtual void PlayerJump(float jumpInput)
    {
        Grounded = false;
        //PlayPlayerJump(); //currently disabled due to sound playing twice, can't figure out exactly why as of right now...
        
        rbody.AddForce(new Vector2(0, jumpInput*jumpSpeed),ForceMode2D.Impulse);
        
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
            //jpost Audio
            PlayThrowTrashBag();
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
            //this.GetComponent<SpriteRenderer>().flipX = false;
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }//Flips left
        else if (horizontalInput > 0)
        {
            //this.GetComponent<SpriteRenderer>().flipX = true;
            transform.localRotation = Quaternion.Euler(0, 180, 0);
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
        PlayerThrow();
        CheckFallingOrJumping();

        //limit velocity
        rbody.velocity = new Vector2(rbody.velocity.x, Mathf.Clamp(rbody.velocity.y, -jumpSpeed * 5, jumpSpeed));
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

    //jpost Audio
    public void PlayFootstep()
    {
        //play the FMOD event for footsteps wood
        FMODUnity.RuntimeManager.PlayOneShot("event:/Player/sx_game_plr_footsteps_wood", GetComponent<Transform>().position);
    }
    //play trash pile collision sfx
    private void PlayTrashCollision()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Interactible/Collisions/sx_game_int_collide_trash", GetComponent<Transform>().position);
    }
    //jpost Audio
    public void PlayThrowTrashBag()
    {
        //play the FMOD event for footsteps wood
        FMODUnity.RuntimeManager.PlayOneShot("event:/Interactible/Trash/sx_game_int_slothfight_trashbag_throw", GetComponent<Transform>().position);
    }
    //jpost Audio
    public void PlayPlayerJump()
    {
        if (Grounded)
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/Player/Vocalizations/sx_game_plr_voc_jump", GetComponent<Transform>().position);
        }
            
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (GravityEnabled && collision.gameObject.tag == "Platform")
        {
            Grounded = true;
            playerAnimator.SetBool("isFalling", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Stairway movement handled in stairmovement script
        CollidingObjects.Add(collision.gameObject);
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
        //jpost Audio
        if(collision.tag == "Trash")
        {
            PlayTrashCollision();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {

        if (QuestCollectionText != null)
        {
            InteractionIndicator.SetActive(false);
            QuestCollectionText.enabled = false;
            QuestCollectionText.text = "Press F to interact.";
        }
        CollidingObjects.Remove(collision.gameObject);
    }
}