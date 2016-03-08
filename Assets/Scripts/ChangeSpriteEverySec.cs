using UnityEngine;
using System.Collections;

public class ChangeSpriteEverySec : MonoBehaviour {

    [SerializeField]
    private Sprite[] sprites;
    [SerializeField]
    private SpriteRenderer _renderer;
    [SerializeField]
    private float tPeriod;

    
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Zone")
        {
            InvokeRepeating("ChangeSprite", tPeriod, tPeriod);
        }
    }

    void OnTriggerExit2D (Collider2D coll)
    {
        if (coll.tag == "Zone")
            CancelInvoke("ChangeSprite");
    }

    void ChangeSprite()
    {
        int newSprite = Random.Range(0, sprites.Length);
        _renderer.sprite = sprites[newSprite];
    }
}
