using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WeaponUiController : MonoBehaviour {

    [SerializeField]
    private Sprite[] spriteList;

    private GameObject player;
    private Weapon playerWeaponScript;
    private GameObject playerAltWeapon;
    private Image weaponSprite;

    void Start()
    {
        weaponSprite = GetComponent<Image>();
        player = GameObject.Find("Player");
        if (player != null)
            playerWeaponScript = player.GetComponent<Weapon>();

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
