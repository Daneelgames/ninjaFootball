using UnityEngine;
using System.Collections;

public class EnemyBulletX3 : MonoBehaviour {

    public GameObject bullet;

    private float hDirection;

    void Start ()
    {
        if (transform.position.x < GameObject.Find("Player").transform.position.x)
            hDirection = 1f;
        else
            hDirection = -1f;


        var bullet_1 = Instantiate(bullet, new Vector2(transform.position.x, transform.position.y), gameObject.transform.rotation) as GameObject;
        var bullet_2 = Instantiate(bullet, new Vector2(transform.position.x, transform.position.y), gameObject.transform.rotation) as GameObject;
        var bullet_3 = Instantiate(bullet, new Vector2(transform.position.x, transform.position.y), gameObject.transform.rotation) as GameObject;

        bullet_1.GetComponent<EnemyBullet>().direction = new Vector2(hDirection, 1).normalized;
        bullet_2.GetComponent<EnemyBullet>().direction = new Vector2(hDirection, 0).normalized;
        bullet_3.GetComponent<EnemyBullet>().direction = new Vector2(hDirection, -1).normalized;

        Destroy(gameObject);
    }
}
