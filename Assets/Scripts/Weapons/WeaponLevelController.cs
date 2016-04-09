using UnityEngine;
using System.Collections;

public class WeaponLevelController : MonoBehaviour {

    public int level = 0;
    [SerializeField]
    private bool melee = false;

    private MeleeWeapon meleeController = null;
    private RangeWeapon rangeController = null;

    void Start()
    {
        if (melee)
            meleeController = GetComponent<MeleeWeapon>();
        else
            rangeController = GetComponent<RangeWeapon>();

    }

    public void Attack()
    {
        if (melee)
        {
            if (level == 0)
            {
                meleeController.Attack0();
            }
            else if (level == 1)
            {
                meleeController.Attack1();
            }
            if (level == 2)
            {
                meleeController.Attack2();
            }

        }
        else
            {

            if (level == 0)
            {
                rangeController.Attack0();
            }
            else if (level == 1)
            {
                rangeController.Attack1();
            }
            if (level == 2)
            {
                rangeController.Attack2();
            }
        }
    }
}
