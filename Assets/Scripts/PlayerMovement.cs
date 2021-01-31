using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //audio vars
    public AudioSource audioSource;
    public AudioClip fireWalkClip;
    public AudioClip fireJumpClip;
    public AudioClip fireJumpLandClip;
    public AudioClip fireWaterDeathClip;


    public float timeWalkingClips;
    float walkingClipTimer;

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

    //Physics Collisions stuff
    private Rigidbody2D rb;
    private CapsuleCollider2D capsuleCollider;
    private SpriteRenderer sprite;

    private bool inLamp;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        sprite = GetComponent<SpriteRenderer>();

        moveSpeed = 5.0f;
        jumpForce = 8.0f;
        maxJumpCount = 2;
        timeWalkingClips = 0.5f;

        inLamp = false;
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

        //Sound
        RunSounds();
    }

    private void RunSounds()
    {
        //play walking sound if moving
        if (moveDir != 0)
        {
            walkingClipTimer += Time.deltaTime;
            if (walkingClipTimer > timeWalkingClips)
            {
                audioSource.PlayOneShot(fireWalkClip);
                walkingClipTimer = 0;
            }
        }
    }

    private void FixedUpdate()
    {
        Simulate();
    }

    private void Simulate()
    {
        bool wasGrounded = isGrounded;
        isGrounded = Physics2D.OverlapCircle(GroundCheck.position, checkRadius, GroundObjects);

        if (!wasGrounded && isGrounded)
        {
            //TODO increase landing sound by velocity
            audioSource.PlayOneShot(fireJumpLandClip);
        }

        if (isGrounded)
        {
            currJumpCount = maxJumpCount;
        }

        rb.velocity = new Vector2(moveDir * moveSpeed, rb.velocity.y);

        if (jumpInput && currJumpCount > 0)
        {
            currJumpCount--;
            audioSource.PlayOneShot(fireJumpClip);
            if (isGrounded)
            {
                rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            }
            else
            {
                rb.AddForce(new Vector2(0f, jumpForce * 0.5f), ForceMode2D.Impulse);
            }
            jumpInput = false;
        }
    }



    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer == 9)
        {
            rb.gravityScale = 0;
            capsuleCollider.enabled = false;
            sprite.enabled = false;
            transform.position = col.gameObject.transform.position;
            moveDir = 0;
            inLamp = true;
            Debug.Log("Player has contactd the lamp player function");
            //SceneManager.LoadScene("SampleScene");
        }
    }

    //void OnTriggerEnter2D(Collider2D col)
    //{
    //    if (col.gameObject.tag == "Lamp")
    //    {
    //        capsuleCollider.enabled = false;
    //        sprite.enabled = false;
    //        transform.position = col.gameObject.transform.position;

    //        Debug.Log("Player has contactd the lamp player function");
    //        //SceneManager.LoadScene("SampleScene");
    //    }
    //}


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
        if (inLamp) { return;  }

        if (Input.GetButtonDown("Jump"))
        {
            jumpInput = true;
        }
        moveDir = Input.GetAxis("Horizontal");
    }

    private void FlipChar()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }
}
