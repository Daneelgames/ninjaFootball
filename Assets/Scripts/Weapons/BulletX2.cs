using UnityEngine;
using System.Collections;

public class BulletX2 : MonoBehaviour {

    public GameObject bullet;

    void Start ()
    {
        var bullet_1 = Instantiate(bullet, new Vector2(transform.position.x, transform.position.y - 0.1f), gameObject.transform.rotation) as GameObject;
        var bullet_2 = Instantiate(bullet, new Vector2(transform.position.x, transform.position.y + 0.1f), gameObject.transform.rotation) as GameObject;

        bullet_1.GetComponent<Bullet>().hRandom = 2f;
        bullet_2.GetComponent<Bullet>().hRandom = 2f;

        Destroy(gameObject);
    }
}
