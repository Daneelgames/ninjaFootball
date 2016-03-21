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
        Time.timeScale = 1;
        _camera = GameObject.Find("_Camera") as GameObject;
        mainCamera = _camera.GetComponent<Camera>();
    }

    public void Pause()
    {
        StartCoroutine(PauseForTime());
        ShakeExplosion();
    }

    IEnumerator PauseForTime()
    {
        ShakeExplosion();
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
        ShakeExplosion();
        Time.timeScale = deadScale;
        float pauseEndTime = Time.realtimeSinceStartup + deadWait;
        while (Time.realtimeSinceStartup < pauseEndTime)
        {
            yield return 0;
        }
        Time.timeScale = 1;
    }

    public void ShakeShot()

    {
        InvokeRepeating("CameraShake", 0, .01f);
        Invoke("StopShaking", 0.1f);
    }

    public void ShakeExplosion()

    {
        InvokeRepeating("CameraExplosionShake", 0, .01f);
        Invoke("StopExplosionShaking", 0.25f);
    }

    void CameraShake()
    {
        if (shakeAmt > 0)
        {
            originalCameraPosition = mainCamera.transform.position;
            float quakeAmt = Random.Range(-1f, 1f) * shakeAmt/2;
            Vector3 pp = mainCamera.transform.position;
            pp.y += quakeAmt;
            pp.x += quakeAmt;
            mainCamera.transform.position = pp;
        }
    }

    void StopShaking()
    {
        CancelInvoke("CameraShake");
        mainCamera.transform.position = originalCameraPosition;
    }

    void CameraExplosionShake()
    {
        if (shakeAmt > 0)
        {
            originalCameraPosition = mainCamera.transform.position;
            float quakeAmt = Random.Range(-1f, 1f) * shakeAmt * 3;
            Vector3 pp = mainCamera.transform.position;
            pp.y += quakeAmt;
            pp.x += quakeAmt;
            mainCamera.transform.position = pp;
        }
    }

    void StopExplosionShaking()
    {
        CancelInvoke("CameraExplosionShake");
        mainCamera.transform.position = originalCameraPosition;
    }
}
