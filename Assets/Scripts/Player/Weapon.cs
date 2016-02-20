using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Weapon : MonoBehaviour {

    public GameObject mainWeapon;
    public GameObject altWeapon;
    public float reloadTimeMax = 0.5f;
    [ReadOnly]
    public GameObject shotPosition;
    [ReadOnly]
    public PlayerSounds playerSound;
    [ReadOnly]
    public int altWeaponAmmo = 100;

    private Text ammoCounter;
    private PlayerMovement playerMovement;
    private float reloadTimeCur = 0;
    private GameObject _lastAltInstance = null;

	// Use this for initialization
	void Start ()
    {
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
        int _altWeaponCost = altWeapon.GetComponent<AlternativeWeaponCost>().cost;
        if (Input.GetButtonDown("Fire1") && reloadTimeCur == 0 && mainWeapon != null)
        {
            playerMovement.shoot = true;
            Instantiate(mainWeapon, new Vector2(shotPosition.transform.position.x, shotPosition.transform.position.y), gameObject.transform.rotation);
            reloadTimeCur = reloadTimeMax;
            playerSound.PlaySound(1);
        }

        if (Input.GetButtonDown("Fire2") && reloadTimeCur == 0 && altWeapon != null && altWeaponAmmo >= _altWeaponCost && _lastAltInstance == null)
        {
            altWeaponAmmo -= _altWeaponCost;
            playerMovement.shoot = true;
            _lastAltInstance = Instantiate(altWeapon, new Vector2(shotPosition.transform.position.x, shotPosition.transform.position.y), gameObject.transform.rotation) as GameObject;
            reloadTimeCur = reloadTimeMax;
            playerSound.PlaySound(1);
        }
    }

    void Reload()
    {
        if (reloadTimeCur > 0)
            reloadTimeCur -= 5f * Time.deltaTime;
        else if (reloadTimeCur < 0)
            reloadTimeCur = 0;

    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "AmmoDrop")
        {
            altWeaponAmmo += 1;
            Destroy(coll.gameObject);
            playerSound.PlaySound(4);
        }
        if (coll.gameObject.tag == "PlayerAmmoDrop")
        {
            altWeaponAmmo += coll.gameObject.GetComponent<PlayerDropController>().amount;
            Destroy(coll.gameObject);
            playerSound.PlaySound(4);
        }
    }
}
