using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    GameObject playerRef;



    private void Awake()
    {
        playerRef = GameObject.FindWithTag("Player");
    }


    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position.Set(playerRef.transform.position.x, transform.position.y, -10.0f);
        transform.position = new Vector3(playerRef.transform.position.x, transform.position.y, -10.0f);
    }
}
