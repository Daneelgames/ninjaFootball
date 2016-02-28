using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

    public GameObject enemy;
    public bool zoneDepend = false;
    [SerializeField]
    private Collider2D playerActiveRoom;
    [SerializeField]
    private Collider2D playerZone;
    [SerializeField]
    private bool canSpawn = true;
    [SerializeField]
    private GameObject spawnedMob = null;
    [SerializeField]
    private Collider2D spawnerRoom;

    private GameObject player;

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

        if (spawnerRoom != playerActiveRoom && !zoneDepend)
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

        if (coll.tag == "Zone" && spawnerRoom == playerActiveRoom && canSpawn && spawnedMob == null && !zoneDepend)
        {
            spawnedMob = Instantiate(enemy, transform.position, transform.rotation) as GameObject;
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
