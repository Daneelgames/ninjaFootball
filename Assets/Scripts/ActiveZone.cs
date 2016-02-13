using UnityEngine;
using System.Collections;

public class ActiveZone : MonoBehaviour {

    [ReadOnly]
    public GameObject _camera;
    [ReadOnly]
    public GameObject activeZone = null;

    private TimeScale timeScaleScript;

    void Start()
    {
        _camera = GameObject.Find("Camera") as GameObject;
        timeScaleScript = GetComponent<TimeScale>();
    }

    void Update()
    {
        if (timeScaleScript.shake != true)
            _camera.transform.position = new Vector3(activeZone.transform.position.x, activeZone.transform.position.y, -1);
        else
            _camera.transform.position = new Vector3(activeZone.transform.position.x + Random.Range(-.05f, .05f), activeZone.transform.position.y + Random.Range(-.05f, .05f), _camera.transform.position.z);
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
