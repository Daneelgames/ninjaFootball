using UnityEngine;
using System.Collections;

public class EnemySpawnerInZone : MonoBehaviour {

    public GameObject enemy;
    [ReadOnly]
    public bool canSpawn = true;

    [ReadOnly]
    public Collider2D playerActiveZone;

    void Start()
    {
        playerActiveZone = GameObject.Find("Zone").GetComponent<Collider2D>() as Collider2D;
    }

    void OnTriggerStay2D (Collider2D coll)
    {
        if (coll == playerActiveZone && canSpawn)
        {
            Instantiate(enemy, transform.position, transform.rotation);
            canSpawn = false;
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll == playerActiveZone)
        {
            canSpawn = true;
        }
    }

}
