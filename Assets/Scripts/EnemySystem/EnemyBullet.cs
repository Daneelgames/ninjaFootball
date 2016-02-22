using UnityEngine;
using System.Collections;

public class EnemyBullet : MonoBehaviour {

    public Vector2 direction;
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private GameObject bulletParticles;

    private Transform _transform;
    private float hSpeed;
    private float translate;

    void Start ()
    {
        _transform = transform;

    }

    void Update()
    {
        _transform.Translate(direction.x * speed * Time.deltaTime, direction.y * speed * Time.deltaTime, 0);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        BulletDestroy();
    }

    void OnTriggerExit2D(Collider2D zone)
    {
        if (zone.tag == "Zone")
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
