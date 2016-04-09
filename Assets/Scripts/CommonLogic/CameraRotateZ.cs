using UnityEngine;
using System.Collections;

public class CameraRotateZ : MonoBehaviour {
    public float rotationSpeed = 1f;

    float rotateDir = 1.0f;
    float rotationOffset = 0.0f;

    // Use this for initialization
    void Start () {
        StartCoroutine(TurnRight(5.0F));
    }
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(0, 0, rotationOffset);

        rotationOffset = Mathf.Lerp(0, rotateDir, rotationSpeed * Time.deltaTime);
    }
    IEnumerator TurnRight(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        StartCoroutine(TurnLeft(5.0F));
        rotateDir = -1.0f;
    }

    IEnumerator TurnLeft(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        StartCoroutine(TurnRight(5.0F));
        rotateDir = 1.0f;
    }
}
