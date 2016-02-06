using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

    public GameObject bullet;
    public int maxBullets = 3;
    public float reloadTimeMax = 0.5f;
    [HideInInspector] public int bulletCount;

    private PlayerMovement playerMovement;
    private float reloadTimeCur = 0;
    private float height;

	// Use this for initialization
	void Start () {
        playerMovement = GetComponent<PlayerMovement>();
        height = gameObject.transform.localScale.y;
	}
	
	// Update is called once per frame
	void Update () {
        Shooting();
        Reload();
	}

    void Shooting()
    {
        if (Input.GetButtonDown("Fire1") && bulletCount < maxBullets && reloadTimeCur == 0)
        {
            var tBullet = Instantiate(bullet, new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + height / 2), gameObject.transform.rotation) as GameObject;
            tBullet.GetComponent<Bullet>().bulletDirection = playerMovement.PlayerDirection;
            reloadTimeCur = reloadTimeMax;
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
