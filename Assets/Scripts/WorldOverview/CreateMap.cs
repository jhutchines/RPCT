using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMap : MonoBehaviour
{

    GameManager gameManager;
    Renderer r_newMap;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (gameManager.st_worldName != null)
        {
            r_newMap = GetComponent<Renderer>();
            r_newMap.material.mainTextureScale = new Vector2(1, 1);
            var bytes = System.IO.File.ReadAllBytes(gameManager.st_mapName);
            var texture = new Texture2D(1, 1);
            texture.LoadImage(bytes);
            texture.Apply();
            texture.filterMode = FilterMode.Point;
            r_newMap.material.mainTexture = texture;
            r_newMap.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
            r_newMap.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            r_newMap.material.SetInt("_ZWrite", 0);
            r_newMap.material.DisableKeyword("_ALPHATEST_ON");
            r_newMap.material.DisableKeyword("_ALPHABLEND_ON");
            r_newMap.material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
            r_newMap.material.renderQueue = 3000;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
