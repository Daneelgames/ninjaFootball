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
        if (player.transform.position.x > transform.position.x)
            hDir = 1.0f;
        else
            hDir = -1.0f;
	}

    void FixedUpdate()
    {
        _rb.AddForce(new Vector2(hDir, 0) * speed);
    }
}
