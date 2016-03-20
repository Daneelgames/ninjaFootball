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
        //Enter room
        if (_roomType == RoomType.Enter)
        {
            FindEmptyTile();
            GameObject player = GameObject.Find("Player") as GameObject;
            List<Vector3> emptyTiles = FindEmptyTile();
            int randomTile = Random.Range(0, emptyTiles.Count);
            player.transform.position = new Vector3(emptyTiles[randomTile].x, emptyTiles[randomTile].y + 0.5f);
        }

        //Exit room
        if (_roomType == RoomType.Exit)
        {
            FindEmptyTile();
            GameObject exit = GameObject.Find("Exit") as GameObject;
            List<Vector3> emptyTiles = FindEmptyTile();
            int randomTile = Random.Range(0, emptyTiles.Count);
            exit.transform.position = new Vector3(emptyTiles[randomTile].x, emptyTiles[randomTile].y + 0.5f);
            emptyTiles.RemoveAt(randomTile);
            for (int i = 0; i < emptyTiles.Count; i ++)
            {
                float chance = Random.Range(0f, 1f);
                if (chance > 0.75)
                    Instantiate(enemySpawner, new Vector3(emptyTiles[i].x, emptyTiles[i].y + 0.5f) , transform.rotation);
            }
        }

        // default
        if ( _roomType == RoomType.Default)
        {
            FindEmptyTile();
            List<Vector3> emptyTiles = FindEmptyTile();
            for (int i = 0; i < emptyTiles.Count; i++)
            {
                float chance = Random.Range(0f, 1f);
                if (chance > 0.5)
                    Instantiate(enemySpawner, new Vector3(emptyTiles[i].x, emptyTiles[i].y + 0.5f), transform.rotation);
            }

        }
    }

    List<Vector3> FindEmptyTile()
    {
        ProceduralTileController[] tiles = gameObject.GetComponentsInChildren<ProceduralTileController>();
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
}
