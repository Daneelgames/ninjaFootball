using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    public Direction bulletDirection = Direction.RIGHT;
    public float speed = 5.0f;
    public int damage = 5;

    private Transform _transform;
    private Weapon weapon;
    private SpriteRenderer bulletRenderer;

    void Start () {
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
        _transform.Translate(translate, 0, 0);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "Enemy")
        {
            other.collider.gameObject.GetComponent<Enemy>().Damage(damage);
        }
        BulletDestroy();
    }

    void CheckScreenPosition()
    {
        if (bulletRenderer.isVisible == false)
            BulletDestroy();
    }

    void BulletDestroy()
    {
        Destroy(gameObject);
        weapon.bulletCount -= 1;
    }
}
