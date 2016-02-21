using UnityEngine;
using System.Collections;

public class DropController : MonoBehaviour {

    public int amount;

    private PlayerMovement pm;

    void Start()
    {
        pm = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (amount == 0)
            Destroy(gameObject);

        if (pm.playerLives <= 0)
            Destroy(gameObject);
    }

}
