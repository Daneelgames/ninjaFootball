using UnityEngine;
using System.Collections;

public class DisableColliderOnTime : MonoBehaviour {

    [SerializeField]
    private float time;
    private Collider2D _collider;

	// Use this for initialization
	void Start () {
        _collider = GetComponent<Collider2D>();

        StartCoroutine(DisableCollider());
	}

    IEnumerator DisableCollider()
    {
        yield return new WaitForSeconds(time);
        _collider.enabled = false;
    }
}
