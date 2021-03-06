﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    //audio vars
    public AudioSource audioSource;
    public AudioClip fireWalkClip;
    public AudioClip fireJumpClip;
    public AudioClip fireJumpLandClip;
    public AudioClip fireWaterDeathClip;


    public int currMothsFollowing;

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
        jumpForce = 6.0f;
        maxJumpCount = 1;
        timeWalkingClips = 0.5f;

        inLamp = false;

        currMothsFollowing = 0;
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
                rb.AddForce(new Vector2(0f, jumpForce * 0.75f), ForceMode2D.Impulse);
            }
            jumpInput = false;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Water")
        {
            Debug.Log("Player has collided with water");
            capsuleCollider.enabled = false;
            transform.position = new Vector3(transform.position.x, transform.position.y, 1);
            audioSource.PlayOneShot(fireWaterDeathClip);
            StartCoroutine(LoadSceneOnDelay(1));
        }
    }

    public void PlayerContactLantern(Collision2D col)
    {
        rb.gravityScale = 0;
        rb.velocity = new Vector2(0.0f, 0.0f);
        capsuleCollider.enabled = false;
        sprite.enabled = false;
        transform.position = col.gameObject.transform.position;
        moveDir = 0;
        inLamp = true;
        Debug.Log("Player has contactd the lamp player function");
        //SceneManager.LoadScene("SampleScene");
    }

    private IEnumerator LoadSceneOnDelay(int delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("GameOver");
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
