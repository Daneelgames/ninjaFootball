using UnityEngine;
using System.Collections;

public class EnemySplathorsMovementController : MonoBehaviour {
    [SerializeField]
    private float amplitude = 1.5f;
    [SerializeField]
    private float attackTime = 1.5f;
    [SerializeField]
    private float speed = 4f;

    private float startPosY;

	void Start () {
        startPosY = transform.position.y;
	}
	

	void Update () {
        transform.Translate(new Vector3(0, speed * Time.deltaTime, 0));

        if (transform.position.y > startPosY + amplitude && speed > 0)
            speed *= -1;
        else if (transform.position.y < startPosY - amplitude && speed < 0)
            speed *= -1;
    }

    public void SlowDown()
    {
            StartCoroutine(SlowSpeed(attackTime));
    }

    IEnumerator SlowSpeed(float waitTime)
    {
        speed /= 2;
        yield return new WaitForSeconds(attackTime);
        speed *= 2;
    }
}
