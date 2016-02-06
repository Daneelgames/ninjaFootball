using UnityEngine;
using System.Collections;

public enum Direction {LEFT, RIGHT};
public class PlayerMovement : MonoBehaviour
{
    public float speed = 2f;
    public bool isOnGround = false;
    public float jumpPower = 7.0f;
    public float tJump = 0f;

    //private Transform _transform;
    private Rigidbody2D _rigidbody;
    private float jumpDirection = 0.0f;
    private Direction playerDirection = Direction.RIGHT;

    public Direction PlayerDirection
    {
        get {
            return playerDirection;
        }
    }

    // Use this for initialization
    void Start()
    {
        //_transform = GetComponent(typeof(Transform)) as Transform;
        _rigidbody = GetComponent(typeof(Rigidbody2D)) as Rigidbody2D;
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        Jump();
        GroundRaycast();
    }

    void MovePlayer()
    {
        float translate = Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime;
        transform.Translate(translate, 0, 0);

        if (translate > 0)
        {
            playerDirection = Direction.RIGHT;
        }
        else if (translate < 0)
        {
            playerDirection = Direction.LEFT;
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
            }
        }
        //Mid-air horizontal movement
        else
        {
            if (Input.GetAxisRaw("Horizontal") != 0)
            {
                jumpDirection = Input.GetAxisRaw("Horizontal") * speed/3 * Time.deltaTime;
            }
            transform.Translate(jumpDirection/2, 0, 0);
        }
    }

    void GroundRaycast()
    {
        if (!isOnGround)
        {
            if (tJump == 0)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.1f, 1 << 8);
                if (hit.collider != null)
                {
                    isOnGround = true;
                    jumpDirection = 0;
                }
            }
            if (tJump > 0)
                tJump -= 10 * Time.deltaTime;
            else if (tJump < 0)
                tJump = 0;

        }

    }
}