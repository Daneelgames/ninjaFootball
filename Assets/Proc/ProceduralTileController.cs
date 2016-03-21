using UnityEngine;
using System.Collections;


public class ProceduralTileController : MonoBehaviour {

    public Vector2 tileCode;

    [SerializeField]
    private float stayChance = 1f;
    [SerializeField]
    private float chance = 0f;

    [SerializeField]
    private Sprite[] tilesTop;
    [SerializeField]
    private Sprite[] tilesBody;

    [SerializeField]
    private GameObject particles;

    [SerializeField]
    private SpriteRenderer[] tileParts;

    private float tileSize = 1f;

    private ProceduralTileController neighbourDown;

    private int health = 10;

    void Start()
    {
        CheckIfStay();
        tileParts[0] = transform.GetChild(0).GetComponent<SpriteRenderer>();
        tileParts[1] = transform.GetChild(1).GetComponent<SpriteRenderer>();
        GetNeighbours();
        InvokeRepeating("CheckNearTiles", .1f, .1f);
        Invoke("StopChecking", 0.5f);
    }

    void CheckNearTiles()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, 0.1f, 1 << 8);
        RaycastHit2D hitUp = Physics2D.Raycast(transform.position, Vector2.up, tileSize, 1 << 8);
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position, Vector2.right, tileSize, 1 << 8);
        RaycastHit2D hitDown = Physics2D.Raycast(transform.position, Vector2.down, tileSize, 1 << 8);
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, Vector2.left, tileSize, 1 << 8);

        if (!hitUp && !hitRight && !hitDown && !hitLeft)
            Destroy(gameObject);
        else if (hit)
            Destroy(gameObject);
    }
    void StopChecking()
    {
        CancelInvoke("CheckNearTiles");
    }

    void CheckIfStay()
    {
        chance = Random.Range(0f, 1f);

        if (chance > stayChance)
            Destroy(gameObject);
    }

    public void Damage(int damage)
    {
        health -= damage;
        CheckHealth();
    }

    void CheckHealth()
    {
        if (health <= 0)
            DestroyTile();
    }

    public void GetNeighbours()
    {
        RaycastHit2D hitUp = Physics2D.Raycast(transform.position, Vector2.up, tileSize, 1 << 8);
        RaycastHit2D hitDown = Physics2D.Raycast(transform.position, Vector2.down, tileSize, 1 << 8);
        
        if (hitUp)
        {
            tileCode.x = 1;
        }
        else
            tileCode.x = 0;
        
        if (hitDown)
        {
            tileCode.y = 1;
            neighbourDown = hitDown.collider.gameObject.GetComponent<ProceduralTileController>();

            if (neighbourDown != null)
                neighbourDown.GetNeighbours();
        }
        else
            tileCode.y = 0;
        
        RebuildTile();
    }

    void RebuildTile()
    {
        tileParts[1].sprite = tilesBody[Random.Range(0, tilesBody.Length)];
        tileParts[0].sortingOrder = 6;
        tileParts[1].sortingOrder = Random.Range(2, 5);
        if (tileCode.x == 1)
        {
            tileParts[0].sprite = tilesTop[0];
        }
        else
        {
            tileParts[0].sprite = tilesTop[Random.Range(1, tilesTop.Length)];
        }
    }

    void DestroyTile()
    {
        Instantiate(particles, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}