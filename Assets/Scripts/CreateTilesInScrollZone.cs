using UnityEngine;
using System.Collections;

public class CreateTilesInScrollZone : MonoBehaviour {

    [SerializeField]
    private Vector3 pos;
    [SerializeField]
    private int sizeX;
    [SerializeField]
    private int sizeY;

    [SerializeField]
    private int x = 0;
    [SerializeField]
    private int y = 0;

    [SerializeField]
    private GameObject tile;

    [SerializeField]
    private Vector3 tilePosition;
    [SerializeField]
    private int tilesCount = 0;


    public void BuildWalls()
    {
        pos = gameObject.transform.position;
        sizeX = Mathf.RoundToInt(transform.localScale.x);
        sizeY = Mathf.RoundToInt(transform.localScale.y);

        for (y = 0; y > sizeY * -1f; y--)
        {
            for (x = 0; x < sizeX; x++)
            {
                 tilePosition = new Vector3(pos.x + 0.5f + x, pos.y - 0.5f + y, 0f);
                Collider2D collider = Physics2D.OverlapArea(new Vector2(pos.x + x + .1f, pos.y + y - .1f), new Vector2(pos.x + x + .8f, pos.y + y - .8f), 1 << 19);
                if (collider == null)
                {
                  Instantiate(tile, tilePosition, transform.rotation);
                    tilesCount += 1;
                }
                  //Instantiate(tile, new Vector3(pos.x + 0.5f + x, pos.y - 0.5f + y, 0f), transform.rotation);

                  //newTile.transform.parent = gameObject.transform;
            }
        }
    }
}