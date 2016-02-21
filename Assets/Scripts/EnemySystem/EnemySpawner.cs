using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

    public GameObject enemy;
    public bool zoneDepend = false;
    [SerializeField]
    private Collider2D playerActiveRoom;
    [SerializeField]
    private Collider2D playerZone;

    private Collider2D spawnerRoom;
    private GameObject player;
    private GameObject spawnedMob = null;
    private bool canSpawn = true;

    void Start()
    {
        player = GameObject.Find("Player");
        playerZone = GameObject.Find("Zone").GetComponent<Collider2D>() as Collider2D;
    }
    // Room spawn
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Room")
            spawnerRoom = coll;
    }

    void Update()
    {
        playerActiveRoom = player.GetComponent<ActiveRoom>().activeRoom.GetComponent<Collider2D>();

        if (spawnerRoom == playerActiveRoom && canSpawn && spawnedMob == null && !zoneDepend)
        {
            spawnedMob = Instantiate(enemy, transform.position, transform.rotation) as GameObject;
            canSpawn = false;
        }

        else if (spawnerRoom != playerActiveRoom && !zoneDepend)
        {
            canSpawn = true;
        }
    }

    //zone spawn
    void OnTriggerStay2D(Collider2D coll)
    {
        if (coll == playerZone && canSpawn && zoneDepend)
        {
            GameObject mob = (GameObject) Instantiate(enemy, transform.position, transform.rotation);
            mob.GetComponent<DestroyOutsideZone>().eZoneDepend = true;
            canSpawn = false;
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll == playerZone && zoneDepend)
        {
            canSpawn = true;
        }
    }

}
