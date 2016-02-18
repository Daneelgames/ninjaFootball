using UnityEngine;
using System.Collections;

public class TimeBomb : MonoBehaviour {

    public float speed = 5.0f;
    public int damage = 20;
    public float hRandom = 1f;
    public float timeLife = 3f;
    public GameObject explodeParticles;
    [ReadOnly]
    public Collider2D playerActiveZone;

    [HideInInspector]
    public Direction bulletDirection = Direction.RIGHT;

    private float hSpeed;
    private float translate;
    private PlayerMovement playerMovement;
    private Rigidbody2D rb;
    private AudioSource audio;

    void Start ()
    {
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        playerActiveZone = GameObject.Find("Zone").GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        hSpeed = Random.Range(0, hRandom);
        audio = GetComponent<AudioSource>();

        bulletDirection = playerMovement.PlayerDirection;
        int moveDirection = bulletDirection == Direction.LEFT ? -1 : 1;

        translate = moveDirection * speed * Time.deltaTime;
        rb.AddForce(new Vector2(translate, hSpeed), ForceMode2D.Impulse);

    }

    void Update()
    {
        timeLife -= 1f * Time.deltaTime;

        if(timeLife <= 0)
        {
            Instantiate(explodeParticles, transform.position, transform.rotation);
            BombDestroy();
        }

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        audio.pitch = Random.Range(0.85f, 1.15f);
        audio.Play();
        if (other.collider.tag == "Enemy")
        {
            other.collider.gameObject.GetComponent<EnemyHealth>().Damage(damage);
            BombDestroy();
        }

    }

    void BombDestroy()
    {
        Instantiate(explodeParticles, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
