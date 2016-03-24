using UnityEngine;
using System.Collections;

public class SpawnInstanceOnStart : MonoBehaviour {

    [SerializeField]
    private GameObject instance;
    [SerializeField]
    private float time;
    [SerializeField]
    private Vector3 position;

    void Start()
    {
        StartCoroutine(SpawnObject(time));
    }

    IEnumerator SpawnObject(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Instantiate(instance, position, transform.rotation);
    }

}
