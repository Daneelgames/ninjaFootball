using UnityEngine;
using System.Collections;

public class EnemyMoveToPlayer : MonoBehaviour
{
    public float speed = 2;

    private float hDir;
    private Transform player;
    private Rigidbody2D _rb;

	// Use this for initialization
	void Start () {
        _rb = GetComponent<Rigidbody2D>();
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
        _rb.velocity = new Vector2(hDir, -1f) * speed;
    }
}
