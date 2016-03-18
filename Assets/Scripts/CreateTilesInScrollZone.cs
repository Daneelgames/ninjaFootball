using UnityEngine;
using System.Collections;

public class CreateTilesInScrollZone : MonoBehaviour {

    [SerializeField]
    private Vector3 position;
    [SerializeField]
    private int sizeX;
    [SerializeField]
    private int sizeY;

    [SerializeField]
    private GameObject tile;

    void Start()
    {
        position = transform.position;
        sizeX = Mathf.RoundToInt(transform.localScale.x);
        sizeY = Mathf.RoundToInt(transform.localScale.y);

        BuildWalls();
    }

    void BuildWalls()
    {
        for (int y = 0; y < sizeY; y++)
        {
            for (int x = 0; x < sizeX; x++)
            {
                Collider2D collider = Physics2D.OverlapArea(new Vector2(), new Vector2());
            }
        }
    }

}
