using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WeaponUiController : MonoBehaviour {

    [SerializeField]
    private Sprite[] spriteList;

    private Weapon playerWeaponScript;
    private GameObject playerAltWeapon;
    private Image weaponSprite;

    void Start()
    {
        weaponSprite = GetComponent<Image>();
        playerWeaponScript = GameObject.Find("Player").GetComponent<Weapon>();
        InvokeRepeating("GetWeapon", 0.1f, 0.1f);
    }

    void GetWeapon()
    {
        if (playerWeaponScript.altWeapon == null)
            weaponSprite.sprite = spriteList[0];
        else
        {
            playerAltWeapon = playerWeaponScript.altWeapon;
            string altWeaponName = playerAltWeapon.name;

            switch (altWeaponName)
            {
                case "Machinegun":
                    weaponSprite.sprite = spriteList[1];
                    break;

                case "Shotgun":
                    weaponSprite.sprite = spriteList[2];
                    break;

                case "TimeBomb":
                    weaponSprite.sprite = spriteList[3];
                    break;
            }
        }
    }
}
