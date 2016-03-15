using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathDiggerLogic : MonoBehaviour {

    [HideInInspector]
    public bool roomsFinished = false;

    [SerializeField]
    private int maxRooms = 9;

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
        Instantiate(room, transform.position, transform.rotation);
        roomCount += 1;
        positions.Add(transform.position);

        SetPath();
    }

    void Update()
    {
        if (roomCount >= maxRooms && !roomsFinished)
            roomsFinished = true;
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
        
        gameObject.transform.position = new Vector3(gameObject.transform.position.x + moveX, gameObject.transform.position.y + moveY, 0);

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

    }
}
