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
    public AudioClip levelEndStinger;


    GameObject manager;


    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindWithTag("GameController");
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
        manager.GetComponent<GameManager>().audioSourceMusic.Stop();
           yield return new WaitForSeconds(1);
        audioSource.PlayOneShot(levelEndStinger);
        yield return new WaitForSeconds(4);

        if (SceneManager.GetActiveScene().name == "Level1")
        {
            SceneManager.LoadScene("Level2");
        }
        else if (SceneManager.GetActiveScene().name == "Level2")
        {
            SceneManager.LoadScene("Level3");
        }
        else if (SceneManager.GetActiveScene().name == "Level3")
        {
            SceneManager.LoadScene("EndMenu");
        }
    }
}
