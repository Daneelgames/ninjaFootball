using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SaveManager : MonoBehaviour {

    public Dictionary<GameObject, Vector3> objectsOnScene;

    private GameObject player;
    private Vector3 playerPos;
    private int playerAmmo;
    private GameObject playerExtraWeapon;

    void Start()
    {
        player = GameObject.Find("Player");
    }

    public void Save()
    {
        playerPos = player.transform.position;
        playerAmmo = player.GetComponent<Weapon>().altWeaponAmmo;
        playerExtraWeapon = player.GetComponent<Weapon>().altWeapon;


    }

    void Load()
    {

    }
}
