using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class AddMarker : MonoBehaviour
{

    CameraControls sc_camera;
    public float fl_imageSize;
    GameManager gameManager;
    GameObject go_newBackground;

    public GameManager.MarkerType markerType;

    // Start is called before the first frame update
    void Start()
    {
        sc_camera = Camera.main.GetComponent<CameraControls>();
        if (fl_imageSize == 0) fl_imageSize = 200;
        gameManager = GameObject.FindWithTag("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (name != GetComponent<Image>().sprite.name) name = GetComponent<Image>().sprite.name;
    }

    public void ClickMarker(bool debugMode)
    {
        if (sc_camera.go_addMarker == null)
        {
            if (markerType == GameManager.MarkerType.Location)
            {
                go_newBackground = Instantiate(sc_camera.go_markerBackground, GameObject.Find("WorldMap").transform);
                go_newBackground.transform.SetSiblingIndex(1);
                GameObject go_newMarker = new GameObject();
                go_newMarker.transform.SetParent(go_newBackground.transform);
                go_newMarker.transform.position = go_newBackground.transform.position;
                go_newMarker.AddComponent<Image>();
                go_newBackground.AddComponent<MarkerData>();
                go_newBackground.GetComponent<MarkerData>().fl_positionZ = -2;
                go_newMarker.GetComponent<RectTransform>().sizeDelta = new Vector2(fl_imageSize, fl_imageSize);
                go_newMarker.GetComponent<Image>().sprite = GetComponent<Image>().sprite;
                go_newMarker.GetComponent<Image>().preserveAspect = true;
                RectTransform rt_newBackground = go_newBackground.GetComponent<RectTransform>();
                rt_newBackground.sizeDelta = new Vector2(go_newMarker.GetComponent<RectTransform>().sizeDelta.x * 2,
                                                                go_newMarker.GetComponent<RectTransform>().sizeDelta.y * 2);
                go_newBackground.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(rt_newBackground.sizeDelta.x - (rt_newBackground.sizeDelta.x * 0.2f),
                                                                                                             rt_newBackground.sizeDelta.y - (rt_newBackground.sizeDelta.y * 0.2f));
            }
            else if (markerType == GameManager.MarkerType.Character)
            {
                go_newBackground = new GameObject();
                go_newBackground.transform.SetParent(GameObject.Find("WorldMap").transform);
                go_newBackground.transform.SetAsLastSibling();
                go_newBackground.AddComponent<Image>();
                go_newBackground.AddComponent<MarkerData>();
                go_newBackground.GetComponent<MarkerData>().fl_positionZ = -3;
                go_newBackground.GetComponent<RectTransform>().sizeDelta = new Vector2(fl_imageSize, fl_imageSize);
                go_newBackground.GetComponent<Image>().sprite = GetComponent<Image>().sprite;
                go_newBackground.GetComponent<Image>().preserveAspect = true;
            }

            go_newBackground.AddComponent<BoxCollider>();
            go_newBackground.GetComponent<BoxCollider>().size = new Vector3(go_newBackground.GetComponent<RectTransform>().sizeDelta.x, 
                                                                            go_newBackground.GetComponent<RectTransform>().sizeDelta.y, 1);
            go_newBackground.name = gameObject.name;
            GameObject go_textBox = Instantiate(sc_camera.go_markerText, go_newBackground.transform);
            go_textBox.GetComponent<RectTransform>().localPosition = new Vector3(0, go_newBackground.GetComponent<RectTransform>().sizeDelta.x -
                                                                               (go_newBackground.GetComponent<RectTransform>().sizeDelta.x / 2) + 100, 0);
            go_newBackground.GetComponent<MarkerData>().go_textbox = go_textBox;
            go_newBackground.GetComponent<MarkerData>().markerType = markerType;
            sc_camera.go_addMarker = go_newBackground;
        }
        else
        {
            Destroy(sc_camera.go_addMarker);
        }
    }
}
