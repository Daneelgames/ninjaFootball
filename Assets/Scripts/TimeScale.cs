using UnityEngine;
using System.Collections;

public class TimeScale : MonoBehaviour {

    [ReadOnly]
    public GameObject _camera;
    [ReadOnly]
    public bool shake = false;

    public float pauseScale = 0.1f;
    public float pauseWait = 0.075f;

    public float deadScale = .2f;
    public float deadWait = 1f;


    void Start()
    {
        _camera = GameObject.Find("Camera") as GameObject;
    }

    void Update()
    {
    }


    public void Pause()
    {
        StartCoroutine(PauseForTime());
    }

    IEnumerator PauseForTime()
    {
        Time.timeScale = pauseScale;
        shake = true;
        float pauseEndTime = Time.realtimeSinceStartup + pauseWait;
        while (Time.realtimeSinceStartup < pauseEndTime)
        {
            yield return 0;
        }
        shake = false;
        Time.timeScale = 1;
    }

    public void PlayerDead()
    {
        StartCoroutine(DeadForTime());
    }

    IEnumerator DeadForTime()
    {
        Time.timeScale = deadScale;
        shake = true;
        float pauseEndTime = Time.realtimeSinceStartup + deadWait;
        while (Time.realtimeSinceStartup < pauseEndTime)
        {
            yield return 0;
        }
        Time.timeScale = 1;
        shake = false;
    }

}
