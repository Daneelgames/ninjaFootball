using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    public float speed = 15.0f;
    public int damage = 5;
    [ReadOnly]
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
        speed += Random.Range(-5f, 5f);
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

        //Flip sprite
        if (translate < 0)
            transform.localScale = new Vector2(-1f, 1f);
        else
            transform.localScale = new Vector2(1f, 1f);

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "Enemy")
        {
            other.collider.gameObject.GetComponent<EnemyHealth>().Damage(damage);
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
