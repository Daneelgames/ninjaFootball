using UnityEngine;
using System.Collections;

public class EnemyDropOnDestroy : MonoBehaviour
{

    [SerializeField]
    private GameObject drop;
    [SerializeField]
    private int minDrop;
    [SerializeField]
    private int maxDrop;
    [SerializeField]
    private EnemyHealth healthScript;
    private bool canDrop = true;

    void Start()
    {
        healthScript = GetComponent<EnemyHealth>();
    }

    void Update()
    {
        if (healthScript.health < 1 && canDrop)
        {
            canDrop = false;
            GameObject dropScript = Instantiate(drop, transform.position, transform.rotation) as GameObject;
            dropScript.GetComponent<DropController>().amount = Random.Range(minDrop, maxDrop);
        }
    }
}
