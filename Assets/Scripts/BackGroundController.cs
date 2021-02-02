using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundController : MonoBehaviour
{
    //Camera stuff
    private GameObject CameraRef;
    private float camStartXPos;
    private float camCurrXPos;
    private float backgroundWidth;

    //Child Object references
    private GameObject[] backgroundLayers;
    private float[] parallaxVals;
    private float[] increments;

    private GameObject ForegroundRef;
    private GameObject MidgroundRef;
    private GameObject BackgroundRef;
    private GameObject SkyColorRef;


    // Start is called before the first frame update
    void Start()
    {
        CameraRef = GameObject.FindWithTag("MainCamera");
        ForegroundRef = GameObject.Find("Foreground");
        MidgroundRef = GameObject.Find("Midground");
        BackgroundRef = GameObject.Find("Background");
        SkyColorRef = GameObject.Find("SkyColor");

        GameObject fore = GameObject.Find("ForegroundBright");
        SpriteRenderer foreSprite = fore.GetComponent<SpriteRenderer>();
        backgroundWidth = foreSprite.size.x;

        Debug.Log("backgroundwidth: " + backgroundWidth);

        camStartXPos = CameraRef.transform.position.x;

        Debug.Log("camStartXPos: " + camStartXPos);

        backgroundLayers = new GameObject[4];
        parallaxVals = new float[4];
        increments = new float[4];

        if (ForegroundRef) backgroundLayers[0] = ForegroundRef;
        parallaxVals[0] = 0.50f;
        increments[0] = 0;
        if (MidgroundRef) backgroundLayers[1] = MidgroundRef;
        parallaxVals[1] = 0.75f;
        increments[1] = 0;
        if (BackgroundRef) backgroundLayers[2] = BackgroundRef;
        parallaxVals[2] = 0.9f;
        increments[2] = 0;
        if (SkyColorRef) backgroundLayers[3] = SkyColorRef;
        parallaxVals[3] = 1;
        increments[3] = 0;
    }

    // Update is called once per frame
    void Update()
    {
        camCurrXPos = CameraRef.transform.position.x;
        Debug.Log("camCurrXPos: " + camCurrXPos);
        for (int i = 0; i < backgroundLayers.Length; i++)
        {

            backgroundLayers[i].transform.position = 
                new Vector3(
                    camStartXPos + increments[i] + (camCurrXPos * parallaxVals[i]),
                    backgroundLayers[i].transform.position.y,
                    backgroundLayers[i].transform.position.z);


            float currLayerPosition = camCurrXPos * (1 - parallaxVals[i]);
            if (currLayerPosition < increments[i] - backgroundWidth) { increments[i] -= backgroundWidth; }
            else if (currLayerPosition > increments[i] + backgroundWidth) { increments[i] += backgroundWidth; }
        }
        
    }
}
