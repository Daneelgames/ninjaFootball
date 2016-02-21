﻿using UnityEngine;
using System.Collections;

public class EnemyMoveInRange : MonoBehaviour {

    [SerializeField]
    private float speed = 2;
    [SerializeField]
    private float hDir = 1;

    private GameObject sprite;

    void Start()
    {
        sprite = transform.Find("Sprite").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (speed > 0)
            sprite.transform.localRotation = Quaternion.Euler(0, 0, 0);
        else
            sprite.transform.localRotation = Quaternion.Euler(0, 180, 0);
        // hDir не меняет направление движения, хз почему
        transform.Translate(hDir * speed * Time.deltaTime, 0, 0);
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "EnemyTurner")
            speed *= -1;
    }
}
