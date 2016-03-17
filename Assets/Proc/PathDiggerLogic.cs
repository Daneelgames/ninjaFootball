using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathDiggerLogic : MonoBehaviour {

    [HideInInspector]
    public bool roomsFinished = false;
    [HideInInspector]
    public int roomBuilded = 0;

    [SerializeField]
    private int maxRooms = 9;
    [SerializeField]
    private GameObject scrollZone;

    private bool doublesChecked = false;

    [SerializeField]
    private float roomWidth = 11f;
    [SerializeField]
    private float roomHeigth = 6f;

    [SerializeField]
    private GameObject room;
    private int roomCount = 0;
    List<Vector3> positions = new List<Vector3>();

    void Start ()
    {
        SetPath();
    }

    void Update()
    {
        if (roomCount >= maxRooms && !roomsFinished)
            roomsFinished = true;

        if (roomBuilded == maxRooms && !doublesChecked)
        {
            doublesChecked = true;
            CheckDublicates();
        }
    }

    void SetPath()
    {
        int direction = -1;
        int pastDir = -1;
        int newDir = -1;

        while (roomCount < maxRooms)
        {
            direction = Random.Range(1, 6);
            if (direction != pastDir)
            {
                newDir = direction;
                pastDir = newDir;
            }
            switch (newDir)
            {
                case 1:
                    MoveAndSpawn(roomWidth, 0);
                    break;

                case 2:
                    MoveAndSpawn(roomWidth, 0);
                    break;

                case 3:
                    MoveAndSpawn(roomWidth * -1, 0);
                    break;

                case 4:
                    MoveAndSpawn(roomWidth * -1, 0);
                    break;

                case 5:
                    MoveAndSpawn(0, roomHeigth * -1);
                    break;

                default:
                    break;
            }

            pastDir = newDir;
        }
    }

    void MoveAndSpawn(float moveX, float moveY)
    {
        bool canSpawn = false;

        if (!positions.Contains(transform.position))
        {
            canSpawn = true;
        }

        if (canSpawn == true)
        {
            Instantiate(room, transform.position, transform.rotation);
            roomCount += 1;
            positions.Add(transform.position);
        }

        gameObject.transform.position = new Vector3(gameObject.transform.position.x + moveX, gameObject.transform.position.y + moveY, 0);

    }

    void CheckDublicates()
    {
        GameObject[,] downPathsSorted = new GameObject[maxRooms, maxRooms];
        GameObject[] downPaths = GameObject.FindGameObjectsWithTag("DownPath");
        for(int i = 0; i < maxRooms; i++)
        {
            for (int j = 0; j < downPaths.Length; j++)
            {
                if (downPaths[j].transform.position.y == i * roomHeigth * -1)
                {
                    downPathsSorted[i,j] = downPaths[j];
                }
            }
        }

        for (int i = 0; i < downPathsSorted.GetLength(0); i++)
        {
            int pathsInRow = 0;
            List<GameObject> doubles = new List<GameObject>();

            for (int j = 0; j < downPathsSorted.GetLength(1); j++)
            {
                if (downPathsSorted[i, j] != null)
                {
                    pathsInRow += 1;
                    doubles.Add(downPathsSorted[i, j]);
                }
            }

            if (pathsInRow > 1)
            {
                int randomIndex = Random.Range(0, doubles.Count);
                doubles.RemoveAt(randomIndex);

                for (int p = 0; p < doubles.Count; p++)
                {
                    doubles[p].gameObject.GetComponent<RebuildDownPathToWall>().RebuildFloor();
                }
            }
        }

        GetRoomSize();
    }

    void GetRoomSize()
    {
        float minX = 0f;
        float maxX = 0f;
        float minY = 0f;
        float maxY = 0f;

        GameObject[] roomBuilders = GameObject.FindGameObjectsWithTag("RoomBuilder");

        for (int i = 0; i < roomBuilders.Length; i++)
        {
            if (roomBuilders[i].transform.position.x < minX)
                minX = roomBuilders[i].transform.position.x;

            if (roomBuilders[i].transform.position.x > maxX)
                maxX = roomBuilders[i].transform.position.x;

            if (roomBuilders[i].transform.position.y < minY)
                minY = roomBuilders[i].transform.position.y;

            if (roomBuilders[i].transform.position.y > maxY)
                maxY = roomBuilders[i].transform.position.y;
        }

        CreateScrollZone(minX, maxX, minY, maxY);
    }

    void CreateScrollZone(float minx, float maxx, float miny, float maxy)
    {
        //get actual position
        float scrollZoneX = minx - roomWidth / 2;
        float scrollZoneY = maxy + roomHeigth / 2;
        float scrollZoneWidth = Vector3.Distance(new Vector3(scrollZoneX, scrollZoneY, 0), new Vector3((maxx + roomWidth/2), (maxy + roomHeigth/2), 0));
        float scrollZoneHeight = Vector3.Distance(new Vector3(scrollZoneX, scrollZoneY, 0), new Vector3((minx - roomWidth / 2), (miny - roomHeigth / 2), 0));
        
        GameObject zone = Instantiate(scrollZone, new Vector3(scrollZoneX, scrollZoneY, 0), transform.rotation) as GameObject;
        zone.transform.localScale = new Vector3(scrollZoneWidth, scrollZoneHeight, 1);
    }
}