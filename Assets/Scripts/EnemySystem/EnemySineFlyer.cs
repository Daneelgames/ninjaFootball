using UnityEngine;
using System.Collections;

public class EnemySineFlyer : MonoBehaviour {

    [SerializeField]
    private float hSpeed = 2;
    [SerializeField]
    private float vSpeed = 2;

    [SerializeField]
    private Rigidbody2D _rb;

    [SerializeField]
    private bool canFlip = true;
    private bool isVisible = false;

    private GameObject player;

    private GameObject sprite;

    private float curSpeedH = 0;
    private float curSpeedV = 0;

    private bool verticalAttack = false;

    void Start()
    {
        player = GameObject.Find("Player");

        if (canFlip)
            sprite = transform.Find("Sprite").gameObject;
    }

    void Update()
    {
        if (canFlip)
        {
            if (curSpeedH > 0)
                sprite.transform.localRotation = Quaternion.Euler(0, 0, 0);
            else
                sprite.transform.localRotation = Quaternion.Euler(0, 180, 0);
        }

        //Vertical attack
        if (!verticalAttack && transform.position.y > player.transform.position.y && Vector2.Distance(new Vector2(transform.position.x, 0), new Vector2(player.transform.position.x, 0)) < 0.25f)
        {
            verticalAttack = true;
        }
    }

    void FixedUpdate()
    {
        if (isVisible)
        {
            if (!verticalAttack)
                _rb.AddForce(new Vector2(curSpeedH, curSpeedV));
            else
                _rb.velocity = new Vector2(0, vSpeed * -2);

            //check horizontal
            if (curSpeedH > 0)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, 1f, 1 << 8);
                if (hit)
                {
                    ChangeCurHorizontalSpeed();
                    print("hit right");
                }
            }
            else if (curSpeedH < 0)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, 1f, 1 << 8);
                if (hit)
                {
                    ChangeCurHorizontalSpeed();
                    print("hit left");
                }
            }

            //check vertical
            if (curSpeedV > 0)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, 1f, 1 << 8);
                if (hit)
                {
                    ChangeCurVerticalSpeed();
                    print("hit up");
                }
            }
            else if (curSpeedV < 0)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1f, 1 << 8);
                if (hit)
                {
                    ChangeCurVerticalSpeed();
                    print("hit down");
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Zone")
        {
            SetSpeedH();

            isVisible = true;

            if (transform.position.y < player.transform.position.y)
                curSpeedV = -1;
            else
                curSpeedV = 1;
        }
    }

    void SetSpeedH()
    {
        if (transform.position.x >= player.transform.position.x)
            curSpeedH = hSpeed * -1;
        else
            curSpeedH = hSpeed;
    }

    void ChangeCurVerticalSpeed()
    {
        SetSpeedH();
        verticalAttack = false;

        if (curSpeedV > 0)
            curSpeedV = vSpeed * -1;
        else
            curSpeedV = vSpeed;
    }

    void ChangeCurHorizontalSpeed()
    {
        if (curSpeedH > 0)
            curSpeedH = hSpeed * -1;
        else if (curSpeedV < 0)
            curSpeedH = hSpeed;
    }

    void OnTriggerExit2D(Collider2D coll)
    {
         if (coll.tag == "Room")
         {
            Destroy(gameObject);
         }
     }
}
