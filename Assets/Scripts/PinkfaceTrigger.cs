using UnityEngine;
using System.Collections;

public class PinkfaceTrigger : MonoBehaviour {

    [SerializeField]
    private GameObject sprites;

    void OnTriggerEnter2D (Collider2D coll)
    {
        if (coll.tag == "Player")
        {
            sprites.SetActive(true);
            Destroy(gameObject, 1.3f);
        }
    }

}
