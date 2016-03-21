using UnityEngine;
using System.Collections;

public class EnemyBullet : MonoBehaviour {

    [SerializeField]
    private int damage = 5;
    public Vector2 direction;
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private GameObject bulletParticles;
    [SerializeField]
    private Collider2D room;

    private Transform _transform;
    private float hSpeed;
    private float translate;
    private PlayerMovement _pm;

    private bool roomGot = false;

    void Start ()
    {
        _transform = transform;
        _pm = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    void Update()
    {
        _transform.Translate(direction.x * speed * Time.deltaTime, direction.y * speed * Time.deltaTime, 0);

        if (_pm.playerLives <= 0)
            BulletDestroy();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "Ground")
        {
            other.gameObject.GetComponent<ProceduralTileController>().Damage(damage);
        }
        BulletDestroy();
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (!roomGot && coll.tag == "Room")
        {
            roomGot = true;
            room = coll;
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.tag == "Zone" || coll == room)
        {
            BulletDestroy();
        }
    }

    void BulletDestroy()
    {
        Instantiate(bulletParticles, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
