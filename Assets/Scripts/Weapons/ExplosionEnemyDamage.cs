﻿using UnityEngine;
using System.Collections;

public class ExplosionEnemyDamage : MonoBehaviour {

    public int damage = 10;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            other.gameObject.GetComponent<EnemyHealth>().Damage(damage);
        }

        if (other.tag == "Hazard")
        {
            other.gameObject.GetComponent<EnemyHealth>().Damage(damage);
        }

        if (other.tag == "Ground")
        {
            other.gameObject.GetComponent<ProceduralTileController>().Damage(damage);
        }

    }
}
