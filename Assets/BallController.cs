using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour {

	public GameObject playerPref;
	public bool inControl = false;
	public float shootAngle;
	public float shootPower;

	private GameObject playerGO;
	private CircleCollider2D cCollider;
	private GameObject ballCursor;
	private Rigidbody2D rb;
	private bool canSpawn = true;
	private float lastInputX = 1;
	private float lastInputY = 1;

	void Start() {
		ballCursor = GameObject.Find("BallCursor");
		rb = GetComponent<Rigidbody2D> ();
		cCollider = GetComponent<CircleCollider2D> ();
		SpawnPlayer ();
	}
	
	// Update is called once per frame
	void Update () {
		if (inControl) {
			transform.position = new Vector2(playerGO.transform.position.x, playerGO.transform.position.y-0.25f);
			if ((Input.GetAxisRaw("Vertical") != 0) || (Input.GetAxisRaw("Horizontal") != 0) )
				{
					lastInputX = Input.GetAxisRaw("Horizontal");
					lastInputY = Input.GetAxisRaw("Horizontal");
					shootAngle = Mathf.Atan2 (Input.GetAxisRaw ("Vertical"),Input.GetAxisRaw ("Horizontal")) * Mathf.Rad2Deg;
					transform.rotation = Quaternion.Euler(0f, 0f, shootAngle);
				}
			if (Input.GetButtonDown ("Fire1"))
			{
				Shoot();
			}
		}
		else if (canSpawn && Input.anyKeyDown)
		{
			SpawnPlayer();
		}

	}
	
	void SpawnPlayer()
	{
		cCollider.enabled = false;
		Instantiate(playerPref, new Vector2(transform.position.x, transform.position.y+0.25f), Quaternion.identity);
		playerGO = GameObject.FindWithTag("Player");
		canSpawn = false;
		inControl = true;
	}
	
	void Shoot()
	{
		playerGO.GetComponent<PlayerController>().Hide();
		inControl = false;
		cCollider.enabled = true;
		rb.AddForce(new Vector2(lastInputX,lastInputY).normalized * shootPower, ForceMode2D.Impulse);
		canSpawn = true;
	}
}
