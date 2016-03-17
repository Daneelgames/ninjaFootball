using UnityEngine;
using System.Collections;
using DevCustom;


public class ProceduralTileController : MonoBehaviour {

    [SerializeField]
    private Sprite[] tilesTop;
    [SerializeField]
    private Sprite[] tilesBody;
    [SerializeField]
    private SpriteRenderer[] tileParts;

    private Vector2 tileCode;
    private float tileSize = 1f;

    private ProceduralTileController neighbourUp;
    private ProceduralTileController neighbourDown;

    void Start()
    {
        GetNeighbours();
    }

    public void GetNeighbours()
    {
        RaycastHit2D hitUp = Physics2D.Raycast(transform.position, Vector2.up, tileSize, 1 << 8);
        RaycastHit2D hitDown = Physics2D.Raycast(transform.position, Vector2.down, tileSize, 1 << 8);


        if (hitUp.collider != null && hitUp.collider.tag == "Ground")
        {
            tileCode.x = 1;
            neighbourUp = hitUp.collider.gameObject.GetComponent<ProceduralTileController>();
        }
        else
            tileCode.x = 0;
        
        if (hitDown.collider != null && hitDown.collider.tag == "Ground")
        {
            tileCode.y = 1;
            neighbourDown = hitDown.collider.gameObject.GetComponent<ProceduralTileController>();
        }
        else
            tileCode.y = 0;
        

        RebuildTile();
    }

    void RebuildTile()
    {
        tileParts[1].sprite = tilesBody[Random.Range(0, tilesBody.Length)];
        if (tileCode.x == 1)
        {
            tileParts[0].sprite = tilesTop[0];
        }
        else
        {
            tileParts[0].sprite = tilesTop[Random.Range(1, tilesTop.Length)];
        }
    }

    void OnDestroy()
    {
        if (neighbourDown != null)
            neighbourDown.GetNeighbours();
    }
}
