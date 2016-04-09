using UnityEngine;
using System.Collections;

public class DropController : MonoBehaviour {

    public int amount;

    private PlayerMovement pm;
    private Rigidbody2D rb;
    private BoxCollider2D playerCollider;
    private BoxCollider2D _collider;

    private float ignoreT = 1f;

    void Start()
    {
        _collider = GetComponent<BoxCollider2D>();
        playerCollider = GameObject.Find("Player").GetComponent<BoxCollider2D>();
        Physics2D.IgnoreCollision(_collider, playerCollider, true);
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(0, 7), ForceMode2D.Impulse);
        pm = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    void Update()
    {

        if (ignoreT > 0)
            ignoreT -= 2 * Time.deltaTime;
        else
            Physics2D.IgnoreCollision(_collider, playerCollider, false);

        if (amount <= 0)
            Destroy(gameObject);

        if (pm.playerLives <= 0)
            Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Hazard")
            Destroy(gameObject);
    }

}
