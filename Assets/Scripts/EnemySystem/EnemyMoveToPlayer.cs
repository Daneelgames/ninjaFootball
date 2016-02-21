using UnityEngine;
using System.Collections;

public class EnemyMoveToPlayer : MonoBehaviour
{
    [SerializeField]
    private float speed = 10;
    [SerializeField]
    private bool isVisible = false;
    private float hDir;
    private Transform player;
    private GameObject sprite;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player").transform;
        sprite = transform.Find("Sprites").gameObject;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (isVisible)
        {
            transform.Translate(hDir * speed * Time.deltaTime, 0, 0);
        }

        if (player.transform.position.x - 1f > transform.position.x)
            {
               sprite.transform.localRotation = Quaternion.Euler(0, 0, 0);
                hDir = 1.0f;
            }
        else if (player.transform.position.x + 1f < transform.position.x)
            {
              sprite.transform.localRotation = Quaternion.Euler(0, 180, 0);
                hDir = -1.0f;
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
