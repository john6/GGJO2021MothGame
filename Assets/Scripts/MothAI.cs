using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MothAI : MonoBehaviour
{
    GameObject playerRef;
    private Vector2 moveDir;
    private bool isFollowing;

    private Rigidbody2D rb;

    GameObject DarkSpriteObject;

    //audio vars
    public AudioSource audioSource;
    public AudioClip MothFlameDeathClip;
    public AudioClip MothDingClip;
    public AudioClip MothFollowClip;
    public AudioClip MothWingFlapClip;
    public float wingFlapClipTimer;
    public float wingFlapTime;

    public AudioClip MothLostClip;

    public float flameDeathVolume;

    private bool facingRight = true;

    //flying vars
    public float moveSpeed;
    private float timeTillNextFlap;
    private float wingFlapSpeed;
    private float wingFlapStrength;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        DarkSpriteObject = transform.GetChild(0).gameObject;

        playerRef = GameObject.FindWithTag("Player");

        flameDeathVolume = 1.0f;
        wingFlapClipTimer = 0f;
        wingFlapTime = 1.5f;

        moveSpeed = 0.5f;
        wingFlapStrength = 1.0f;
        timeTillNextFlap = wingFlapSpeed;
        wingFlapSpeed = 1.3f;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Animate();
    }

    private void FixedUpdate()
    {
        Simulate();
    }

    private void Simulate()
    {
        timeTillNextFlap -= Time.deltaTime;

        if (timeTillNextFlap > 0)
        {
            return;
        }

        timeTillNextFlap = wingFlapSpeed;

        Vector2 down = new Vector2(0.0f, -1.0f);

        Vector2 downWardVelocity = Vector3.Project(rb.velocity, down);
        float increaseInSpeed = downWardVelocity.magnitude;
        //up impulse
        rb.AddForce(new Vector2(0.0f, wingFlapStrength + increaseInSpeed), ForceMode2D.Impulse);

        if (isFollowing)
        {
            //wingFlapClipTimer += Time.deltaTime;
            //if (wingFlapClipTimer > wingFlapTime)
            //{
            //    audioSource.PlayOneShot(MothWingFlapClip);
            //    wingFlapClipTimer = 0;
            //}
            audioSource.PlayOneShot(MothWingFlapClip);
            //rb.velocity = new Vector2(moveDir * moveSpeed, rb.velocity.y);
            Vector2 playerCurrPos = new Vector2(playerRef.transform.position.x, playerRef.transform.position.y);
            Vector2 currPos = new Vector2(transform.position.x, transform.position.y);
            moveDir = playerCurrPos - currPos;
            moveDir.Normalize();


            //towardPlayer impulse
            rb.AddForce(new Vector2(moveDir.x * moveSpeed, moveDir.y * moveSpeed), ForceMode2D.Impulse);
        }


    }

    void OnTriggerEnter2D(Collider2D col)
    {
        FollowPlayer(col);
    }

    private void FollowPlayer(Collider2D col)
    {
        //Debug.Log("Moth is following player");
        if (col.gameObject.tag == "Player" && !isFollowing)
        {
            audioSource.PlayOneShot(MothFollowClip, flameDeathVolume);
            isFollowing = true;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        //Debug.Log("Moth Collision");
        if (col.gameObject.tag == "Player")
        {
            Die();
        }

    }

    private void Die()
    {
        audioSource.PlayOneShot(MothFlameDeathClip, flameDeathVolume);
        GetComponent<SpriteRenderer>().enabled = false;
        DarkSpriteObject.GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        //audioSource.Play();
        Destroy(gameObject, 1.5f);
    }

    private void Animate()
    {
        float xDir = rb.velocity.x;
        if (xDir < 0 && facingRight)
        {
            FlipChar();
        }
        else if (xDir > 0 && !facingRight)
        {
            FlipChar();
        }
    }

    private void FlipChar()
    {
        facingRight = !facingRight;

        transform.Rotate(0f, 180f, 0f);
    }
}
