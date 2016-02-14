using UnityEngine;
using System.Collections;

public class ActiveRoom : MonoBehaviour {

    [ReadOnly]
    public GameObject activeRoom;
    [ReadOnly]
    public ScrollController cameraController;

    void Start()
    {
        cameraController = GameObject.Find("_Camera").GetComponent<ScrollController>();
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Room")
        {
            activeRoom = coll.gameObject;
            cameraController.SetActiveRoom();
        }
    }

    void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.tag == "Room" && activeRoom != coll.gameObject)
        {
            activeRoom = coll.gameObject;
            cameraController.SetActiveRoom();
        }
    }

}
