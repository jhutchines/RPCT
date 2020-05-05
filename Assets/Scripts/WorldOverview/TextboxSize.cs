using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextboxSize : MonoBehaviour
{
    Text textbox;
    InputField newText;
    BoxCollider textCollider;
    public string st_text;

    CameraControls cc_camera;

    // Start is called before the first frame update
    void Start()
    {
        textbox = GetComponent<Text>();
        newText = GetComponent<InputField>();
        textCollider = GetComponent<BoxCollider>();
        cc_camera = Camera.main.GetComponent<CameraControls>();
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<RectTransform>().sizeDelta = new Vector2((newText.text.Length + 1) * 100, 200);
        st_text = textbox.text;
        if (cc_camera.go_objectSelected != gameObject && newText.text.Length == 0) newText.text = "Unknown";
        textCollider.size = new Vector3(GetComponent<RectTransform>().sizeDelta.x, GetComponent<RectTransform>().sizeDelta.y, 1);
    }
}
