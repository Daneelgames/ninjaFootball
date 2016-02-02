using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour {

	public bool inControl = false;
	public GameObject playerPref;

	private GameObject playerGO;
	private CircleCollider2D cCollider;

	void Start() {
		cCollider = GetComponent<CircleCollider2D> ();
		SpawnPlayer ();
	}
	
	// Update is called once per frame
	void Update () {

		if (inControl) {
			if (playerGO != null){
				transform.position = new Vector2(playerGO.transform.position.x, playerGO.transform.position.y-0.25f);
			}
		}

	}

	void SpawnPlayer()
	{
		cCollider.enabled = false;
		Instantiate(playerPref, new Vector2(transform.position.x, transform.position.y+0.25f), Quaternion.identity);
		playerGO = GameObject.FindWithTag("Player");
		inControl = true;
	}
}
