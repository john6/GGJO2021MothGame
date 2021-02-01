using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public AudioSource audioSourceMusic;
    public AudioSource audioSourceAmbience;
    public AudioSource titleSongSource;

    public AudioClip mainSong;
    public AudioClip titleStart;
    public AudioClip titleLoop;
    public AudioClip generalAmbience;
    public List<SpriteRenderer> darkSprites;

    private float levelTimeTotal;
    private float currTimeRemaining;
    private float darkenSpeed;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnEnable()
    {
        Debug.Log("OnEnable called");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

        // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        currTimeRemaining -= Time.deltaTime;
        currTimeRemaining = Mathf.Max(currTimeRemaining, 0.0f);
        UpdateDarkSprites();
    }

    private void UpdateDarkSprites()
    {
        foreach (SpriteRenderer sprite in darkSprites)
        {
            if (sprite)
            {
                float colorLevel = currTimeRemaining / levelTimeTotal;
                sprite.color = new Color(colorLevel, colorLevel, colorLevel);
            }
        }
    }

    private void GetDarkSprites()
    {
        GameObject[] darkGameObjects = GameObject.FindGameObjectsWithTag("DarkSprite");
        foreach (GameObject obj in darkGameObjects)
        {
            darkSprites.Add(obj.GetComponent<SpriteRenderer>());
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);
        Debug.Log(mode);
        if (scene.name == "MainMenu")
        {
            //StartCoroutine(playTitleSound());
            titleSongSource.clip = titleLoop;
            titleSongSource.loop = true;
            titleSongSource.Play();
        }
        if (scene.name == "Level1")
        {
            levelTimeTotal = 30.0f;
            currTimeRemaining = levelTimeTotal;
            darkenSpeed = 1;


            titleSongSource.Stop();

            audioSourceMusic.clip = mainSong;
            audioSourceMusic.loop = true;
            audioSourceMusic.Play();

            audioSourceAmbience.clip = generalAmbience;
            audioSourceAmbience.loop = true;
            audioSourceAmbience.Play();

            GetDarkSprites();
        }
        else if (scene.name == "Level2")
        {
            levelTimeTotal = 20.0f;
            currTimeRemaining = levelTimeTotal;
            darkenSpeed = 1;

            audioSourceMusic.clip = mainSong;
            audioSourceMusic.loop = true;
            audioSourceMusic.Play();

            audioSourceAmbience.clip = generalAmbience;
            audioSourceAmbience.loop = true;
            audioSourceAmbience.Play();

            GetDarkSprites();
        }
        else if (scene.name == "Level3")
        {
            levelTimeTotal = 15.0f;
            currTimeRemaining = levelTimeTotal;
            darkenSpeed = 1;

            audioSourceMusic.clip = mainSong;
            audioSourceMusic.loop = true;
            audioSourceMusic.Play();

            audioSourceAmbience.clip = generalAmbience;
            audioSourceAmbience.loop = true;
            audioSourceAmbience.Play();

            GetDarkSprites();
        }
    }

    IEnumerator playTitleSound()
    {
        titleSongSource.clip = titleStart;
        titleSongSource.Play();
        yield return new WaitForSeconds(titleStart.length);
        titleSongSource.clip = titleLoop;
        titleSongSource.Play();
    }


}
