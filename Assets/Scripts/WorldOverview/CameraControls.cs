using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    public float fl_speed;
    private float fl_defaultSpeed;
    public float fl_minZoom;
    public float fl_maxZoom;
    private Camera c_camera;

    // Start is called before the first frame update
    void Start()
    {
        c_camera = GetComponent<Camera>();
        fl_defaultSpeed = fl_speed;
    }

    // Update is called once per frame
    void Update()
    {
        MoveCamera();
        if (Input.GetKey(KeyCode.LeftShift))
        {
            fl_speed = fl_defaultSpeed * 2;
        }
        else
        {
            fl_speed = fl_defaultSpeed;
        }

        fl_defaultSpeed = c_camera.orthographicSize * 2;
    }

    void MoveCamera()
    {
        transform.position += new Vector3(Input.GetAxis("Horizontal") * fl_speed * Time.deltaTime, 
                                          Input.GetAxis("Vertical") * fl_speed * Time.deltaTime, 
                                          0);
        c_camera.orthographicSize -= Input.GetAxis("Mouse ScrollWheel") * (fl_speed * 80) * Time.deltaTime;
        c_camera.orthographicSize = Mathf.Clamp(c_camera.orthographicSize, fl_minZoom, fl_maxZoom);
    }
}
