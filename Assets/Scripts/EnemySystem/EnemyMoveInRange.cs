using UnityEngine;
using System.Collections;

public class EnemyMoveInRange : MonoBehaviour {

    [SerializeField]
    private float speed = 2;
    [SerializeField]
    private float hDir = 0;
    [SerializeField]
    private float vDir = 0;
    [SerializeField]
    private bool canFlip = true;

    private GameObject sprite;
    [SerializeField]
    private bool visible = false;

    private RaycastHit2D hitRight;
    private RaycastHit2D hitLeft;

    void Start()
    {
        if (canFlip)
            sprite = transform.Find("Sprite").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        CheckCast();

        if (canFlip)
        {
            if (speed > 0)
                sprite.transform.localRotation = Quaternion.Euler(0, 0, 0);
            else
                sprite.transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        // hDir не меняет направление движения, хз почему
        if (visible)
            transform.Translate(hDir * speed * Time.deltaTime, vDir * speed * Time.deltaTime, 0);
    }

    void FixedUpdate()
    {
        if (visible)
        {
            hitRight = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 0.5f), Vector2.right, 0.8f, 1 << 8);
            hitLeft = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 0.5f), Vector2.left, 0.8f, 1 << 8);
        }
    }

    void CheckCast()
    {
        if (hitRight)
            speed = -1.5f;

        else if (hitLeft)
            speed = 1.5f;
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Zone")
            visible = true;
    }

    void OnTriggerExit2D (Collider2D coll)
    {
        if (coll.tag == "Zone")
            visible = false;
    }
}
