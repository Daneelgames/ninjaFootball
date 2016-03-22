using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomBuilder : MonoBehaviour {

    public enum RoomType { Enter, Exit, Default, };

    public RoomType _roomType = RoomType.Default;

    [SerializeField]
    private int neighbourRoomCount = 0;

    [SerializeField]
    private GameObject enemySpawner;
    [SerializeField]
    private GameObject chest;
    [SerializeField]
    private GameObject wallTrap;
    [SerializeField]
    private GameObject circularSaw;

    [SerializeField]
    private GameObject[] upPath;
    [SerializeField]
    private GameObject[] upWall;
    [SerializeField]
    private GameObject[] rightPath;
    [SerializeField]
    private GameObject[] rightWall;
    [SerializeField]
    private GameObject[] downPath;
    [SerializeField]
    private GameObject[] downWall;
    [SerializeField]
    private GameObject[] leftPath;
    [SerializeField]
    private GameObject[] leftWall;


    private float roomWidth = 11f;
    private float roomHeigth = 6f;

    private PathDiggerLogic digger;

    private bool pathUp = false;
    private bool pathRight = false;
    private bool pathDown = false;
    private bool pathLeft = false;

    ProceduralTileController[] tiles;

    void Start () {
        digger = GameObject.Find("PathDigger").GetComponent<PathDiggerLogic>();
        Cast();
    }

    void Cast()
    {
            RaycastHit2D hitUp = Physics2D.Raycast(transform.position, Vector2.up, roomHeigth, 1 << 19);
            RaycastHit2D hitRight = Physics2D.Raycast(transform.position, Vector2.right, roomWidth, 1 << 19);
            RaycastHit2D hitDown = Physics2D.Raycast(transform.position, Vector2.down, roomHeigth, 1 << 19);
            RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, Vector2.left, roomWidth, 1 << 19);
            
            // up
            if (hitUp.collider != null && hitUp.collider.tag == "RoomBuilder")
            {
                pathUp = true;
                neighbourRoomCount += 1;
            }
            
            //right 
            if (hitRight.collider != null && hitRight.collider.tag == "RoomBuilder")
            {
                pathRight = true;
                neighbourRoomCount += 1;
            }
            
           //down
            if (hitDown.collider != null && hitDown.collider.tag == "RoomBuilder")
            {
                pathDown = true;
                neighbourRoomCount += 1;
            }
            
            //left
            if (hitLeft.collider != null && hitLeft.collider.tag == "RoomBuilder")
            {
                pathLeft = true;
                neighbourRoomCount += 1;
            }

        BuildRoom();
    }

    void BuildRoom()
    {
        //up
        if (pathUp)
        {
            GameObject template = Instantiate(upPath[Random.Range(0,upPath.Length)], transform.position, transform.rotation) as GameObject;
            template.transform.parent = gameObject.transform;
        }
        else
        {
            GameObject template = Instantiate(upWall[Random.Range(0, upWall.Length)], transform.position, transform.rotation) as GameObject;
            template.transform.parent = gameObject.transform;
        }

        //right
        if (pathRight)
        {
            GameObject template = Instantiate(rightPath[Random.Range(0, rightPath.Length)], transform.position, transform.rotation) as GameObject;
            template.transform.parent = gameObject.transform;
        }
        else
        {
            GameObject template = Instantiate(rightWall[Random.Range(0, rightWall.Length)], transform.position, transform.rotation) as GameObject;
            template.transform.parent = gameObject.transform;
        }

        //down
        if (pathDown)
        {
            GameObject template = Instantiate(downPath[Random.Range(0, downPath.Length)], transform.position, transform.rotation) as GameObject;
            template.transform.parent = gameObject.transform;
        }
        else
        {
            GameObject template = Instantiate(downWall[Random.Range(0, downWall.Length)], transform.position, transform.rotation) as GameObject;
            template.transform.parent = gameObject.transform;
        }

        //left
        if (pathLeft)
        {
            GameObject template = Instantiate(leftPath[Random.Range(0, leftPath.Length)], transform.position, transform.rotation) as GameObject;
            template.transform.parent = gameObject.transform;
        }
        else
        {
            GameObject template = Instantiate(leftWall[Random.Range(0, leftWall.Length)], transform.position, transform.rotation) as GameObject;
            template.transform.parent = gameObject.transform;
        }

        BuildContent();
        digger.roomBuilded += 1;
    }

    void BuildContent()
    {
        //get all tiles
        tiles = gameObject.GetComponentsInChildren<ProceduralTileController>();

        //Enter room
        if (_roomType == RoomType.Enter)
        {
            FindEmptyTile();
            GameObject player = GameObject.Find("Player") as GameObject;
            List<Vector3> emptyTiles = FindEmptyTile();
            int randomTile = Random.Range(0, emptyTiles.Count);
            player.transform.position = new Vector3(emptyTiles[randomTile].x, emptyTiles[randomTile].y + 0.5f);
            emptyTiles.RemoveAt(randomTile);
            
            //CreateChest
            int chestWeaponTile = Random.Range(0, emptyTiles.Count);
            float chestWeaponChance = Random.Range(0f, 1f);
            if (chestWeaponChance > 0.5)
            {
                Instantiate(chest, new Vector3(emptyTiles[chestWeaponTile].x, emptyTiles[chestWeaponTile].y + 0.5f), transform.rotation);
                emptyTiles.RemoveAt(chestWeaponTile);
            }
            //saw
            for (int i = 0; i < emptyTiles.Count; i++)
            {
                int chance = Random.Range(1, 20);
                if (chance == 1)
                {
                    Instantiate(circularSaw, new Vector3(emptyTiles[i].x, emptyTiles[i].y + 0.5f), transform.rotation);
                    emptyTiles.RemoveAt(i);
                }
            }
        }

        //Exit room
        if (_roomType == RoomType.Exit)
        {
            FindEmptyTile();
            GameObject exit = GameObject.Find("Exit") as GameObject;
            List<Vector3> emptyTiles = FindEmptyTile();
            int randomTile = Random.Range(0, emptyTiles.Count);
            exit.transform.position = new Vector3(emptyTiles[randomTile].x, emptyTiles[randomTile].y + 0.5f);
            //enemy
            emptyTiles.RemoveAt(randomTile);
            for (int i = 0; i < emptyTiles.Count; i ++)
            {
                int chance = Random.Range(1, 20);
                if (chance == 1)
                {
                    Instantiate(enemySpawner, new Vector3(emptyTiles[i].x, emptyTiles[i].y + 0.5f), transform.rotation);
                    emptyTiles.RemoveAt(i);
                }
            }
            //saw
            for (int i = 0; i < emptyTiles.Count; i++)
            {
                int chance = Random.Range(1, 30);
                if (chance == 1)
                {
                    Instantiate(circularSaw, new Vector3(emptyTiles[i].x, emptyTiles[i].y + 1f), transform.rotation);
                    emptyTiles.RemoveAt(i);
                }
            }
        }

        // default
        if ( _roomType == RoomType.Default)
        {
            FindEmptyTile();
            FindEmptyTileWithOffset();
            List<Vector3> emptyTiles = FindEmptyTile();
            List<Vector3> emptyWallTiles = FindEmptyTileWithOffset();

            //CreateChest
            int chestTile = Random.Range(0, emptyTiles.Count);
            int chance = Random.Range(1, 20);
            if (chance == 1)
            {
                Instantiate(chest, new Vector3(emptyTiles[chestTile].x, emptyTiles[chestTile].y + 0.5f), transform.rotation);
                emptyTiles.RemoveAt(chestTile);
            }

            //create enemies
            for (int i = 0; i < emptyTiles.Count; i++)
            {
                int enemyChance = Random.Range(1, 10);
                if (enemyChance == 1)
                {
                    Instantiate(enemySpawner, new Vector3(emptyTiles[i].x, emptyTiles[i].y + 0.5f), transform.rotation);
                }
            }

            //saw
            for (int i = 0; i < emptyTiles.Count; i++)
            {
                int sawChance = Random.Range(1, 20);
                if (sawChance == 1)
                {
                    Instantiate(circularSaw, new Vector3(emptyTiles[i].x, emptyTiles[i].y + 0.5f), transform.rotation);
                    emptyTiles.RemoveAt(i);
                }
            }

            //create arrow trap
            int arrowTrapTile = Random.Range(0, emptyWallTiles.Count);
            int arrowChance = Random.Range(1, 5);
            if (arrowChance == 1)
            {
                Instantiate(wallTrap, new Vector3(emptyWallTiles[arrowTrapTile].x, emptyWallTiles[arrowTrapTile].y), transform.rotation);
                emptyWallTiles.RemoveAt(arrowTrapTile);
            }
        }
    }

    List<Vector3> FindEmptyTile()
    {
        List<Vector3> emptyTiles = new List<Vector3>();
        for (int i = 0; i < tiles.Length; i++)
        {
            if (tiles[i].tileCode.x == 0 && tiles[i].gameObject.transform.position.y < transform.position.y - 1 && tiles[i].gameObject.transform.position.y > transform.position.y - roomHeigth + 1)
            {
                RaycastHit2D hitUp = Physics2D.Raycast(tiles[i].gameObject.transform.position, Vector2.up, 2, 1 << 8);
                if (hitUp == false)
                    emptyTiles.Add(tiles[i].gameObject.transform.position);
            }
        }
        return emptyTiles;
    }

    List<Vector3> FindEmptyTileWithOffset()
    {
        float checkEmptyonRight = Random.Range(0f, 1f);

        List<Vector3> emptyWallTiles = new List<Vector3>();
        for (int i = 0; i < tiles.Length; i++)
        {
            if (tiles[i].tileCode.x == 0 && tiles[i].gameObject.transform.position.y < transform.position.y && tiles[i].gameObject.transform.position.y > transform.position.y - roomHeigth)
            {
                if(checkEmptyonRight > 0.5f)
                {
                    RaycastHit2D hitRight = Physics2D.Raycast(new Vector3(tiles[i].gameObject.transform.position.x, tiles[i].gameObject.transform.position.y + 1f, tiles[i].gameObject.transform.position.z), Vector2.right, 5, 1 << 8);
                    if (!hitRight)
                        emptyWallTiles.Add(new Vector3(tiles[i].gameObject.transform.position.x, tiles[i].gameObject.transform.position.y + 1f, tiles[i].gameObject.transform.position.z));
                }
                else
                {
                    RaycastHit2D hitLeft = Physics2D.Raycast(new Vector3(tiles[i].gameObject.transform.position.x, tiles[i].gameObject.transform.position.y + 1f, tiles[i].gameObject.transform.position.z), Vector2.left, 5, 1 << 8);
                    if (!hitLeft)
                        emptyWallTiles.Add(new Vector3(tiles[i].gameObject.transform.position.x, tiles[i].gameObject.transform.position.y + 1f, tiles[i].gameObject.transform.position.z));
                }
            }
        }
        return emptyWallTiles;
    }
}