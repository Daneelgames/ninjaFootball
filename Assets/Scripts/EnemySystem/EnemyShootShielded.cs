using UnityEngine;
using System.Collections;

public class EnemyShootShielded : MonoBehaviour {

    [SerializeField]
    private GameObject projectile;
    [SerializeField]
    private float attackTimer;

    private bool isVisible = false;
    private GameObject sprite;
    private Animator spriteAnimator;
    private GameObject shotHolder;
    private EnemyHealth enemyHealthScript;

    private bool canStartCoroutine = true;

    void Start()
    {
        enemyHealthScript = GetComponent<EnemyHealth>();
        shotHolder = transform.Find("ShotHolder").gameObject;
        sprite = transform.Find("Sprite").gameObject;
        spriteAnimator = sprite.GetComponent<Animator>();
    }

    void Update()
    {
        if (canStartCoroutine && isVisible)
            StartCoroutine(Shoot(attackTimer));
    }

    IEnumerator Shoot(float waitTime)
    {  
        canStartCoroutine = false;
        enemyHealthScript.invincible = true;
        yield return new WaitForSeconds(waitTime);
        enemyHealthScript.invincible = false;
        spriteAnimator.SetTrigger("Shoot");
        yield return new WaitForSeconds(waitTime);
        Instantiate(projectile, shotHolder.transform.position, transform.rotation);
        yield return new WaitForSeconds(waitTime*2);
        enemyHealthScript.invincible = true;
        yield return new WaitForSeconds(waitTime * 2);
        canStartCoroutine = true;
    }

    void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.tag == "Zone")
            isVisible = true;
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.tag == "Zone")
            isVisible = false;
    }
}
