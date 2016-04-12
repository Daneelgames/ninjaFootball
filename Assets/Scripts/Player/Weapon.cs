using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Weapon : MonoBehaviour {

    public List<GameObject> weaponList;
    [SerializeField]
    private List<GameObject> allWeapons;

    public List<int> weaponLevel;
    
    [SerializeField]
    private Sprite[] weaponIcons;

    private Image activeWeaponIcon;

    [HideInInspector]
    public int activeWeapon = 0;
    
    private PlayerSounds playerSound;
    private Animator canvasAnimator;
    private PlayerMovement playerMovement;
    
    private Text ammoCounter;
    
    private WeaponLevelUIController lvl1;
    private WeaponLevelUIController lvl2;

    [SerializeField]
    private PlayerFeedbackTextController feedbackController;

    private WeaponLevelController activeWeaponLevelController;

    // Use this for initialization
    void Start ()
    {
        activeWeaponIcon = GameObject.Find("WeaponIcon").GetComponent<Image>();

        lvl1 = GameObject.Find("Lvl1WeaponBar").GetComponent<WeaponLevelUIController>();
        lvl2 = GameObject.Find("Lvl2WeaponBar").GetComponent<WeaponLevelUIController>();

        activeWeaponLevelController = weaponList[activeWeapon].GetComponent<WeaponLevelController>();
        activeWeaponIcon.sprite = weaponIcons[activeWeapon];

        SetWeaponLevel();
        
        playerSound = transform.Find("PlayerSprites").GetComponent<PlayerSounds>();
        canvasAnimator = GameObject.Find("Canvas").GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();

        InvokeRepeating("GetExpBars", 1f, 1f);
	}
	
    void GetExpBars()
    {
        if (lvl1 == null || lvl2 == null)
        {
            lvl1 = GameObject.Find("Lvl1WeaponBar").GetComponent<WeaponLevelUIController>();
            lvl2 = GameObject.Find("Lvl2WeaponBar").GetComponent<WeaponLevelUIController>();

            CancelInvoke("GetExpBars");
        }
    }

	// Update is called once per frame
	void Update ()
    {
        if (playerMovement.playerLives > 0)
        {
            Shooting();
            ChangeWeapon();
        }
    }

    public void SetWeaponLevel()
    {
        for (int i = 0; i < weaponList.Count; i++)
        {
            if (weaponList[i] != weaponList[activeWeapon])
                weaponList[i].SetActive(false);
            else
                weaponList[i].SetActive(true);
        }

        lvl1.curExpForLvl = weaponLevel[activeWeapon];
        lvl2.curExpForLvl = weaponLevel[activeWeapon];

        if (weaponLevel[activeWeapon] < 50)
        {
            weaponList[activeWeapon].GetComponent<WeaponLevelController>().level = 0;
        }
        else if (weaponLevel[activeWeapon] >= 50 && weaponLevel[activeWeapon] < 100)
        {
            weaponList[activeWeapon].GetComponent<WeaponLevelController>().level = 1;
        }
        else if (weaponLevel[activeWeapon] >= 100)
        {
            weaponList[activeWeapon].GetComponent<WeaponLevelController>().level = 2;
        }
    }

    void Shooting()
    {

        if (Input.GetButtonDown("Fire1"))
        {
            activeWeaponLevelController.Attack();
            playerMovement.shoot = true;
        }
    }

    void ChangeWeapon()
    {
        if (Input.GetButtonDown("WeaponNext"))
        {
            if (activeWeapon != weaponList.Count-1)
                activeWeapon += 1;
            else
                activeWeapon = 0;

            canvasAnimator.SetTrigger("GetWeapon");
            activeWeaponIcon.sprite = weaponIcons[activeWeapon];
            SetWeaponLevel();
            activeWeaponLevelController = weaponList[activeWeapon].GetComponent<WeaponLevelController>();
        }
        else if (Input.GetButtonDown("WeaponPrev"))
        {
            if (activeWeapon != 0)
                activeWeapon -= 1;
            else
                activeWeapon = weaponList.Count - 1;

            canvasAnimator.SetTrigger("GetWeapon");
            activeWeaponIcon.sprite = weaponIcons[activeWeapon];
            SetWeaponLevel();
            activeWeaponLevelController = weaponList[activeWeapon].GetComponent<WeaponLevelController>();
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "PlayerAmmoDrop")
        {
            int exp = coll.gameObject.GetComponent<DropController>().amount;
            weaponLevel[activeWeapon] += exp;
            feedbackController.GetExp(exp);
            SetWeaponLevel();
            
            Destroy(coll.gameObject);
            playerSound.PlaySound(4);
            canvasAnimator.SetTrigger("GetExp");
        }
    }

    public void GetNewWeapon(string weaponName)
    {
        GameObject newWeapon = null;

        for (int i = 0; i < allWeapons.Count; i ++)
        {
            if (allWeapons[i].name == weaponName);
                newWeapon = allWeapons[i];
        }

        weaponList.Add(newWeapon);
        feedbackController.GetWeapon(newWeapon.name);
        weaponLevel.Add(0);
        activeWeapon = weaponList.Count - 1;
        canvasAnimator.SetTrigger("GetWeapon");
        activeWeaponIcon.sprite = weaponIcons[activeWeapon];
        SetWeaponLevel();
        activeWeaponLevelController = weaponList[activeWeapon].GetComponent<WeaponLevelController>();
    }
}