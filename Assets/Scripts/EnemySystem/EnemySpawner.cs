using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

    public GameObject[] enemies;
    [SerializeField]
    private bool canSpawn = true;
    [SerializeField]
    private GameObject spawnedMob = null;

    private GameObject player;
    private PlayerMovement _pm;

    private int mobToSpawn = 0;

    void Start()
    {
        player = GameObject.Find("Player");
        _pm = player.GetComponent<PlayerMovement>();
        mobToSpawn = Random.Range(0, enemies.Length);
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
            spawnedMob = Instantiate(enemies[mobToSpawn], transform.position, transform.rotation) as GameObject;
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
