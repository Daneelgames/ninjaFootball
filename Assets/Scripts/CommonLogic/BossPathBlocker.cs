using UnityEngine;
using System.Collections;

public class BossPathBlocker : MonoBehaviour {

    private GameObject _Blocker;

	// Use this for initialization
	void Start () {
        _Blocker = transform.Find("Blocker").gameObject;

        _Blocker.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            _Blocker.SetActive(true);
        }
    }

}
