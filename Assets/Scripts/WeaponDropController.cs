using UnityEngine;
using System.Collections;

public class WeaponDropController : MonoBehaviour {

    public GameObject weapon;

    [SerializeField]
    private Sprite[] weaponSpriteList;
    private SpriteRenderer weaponSprite;

    private PlayerMovement pm;
    private Rigidbody2D rb;
    private BoxCollider2D playerCollider;
    private BoxCollider2D _collider;

    private float ignoreT = 1f;

    void Start()
    {
        weaponSprite = transform.Find("Sprite").GetComponent<SpriteRenderer>();
        _collider = GetComponent<BoxCollider2D>();
        playerCollider = GameObject.Find("Player").GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        pm = GameObject.Find("Player").GetComponent<PlayerMovement>();

        rb.AddForce(new Vector2(0, 10), ForceMode2D.Impulse);
        Physics2D.IgnoreCollision(_collider, playerCollider, true);

        SetWeaponSprite();
    }

    void SetWeaponSprite()
    {
        switch (weapon.name)
        {

            case "Machinegun":
                weaponSprite.sprite = weaponSpriteList[0];
                break;

            case "Shotgun":
                weaponSprite.sprite = weaponSpriteList[1];
                break;

            case "TimeBomb":
                weaponSprite.sprite = weaponSpriteList[2];
                break;
        }
    }

    void Update()
    {

        if (ignoreT > 0)
            ignoreT -= 2 * Time.deltaTime;
        else
            Physics2D.IgnoreCollision(_collider, playerCollider, false);

        if (pm.playerLives <= 0)
            Destroy(gameObject);
    }

    void OnTriggerEnter2D (Collider2D coll)
    {
        if (coll.tag == "Hazard")
            Destroy(gameObject);
    }
}
