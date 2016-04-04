using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Weapon : MonoBehaviour {

    public GameObject[] mainWeapon;
    [SerializeField]
    private GameObject _weaponDrop;

    [HideInInspector]
    public int[] weaponLevel;

    [HideInInspector]
    public int activeWeapon = 0;
    [SerializeField]
    private float reloadTimeMax = 0.5f;
    [SerializeField]
    private float reloadTimeMaxAlt = 1f;
    private float reloadTimeCur = 0;
    private float reloadTimeCurAlt = 0;
    
    private GameObject shotPosition;
    private PlayerSounds playerSound;
    private Animator canvasAnimator;
    private PlayerMovement playerMovement;
    private GameObject _lastAltInstance = null;
    private int _altWeaponCost;
    private Text ammoCounter;
    private TimeScale timeScale;

    private WeaponLevelUIController lvl1;
    private WeaponLevelUIController lvl2;

    // Use this for initialization
    void Start ()
    {
        lvl1 = GameObject.Find("Lvl1WeaponBar").GetComponent<WeaponLevelUIController>();
        lvl2 = GameObject.Find("Lvl2WeaponBar").GetComponent<WeaponLevelUIController>();

        timeScale = GetComponent<TimeScale>();
        shotPosition = transform.Find("PlayerSprites/Shot").gameObject;
        playerSound = transform.Find("PlayerSprites").GetComponent<PlayerSounds>();
        canvasAnimator = GameObject.Find("Canvas").GetComponent<Animator>();
        ammoCounter = GameObject.Find("AmmoCounter").GetComponent<Text>();
        playerMovement = GetComponent<PlayerMovement>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        SetWeaponLevel();

        if (playerMovement.playerLives > 0)
        {
            Shooting();
            Reload();
        }
    }

    void SetWeaponLevel()
    {
        if (weaponLevel[activeWeapon] <= 50)
            lvl1.curExpForLvl = weaponLevel[activeWeapon];
        else if (weaponLevel[activeWeapon] >= 50)
            lvl2.curExpForLvl = weaponLevel[activeWeapon];
    }

    void Shooting()
    {

        if (Input.GetButtonDown("Fire1") && reloadTimeCur == 0 && mainWeapon != null)
        {
            playerMovement.shoot = true;
            Instantiate(mainWeapon[activeWeapon], new Vector2(shotPosition.transform.position.x, shotPosition.transform.position.y), gameObject.transform.rotation);
            timeScale.Shoot();
            reloadTimeCur = reloadTimeMax;
        }
    }

    void Reload()
    {
        if (reloadTimeCur > 0)
            reloadTimeCur -= 1f * Time.deltaTime;
        else if (reloadTimeCur < 0)
            reloadTimeCur = 0;

        if (reloadTimeCurAlt > 0)
            reloadTimeCurAlt -= 1f * Time.deltaTime;
        else if (reloadTimeCurAlt < 0)
            reloadTimeCurAlt = 0;
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "PlayerAmmoDrop")
        {
            weaponLevel[activeWeapon] += coll.gameObject.GetComponent<DropController>().amount;
            Destroy(coll.gameObject);
            playerSound.PlaySound(4);
            canvasAnimator.SetTrigger("GetAmmo");
        }
    }
}
