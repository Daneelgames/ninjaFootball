using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyHiveyeBehaviour : MonoBehaviour {

    [SerializeField]
    private GameObject mobToSpawn;
    [SerializeField]
    private float periodTime = 1f;
    [SerializeField]
    private int mobsMax = 10;
    [SerializeField]
    private Transform spawnHolder;

    [SerializeField]
    private float randomSpawnOffset;

    [SerializeField]
    private List<GameObject> aliveMobs;

    private int mobsCur = 0;
    private float spawnCooldown = 0;
    private PlayerMovement _pm;
    private bool visible = false;
    
    void Start()
    {
        _pm = GameObject.Find("Player").GetComponent<PlayerMovement>();
        InvokeRepeating("SortList", 1f, 1f);
    }

    void SortList()
    {
        for (var i = aliveMobs.Count - 1; i > -1; i--)
        {
            if (aliveMobs[i] == null)
            {
                MobKilled(aliveMobs[i]);
                aliveMobs.RemoveAt(i);
            }
        }
    }

    void Update ()
    {
        MobReset();
        if (visible && mobsCur < mobsMax)
        {
            if (spawnCooldown >= 0)
                spawnCooldown -= 1 * Time.deltaTime;
            else
                SpawnMob();
        }
    }

    void SpawnMob()
    {
        spawnCooldown = periodTime;
        GameObject lastMob = Instantiate(mobToSpawn, new Vector2(spawnHolder.position.x + Random.Range(-randomSpawnOffset, randomSpawnOffset), spawnHolder.position.y + Random.Range(-randomSpawnOffset, randomSpawnOffset)), spawnHolder.rotation) as GameObject;
        aliveMobs.Add(lastMob);
        mobsCur += 1;
    }

    void MobKilled(GameObject killedMob)
    {
        mobsCur -= 1;
    }

    void MobReset()
    {
        if (_pm.playerLives <= 0)
        {
            for (var i = aliveMobs.Count - 1; i > -1; i--)
            {
                Destroy(aliveMobs[i], 0.25f);
                aliveMobs.RemoveAt(i);
            }
        }
    }

    void OnTriggerEnter2D (Collider2D coll)
    {
        if (coll.tag == "Zone")
            visible = true;
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.tag == "Zone")
            visible = false;
    }
}