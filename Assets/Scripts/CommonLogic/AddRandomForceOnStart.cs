using UnityEngine;
using System.Collections;

public class AddRandomForceOnStart : MonoBehaviour {

    private Rigidbody2D rb;
    [SerializeField]
    private float random;

    void Start () {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(Random.Range(-random, random), Random.Range(-random, random)), ForceMode2D.Impulse);
	}
}
