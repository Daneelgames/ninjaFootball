using UnityEngine;
using System.Collections;

public class DestroyOnHazard : MonoBehaviour {

    [ReadOnly]
    public Transform explosion;

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Hazard")
        {
            Destroy(gameObject);
            Instantiate(explosion, new Vector3(transform.position.x, transform.position.y, 0), transform.rotation);
        }
    }

}
