using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuGetMap : MonoBehaviour
{
    Text tx_mapName;
    public MainMenuNewWorld sc_newWorld;

    // Start is called before the first frame update
    void Start()
    {
        tx_mapName = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (sc_newWorld == null)
        {
            sc_newWorld = GameObject.Find("NewWorldImage").GetComponent<MainMenuNewWorld>();
        }

        if (sc_newWorld.st_mapName != null)
        {
            tx_mapName.text = sc_newWorld.st_mapName;
        }
    }

    public void SelectMap()
    {
        
    }
}
