using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TileManager : MonoBehaviour
{
    public enum TileTypes
    {
        Grass,
        Rock,
        Mountain,
        Water,
        Desert
    }

    public enum TileBuildings
    {
        None,
        Road,
        House
    }
    

    [Header("Debug Menu")]
    public int in_tileAmount;
    public int mouseHitX;
    public int mouseHitY;
    TileTypes[,] go_tileListType;
    TileBuildings[,] go_tileListBuilding;

    // Start is called before the first frame update
    void Start()
    {
        go_tileListType = new TileTypes[in_tileAmount,in_tileAmount];
        go_tileListBuilding = new TileBuildings[in_tileAmount, in_tileAmount];
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 50))
        {
            mouseHitX = Mathf.RoundToInt(hit.point.x);
            mouseHitY = Mathf.RoundToInt(hit.point.y);
        }
    }
}
