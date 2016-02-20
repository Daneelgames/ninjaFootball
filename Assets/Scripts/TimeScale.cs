using UnityEngine;
using System.Collections;

public class TimeScale : MonoBehaviour {

    [ReadOnly]
    public GameObject _camera;
    [ReadOnly]
    public Camera mainCamera;

    public float pauseScale;
    public float pauseWait;

    public float deadScale;
    public float deadWait;

    public float shakeAmt = .05f;

    private Vector3 originalCameraPosition;

    void Start()
    {
        _camera = GameObject.Find("_Camera") as GameObject;
        mainCamera = _camera.GetComponent<Camera>();
    }

    public void Pause()
    {
        StartCoroutine(PauseForTime());
    }

    IEnumerator PauseForTime()
    {
        Shake();
        Time.timeScale = pauseScale;
        float pauseEndTime = Time.realtimeSinceStartup + pauseWait;
        while (Time.realtimeSinceStartup < pauseEndTime)
        {
            yield return 0;
        }
        Time.timeScale = 1;
    }

    public void PlayerDead()
    {
        StartCoroutine(DeadForTime());
    }

    IEnumerator DeadForTime()
    {
        Shake();
        Time.timeScale = deadScale;
        float pauseEndTime = Time.realtimeSinceStartup + deadWait;
        while (Time.realtimeSinceStartup < pauseEndTime)
        {
            yield return 0;
        }
        Time.timeScale = 1;
    }

    void Shake()

    {
        InvokeRepeating("CameraShake", 0, .01f);
        Invoke("StopShaking", 0.2f);
    }

    void CameraShake()
    {
        if (shakeAmt > 0)
        {
            originalCameraPosition = mainCamera.transform.position;
            float quakeAmt = Random.value * shakeAmt * 2 - shakeAmt;
            Vector3 pp = mainCamera.transform.position;
            pp.y += quakeAmt; // can also add to x and/or z
            mainCamera.transform.position = pp;
        }
    }

    void StopShaking()
    {
        CancelInvoke("CameraShake");
        mainCamera.transform.position = originalCameraPosition;
    }
}
