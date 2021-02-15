using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    public List<Material> darkMaterials;
    public List<GameObject> darkMaterialObjects;

    public Text MothsFollowingText;
    public int currLevelMothReq;

    public GameObject playerRef;

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
        if (SceneManager.GetActiveScene().name != "MainMenu")
        UpdateLevel();
    }

    private void UpdateLevel()
    {
        currTimeRemaining -= Time.deltaTime;
        currTimeRemaining = Mathf.Max(currTimeRemaining, 0.0f);
        UpdateDarkSprites();
        UpdateDarkmaterials();
        MothsFollowingText.text = "Moths Following: " + playerRef.GetComponent<PlayerMovement>().currMothsFollowing + "/" + currLevelMothReq;
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
                //float colorLevel = 1;
                sprite.color = new Color(colorLevel, colorLevel, colorLevel);
            }
        }
    }

    private void GetSceneRefs()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");
        MothsFollowingText = GameObject.FindGameObjectWithTag("UI_Text").GetComponent<Text>();
    }

    private void UpdateDarkmaterials()
    {
        //foreach (Material mat in darkMaterials)
        //{
        //    if (mat)
        //    {
        //        float colorLevel = currTimeRemaining / levelTimeTotal;
        //        mat.SetFloat("tint", colorLevel);

        //    }
        //}
        float colorLevel = currTimeRemaining / levelTimeTotal;
        Color greyColor = new Color(colorLevel, colorLevel, colorLevel);
        float screenHeight = Screen.currentResolution.height;
        float screenWidth = Screen.currentResolution.width;
        //Web player uninverts everything so Im gonna redo the controls I guess
        //UNITY PLAYER & EXE VERSION
        //Vector3 pixelPosition = new Vector3(Screen.currentResolution.width / 2, screenHeight - (playerRef.transform.position.y * 100) - 215);
        //WEB PLAYER LOCATION
        Vector3 pixelPosition = new Vector3(Screen.currentResolution.width / 2, (playerRef.transform.position.y * 100));
        foreach (GameObject matObj in darkMaterialObjects)
        {
            if (matObj)
            {
                //matObj.GetComponent<Renderer>().material.SetFloat("Tint", colorLevel);
               // Debug.Log("Tint: " + matObj.GetComponent<SpriteRenderer>().material.GetFloat("Tint"));

                //matObj.GetComponent<SpriteRenderer>().material.SetColor("_Color", greyColor);
                matObj.GetComponent<SpriteRenderer>().material.SetVector("_Light_Origin", pixelPosition);
                matObj.GetComponent<SpriteRenderer>().material.SetFloat("_Pure_black_dist",
                //When the level timer is early, more of the level will be colored
                    (screenWidth * 0.25f) + ((colorLevel * 2) * (screenWidth * 0.25f)));
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

    private void GetDarkMaterials()
    {
        //GameObject[] darkGameObjects = GameObject.FindGameObjectsWithTag("DarkSprite");
        //foreach (GameObject obj in darkGameObjects)
        //{
        //    darkMaterials.Add(obj.GetComponent<Material>());
        //}
        darkMaterialObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag("DarkSprite"));
        //foreach (GameObject obj in darkGameObjects)
        //{
        //    darkMaterials.Add(obj.GetComponent<Material>());
        //}
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
            levelTimeTotal = 38.0f;
            currLevelMothReq = 3;
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

            GetSceneRefs();
            GetDarkSprites();
            GetDarkMaterials();
        }
        else if (scene.name == "Level2")
        {
            levelTimeTotal = 28.0f;
            currLevelMothReq = 4;
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

            GetSceneRefs();
            GetDarkSprites();
            GetDarkMaterials();
        }
        else if (scene.name == "Level3")
        {
            levelTimeTotal = 25.0f;
            currLevelMothReq = 4;
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

            GetSceneRefs();
            GetDarkSprites();
            GetDarkMaterials();
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
