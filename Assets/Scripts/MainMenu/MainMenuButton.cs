using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuButton : MonoBehaviour
{
    public MainMenu.ButtonType buttonType;
    public Vector3 v3_moveTowards;
    public Vector3 v3_moveOffscreen;
    public GameObject go_window;

    MainMenu mm_canvas;
    Vector3 v3_startPos;
    Text tx_button;

    ColorBlock startColor;
    ColorBlock newColor;

    // Start is called before the first frame update
    void Start()
    {
        mm_canvas = transform.parent.GetComponent<MainMenu>();
        tx_button = GetComponent<Text>();
        v3_startPos = transform.localPosition;

        if (v3_moveOffscreen.x == 0) v3_moveOffscreen.x = transform.localPosition.x;
        if (v3_moveOffscreen.y == 0) v3_moveOffscreen.y = transform.localPosition.y;

        if (v3_moveTowards.x == 0) v3_moveTowards.x = transform.localPosition.x;
        if (v3_moveTowards.y == 0) v3_moveTowards.y = transform.localPosition.y;

        if (go_window == null && transform.childCount > 0) go_window = transform.GetChild(0).gameObject;

        startColor = GetComponent<Button>().colors;
        newColor = startColor;
        newColor.normalColor = startColor.highlightedColor;
    }

    // Update is called once per frame
    void Update()
    {
        if (buttonType == mm_canvas.buttonType)
        {
            transform.localPosition = Vector2.MoveTowards(transform.localPosition, v3_moveTowards, 130f * Time.deltaTime);
            GetComponent<Button>().colors = newColor;
        }
        else if (mm_canvas.buttonType == MainMenu.ButtonType.None)
        {
            transform.localPosition = Vector2.MoveTowards(transform.localPosition, v3_startPos, 2000f * Time.deltaTime);
            GetComponent<Button>().colors = startColor;
        }
        else
        {
            transform.localPosition = Vector2.MoveTowards(transform.localPosition, v3_moveOffscreen, 850f * Time.deltaTime);
        }

        if (go_window != null)
        {
            if (transform.localPosition == v3_moveTowards) go_window.SetActive(true);
            else go_window.SetActive(false);
        }

        if (transform.localPosition == v3_moveOffscreen) tx_button.enabled = false;
        else tx_button.enabled = true;
    }
}
