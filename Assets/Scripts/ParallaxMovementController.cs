using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxMovementController : MonoBehaviour
{
    //Camera stuff
    private GameObject cam;

    private float backgroundStartXPos;
    private float camStartXPos;

    private float spriteWidth;
    public float parallaxVal;


    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.FindWithTag("MainCamera");

        camStartXPos = cam.transform.position.x;

        backgroundStartXPos = transform.position.x;

        spriteWidth = gameObject.GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        //float currPos = cam.transform.position.x * (1 - parallaxVal);
        //float moveDist = cam.transform.position.x * parallaxVal;

        //transform.position = new Vector3(backgroundStartXPos + camStartXPos + moveDist, transform.position.y, transform.position.z);

        //if (currPos > camStartXPos + spriteWidth) { camStartXPos += spriteWidth; }
        //else if (currPos < camStartXPos - spriteWidth) { camStartXPos -= spriteWidth; }
        
        
        //BRUH
    }


    private void FixedUpdate()
    {
        float currPos = cam.transform.position.x * (1 - parallaxVal);
        float moveDist = cam.transform.position.x * parallaxVal;

        transform.position = new Vector3(backgroundStartXPos + camStartXPos + moveDist, transform.position.y, transform.position.z);

        if (currPos > camStartXPos + spriteWidth) { camStartXPos += spriteWidth; }
        else if (currPos < camStartXPos - spriteWidth) { camStartXPos -= spriteWidth; }
    }
}
