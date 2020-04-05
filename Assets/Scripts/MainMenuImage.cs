using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuImage : MonoBehaviour
{

    public Color startColour;
    public Color changeColour;
    public float fl_delay;
    Color newColour;
    Color newStartColour;

    // Start is called before the first frame update
    void Start()
    {
        newColour = GetComponent<Image>().color;
        newStartColour = newColour;
        startColour = changeColour;
        StartCoroutine(WaitForFade(fl_delay));
    }

    // Update is called once per frame
    void Update()
    {
        ChangeColour();
    }

    void ChangeColour()
    {
        newColour = Color.Lerp(startColour, changeColour, Mathf.PingPong(Time.time, fl_delay));
        GetComponent<Image>().color = newColour;
    }

    IEnumerator WaitForFade(float time)
    {
        yield return new WaitForSeconds(time);
        startColour = newStartColour;
    }
}
