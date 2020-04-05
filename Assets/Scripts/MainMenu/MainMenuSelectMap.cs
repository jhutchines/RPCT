using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuSelectMap : MonoBehaviour
{
    GameObject go_newWorld;
    public string st_fullName;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (go_newWorld == null) go_newWorld = GameObject.Find("NewWorldImage");
    }

    public void MapSelected()
    {
        go_newWorld.GetComponent<MainMenuNewWorld>().st_mapName = transform.GetChild(0).GetComponent<Text>().text;
        go_newWorld.GetComponent<MainMenuNewWorld>().st_fullMapName = st_fullName;
    }
    
}
