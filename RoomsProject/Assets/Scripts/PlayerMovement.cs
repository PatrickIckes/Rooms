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

    //Used to track when items are picked up
    public Inventory inventory;
    
    private void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
        colliderTrigger = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 currentPos = rbody.position;
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        float jumpInput = Input.GetAxis("Jump");
        Vector2 inputVector = new Vector2(horizontalInput, verticalInput);
        inputVector = Vector2.ClampMagnitude(inputVector, 1);
        Vector2 movement = inputVector * movementSpeed;
        Vector2 newPos = currentPos + movement * Time.fixedDeltaTime;
        rbody.MovePosition(newPos);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && withinInteractable)
        {
            SceneManager.LoadScene(1);
        }
    }

    //private void OnControllerColliderHit(ControllerColliderHit hit)
    //{
    //    IInventoryItem item = hit.collider.GetComponent<IInventoryItem>();
    //    if(item != null)
    //    {
    //        inventory.AddItem(item);
    //    }
    //}

    //private void CheckForEntrance()
    //{

    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IInventoryItem item = GetComponent<IInventoryItem>();
        if (item != null)
        {
            inventory.AddItem(item);
        }

        if(collision.name == "Door")
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