using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnablePlatformController : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip leafBurnClip;

    private int health;
    private float deathTime;
    private bool isDying;

    void Awake()
    {
        health = 1;
        deathTime = 1.2f;
        isDying = false;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //if (health <= 0)
        //{
        //    isDying = true;
        //}
        if (isDying)
        {
            isDying = false;
            StartCoroutine(DieOnDelay(deathTime));
        }
    }

    private IEnumerator DieOnDelay(float deathTime)
    {
        audioSource.PlayOneShot(leafBurnClip);
        yield return new WaitForSeconds(deathTime);
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            // health -= 1;
            isDying = true;
        }
    }


}
