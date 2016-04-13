using UnityEngine;
using System.Collections;

public class EnemyBeeyesBehaviour : MonoBehaviour {

    [SerializeField]
    private float speed;
    [SerializeField]
    private Rigidbody2D _rb;

    [SerializeField]
    private bool isVisible = false;
    
    [SerializeField]
    private GameObject sprite;

    private GameObject player;
    [SerializeField]
    private GameObject bump;
    
    // Use this for initialization
    void Start () {
        player = GameObject.Find("Player");
	}
	
	void FixedUpdate () {
        if (isVisible)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, (player.transform.position - transform.position), 1f, 1 << 11);
            if (hit && hit.collider.gameObject != gameObject)
                bump = hit.collider.gameObject;
            else
                bump = null;

            if (bump == null)
                _rb.AddForce((player.transform.position - transform.position) * speed);
            else
                _rb.AddForce((bump.transform.position - transform.position) * speed * -2);
        }
    }

    void Update ()
    {
        if (player.transform.position.x > transform.position.x)
            sprite.transform.localRotation = Quaternion.Euler(0, 0, 0);
        else
            sprite.transform.localRotation = Quaternion.Euler(0, 180, 0);
    }
    
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Zone")
        {
            isVisible = true;
        }
    }
    
    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.tag == "Room")
        {
            Destroy(gameObject);
        }

        else if (coll.tag == "Zone")
        {
            isVisible = false;
        }
    }
}
