using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenTool : MonoBehaviour
{
    CameraControls sc_camera;
    public GameObject go_measurements;

    // Start is called before the first frame update
    void Start()
    {
        sc_camera = Camera.main.GetComponent<CameraControls>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (sc_camera.toolSelected)
        {
            case GameManager.ToolSelected.None:
                {
                    go_measurements.SetActive(false);
                }
                break;

            case GameManager.ToolSelected.Measurement:
                {
                    go_measurements.SetActive(true);
                }
                break;
        }
    }
}
