using UnityEngine;
using System.Collections;

public class AddForceToColliderRB : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other)
    {
        Rigidbody2D _rb = other.gameObject.GetComponent<Rigidbody2D>() as Rigidbody2D;
        if (_rb != null)
            _rb.AddForce(new Vector2(0, 10), ForceMode2D.Force);
    }
}
