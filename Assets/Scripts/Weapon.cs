using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

    public GameObject[] weapon;
    public float reloadTimeMax = 0.5f;
    [ReadOnly]
    public GameObject shotPosition;
    [ReadOnly]
    public PlayerSounds playerSound;

    private PlayerMovement playerMovement;
    private float reloadTimeCur = 0;

	// Use this for initialization
	void Start ()
    {
        playerMovement = GetComponent<PlayerMovement>();
	}
	
	// Update is called once per frame
	void Update () {
        if (playerMovement.playerLives > 0)
        {
            Shooting();
            Reload();
        }
	}

    void Shooting()
    {
        if (Input.GetButtonDown("Fire1") && reloadTimeCur == 0)
        {
            playerMovement.shoot = true;
            Instantiate(weapon[Random.Range(0, weapon.Length)], new Vector2(shotPosition.transform.position.x, shotPosition.transform.position.y), gameObject.transform.rotation);
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
}
