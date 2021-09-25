using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Movement")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float jumpSpeed = 12f;

    private SpriteRenderer sr;
    private bool lastDir;
    private bool inAir = false;


    Rigidbody2D myRigidBody;
    Collider2D myCollider2D;

    private void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        myCollider2D = GetComponent<Collider2D>();
    }

    private void Update()
    {
        Move();
        FlipSprite();
        Jump();
    }

    // To adjust speed I set it to be changed in the inspector but also to change how fast the player drops
    // you have to adjust the gravity of the world in the project settings
    private void Jump()
    {
        // To see if the player is on the ground
        if (!myCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;
        }

        if (Input.GetButtonDown("Jump"))
        {
            Vector2 jumpVelocity = new Vector2(0f, jumpSpeed);
            myRigidBody.velocity += jumpVelocity;
        }
    }

    // Code for moving the character left and right
    private void Move()
    {

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector2.left * Time.deltaTime * moveSpeed, Space.Self);
        }

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector2.right * Time.deltaTime * moveSpeed, Space.Self);
        }
    }

    // To flip the character when they are going right or left
    private void FlipSprite()
    {
        // Uses the sprite renderer to check if the player is going right or left
        // check the flip check in the sprite renderer
        if (Input.GetAxis("Horizontal") < 0)
        {
            sr.flipX = true;
            lastDir = true;
        }
        else if (Input.GetAxis("Horizontal") > 0)
        {
            sr.flipX = false;
            lastDir = false;
        }
        else
        {
            sr.flipX = lastDir;
        }
    }
}
