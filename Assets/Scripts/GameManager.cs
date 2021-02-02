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
    public AudioClip EndGameStinger;
    public AudioClip generalAmbience;
    public List<SpriteRenderer> darkSprites;

    private float levelTimeTotal;
    private float currTimeRemaining;
    private float darkenSpeed;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        Application.targetFrameRate = 120;
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
        currTimeRemaining -= Time.deltaTime;
        currTimeRemaining = Mathf.Max(currTimeRemaining, 0.0f);
        UpdateDarkSprites();
    }

    void FixedUpdate()
    {

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
            StartCoroutine(playMainMenuMusicSequentially());
            //titleSongSource.clip = titleLoop;
            //titleSongSource.loop = true;
            //titleSongSource.Play();
        }
        if (scene.name == "Level1")
        {
            levelTimeTotal = 30.0f;
            currTimeRemaining = levelTimeTotal;
            darkenSpeed = 1;


            titleSongSource.Stop();

            audioSourceMusic.clip = mainSong;
            audioSourceMusic.loop = true;
            audioSourceMusic.volume = 0;
            audioSourceMusic.Play();
            StartCoroutine(StartFade(audioSourceMusic, 5, 1));

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
            audioSourceMusic.volume = 0;
            audioSourceMusic.Play();
            StartCoroutine(StartFade(audioSourceMusic, 5, 1));

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
            audioSourceMusic.volume = 0;
            audioSourceMusic.Play();
            StartCoroutine(StartFade(audioSourceMusic, 5, 1));

            audioSourceAmbience.clip = generalAmbience;
            audioSourceAmbience.loop = true;
            audioSourceAmbience.Play();

            GetDarkSprites();
        }
        else if (scene.name == "EndMenu")
        {
            audioSourceMusic.clip = EndGameStinger;
            audioSourceMusic.loop = false;
            audioSourceMusic.Play();
        }
    }

    IEnumerator playTitleSound()
    {
        //The music.length doesn't line up perfectly so this has an awkward pause
        titleSongSource.clip = titleStart;
        titleSongSource.loop = false;
        titleSongSource.Play();
        yield return new WaitForSeconds(titleStart.length);
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            titleSongSource.clip = titleLoop;
            titleSongSource.loop = true;
            titleSongSource.Play();
        }
    }

    IEnumerator playMainMenuMusicSequentially()
    {
        yield return null;
        //2.Assign current AudioClip to audiosource
        titleSongSource.clip = titleStart;
        titleSongSource.loop = false;
        //3.Play Audio
        titleSongSource.Play();
        //4.Wait for it to finish playing
        while (titleSongSource.isPlaying)
        {
            yield return null;
        }
        //5. Go back to #2 and play the next audio in the adClips array
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            titleSongSource.clip = titleLoop;
            titleSongSource.loop = true;
            titleSongSource.Play();
        }
    }


    public static IEnumerator StartFade(AudioSource audioSource, float duration, float targetVolume)
    {
        float currentTime = 0;
        float start = audioSource.volume;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        yield break;
    }

}
