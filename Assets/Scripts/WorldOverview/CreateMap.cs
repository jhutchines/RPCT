using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateMap : MonoBehaviour
{

    GameManager gameManager;
    Image i_newMap;
    GameObject go_background;

    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.Find("GameManager") != null)
        {
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            if (gameManager.st_worldName != null)
            {
                i_newMap = GetComponent<Image>();
                i_newMap.material.mainTextureScale = new Vector2(1, 1);
                var bytes = System.IO.File.ReadAllBytes(gameManager.st_mapName);
                var texture = new Texture2D(1, 1);
                texture.LoadImage(bytes);
                //texture.Resize(texture.width, texture.height);
                texture.filterMode = FilterMode.Trilinear;
                i_newMap.material.mainTexture = texture;
            }
        }
        go_background = GameObject.Find("Collider");
        go_background.transform.position = transform.position;
        go_background.transform.localScale = new Vector3(GetComponent<RectTransform>().sizeDelta.x, GetComponent<RectTransform>().sizeDelta.y, 1);
        go_background.AddComponent<BoxCollider>();
        Debug.Log("Finished Map");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
