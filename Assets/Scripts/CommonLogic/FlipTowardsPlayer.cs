using UnityEngine;
using System.Collections;

public class FlipTowardsPlayer : MonoBehaviour {

    private Transform player;
    private float scaleX = 1f;

    // Use this for initialization
    void Start ()
    {
        player = GameObject.Find("Player").transform;

    }
	
	// Update is called once per frame
	void Update ()
    {
        transform.localScale = new Vector3(scaleX, 1, 1);

        if (player.transform.position.x > transform.position.x)
            scaleX = 1f;
        else
            scaleX = -1f;

    }
}
