﻿using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

    public int health = 10;

    [ReadOnly]
    public SpriteRenderer spriteRednerer;
    [ReadOnly]
    public TimeScale timeScaleScript;
    public Transform explosion;

    private AudioSource _audio;

    [SerializeField]
    private GameObject drop;
    [SerializeField]
    private int minDrop;
    [SerializeField]
    private int maxDrop;

    void Start()
    {
        timeScaleScript = GameObject.FindGameObjectWithTag("Player").GetComponent<TimeScale>();
        _audio = GetComponent<AudioSource>() as AudioSource;
        spriteRednerer = GetComponentInChildren<SpriteRenderer>() as SpriteRenderer;
    }

    public void Damage(int dmg)
    {
        StartCoroutine(Blink());
        _audio.Play();
        _audio.pitch = Random.Range(.7f, 1.3f);
        health -= dmg;
        if (health <= 0)
            EnemyDestroy();
    }

    IEnumerator Blink()
    {
        spriteRednerer.material.color = Color.magenta;
        yield return new WaitForSeconds(.1F);
        spriteRednerer.material.color = Color.white;
    }

    void EnemyDestroy()
    {
        Instantiate(explosion, new Vector3(transform.position.x, transform.position.y, 0), transform.rotation);
        Destroy(gameObject);
        for (int i = 0; i < Random.Range(minDrop, maxDrop); i++)
            Instantiate(drop, transform.position, transform.rotation);
    }
}
