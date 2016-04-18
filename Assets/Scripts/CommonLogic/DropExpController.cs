using UnityEngine;
using System.Collections;

public class DropExpController : MonoBehaviour {
    
    public int amount;
    
    private PlayerMovement pm;
    private Rigidbody2D rb;
    private BoxCollider2D playerCollider;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(Random.Range(-2, 2), Random.Range(1, 5)), ForceMode2D.Impulse);
        rb.AddTorque(Random.Range(-3, 3), ForceMode2D.Impulse);
        pm = GameObject.Find("Player").GetComponent<PlayerMovement>();


    }

    void Update()
    {
        if (amount <= 0)
            Destroy(gameObject);

        if (pm.playerLives <= 0)
            Destroy(gameObject);
    }
}
