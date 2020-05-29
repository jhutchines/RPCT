using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Save : MonoBehaviour
{
    public string st_filePath;
    public bool bl_canSave = true;
    GameManager gameManager;
    GameObject worldMap;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        worldMap = GameObject.Find("WorldMap");
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.bl_debugMode && Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.S)) SaveWorld();
    }

    public void SaveWorld()
    {
        if (bl_canSave)
        {
            string saveLocation = st_filePath + gameManager.st_worldName;
            if (Directory.Exists(saveLocation))
            {
                foreach (var file in Directory.GetFiles(saveLocation))
                {
                    File.Delete(file);
                }
                Directory.Delete(saveLocation);
            }
            Directory.CreateDirectory(saveLocation);
            string mapData = saveLocation + "/.MapData.txt";
            File.Create(mapData).Dispose();
            StreamWriter mapWriter = new StreamWriter(mapData);
            mapWriter.WriteLine(gameManager.st_mapName);
            mapWriter.WriteLine(gameManager.fl_worldSizeX);
            mapWriter.WriteLine(gameManager.fl_worldSizeY);
            mapWriter.WriteLine(gameManager.fl_partySpeed);
            mapWriter.Close();

            for (int i = 0; i < worldMap.transform.childCount; i++)
            {
                GameObject newMarker = worldMap.transform.GetChild(i).gameObject;
                if (newMarker.GetComponent<MarkerData>() == null) continue;
                string newFile = saveLocation + "/" + newMarker.name + "_" + i + ".txt";
                File.Create(newFile).Dispose();
                StreamWriter writer = new StreamWriter(newFile);
                writer.WriteLine(newMarker.name);
                writer.WriteLine(newMarker.GetComponent<RectTransform>().localPosition.x);
                writer.WriteLine(newMarker.GetComponent<RectTransform>().localPosition.y);
                writer.WriteLine(newMarker.GetComponent<RectTransform>().localPosition.z);
                writer.WriteLine(newMarker.GetComponent<MarkerData>().go_textbox.GetComponent<InputField>().text);
                writer.Close();
                Debug.Log("Saved " + newMarker + " to " + newFile);
            }

            StartCoroutine(GameSaved());
        }
    }

    IEnumerator GameSaved()
    {
        bl_canSave = false;
        Text saveText = transform.GetChild(0).GetComponent<Text>();
        saveText.fontStyle = FontStyle.Bold;
        saveText.text = "Done!";
        yield return new WaitForSeconds(2);
        saveText.fontStyle = FontStyle.Normal;
        saveText.text = "Save";
        bl_canSave = true;
    }
}
