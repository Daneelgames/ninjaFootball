using UnityEngine;
using System.Collections;

public class DestroyOutsideZone : MonoBehaviour {

    [SerializeField]
    private Collider2D playerActiveRoom;
    

    private Collider2D ownerRoom;
    private ActiveRoom playerActiveRoomComponent;

    private bool canDestroy = false;

    void Start()
    {
        playerActiveRoomComponent = GameObject.Find("Player").GetComponent<ActiveRoom>();
    }

    void Update()
    {
        playerActiveRoom = playerActiveRoomComponent.activeRoom.GetComponent<Collider2D>();
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (ownerRoom == null && coll.tag == "Room")
            ownerRoom = coll;
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

        if(coll == ownerRoom)
            Destroy(gameObject);
    }
}
