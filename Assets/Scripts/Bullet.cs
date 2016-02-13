using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    public float speed = 5.0f;
    public int damage = 5;
    [ReadOnly]
    public GameObject bulletParticles;
    [ReadOnly]
    public Collider2D playerActiveZone;

    [HideInInspector]
    public Direction bulletDirection = Direction.RIGHT;

    private Transform _transform;
    private Weapon weapon;
    private float hSpeed;
    private float translate;

    void Start ()
    {
        playerActiveZone = GameObject.Find("Zone").GetComponent<Collider2D>();
        weapon = GameObject.Find("Player").GetComponent<Weapon>();
        weapon.bulletCount += 1;
        hSpeed = Random.Range(-5f, 5f);
        _transform = transform;
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
        weapon.bulletCount -= 1;
        Instantiate(bulletParticles, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
