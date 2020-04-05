using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    
    public enum ButtonType
    {
        None,
        New,
        Load,
        Exit
    }
    public ButtonType buttonType;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewWorld()
    {
        if (buttonType == ButtonType.None) buttonType = ButtonType.New;
        else buttonType = ButtonType.None;
    }

    public void LoadWorld()
    {
        if (buttonType == ButtonType.None) buttonType = ButtonType.Load;
        else buttonType = ButtonType.None;
    }

    public void ExitGame()
    {

    }

}
