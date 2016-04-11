using UnityEngine;
using System.Collections;

public class BulletMovementController : MonoBehaviour {

    public float speed = 10f;
    public int damage = 2;
    public float hRandom = 1f;
    public GameObject bulletParticles;
    [ReadOnly]
    public Collider2D playerActiveZone;

    [HideInInspector]
    public Direction bulletDirection = Direction.RIGHT;

    [SerializeField]
    private bool destroyOnWall = true;

    private Transform _transform;
    private float hSpeed;
    private float translate;
    private PlayerMovement playerMovement;

    void Start ()
    {
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
        if (destroyOnWall)
            Destroy(gameObject);

    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.tag == "Zone")
        {
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        Instantiate(bulletParticles, transform.position, transform.rotation);
    }
}
