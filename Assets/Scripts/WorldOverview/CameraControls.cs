using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class CameraControls : MonoBehaviour
{
    [Header("Controls")]
    public float fl_speed;
    public float fl_mouseSpeed;
    private float fl_defaultSpeed;
    public float fl_minZoom;
    public float fl_maxZoom;
    private Camera c_camera;
    public RectTransform rt_image;
    float fl_clampX;
    float fl_clampY;

    [Header("UI")]
    public GameObject go_scrollbar;
    public GameObject go_uiStart;
    public GameObject go_uiEnd;
    Vector3 v3_uiStartPos;
    Vector3 v3_uiEndPos;
    public GameObject go_locationsUI;
    public Text tx_locationsButton;
    public GameObject go_charactersUI;
    public Text tx_charactersButton;
    public GameObject go_toolsUI;
    public Text tx_toolsButton;
    public GameObject go_pauseScreen;
    bool bl_hideUI = true;
    public bool bl_paused;
    public GameManager.ToolSelected toolSelected;

    [Header("Markers")]
    public GameObject go_addMarker;
    public GameObject go_markerBackground;
    public GameObject go_markerText;
    public GameManager.MarkerType markerType;

    [Header("Raycasts")]
    public RaycastHit rc_hit;
    public GameObject go_objectHit;
    public GameObject go_objectSelected;
    public bool bl_overUI;

    GameManager gameManager;
    float fl_heldTime;

    // Start is called before the first frame update
    void Start()
    {
        c_camera = GetComponent<Camera>();
        fl_defaultSpeed = fl_speed;
        fl_clampX = rt_image.sizeDelta.x / 2;
        fl_clampY = rt_image.sizeDelta.y / 2;
        fl_maxZoom = (rt_image.sizeDelta.x - rt_image.sizeDelta.y) * 2;
        if (fl_maxZoom < 0) fl_maxZoom = fl_maxZoom * -1;
        fl_minZoom = fl_maxZoom / 10;
        v3_uiStartPos = go_uiStart.transform.position;
        v3_uiEndPos = go_uiEnd.transform.position;
        go_scrollbar.transform.position = v3_uiEndPos;
        go_objectSelected = gameObject;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (go_objectSelected == null) go_objectSelected = gameObject;
        v3_uiStartPos = go_uiStart.transform.position;
        v3_uiEndPos = go_uiEnd.transform.position;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            fl_speed = fl_defaultSpeed * 2;
        }
        else
        {
            fl_speed = fl_defaultSpeed;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (bl_paused) bl_paused = false;
            else bl_paused = true;
        }

        fl_defaultSpeed = c_camera.orthographicSize * 2;
        fl_mouseSpeed = fl_defaultSpeed * 4;

        if (go_objectSelected.GetComponent<Text>() == null && !bl_paused)
        {
            MoveCamera();

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (bl_hideUI) bl_hideUI = false;
                else bl_hideUI = true;
            }
        }
        HideUI();
        ChangeUI();
        if (!bl_paused)
        {
            Time.timeScale = 1;
            MouseLocation();
            bl_overUI = IsPointerOverUIElement();
        }
        else
        {
            go_objectHit = gameObject;
            go_objectSelected = gameObject;
            Time.timeScale = 0;
        }

    }

    void MoveCamera()
    {
        transform.position += new Vector3(Input.GetAxis("Horizontal") * fl_speed * Time.deltaTime,
                                          Input.GetAxis("Vertical") * fl_speed * Time.deltaTime,
                                          0);

        if (Input.GetMouseButton(2))
        {
            fl_heldTime += Time.deltaTime;
        }

        if (Input.GetMouseButtonUp(2))
        {
            if (fl_heldTime <= 0.4f)
            {
                GetComponent<MeasureDistance>().ChangeOffset();
            }
            fl_heldTime = 0;
        }

        if (fl_heldTime >= 0.4f)
        {
            transform.position -= new Vector3(Input.GetAxis("Mouse X") * fl_mouseSpeed * Time.deltaTime,
                                              Input.GetAxis("Mouse Y") * fl_mouseSpeed * Time.deltaTime,
                                              0);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -fl_clampX, fl_clampX), Mathf.Clamp(transform.position.y, -fl_clampY, fl_clampY), -10);
        c_camera.orthographicSize -= Input.GetAxis("Mouse ScrollWheel") * (fl_speed * 40) * Time.deltaTime;
        c_camera.orthographicSize = Mathf.Clamp(c_camera.orthographicSize, fl_minZoom, fl_maxZoom);
    }

    void HideUI()
    {
        if (bl_hideUI)
        {
            go_scrollbar.transform.position = Vector3.MoveTowards(go_scrollbar.transform.position, v3_uiEndPos, 700f * Time.deltaTime);
        }
        else
        {
            go_scrollbar.transform.position = Vector3.MoveTowards(go_scrollbar.transform.position, v3_uiStartPos, 700f * Time.deltaTime);
        }

        if (bl_paused) go_pauseScreen.SetActive(true);
        else go_pauseScreen.SetActive(false);
    }

    void ChangeUI()
    {
        switch (markerType)
        {
            case GameManager.MarkerType.Location:
                {
                    go_locationsUI.SetActive(true);
                    go_charactersUI.SetActive(false);
                    go_toolsUI.SetActive(false);
                    tx_locationsButton.fontStyle = FontStyle.Bold;
                    tx_charactersButton.fontStyle = FontStyle.Normal;
                    tx_toolsButton.fontStyle = FontStyle.Normal;
                }
                break;

            case GameManager.MarkerType.Character:
                {
                    go_charactersUI.SetActive(true);
                    go_locationsUI.SetActive(false);
                    go_toolsUI.SetActive(false);
                    tx_charactersButton.fontStyle = FontStyle.Bold;
                    tx_locationsButton.fontStyle = FontStyle.Normal;
                    tx_toolsButton.fontStyle = FontStyle.Normal;
                }
                break;

            case GameManager.MarkerType.Tools:
                {
                    go_toolsUI.SetActive(true);
                    go_locationsUI.SetActive(false);
                    go_charactersUI.SetActive(false);
                    tx_toolsButton.fontStyle = FontStyle.Bold;
                    tx_locationsButton.fontStyle = FontStyle.Normal;
                    tx_charactersButton.fontStyle = FontStyle.Normal;
                }
                break;

            case GameManager.MarkerType.Both:
                {
                    go_locationsUI.SetActive(true);
                    go_charactersUI.SetActive(true);
                }
                break;
        }
    }

    void MouseLocation()
    {
        if (go_addMarker != null) go_objectHit = go_addMarker;
        else
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out rc_hit, Mathf.Infinity))
            {
                go_objectHit = rc_hit.transform.gameObject;
            }
        }

        if (Input.GetMouseButtonDown(0)) go_objectSelected = go_objectHit;
    }

    public static bool IsPointerOverUIElement()
    {
        var eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        return results.Count > 0;
    }

    public void ReturnToMenu()
    {
        gameManager.st_worldName = "";
        gameManager.st_mapName = "";
        gameManager.st_saveLocation = "";
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
}