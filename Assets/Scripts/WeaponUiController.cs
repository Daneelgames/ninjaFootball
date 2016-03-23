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
        InvokeRepeating("GetWeapon", 0.1f, 0.1f);
    }

    void GetWeapon()
    {
        if (player == null)
        {
            player = GameObject.Find("Player");
        }

        else if(player != null)
        {
            if (playerWeaponScript == null)
                playerWeaponScript = player.GetComponent<Weapon>();

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
}
