using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

    public int health = 10;
    public bool invincible = false;

    [SerializeField]
    private Transform explosion;

    [SerializeField]
    private SpriteRenderer spriteRednerer;
    private bool canDestroy = true;


    public void Damage(int dmg)
    {
        if (!invincible)
        {
            health -= dmg;
            StartCoroutine(Blink());
            if (health <= 0)
                EnemyDestroy();
        }
    }

    IEnumerator Blink()
    {
        spriteRednerer.material.color = Color.magenta;
        yield return new WaitForSeconds(.1F);
        spriteRednerer.material.color = Color.white;
    }

    void EnemyDestroy()
    {
        if (canDestroy)
        {
            canDestroy = false;
            Instantiate(explosion, new Vector3(transform.position.x, transform.position.y, 0), transform.rotation);
            Destroy(gameObject, 0.1f);
        }
    }
}
