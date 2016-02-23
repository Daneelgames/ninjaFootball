using UnityEngine;
using System.Collections;

public class EnemyDosehadsShooter : MonoBehaviour {

    [SerializeField]
    private GameObject projectile;
    [SerializeField]
    private float attackTime = 1f;

    private GameObject player;
    private GameObject head_1;
    private GameObject head_2;
    private GameObject head_3;
    private Animator animator_1;
    private Animator animator_2;
    private Animator animator_3;
    private bool canStartCoroutine = true;

    void Start()
    {
        player = GameObject.Find("Player");

        head_1 = transform.Find("Head_1").gameObject;
        head_2 = transform.Find("Head_2").gameObject;
        head_3 = transform.Find("Head_3").gameObject;
        animator_1 = head_1.transform.Find("Sprite").GetComponent<Animator>();
        animator_2 = head_2.transform.Find("Sprite").GetComponent<Animator>();
        animator_3 = head_3.transform.Find("Sprite").GetComponent<Animator>();
    }

    void Update ()
    {
        if (canStartCoroutine)
            StartCoroutine(Shoot(attackTime));
    }

    IEnumerator Shoot(float waitTime)
    {
        float _directionH;
        if (transform.position.x > player.transform.position.x)
            _directionH = -1f;
        else
            _directionH = 1f;

        canStartCoroutine = false;
        yield return new WaitForSeconds(waitTime * 0.5f);
        animator_1.SetTrigger("Shoot");
        yield return new WaitForSeconds(waitTime * 0.5f);
        GameObject lastBullet_1 = Instantiate(projectile, head_1.transform.position, transform.rotation) as GameObject;
        lastBullet_1.GetComponent<EnemyBullet>().direction = new Vector2(_directionH, 0);
        yield return new WaitForSeconds(waitTime * 0.5f);

        animator_2.SetTrigger("Shoot");
        yield return new WaitForSeconds(waitTime * 0.5f);
        GameObject lastBullet_2 = Instantiate(projectile, head_2.transform.position, transform.rotation) as GameObject;
        lastBullet_2.GetComponent<EnemyBullet>().direction = new Vector2(_directionH, -0.25f);
        yield return new WaitForSeconds(waitTime * 0.5f);

        animator_3.SetTrigger("Shoot");
        yield return new WaitForSeconds(waitTime * 0.5f);
        GameObject lastBullet_3 = Instantiate(projectile, head_3.transform.position, transform.rotation) as GameObject;
        lastBullet_3.GetComponent<EnemyBullet>().direction = new Vector2(_directionH, -0.2f);
        yield return new WaitForSeconds(waitTime * 0.5f);
        canStartCoroutine = true;
    }

}
