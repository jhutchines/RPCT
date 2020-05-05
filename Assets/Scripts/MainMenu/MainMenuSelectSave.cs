using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuSelectSave : MonoBehaviour
{
    GameObject go_loadWorld;
    public string st_fullName;
    public string st_mapName;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (go_loadWorld == null) go_loadWorld = GameObject.Find("LoadWorldImage");
    }

    public void MapSelected()
    {
        go_loadWorld.GetComponent<MainMenuLoadWorld>().st_worldName = transform.GetChild(0).GetComponent<Text>().text;
        go_loadWorld.GetComponent<MainMenuLoadWorld>().st_saveLocation = st_fullName;
        go_loadWorld.GetComponent<MainMenuLoadWorld>().st_mapName = st_mapName;
    }
}
