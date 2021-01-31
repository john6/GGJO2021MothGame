using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public AudioSource audioSourceMusic;
    public AudioSource audioSourceAmbience;
    public AudioClip mainSong;
    public AudioClip generalAmbience;



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


    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);
        Debug.Log(mode);
        if (scene.name == "Level1")
        {
            audioSourceMusic.clip = mainSong;
            audioSourceMusic.loop = true;
            audioSourceMusic.Play();

            audioSourceAmbience.clip = generalAmbience;
            audioSourceAmbience.loop = true;
            audioSourceAmbience.Play();
        }
    }

}
