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

    private RaycastHit2D hit;

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
        hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 0.25f), new Vector2(transform.position.x + speed, transform.position.y + 0.25f), 0.5f, 1 << 8);
    }

    void CheckCast()
    {
        if (hit == true)
            speed *= -1;
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
