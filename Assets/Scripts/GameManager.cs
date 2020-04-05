using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public string st_worldName;
    public string st_mapName;
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
        
    }

    public void SaveGame()
    {
        
    }
}
