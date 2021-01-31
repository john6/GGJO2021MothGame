using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LanternController : MonoBehaviour
{

    public Sprite fireEmptySprite;
    public Sprite fireFullSprite;


    public AudioSource audioSource;
    public AudioClip LampOpenClip;
    public AudioClip LampCloseClip;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //void OnTriggerEnter2D(Collider2D col)
    //{
    //    if (col.gameObject.tag == "Player")
    //    {
    //        gameObject.GetComponent<SpriteRenderer>().sprite = fireFullSprite;

    //        Debug.Log("Player has contactd the lamp");
    //        SceneManager.LoadScene("SampleScene");
    //    }
    //}

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            audioSource.PlayOneShot(LampCloseClip);
            gameObject.GetComponent<SpriteRenderer>().sprite = fireFullSprite;

            //Debug.Log("Player has contactd the lamp");
            StartCoroutine(DelayedSceneLoad());
        }
    }

    private IEnumerator DelayedSceneLoad()
    {
        yield return new WaitForSeconds(6);
        SceneManager.LoadScene("Level2");
    }
}
