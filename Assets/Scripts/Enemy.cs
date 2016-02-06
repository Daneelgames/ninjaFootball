using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public int health = 10;

    public void Damage(int dmg)
    {
        health -= dmg;
        if (health <= 0)
            Destroy(gameObject);
    }
}
