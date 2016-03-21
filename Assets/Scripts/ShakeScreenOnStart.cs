using UnityEngine;
using System.Collections;

public class ShakeScreenOnStart : MonoBehaviour {

    [SerializeField]
    private AudioSource _audio;

    private TimeScale timeScaleScript;
    private float distance;

    // Use this for initialization
    void Start ()
    {
        distance = Vector3.Distance(GameObject.Find("Player").transform.position, transform.position);
        if (distance < 10)
        {
            _audio.Play();
            timeScaleScript = GameObject.Find("Player").GetComponent<TimeScale>();
            timeScaleScript.Pause();
        }
    }
}
