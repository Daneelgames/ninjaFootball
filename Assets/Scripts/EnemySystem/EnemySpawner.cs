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
    private ActiveRoom playerActiveRoomScript;

    void Start()
    {
        player = GameObject.Find("Player");
        _pm = player.GetComponent<PlayerMovement>();
        playerActiveRoomScript = player.GetComponent<ActiveRoom>();
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
        playerActiveRoom = playerActiveRoomScript.activeRoom.GetComponent<Collider2D>();
    }

    void MobSpawn()
    {
        if (spawnerRoom == playerActiveRoom && canSpawn && spawnedMob == null && _pm.playerLives > 0)
        {
            canSpawn = false;
            spawnedMob = Instantiate(enemy, transform.position, transform.rotation) as GameObject;
        }
        /*
        if (spawnerRoom != playerActiveRoom)
        {
            canSpawn = true;
            if (spawnedMob != null)
                Destroy(spawnedMob);
        } */
    }

    void MobReset()
    {
        //destroy mob if player is dead
        if (_pm.playerLives <= 0)
        {
            if (spawnedMob != null)
                Destroy(spawnedMob, 0.25f);

            canSpawn = true;
        }
    }
}
