using UnityEngine;
using System.Collections;

public class EnemyMoveToPlayer : MonoBehaviour
{
    public float speed = 10;
    public bool isVisible = false;

    private float hDir;
    private Transform player;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
        if (player.transform.position.x - 1f > transform.position.x)
            hDir = 1.0f;
        else if
            (player.transform.position.x + 1f < transform.position.x)
                hDir = -1.0f;
	}

    void FixedUpdate()
    {
        if (isVisible)
        {
            transform.Translate(hDir * speed * Time.deltaTime, 0, 0);
        }
    }

    void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.tag == "Zone")
            isVisible = true;
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.tag == "Zone")
            isVisible = false;
    }
}
