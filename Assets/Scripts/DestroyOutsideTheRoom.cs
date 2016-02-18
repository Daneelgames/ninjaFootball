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

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Room")
            objectRoom = coll;
    }

    void Update()
    {
        playerActiveRoom = player.GetComponent<ActiveRoom>().activeRoom.GetComponent<Collider2D>();

    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.tag == "Zone" && objectRoom != playerActiveRoom)
        {
            Destroy(gameObject);
            print(gameObject.name);
        }
    }
}
