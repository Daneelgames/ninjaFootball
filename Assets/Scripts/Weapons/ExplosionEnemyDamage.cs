using UnityEngine;
using System.Collections;

public class ExplosionEnemyDamage : MonoBehaviour {

    public int damage = 10;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            other.gameObject.GetComponent<EnemyHealth>().Damage(damage);
        }

    }
}
