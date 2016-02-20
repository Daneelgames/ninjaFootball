using UnityEngine;
using System.Collections;

public class EnemySpawnerInRoom : MonoBehaviour {

    public GameObject enemy;
    [ReadOnly]
    public bool canSpawn = true;
    [ReadOnly]
    public Collider2D playerActiveRoom;
    public Collider2D spawnerRoom;

    private GameObject player;
    private GameObject spawnedMob = null;

    void Start()
    {
        player = GameObject.Find("Player");
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Room")
            spawnerRoom = coll;
    }

    void Update()
    {
        playerActiveRoom = player.GetComponent<ActiveRoom>().activeRoom.GetComponent<Collider2D>();

        if (spawnerRoom == playerActiveRoom && canSpawn && spawnedMob == null)
        {
            spawnedMob = Instantiate(enemy, transform.position, transform.rotation) as GameObject;
            canSpawn = false;
        }

        else if (spawnerRoom != playerActiveRoom)
        {
            canSpawn = true;
        }
    }

}
