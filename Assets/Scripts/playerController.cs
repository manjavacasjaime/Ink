using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    //groundCheckPoint values
    public Transform groundCheckPoint;
    public LayerMask groundLayer;
    private bool isTouchingGround;
    private Vector2 groundCheckPointA;
    private Vector2 groundCheckPointB;

    //movement values
    public float speed = 7f;
    public float jumpSpeed = 7f;
    private float movement = 0f;
    private float mylocalScaleX;
    private float mylocalScaleY;
    private float mylocalScaleZ;

    //component values
    Animator playerAnimator;
    Rigidbody2D rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        mylocalScaleX = transform.localScale.x;
        mylocalScaleY = transform.localScale.y;
        mylocalScaleZ = transform.localScale.z;

        playerAnimator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        checkGround();
        movementControls();

        //jump
        if (!isTouchingGround) playerAnimator.Play("Flamer_Jump");
    }

    void movementControls()
    {
        movement = Input.GetAxis("Horizontal");

        //movement
        if (movement > 0f) //right
        {
            rigidBody.velocity = new Vector2(0.7f * speed, rigidBody.velocity.y);
            transform.localScale = new Vector3(mylocalScaleX, mylocalScaleY, mylocalScaleZ);
            if (isTouchingGround) playerAnimator.Play("Flamer_Run");
        }
        else if (movement < 0f) //left
        {
            rigidBody.velocity = new Vector2(-0.7f * speed, rigidBody.velocity.y);
            transform.localScale = new Vector3(-mylocalScaleX, mylocalScaleY, mylocalScaleZ);
            if (isTouchingGround) playerAnimator.Play("Flamer_Run");
        }
        else //idle
        {
            rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
            if (isTouchingGround) playerAnimator.Play("Flamer_Idle");
        }

        //jump
        if (Input.GetButtonDown("Jump") && isTouchingGround)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpSpeed);
        }
    }

    void checkGround()
    {
        groundCheckPointA = new Vector2(groundCheckPoint.position.x - mylocalScaleX, groundCheckPoint.position.y);
        groundCheckPointB = new Vector2(groundCheckPoint.position.x + mylocalScaleX, groundCheckPoint.position.y);
        isTouchingGround = Physics2D.OverlapArea(groundCheckPointA, groundCheckPointB, groundLayer);
    }
}
