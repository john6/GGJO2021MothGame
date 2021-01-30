using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MothAI : MonoBehaviour
{
    GameObject playerRef;
    public float moveSpeed;
    private Vector2 moveDir;
    private bool isFollowing;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        playerRef = GameObject.FindWithTag("Player");
        moveSpeed = 1.5f;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        Simulate();
    }

    private void Simulate()
    {
        if (isFollowing)
        {
            //rb.velocity = new Vector2(moveDir * moveSpeed, rb.velocity.y);
            Vector2 playerCurrPos = new Vector2(playerRef.transform.position.x, playerRef.transform.position.y);
            Vector2 currPos = new Vector2(transform.position.x, transform.position.y);
            moveDir = playerCurrPos - currPos;
            moveDir.Normalize();
            //rb.AddForce(moveDir, ForceMode2D.Impulse);
            rb.velocity = new Vector2(moveDir.x * moveSpeed, moveDir.y * moveSpeed);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("On trigger has triggered");
        if (col.gameObject.tag == "Player")
        {
            isFollowing = true;
        }
    }


    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("On Collision has triggered");
        if (col.gameObject.tag == "Player")
        {
            Die();
        }

    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
