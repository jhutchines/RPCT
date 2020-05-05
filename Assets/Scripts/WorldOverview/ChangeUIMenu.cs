using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeUIMenu : MonoBehaviour
{
    public GameManager.MarkerType markerType;
    CameraControls cc_camera;

    // Start is called before the first frame update
    void Start()
    {
        cc_camera = Camera.main.GetComponent<CameraControls>();
    }

    public void ChangeUI()
    {
        cc_camera.markerType = markerType;
    }
}
