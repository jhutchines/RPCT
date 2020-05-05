using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Load : MonoBehaviour
{
    GameManager gameManager;
    string saveLocation;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.bl_loadWorld) LoadWorld();
    }

    void LoadWorld()
    {
        gameManager.bl_loadWorld = false;
        saveLocation = gameManager.st_saveLocation + "/";
        foreach (var file in Directory.GetFiles(saveLocation))
        {
            if (file == saveLocation + ".MapData.txt") continue;
            string[] lines = File.ReadAllLines(file);
            Debug.Log(lines[0] + ".prefab");
            GameObject loadObject = Instantiate(Resources.Load(lines[0].ToString()) as GameObject, GameObject.Find("WorldMap").transform);
            loadObject.name = loadObject.name.Remove(loadObject.name.IndexOf("("));
            if (loadObject.GetComponent<MarkerData>().markerType == GameManager.MarkerType.Location) loadObject.transform.SetSiblingIndex(1);
            else loadObject.transform.SetAsLastSibling();
            loadObject.GetComponent<RectTransform>().localPosition = new Vector3(float.Parse(lines[1]), float.Parse(lines[2]), float.Parse(lines[3]));
            loadObject.GetComponent<MarkerData>().go_textbox.GetComponent<InputField>().text = lines[4];
            Debug.Log(file);
        }
    }
}
