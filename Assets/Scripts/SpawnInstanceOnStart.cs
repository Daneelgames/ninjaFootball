using UnityEngine;
using System.Collections;

public class SpawnInstanceOnStart : MonoBehaviour {

    [SerializeField]
    private GameObject gm;
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
        Instantiate(gm, position, transform.rotation);
    }

}
