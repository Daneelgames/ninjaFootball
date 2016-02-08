using UnityEngine;
using System.Collections;

public class ActiveZone : MonoBehaviour {

    [ReadOnly]
    public GameObject _camera;
    [ReadOnly]
    public GameObject activeZone = null;

    void Start()
    {
        _camera = GameObject.Find("Camera") as GameObject;
    }

    void Update()
    {
        _camera.transform.position = new Vector3(activeZone.transform.position.x, activeZone.transform.position.y, -1);
    }

    void OnTriggerStay2D(Collider2D zone)
    {
        if (zone.tag == "Zone")
        {
            //Set this zone to Active Zone
            activeZone = zone.gameObject;
        }
    }
}
