using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    public float speed = 15.0f;
    public int damage = 5;
    public float hRandom = 1f;
    [ReadOnly]
    public GameObject bulletParticles;
    [ReadOnly]
    public Collider2D playerActiveZone;

    [HideInInspector]
    public Direction bulletDirection = Direction.RIGHT;

    private Transform _transform;
    private float hSpeed;
    private float translate;
    private PlayerMovement playerMovement;

    void Start ()
    {
        //hRandom = Random.Range(-1F, 1F);
        speed += Random.Range(-3f, 3f);
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        playerActiveZone = GameObject.Find("Zone").GetComponent<Collider2D>();
        hSpeed = Random.Range(-hRandom, hRandom);
        _transform = transform;
        bulletDirection = playerMovement.PlayerDirection;
    }
	
	void Update () {
        MoveBullet();
	}

    void MoveBullet()
    {
        int moveDirection = bulletDirection == Direction.LEFT ? -1 : 1;

        translate = moveDirection * speed * Time.deltaTime;
        _transform.Translate(translate, hSpeed * Time.deltaTime, 0);

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "Enemy")
        {
            other.collider.gameObject.GetComponent<EnemyHealth>().Damage(damage);
        }

        if (other.collider.tag == "Hazard")
        {
            other.collider.gameObject.GetComponent<EnemyHealth>().Damage(damage);
        }

        if (other.collider.tag == "Ground")
        {
            other.gameObject.GetComponent<ProceduralTileController>().Damage(damage);
        }
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
