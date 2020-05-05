using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class MainMenuFindSaves : MonoBehaviour
{
    public bool bl_savesLoaded;
    public GameObject go_button;
    public GameObject go_content;
    public string st_filePath;
    int in_saveCount;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!bl_savesLoaded)
        {
            UpdateSaves();
        }

        go_content.GetComponent<RectTransform>().sizeDelta = new Vector2(0, (in_saveCount * 20) + 10);
    }

    void UpdateSaves()
    {
        bl_savesLoaded = true;
        foreach (string file in Directory.GetDirectories(st_filePath))
        {
            GameObject go_newButton = Instantiate(go_button, go_content.transform);
            go_newButton.transform.localPosition = new Vector2(go_newButton.transform.localPosition.x, go_newButton.transform.localPosition.y - (20 * in_saveCount));
            go_newButton.transform.GetChild(0).GetComponent<Text>().text = file.Substring(6);
            go_newButton.GetComponent<MainMenuSelectSave>().st_fullName = file;
            var saveFiles = Directory.GetFiles(file + "/");
            StreamReader reader = new StreamReader(saveFiles[0]);
            go_newButton.GetComponent<MainMenuSelectSave>().st_mapName = reader.ReadLine();
            reader.Close();

            in_saveCount++;
        }
    }
}
