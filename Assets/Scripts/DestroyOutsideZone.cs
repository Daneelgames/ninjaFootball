using UnityEngine;
using System.Collections;

public class DestroyOutsideZone : MonoBehaviour {

    [SerializeField]
    private Collider2D playerActiveRoom;

    private GameObject player;

    private bool canDestroy = false;

    void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        playerActiveRoom = player.GetComponent<ActiveRoom>().activeRoom.GetComponent<Collider2D>();
    }


    void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.tag == "Room" && coll != playerActiveRoom && canDestroy)
            Destroy(gameObject);
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.tag == "Zone")
        {
            canDestroy = true;
        }
    }
}
