using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerData : MonoBehaviour
{
    CameraControls sc_camera;
    public GameObject go_textbox;
    public float fl_positionZ;
    public GameManager.MarkerType markerType;

    // Start is called before the first frame update
    void Start()
    {
        sc_camera = Camera.main.GetComponent<CameraControls>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!sc_camera.bl_paused)
        {
            if (sc_camera.go_addMarker == gameObject) FollowCursor();

            if (Input.GetMouseButtonDown(1) && (sc_camera.go_addMarker == null && sc_camera.go_objectHit == gameObject))
            {
                sc_camera.go_addMarker = gameObject;
            }

            if (sc_camera.go_objectHit == gameObject || sc_camera.go_objectSelected == gameObject ||
                sc_camera.go_objectHit == go_textbox.gameObject || sc_camera.go_objectSelected == go_textbox || Input.GetKey(KeyCode.LeftAlt)) go_textbox.SetActive(true);
            else go_textbox.SetActive(false);
        }
    }

    void FollowCursor()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
        {
            transform.position = new Vector3(hit.point.x, hit.point.y, fl_positionZ);
            if (Input.GetMouseButtonUp(0)) WaitToPlace();
            if (Input.GetMouseButtonDown(1))
            {
                sc_camera.go_addMarker = null;
                Destroy(gameObject);
            }
        }
    }

    void WaitToPlace()
    {
        sc_camera.go_addMarker = null;
        if (sc_camera.bl_overUI) Destroy(gameObject);
    }
}
