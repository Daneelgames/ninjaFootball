using UnityEngine;
using System.Collections;

public class ShakeScreenOnStart : MonoBehaviour {

    private TimeScale timeScaleScript;

    // Use this for initialization
    void Start ()
    {
        timeScaleScript = GameObject.Find("Player").GetComponent<TimeScale>();
        timeScaleScript.Pause();

    }
}
