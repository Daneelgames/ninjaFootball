using UnityEngine;
using System.Collections;

public class DestroyOutsideZone : MonoBehaviour {


    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.tag == "Zone")
            Destroy(gameObject);
    }
}
