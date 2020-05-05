using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuLoadWorld : MonoBehaviour
{
    public string st_worldName;
    public string st_saveLocation;
    public string st_mapName;

    public GameObject go_arrow;

    public string st_sceneName;

    GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

        if (st_worldName != "") go_arrow.SetActive(true);
        else go_arrow.SetActive(false);

        if (gameManager.bl_debugMode && Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Space)) Debug.Log(st_worldName);
    }

    public void LoadWorld()
    {
        gameManager.st_worldName = st_worldName;
        gameManager.st_saveLocation = st_saveLocation;
        gameManager.st_mapName = st_mapName;
        gameManager.bl_loadWorld = true;
        SceneManager.LoadScene(st_sceneName);
    }
}
