using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuFindMaps : MonoBehaviour
{
    public bool bl_mapsLoaded;
    public GameObject go_button;
    public GameObject go_content;
    public string st_filePath;
    int in_mapCount;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (bl_mapsLoaded) in_mapCount = 0;
        else
        {
            UpdateMaps();
        }
    }

    void UpdateMaps()
    {
        bl_mapsLoaded = true;
        foreach (string file in System.IO.Directory.GetFiles(st_filePath))
        {
            if (file.Substring(file.Length-3) == "png" || file.Substring(file.Length-3) == "jpg")
            {
                GameObject go_newButton = Instantiate(go_button, go_content.transform);
                go_newButton.transform.localPosition = new Vector2(go_newButton.transform.localPosition.x, go_newButton.transform.localPosition.y - (20 * in_mapCount));
                go_newButton.transform.GetChild(0).GetComponent<Text>().text = file.Substring(5).Remove(file.Substring(5).Length-4);
                go_newButton.GetComponent<MainMenuSelectMap>().st_fullName = file;

                in_mapCount++;
            }
        }
    }
}
