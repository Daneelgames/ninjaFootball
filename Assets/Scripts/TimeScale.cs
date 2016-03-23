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

    public void Shoot()
    {
        ShootShake();
    }
    
    public void PlayerDead()
    {
        StartCoroutine(DeadForTime());
    }

    IEnumerator DeadForTime()
    {
        ExplosionShake();
        Time.timeScale = deadScale;
        float pauseEndTime = Time.realtimeSinceStartup + deadWait;
        while (Time.realtimeSinceStartup < pauseEndTime)
        {
            yield return 0;
        }
        Time.timeScale = 1;
    }

    public void ExplosionShake()

    {
        InvokeRepeating("CameraExplosionShake", 0, .01f);
        Invoke("StopExplosionShaking", 0.2f);
    }

    void CameraExplosionShake()
    {
        if (shakeAmt > 0)
        {
            originalCameraPosition = mainCamera.transform.position;
            float quakeAmt = Random.Range(-1, 1) * shakeAmt * 2;
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

    void ShootShake()

    {
        InvokeRepeating("CameraShootShake", 0, .01f);
        Invoke("StopShootShaking", 0.1f);
    }
    void CameraShootShake()
    {
        if (shakeAmt > 0)
        {
            originalCameraPosition = mainCamera.transform.position;
            float quakeAmt = Random.Range(-1, 1) * shakeAmt;
            Vector3 pp = mainCamera.transform.position;
            pp.y += quakeAmt;
            pp.x += quakeAmt;
            mainCamera.transform.position = pp;
        }
    }

    void StopShootShaking()
    {
        CancelInvoke("CameraShootShake");
        mainCamera.transform.position = originalCameraPosition;
    }
}
