using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

    public int maxBullets = 3;
    public float reloadTimeMax = 0.5f;
    [ReadOnly]
    public int bulletCount;
    [ReadOnly]
    public GameObject bullet;
    [ReadOnly]
    public GameObject shotPosition;
    [ReadOnly]
    public PlayerSounds playerSound;

    private PlayerMovement playerMovement;
    private float reloadTimeCur = 0;

	// Use this for initialization
	void Start () {
        playerMovement = GetComponent<PlayerMovement>();
        bulletCount = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (playerMovement.playerLives > 0)
        {
            Shooting();
            Reload();
        }

        // reset bullets
        if (bulletCount < 0)
            bulletCount = 0;
	}

    void Shooting()
    {
        if (Input.GetButtonDown("Fire1") && bulletCount < maxBullets && reloadTimeCur == 0)
        {
            playerMovement.shoot = true;
            var tBullet = Instantiate(bullet, new Vector2(shotPosition.transform.position.x, shotPosition.transform.position.y), gameObject.transform.rotation) as GameObject;
            tBullet.GetComponent<Bullet>().bulletDirection = playerMovement.PlayerDirection;
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
