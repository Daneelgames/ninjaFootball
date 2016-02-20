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

    void OnDestroy()
    {
        for (int i = 0; i < Random.Range(minDrop, maxDrop); i++)
            Instantiate(drop, transform.position, transform.rotation);
    }
}
