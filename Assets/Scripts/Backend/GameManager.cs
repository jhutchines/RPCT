using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public string st_worldName;
    public string st_saveLocation;
    public string st_mapName;
    public bool bl_debugMode;
    public bool bl_loadWorld;

    public float fl_audioVolume;
    GameObject audioSlider;
    public AudioClip[] audioClips;
    int currentAudio;

    public enum MarkerType
    {
        Location,
        Character,
        Both
    }

    GameObject[] saveObjs;

    private void Awake()
    {
        GameObject[] go_managers = GameObject.FindGameObjectsWithTag("Game Manager");

        if (go_managers.Length > 1) Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Audio();
    }

    void Audio()
    {
        GetComponent<AudioSource>().volume = fl_audioVolume;
        if (audioSlider == null)
        {
            audioSlider = GameObject.Find("AudioSlider");
        }
        else
        {
            fl_audioVolume = audioSlider.GetComponent<Slider>().value;
        }

        if (!GetComponent<AudioSource>().isPlaying)
        {
            if (currentAudio == audioClips.Length - 1) currentAudio = 0;
            else currentAudio++;

            GetComponent<AudioSource>().clip = audioClips[currentAudio];
            GetComponent<AudioSource>().Play();
        }

    }
}
