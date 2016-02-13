using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

    public GameObject enemy;
    public bool canSpawn;

    [ReadOnly]
    public Collider2D zone;
    [ReadOnly]
    public Collider2D playerActiveZone;

    private ActiveZone player;
    private GameObject instanceEnemy;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<ActiveZone>() as ActiveZone;
    }

    void OnTriggerEnter2D (Collider2D coll)
    {
        if (coll.tag == "Zone")
            zone = coll;
    }
	
	// Update is called once per frame
	void Update ()
    {
        playerActiveZone = player.activeZone.GetComponent<Collider2D>() as Collider2D;
        if (canSpawn)
        {
            if (zone == playerActiveZone)
            {
                Spawn();
                canSpawn = false;
            }
        }
        else
        {
            if (zone != playerActiveZone)
            {
                canSpawn = true;

                if (instanceEnemy != null)
                    Destroy(instanceEnemy);
            }
        }
    }

    void Spawn()
    {
        instanceEnemy = GameObject.Instantiate(enemy, transform.position, transform.rotation) as GameObject;
    }
}
