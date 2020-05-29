using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeasureData : MonoBehaviour
{

    public int in_childPosition;
    public float fl_lineLength;
    public float fl_timeTaken;
    public float fl_partySpeed;
    public float fl_rotationCheck;
    public Vector3 pos1;
    public Vector3 pos2;
    public Vector3 pairedPos;
    public Vector3 go_lookAt;
    public Vector3 v3NewLookAt;
    CameraControls cc_camera;
    GameObject go_textBox;

    // Start is called before the first frame update
    void Start()
    {
        cc_camera = Camera.main.GetComponent<CameraControls>();
        go_textBox = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (cc_camera.go_addMarker == gameObject)
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
            {
                transform.position = new Vector3(hit.point.x, hit.point.y, -4);

                if (!cc_camera.bl_overUI && Input.GetMouseButtonDown(0))
                {
                    cc_camera.go_addMarker = null;
                }

                if (transform.parent.childCount > 1 && Input.GetMouseButtonDown(1))
                {
                    cc_camera.go_addMarker = transform.parent.GetChild(transform.parent.childCount - 2).gameObject;
                    Destroy(gameObject);
                }
            }
        }

        if (in_childPosition != 0)
        {
            go_textBox.SetActive(true);

            go_textBox.transform.position = ((transform.position + pairedPos) / 2) + go_textBox.transform.up;
            Quaternion v3LookAt = Quaternion.FromToRotation(Vector3.left, pairedPos - transform.position);
            fl_rotationCheck = v3LookAt.eulerAngles.z;
            if (fl_rotationCheck < 270 && fl_rotationCheck > 90) v3NewLookAt = Vector3.right;
            else v3NewLookAt = Vector3.left;
            go_textBox.transform.rotation = Quaternion.FromToRotation(v3NewLookAt, pairedPos - transform.position);

            fl_lineLength = Vector3.Distance(pos1, pos2);
            fl_timeTaken = fl_lineLength / fl_partySpeed;
            go_textBox.GetComponent<Text>().text = fl_lineLength.ToString("F2") + "mi" + "\n" + fl_timeTaken.ToString("F2") + " days";
        }
        else go_textBox.SetActive(false);
    }
}
