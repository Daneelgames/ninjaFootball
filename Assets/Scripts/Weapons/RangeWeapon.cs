using UnityEngine;
using System.Collections;

public class RangeWeapon : MonoBehaviour {

    [SerializeField]
    private GameObject[] projectile;
    [SerializeField]
    private Transform shotHolder;

    [SerializeField]
    private TimeScale timeScale;

    [SerializeField]
    private int damage0 = 5;
    [SerializeField]
    private int damage1 = 10;
    [SerializeField]
    private int damage2 = 20;

    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private float coolDownS = 0.25f;
    [SerializeField]
    private float coolDownM = 0.75f;
    [SerializeField]
    private float coolDownB = 1.5f;

    private float coolDownCur = 0f;

    [SerializeField]
    private PlayerMovement _pm;
    

    void Update()
    {
        if (coolDownCur > 0f)
            coolDownCur -= 1f * Time.deltaTime;
        else if (coolDownCur < 0f)
            coolDownCur = 0f;
    }

    public void Attack0()
    {
        if (coolDownCur == 0)
        {
            _animator.SetTrigger("Shoot");
            timeScale.Shoot();
            coolDownCur = coolDownS;
            GameObject lastProjectile = Instantiate(projectile[0], shotHolder.position, Quaternion.Euler(0, 0, 0)) as GameObject;
            BulletMovementController controller = lastProjectile.GetComponent<BulletMovementController>() as BulletMovementController;
            controller.damage = damage0;
            if (_pm.playerDirection == Direction.RIGHT)
            {
                controller.bulletDirection = Direction.RIGHT;
            }
            else
            {
                controller.bulletDirection = Direction.LEFT;
            }
        }
    }

    public void Attack1()
    {
        if (coolDownCur == 0)
        {
            _animator.SetTrigger("Shoot");
            timeScale.Shoot();
            coolDownCur = coolDownM;
            GameObject lastProjectile = Instantiate(projectile[1], shotHolder.position, Quaternion.Euler(0, 0, 0)) as GameObject;
            BulletMovementController controller = lastProjectile.GetComponent<BulletMovementController>() as BulletMovementController;
            controller.damage = damage1;
            if (_pm.playerDirection == Direction.RIGHT)
            {
                controller.bulletDirection = Direction.RIGHT;
            }
            else
            {
                controller.bulletDirection = Direction.LEFT;
            }
        }
    }

    public void Attack2()
    {
        if (coolDownCur == 0)
        {
            _animator.SetTrigger("Shoot");
            timeScale.Shoot();
            coolDownCur = coolDownB;
            GameObject lastProjectile = Instantiate(projectile[2], shotHolder.position, Quaternion.Euler(0, 0, 0)) as GameObject;
            BulletMovementController controller = lastProjectile.GetComponent<BulletMovementController>() as BulletMovementController;
            controller.damage = damage2;
            if (_pm.playerDirection == Direction.RIGHT)
            {
                controller.bulletDirection = Direction.RIGHT;
            }
            else
            {
                controller.bulletDirection = Direction.LEFT;
            }
        }
    }
}
