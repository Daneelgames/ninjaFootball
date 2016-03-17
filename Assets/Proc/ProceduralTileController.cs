using UnityEngine;
using System.Collections;
using DevCustom;


public class ProceduralTileController : MonoBehaviour {
      
    [SerializeField]
    private Sprite[] tiles;
    [SerializeField]
    private SpriteRenderer[] tileParts;

    private Vector4 tileCode = Vector4.zero;
    private float tileSize = 1f;

    private ProceduralTileController neighbourUp;
    private ProceduralTileController neighbourRight;
    private ProceduralTileController neighbourDown;
    private ProceduralTileController neighbourLeft;

    void Start()
    {
        GetNeighbours();
    }

    public void GetNeighbours()
    {
        RaycastHit2D hitUp = Physics2D.Raycast(transform.position, Vector2.up, tileSize, 1 << 8);
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position, Vector2.right, tileSize, 1 << 8);
        RaycastHit2D hitDown = Physics2D.Raycast(transform.position, Vector2.down, tileSize, 1 << 8);
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, Vector2.left, tileSize, 1 << 8);


        if (hitUp.collider != null && hitUp.collider.tag == "Ground")
        {
            tileCode.x = 1;
            neighbourUp = hitUp.collider.gameObject.GetComponent<ProceduralTileController>();
        }
        else
            tileCode.x = 0;

        if (hitRight.collider != null && hitRight.collider.tag == "Ground")
        {
            tileCode.y = 1;
            neighbourRight = hitRight.collider.gameObject.GetComponent<ProceduralTileController>();
        }
        else
            tileCode.y = 0;

        if (hitDown.collider != null && hitDown.collider.tag == "Ground")
        {
            tileCode.z = 1;
            neighbourDown = hitDown.collider.gameObject.GetComponent<ProceduralTileController>();
        }
        else
            tileCode.z = 0;

        if (hitLeft.collider != null && hitLeft.collider.tag == "Ground")
        {
            tileCode.w = 1;
            neighbourLeft = hitLeft.collider.gameObject.GetComponent<ProceduralTileController>();
        }
        else
            tileCode.w = 0;

        RebuildTile();
    }

    void RebuildTile()
    {
        if (tileCode.x == 1)
            tileParts[1].sprite = tiles[9];
        else
            tileParts[1].sprite = tiles[1];
        
        if (tileCode.y == 1)
            tileParts[5].sprite = tiles[10];
        else
            tileParts[5].sprite = tiles[5];

        if (tileCode.z == 1)
            tileParts[7].sprite = tiles[9];
        else
            tileParts[7].sprite = tiles[7];

        if (tileCode.w == 1)
            tileParts[3].sprite = tiles[10];
        else
            tileParts[3].sprite = tiles[3];
    }

    void OnDestroy()
    {
        if (neighbourUp != null)
            neighbourUp.GetNeighbours();
        if (neighbourRight != null)
            neighbourRight.GetNeighbours();
        if (neighbourDown != null)
            neighbourDown.GetNeighbours();
        if (neighbourLeft != null)
            neighbourLeft.GetNeighbours();
    }
}
