using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

    public GameObject enemy;
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
        MobReset();
        MobSpawn();
    }   
    
    void MobSpawn()
    {
        if (canSpawn && spawnedMob == null && _pm.playerLives > 0)
        {
            canSpawn = false;
            spawnedMob = Instantiate(enemy, transform.position, transform.rotation) as GameObject;
        }
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
