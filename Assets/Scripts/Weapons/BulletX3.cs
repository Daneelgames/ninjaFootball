using UnityEngine;
using System.Collections;

public class BulletX3 : MonoBehaviour {

    public GameObject bullet;

    void Start ()
    {
        var bullet_1 = Instantiate(bullet, new Vector2(transform.position.x, transform.position.y - 0.3f), gameObject.transform.rotation) as GameObject;
        var bullet_2 = Instantiate(bullet, new Vector2(transform.position.x, transform.position.y), gameObject.transform.rotation) as GameObject;
        var bullet_3 = Instantiate(bullet, new Vector2(transform.position.x, transform.position.y + 0.3f), gameObject.transform.rotation) as GameObject;

        bullet_1.GetComponent<Bullet>().hRandom = 7f;
        bullet_2.GetComponent<Bullet>().hRandom = 7f;
        bullet_3.GetComponent<Bullet>().hRandom = 7f;

        Destroy(gameObject);
    }
}
