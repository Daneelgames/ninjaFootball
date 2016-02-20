using UnityEngine;
using System.Collections;

public class DestroyOutsideTheRoom : MonoBehaviour {

    [ReadOnly]
    public Collider2D playerActiveRoom;
    public Collider2D objectRoom;

    private GameObject player;

    void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        playerActiveRoom = player.GetComponent<ActiveRoom>().activeRoom.GetComponent<Collider2D>();

    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Room")
            objectRoom = coll;
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.tag == "Zone" && coll == playerActiveRoom)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.tag == "Room" && coll != playerActiveRoom)
            Destroy(gameObject);
    }
}
