using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Weapon : MonoBehaviour {

    [SerializeField]
    private GameObject mainWeapon;
    [SerializeField]
    private GameObject _weaponDrop;

    [SerializeField]
    private float reloadTimeMax = 0.5f;
    [SerializeField]
    private float reloadTimeMaxAlt = 1f;
    private float reloadTimeCur = 0;
    private float reloadTimeCurAlt = 0;

    public GameObject altWeapon;
    public int altWeaponAmmo = 100;

    private GameObject shotPosition;
    private PlayerSounds playerSound;
    private Animator canvasAnimator;
    private PlayerMovement playerMovement;
    private GameObject _lastAltInstance = null;
    private int _altWeaponCost;
    private Text ammoCounter;

    // Use this for initialization
    void Start ()
    {
        shotPosition = transform.Find("PlayerSprites/Shot").gameObject;
        playerSound = transform.Find("PlayerSprites").GetComponent<PlayerSounds>();
        canvasAnimator = GameObject.Find("Canvas").GetComponent<Animator>();
        ammoCounter = GameObject.Find("AmmoCounter").GetComponent<Text>();
        playerMovement = GetComponent<PlayerMovement>();
	}
	
	// Update is called once per frame
	void Update () {
        if (playerMovement.playerLives > 0)
        {
            Shooting();
            Reload();
        }
        ammoCounter.text = "" + altWeaponAmmo.ToString("000");
    }

    void Shooting()
    {
        if (altWeapon != null)
            _altWeaponCost = altWeapon.GetComponent<AlternativeWeaponCost>().cost;

        if (Input.GetButtonDown("Fire1") && reloadTimeCur == 0 && mainWeapon != null)
        {
            playerMovement.shoot = true;
            Instantiate(mainWeapon, new Vector2(shotPosition.transform.position.x, shotPosition.transform.position.y), gameObject.transform.rotation);
            reloadTimeCur = reloadTimeMax;
        }

        if (Input.GetButtonDown("Fire2") && reloadTimeCurAlt == 0)
        {
            if (altWeaponAmmo >= _altWeaponCost && _lastAltInstance == null && altWeapon != null)
            {
                altWeaponAmmo -= _altWeaponCost;
                playerMovement.shoot = true;
                _lastAltInstance = Instantiate(altWeapon, new Vector2(shotPosition.transform.position.x, shotPosition.transform.position.y), gameObject.transform.rotation) as GameObject;
                reloadTimeCurAlt = reloadTimeMaxAlt;
            }
            else if (altWeapon == null)
            {
                playerSound.PlaySound(5);
                canvasAnimator.SetTrigger("GetWeapon");
            }
            else if (altWeaponAmmo < _altWeaponCost)
            {
                playerSound.PlaySound(5);
                canvasAnimator.SetTrigger("GetAmmo");
            }
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
        if (coll.gameObject.tag == "WeaponDrop")
        {
            altWeapon = coll.gameObject.GetComponent<WeaponDropController>().weapon;
            Destroy(coll.gameObject);
            playerSound.PlaySound(4);
            canvasAnimator.SetTrigger("GetWeapon");
        }

        if (coll.gameObject.tag == "PlayerAmmoDrop")
        {
            altWeaponAmmo += coll.gameObject.GetComponent<DropController>().amount;
            Destroy(coll.gameObject);
            playerSound.PlaySound(4);
            canvasAnimator.SetTrigger("GetAmmo");
        }
    }
}
