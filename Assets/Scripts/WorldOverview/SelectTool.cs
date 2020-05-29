using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectTool : MonoBehaviour
{

    public GameManager.ToolSelected toolSelected;
    CameraControls cameraControls;

    // Start is called before the first frame update
    void Start()
    {
        cameraControls = Camera.main.GetComponent<CameraControls>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeSelection()
    {
        if (cameraControls.toolSelected == toolSelected) cameraControls.toolSelected = GameManager.ToolSelected.None;
        else cameraControls.toolSelected = toolSelected;
    }
}
