using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateWorldSize : MonoBehaviour
{
    bool firstUpdate = true;

    public enum MeasurementType
    {
        Width,
        Height,
        Speed
    }
    public MeasurementType measurementType;

    GameManager gameManager;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (firstUpdate)
        {
            switch (measurementType)
            {
                case MeasurementType.Width:
                    {
                        GetComponent<InputField>().text = gameManager.fl_worldSizeX.ToString();
                    }
                    break;

                case MeasurementType.Height:
                    {
                        GetComponent<InputField>().text = gameManager.fl_worldSizeY.ToString();
                    }
                    break;

                case MeasurementType.Speed:
                    {
                        GetComponent<InputField>().text = gameManager.fl_partySpeed.ToString();
                    }
                    break;
            }
            firstUpdate = false;
        }
    }

    public void UpdateMeasurements()
    {
        switch (measurementType)
        {
            case MeasurementType.Width:
                {
                    gameManager.fl_worldSizeX = float.Parse(GetComponent<InputField>().text);
                }
                break;

            case MeasurementType.Height:
                {
                    gameManager.fl_worldSizeY = float.Parse(GetComponent<InputField>().text);
                }
                break;

            case MeasurementType.Speed:
                {
                    gameManager.fl_partySpeed = float.Parse(GetComponent<InputField>().text);
                }
                break;
        }
    }
}
