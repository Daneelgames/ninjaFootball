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

    private TimeScale timeScaleScript;
    private PlayerMovement playerMovement;
    private float reloadTimeCur = 0;

	// Use this for initialization
	void Start ()
    {
        timeScaleScript = GetComponent<TimeScale>();
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
            timeScaleScript.Pause();
            playerMovement.shoot = true;
            var tBullet1 = Instantiate(bullet, new Vector2(shotPosition.transform.position.x, shotPosition.transform.position.y + 0.25f), gameObject.transform.rotation) as GameObject;
            var tBullet2 = Instantiate(bullet, new Vector2(shotPosition.transform.position.x, shotPosition.transform.position.y - 0.25f), gameObject.transform.rotation) as GameObject;
            tBullet1.GetComponent<Bullet>().bulletDirection = playerMovement.PlayerDirection;
            tBullet2.GetComponent<Bullet>().bulletDirection = playerMovement.PlayerDirection;
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
