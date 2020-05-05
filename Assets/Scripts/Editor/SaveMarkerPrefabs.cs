using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveMarkerPrefabs : MonoBehaviour
{

    GameManager gameManager;
    CameraControls cc_camera;

    public GameObject go_locationsMarkers;
    public GameObject go_characterMarkers;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        cc_camera = Camera.main.GetComponent<CameraControls>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.bl_debugMode && (Input.GetKey(KeyCode.LeftShift)) && Input.GetKeyDown(KeyCode.D)) RunDebug();
    }

    void RunDebug()
    {
        cc_camera.markerType = GameManager.MarkerType.Both;

        foreach (var file in System.IO.Directory.GetFiles("Assets/Prefabs/Markers/"))
        {
            System.IO.File.Delete(file);
        }


        for (int i = 0; i < go_locationsMarkers.transform.childCount; i++)
        {
            go_locationsMarkers.transform.GetChild(i).GetComponent<AddMarker>().ClickMarker(true);
        }

        for (int i = 0; i < go_characterMarkers.transform.childCount; i++)
        {
            go_characterMarkers.transform.GetChild(i).GetComponent<AddMarker>().ClickMarker(true);
        }

        cc_camera.markerType = GameManager.MarkerType.Location;
    }
}
