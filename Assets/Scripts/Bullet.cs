using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    public float speed = 5.0f;
    public int damage = 5;
    [ReadOnly]
    public GameObject bulletParticles;
    [HideInInspector]
    public Direction bulletDirection = Direction.RIGHT;

    private Transform _transform;
    private Weapon weapon;
    private SpriteRenderer bulletRenderer;
    private float hSpeed;

    void Start () {
        hSpeed = Random.Range(-.5f, .5f);
        _transform = transform;
        weapon = GameObject.Find("Player").GetComponent<Weapon>();
        bulletRenderer = GetComponent<SpriteRenderer>();
        weapon.bulletCount += 1;
	}
	
	void Update () {
        MoveBullet();
        CheckScreenPosition();
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
        }
        Instantiate(bulletParticles, transform.position, transform.rotation);
        BulletDestroy();
    }

    void CheckScreenPosition()
    {
        if (bulletRenderer.isVisible == false)
            BulletDestroy();
    }

    void BulletDestroy()
    {
        weapon.bulletCount -= 1;
        Destroy(gameObject);
    }
}
