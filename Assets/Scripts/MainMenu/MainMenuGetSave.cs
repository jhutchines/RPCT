using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuGetSave : MonoBehaviour
{
    Text tx_saveName;
    public MainMenuLoadWorld sc_newWorld;

    // Start is called before the first frame update
    void Start()
    {
        tx_saveName = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (sc_newWorld == null)
        {
            sc_newWorld = GameObject.Find("LoadWorldImage").GetComponent<MainMenuLoadWorld>();
        }

        if (sc_newWorld.st_worldName != null)
        {
            tx_saveName.text = sc_newWorld.st_worldName;
        }
    }
}
