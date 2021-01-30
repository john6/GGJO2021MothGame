using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed;
    private bool facingRight = true;
    private float moveDir;
    private bool jumpInput;
    private float jumpForce;
    public float checkRadius;

    public Transform CeilingCheck;
    public Transform GroundCheck;
    public LayerMask GroundObjects;
    private bool isGrounded;

    public int currJumpCount;
    private int maxJumpCount;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        moveSpeed = 5.0f;
        jumpForce = 5.0f;
        maxJumpCount = 2;
    }

    // Start is called before the first frame update
    void Start()
    {
        currJumpCount = maxJumpCount;
    }

    // Update is called once per frame
    void Update()
    {
        //Get inputs
        GetInputs();

        //animate
        Animate();
    }

    private void FixedUpdate()
    {
        Simulate();
    }

    private void Simulate()
    {
        isGrounded = Physics2D.OverlapCircle(GroundCheck.position, checkRadius, GroundObjects);

        if (isGrounded)
        {
            currJumpCount = maxJumpCount;
        }

        rb.velocity = new Vector2(moveDir * moveSpeed, rb.velocity.y);

        if (jumpInput && currJumpCount > 0)
        {
            currJumpCount--;
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            jumpInput = false;
        }
    }

    private void Animate()
    {
        if (moveDir < 0 && facingRight)
        {
            FlipChar();
        }
        else if (moveDir > 0 && !facingRight)
        {
            FlipChar();
        }
    }

    private void GetInputs()
    {
        if (Input.GetButtonDown("Jump"))
        {
            jumpInput = true;
        }
        //else
        //{
        //    jumpInput = false;
        //}

        moveDir = Input.GetAxis("Horizontal");
    }

    private void FlipChar()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }
}
