using UnityEngine;
using System.Collections;

public class MeleeWeapon : MonoBehaviour {

    [SerializeField]
    private Collider2D _collider;
    [SerializeField]
    private int damage0 = 5;
    [SerializeField]
    private int damage1 = 10;
    [SerializeField]
    private int damage2 = 20;
    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private TimeScale timeScale;

    [SerializeField]
    private float coolDownS = 0.25f;
    [SerializeField]
    private float coolDownM = 0.75f;
    [SerializeField]
    private float coolDownB = 1.5f;

    [SerializeField]
    private GameObject swordHit;
    private Transform hitHolder;


    private float coolDownCur = 0f;

    private int level = 0;
    
    void Start()
    {
        hitHolder = transform.GetChild(0);
    }

    void Update()
    {
        if (coolDownCur > 0f)
            coolDownCur -= 1f * Time.deltaTime;
        else if (coolDownCur < 0f)
            coolDownCur = 0f;
    }

    public void CreateFlash()
    {
        GameObject swordFlash = Instantiate(swordHit, hitHolder.position, hitHolder.rotation) as GameObject;
        swordFlash.transform.localScale = transform.localScale;
    }

    public void Attack0()
    {
        if (coolDownCur <= 0)
        {
            int chance = Random.Range(0, 3);

            level = 0;
            timeScale.Shoot();

            switch (chance)
                {
                case 0:
                    _animator.SetTrigger("0_0");
                    break;
                case 1:
                    _animator.SetTrigger("0_1");
                    break;
                case 2:
                    _animator.SetTrigger("0_2");
                    break;
                }
            coolDownCur = coolDownS;
        }
    }

    public void Attack1()
    {
        if (coolDownCur <= 0)
        {
            int chance = Random.Range(0, 3);

            level = 1;
            timeScale.Shoot();

            switch (chance)
            {
                case 0:
                    _animator.SetTrigger("1_0");
                    break;
                case 1:
                    _animator.SetTrigger("1_1");
                    break;
                case 2:
                    _animator.SetTrigger("1_2");
                    break;
            }
            coolDownCur = coolDownM;
        }
    }

    public void Attack2()
    {
        if (coolDownCur <= 0)
        {
            int chance = Random.Range(0, 3);

            level = 2;
            timeScale.Shoot();

            switch (chance)
            {
                case 0:
                    _animator.SetTrigger("2_0");
                    break;
                case 1:
                    _animator.SetTrigger("2_1");
                    break;
                case 2:
                    _animator.SetTrigger("2_2");
                    break;
            }
            coolDownCur = coolDownB;
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Enemy")
        {
            if (level == 0)
                coll.GetComponent<EnemyHealth>().Damage(damage0);
            else if (level == 1)
                coll.GetComponent<EnemyHealth>().Damage(damage1);
            else if (level == 2)
                coll.GetComponent<EnemyHealth>().Damage(damage2);
        }
    }
}
