using UnityEngine;
using System.Collections;

public class EnemyJumper : MonoBehaviour {

    [SerializeField]
    private float jumpPower;
    [SerializeField]
    private float waitBeforeJumpMax;
    [SerializeField]
    private float waitBeforeJumpCur = 0;
    [SerializeField]
    private bool isOnGround = false;
    [SerializeField]
    private AudioSource _audio;

    private GameObject player;
    private GameObject sprite;
    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private bool isVisible = false;
    private float hDir = 1f;
    private float tJump = 0f;
    private bool canLand = true;

    void Start ()
    {
        player = GameObject.Find("Player");
        sprite = transform.Find("Sprite").gameObject;
        _animator = sprite.GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Jump();
        GroundRaycast();

        if (transform.position.x < player.transform.position.x)
        {
            sprite.transform.localRotation = Quaternion.Euler(0, 0, 0);
            hDir = 1f;
        }
        else
        {
            sprite.transform.localRotation = Quaternion.Euler(0, 180, 0);
            hDir = -1f;
        }

        if (isOnGround && waitBeforeJumpCur > 0)
            waitBeforeJumpCur -= 1f * Time.deltaTime;
    }

    void Jump()
    {
        if (isOnGround && waitBeforeJumpCur <= 0 && isVisible)
        {
            _animator.SetTrigger("Jump");
            _audio.Play();
            _audio.pitch = Random.Range(0.8f, 1.2f);
            _rigidbody.AddForce(new Vector2(hDir * jumpPower, jumpPower * 1.5f), ForceMode2D.Impulse);
            isOnGround = false;
            tJump = 1f;
            waitBeforeJumpCur = waitBeforeJumpMax;
        }
    }

    void GroundRaycast()
    {
        if (tJump == 0)
        {
            RaycastHit2D hit0 = Physics2D.Raycast(new Vector2(transform.position.x - 0.4f, transform.position.y), Vector2.down, 0.1f, 1 << 8);
            RaycastHit2D hit1 = Physics2D.Raycast(transform.position, Vector2.down, 0.1f, 1 << 8);
            RaycastHit2D hit2 = Physics2D.Raycast(new Vector2(transform.position.x + 0.4f, transform.position.y), Vector2.down, 0.1f, 1 << 8);
            if (hit0.collider != null || hit1.collider != null || hit2.collider != null)
            {
                isOnGround = true;
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

    void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.tag == "Zone")
            isVisible = true;
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.tag == "Zone")
            isVisible = false;
    }
}
