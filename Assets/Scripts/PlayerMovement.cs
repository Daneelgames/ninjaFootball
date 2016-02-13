using UnityEngine;
using System.Collections;

public enum Direction {LEFT, RIGHT};
public class PlayerMovement : MonoBehaviour
{
    [HideInInspector] public float jumpPower = 7.0f;
    [HideInInspector] public float tJump = 0f;
    [HideInInspector] public bool shoot = false;

    public float speed = 2f;
    public int playerLives = 1;

    [ReadOnly]
    public GameObject explosionParticles;
    [ReadOnly]
    public GameObject playerSprite;

    [ReadOnly]
    public float maxVelocity = 5f;
    [ReadOnly]
    public float minVelocity = -5f;
    [ReadOnly]
    public new SpriteRenderer renderer;
    [ReadOnly]
    public bool isOnGround = false;
    [ReadOnly]
    public Animator _animator;
    [ReadOnly]
    public PlayerSounds playerSound;
    [ReadOnly]
    public Transform activeCheckpoint;

    private TimeScale timeScaleScript;
    private Rigidbody2D _rigidbody;
    private Direction playerDirection = Direction.RIGHT;
    private Weapon weapon;
    private float jumpDirection = 0.0f;
    private float translate;
    private bool hurt = false;
    private bool dialog = false;
    private bool canLand = true;

    public Direction PlayerDirection
    {
        get {
            return playerDirection;
        }
    }

    void Start()
    {
        timeScaleScript = GetComponent<TimeScale>();
        _rigidbody = GetComponent(typeof(Rigidbody2D)) as Rigidbody2D;
        weapon = GetComponent<Weapon>() as Weapon;
    }

    void Update()
    {
        if (playerLives > 0 && !dialog)
        {
            MovePlayer();
            Jump();
        }
        else
            translate = 0;

        GroundRaycast();
        Animator();
    }

    void FixedUpdate()
    {
        if (_rigidbody.velocity.magnitude > maxVelocity)
            _rigidbody.velocity = _rigidbody.velocity.normalized * maxVelocity;

        if (_rigidbody.velocity.magnitude < minVelocity)
            _rigidbody.velocity = _rigidbody.velocity.normalized * minVelocity;
    }

    void MovePlayer()
    {
        translate = Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime;
        //check wall
        RaycastHit2D hit0 = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 0.1f), new Vector2(Input.GetAxisRaw("Horizontal"), 0), 0.35f, 1 << 8);
        RaycastHit2D hit1 = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 0.5f), new Vector2(Input.GetAxisRaw("Horizontal"), 0), 0.35f, 1 << 8);
        RaycastHit2D hit2 = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 0.9f), new Vector2(Input.GetAxisRaw("Horizontal"), 0), 0.35f, 1 << 8);
        if (hit0.collider == null && hit1.collider == null && hit2.collider == null)
        {
            if (isOnGround)
            {
                if (canLand)
                {
                    playerSound.PlaySound(3);
                    canLand = false;
                }

                transform.Translate(translate, 0, 0);
            }
            else if (Input.GetAxisRaw("Horizontal") != 0)
                {
                    jumpDirection = Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime;
                    transform.Translate(jumpDirection, 0, 0);
                }
            else if (Input.GetAxisRaw("Horizontal") == 0)
                transform.Translate(jumpDirection / 4, 0, 0);
        }
        //else if (hit0.collider.tag != "Ground" && hit1.collider.tag != "Ground" && hit2.collider.tag != "Ground")

        if (translate > 0)
        {
            playerDirection = Direction.RIGHT;
            playerSprite.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else if (translate < 0)
        {
            playerDirection = Direction.LEFT;
            playerSprite.transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
    }

    void Jump()
    {
        if (isOnGround)
        {
            if (Input.GetButtonDown("Jump"))
            {
                _rigidbody.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
                isOnGround = false;
                tJump = 1f;
                playerSound.PlaySound(0);
            }
        }
        //Mid-air horizontal movement
       /* else
        {
            if (Input.GetAxisRaw("Horizontal") != 0)
            {
                jumpDirection = Input.GetAxisRaw("Horizontal") * speed/3 * Time.deltaTime;
            }
            transform.Translate(jumpDirection/2, 0, 0);
        }
        */
    }

    void GroundRaycast()
    {
            if (tJump == 0)
            {
                RaycastHit2D hit0 = Physics2D.Raycast(new Vector2(transform.position.x - 0.27f, transform.position.y), Vector2.down, 0.1f, 1 << 8);
                RaycastHit2D hit1 = Physics2D.Raycast(transform.position, Vector2.down, 0.1f, 1 << 8);
                RaycastHit2D hit2 = Physics2D.Raycast(new Vector2(transform.position.x + 0.27f, transform.position.y), Vector2.down, 0.1f, 1 << 8);
                if (hit0.collider != null || hit1.collider != null || hit2.collider != null)
                {
                    isOnGround = true;
                    jumpDirection = 0;
                }
                else if (hit0.collider == null && hit1.collider == null && hit2.collider == null)
                    isOnGround = false;
            }

        if (!isOnGround)
        {
            if (!canLand)
                canLand = true;

            if (tJump > 0)
                tJump -= 10 * Time.deltaTime;

            else if (tJump < 0)
                tJump = 0;

        }
    }

    void OnCollisionEnter2D(Collision2D enemy)
    {
        if (enemy.gameObject.tag == "Enemy" && playerLives > 0)
        {
            playerSound.PlaySound(2);
            StartCoroutine(Damage(0.5F));
            StartCoroutine(Blinking(.1F));
            Destroy(enemy.gameObject);
        }
    }

    IEnumerator Damage(float waitTime)
    {
        Instantiate(explosionParticles, new Vector3(transform.position.x, transform.position.y, 0), transform.rotation);
        _rigidbody.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
        timeScaleScript.PlayerDead();
        Physics2D.IgnoreLayerCollision(10, 11, true);
        hurt = true;
        playerLives = 0;
        yield return new WaitForSeconds(waitTime + waitTime/2);
        hurt = false;
        if (!renderer.enabled)
            renderer.enabled = true;
        yield return new WaitForSeconds(waitTime/2);
        transform.position = activeCheckpoint.position;
        playerLives = 1;
        Physics2D.IgnoreLayerCollision(10, 11, false);

    }

    IEnumerator Blinking(float waitTime)
    {
        while (hurt)
        {
            renderer.enabled = false;
            yield return new WaitForSeconds(waitTime);
            renderer.enabled = true;
            yield return new WaitForSeconds(waitTime);
        }

    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Checkpoint" && activeCheckpoint != coll.gameObject.transform)
        {
            activeCheckpoint = coll.gameObject.transform;
            coll.GetComponent<AudioSource>().Play();
        }
    }

    public void DialogStart()
    {
        dialog = true;
        weapon.enabled = false;
    }

    public void DialogOver()
    {
        dialog = false;
        weapon.enabled = true;
    }

    public void Animator()
    {
        _animator.SetInteger("Lives", playerLives);

        if (shoot)
        {
            _animator.SetTrigger("Shoot");
            shoot = false;
        }

        if (translate != 0)
            _animator.SetBool("Moving", true);
        else
            _animator.SetBool("Moving", false);

        if (isOnGround)
            _animator.SetBool("Grounded", true);
        else
            _animator.SetBool("Grounded", false);
    }
}