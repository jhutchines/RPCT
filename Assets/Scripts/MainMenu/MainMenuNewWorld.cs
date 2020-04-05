using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuNewWorld : MonoBehaviour
{
    public string st_worldName;
    public string st_mapName;
    public string st_fullMapName;

    public Text tx_worldName;
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
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("MainMenu"))
        {
            st_worldName = tx_worldName.text;
            if (st_worldName != "" && st_mapName != "") go_arrow.SetActive(true);
            else go_arrow.SetActive(false);

            if (Input.GetKeyDown(KeyCode.Space)) Debug.Log(st_worldName);
        }
    }

    public void StartNew()
    {
        gameManager.st_worldName = st_worldName;
        gameManager.st_mapName = st_fullMapName;
        SceneManager.LoadScene(st_sceneName);
    }
}
