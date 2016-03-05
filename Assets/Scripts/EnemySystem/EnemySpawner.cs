using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

    public GameObject enemy;
    [SerializeField]
    private Collider2D playerActiveRoom;
    [SerializeField]
    private bool canSpawn = true;
    [SerializeField]
    private GameObject spawnedMob = null;
    [SerializeField]
    private Collider2D spawnerRoom = null;

    private GameObject player;
    private PlayerMovement _pm;

    void Start()
    {
        player = GameObject.Find("Player");
        _pm = player.GetComponent<PlayerMovement>();
    }
    // Room spawn
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Room")
            spawnerRoom = coll;
    }

    void Update()
    {
        GetPlayerRoom();
        MobReset();
        MobSpawn();
    }

    void GetPlayerRoom()
    {
        playerActiveRoom = player.GetComponent<ActiveRoom>().activeRoom.GetComponent<Collider2D>();

        if (spawnerRoom != playerActiveRoom)
            canSpawn = true;
        else if (spawnedMob == null && _pm.playerLives <= 0)
            canSpawn = true;
    }

    void MobSpawn()
    {
        if (spawnerRoom == playerActiveRoom && canSpawn && spawnedMob == null && _pm.playerLives > 0)
        {
            canSpawn = false;
            spawnedMob = Instantiate(enemy, transform.position, transform.rotation) as GameObject;
        }

    }

    void MobReset()
    {
        //destroy mob if player is dead
        if (_pm.playerLives <= 0 && spawnedMob != null)
        {
            Destroy(spawnedMob, 0.5f);
            canSpawn = true;
        }
    }

    /*
    //spawn on zone collide
    void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.tag == "Zone" && spawnerRoom == playerActiveRoom && canSpawn && spawnedMob == null && _pm.playerLives > 0)
        {
            canSpawn = false;
            spawnedMob = Instantiate(enemy, transform.position, transform.rotation) as GameObject;
        }
    } */

}
