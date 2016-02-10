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

    void Start ()
    {
        weapon = GameObject.Find("Player").GetComponent<Weapon>();
        weapon.bulletCount += 1;
        hSpeed = Random.Range(-.5f, .5f);
        _transform = transform;
	}
	
	void Update () {
        MoveBullet();
	}

    void MoveBullet()
    {
        int moveDirection = bulletDirection == Direction.LEFT ? -1 : 1;

        float translate = moveDirection * speed * Time.deltaTime;
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
            other.collider.gameObject.GetComponent<Enemy>().Damage(damage);
            Instantiate(bulletParticles, transform.position, transform.rotation);
            BulletDestroy();
        }
    }

    void OnTriggerStay2D(Collider2D zone)
    {
        playerActiveZone = GameObject.Find("Player").GetComponent<ActiveZone>().activeZone.GetComponent<Collider2D>() as Collider2D;
        if (zone.tag == "Zone" && zone != playerActiveZone)
        {
                BulletDestroy();
        }
    }
    void OnTriggerExit2D(Collider2D zone)
    {
        playerActiveZone = GameObject.Find("Player").GetComponent<ActiveZone>().activeZone.GetComponent<Collider2D>() as Collider2D;
        if (zone.tag == "Zone" && zone == playerActiveZone)
        {
                BulletDestroy();
        }
    }

    void BulletDestroy()
    {
        weapon.bulletCount -= 1;
        Destroy(gameObject);
    }
}
