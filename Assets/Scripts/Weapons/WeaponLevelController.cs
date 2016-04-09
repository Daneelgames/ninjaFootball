using UnityEngine;
using System.Collections;

public class WeaponLevelController : MonoBehaviour {

    public int level = 0;

    public void Attack()
    {
        if (level == 0)
        {
            print("lvl_0");
        }
        else if (level == 1)
        {
            print("lvl_1");
        }
        if (level == 2)
        {
            print("lvl_2");
        }
    }
}
