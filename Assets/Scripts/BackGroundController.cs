using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundController : MonoBehaviour
{
    GameObject playerRef;


    // Start is called before the first frame update
    void Start()
    {
        playerRef = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(playerRef.transform.position.x, transform.position.y, transform.position.z);
    }
}
