using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

    public int health = 10;

    private AudioSource _audio;

    void Awake()
    {
        _audio = GetComponent<AudioSource>() as AudioSource;
    }

    public void Damage(int dmg)
    {
        _audio.Play();
        _audio.pitch = Random.Range(.7f, 1.3f);
        health -= dmg;
        if (health <= 0)
            OnDestroy();
    }

    void OnDestroy()
    {
        Destroy(gameObject);
    }
}
