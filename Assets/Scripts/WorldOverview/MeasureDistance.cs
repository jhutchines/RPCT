using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeasureDistance : MonoBehaviour
{

    public GameObject go_measure;
    public GameObject go_measurementObject;
    public GameObject go_image;
    public Text tx_distance;

    int in_offset = 1;
    float fl_totalDistance;
    float fl_daysTravel;
    CameraControls cc_camera;
    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        cc_camera = Camera.main.GetComponent<CameraControls>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (cc_camera.toolSelected == GameManager.ToolSelected.Measurement) Measurements();
        else
        {
            if (go_measurementObject.transform.childCount > 0)
            {
                for (int i = 0; i < go_measurementObject.transform.childCount; i++)
                {
                    Destroy(go_measurementObject.transform.GetChild(i).gameObject);
                }
            }
            go_measurementObject.GetComponent<LineRenderer>().positionCount = 0;
        }
    }

    void Measurements()
    {
        if (cc_camera.go_addMarker == null)
        {
            GameObject newSpawn = Instantiate(go_measure, cc_camera.rc_hit.point, transform.rotation);
            newSpawn.transform.SetParent(go_measurementObject.transform);
            cc_camera.go_addMarker = newSpawn;
        }

        else if (cc_camera.go_addMarker.GetComponent<MeasureData>() == null)
        {
            if (cc_camera.go_addMarker != null) Destroy(cc_camera.go_addMarker);
            GameObject newSpawn = Instantiate(go_measure, cc_camera.rc_hit.point, transform.rotation);
            newSpawn.transform.SetParent(go_measurementObject.transform);
            cc_camera.go_addMarker = newSpawn;
        }

        go_measurementObject.GetComponent<LineRenderer>().positionCount = go_measurementObject.transform.childCount - in_offset;
        for (int i = 0; i < go_measurementObject.transform.childCount - in_offset; i++)
        {
            go_measurementObject.GetComponent<LineRenderer>().SetPosition(i, go_measurementObject.transform.GetChild(i).transform.position);
        }
        
        UpdateDistance();
        
    }

    public void ChangeOffset()
    {
        if (cc_camera.toolSelected == GameManager.ToolSelected.Measurement)
        {
            if (in_offset == 0) in_offset = 1;
            else in_offset = 0;
        }
    }

    void UpdateDistance()
    {
        fl_totalDistance = 0;
        for (int i = 1; i < go_measurementObject.transform.childCount - in_offset; i++)
        {
            go_measurementObject.transform.GetChild(i).GetComponent<MeasureData>().in_childPosition = i;

            float dividerX = go_image.GetComponent<RectTransform>().sizeDelta.x / gameManager.fl_worldSizeX;
            float dividerY = go_image.GetComponent<RectTransform>().sizeDelta.y / gameManager.fl_worldSizeY;
            Vector3 pos1 = new Vector3(go_measurementObject.transform.GetChild(i - 1).transform.position.x / dividerX, 
                                       go_measurementObject.transform.GetChild(i - 1).transform.position.y / dividerY,
                                       go_measurementObject.transform.GetChild(i - 1).transform.position.z);
            Vector3 pos2 = new Vector3(go_measurementObject.transform.GetChild(i).transform.position.x / dividerX, 
                                       go_measurementObject.transform.GetChild(i).transform.position.y / dividerY, 
                                       go_measurementObject.transform.GetChild(i).transform.position.z);
            fl_totalDistance += Vector3.Distance(pos1, pos2);

            go_measurementObject.transform.GetChild(i).GetComponent<MeasureData>().pos1 = pos1;
            go_measurementObject.transform.GetChild(i).GetComponent<MeasureData>().pos2 = pos2;
            go_measurementObject.transform.GetChild(i).GetComponent<MeasureData>().pairedPos = go_measurementObject.transform.GetChild(i - 1).transform.position;
            go_measurementObject.transform.GetChild(i).GetComponent<MeasureData>().fl_partySpeed = gameManager.fl_partySpeed;
        }

        if (go_measurementObject.transform.childCount > 0 && in_offset == 1)
            go_measurementObject.transform.GetChild(go_measurementObject.transform.childCount - 1).GetComponent<MeasureData>().in_childPosition = 0;

        fl_daysTravel = fl_totalDistance / gameManager.fl_partySpeed;
        tx_distance.text = fl_totalDistance.ToString("F2") + "mi" + "\n" + fl_daysTravel.ToString("F2") + " days";
    }
}
