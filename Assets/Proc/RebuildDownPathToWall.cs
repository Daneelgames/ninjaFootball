using UnityEngine;
using System.Collections;

public class RebuildDownPathToWall : MonoBehaviour {


    [SerializeField]
    private GameObject[] downWall;

    public void RebuildFloor()
    {
        Instantiate(downWall[0], transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
