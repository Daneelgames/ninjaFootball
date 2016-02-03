using UnityEngine;
using System.Collections;

public class BallCursorController : MonoBehaviour
{

    public float radius = 2;
    public float angle = 0;
    public float targetAngle = 0;
    public float speed = 10;
	
    float velocity = 0;

    void Update()
    {

		if ((Input.GetAxisRaw("Vertical") != 0) || (Input.GetAxisRaw("Horizontal") != 0) )
        {
            targetAngle = Mathf.Atan2(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"));

            var shortest = Mathf.DeltaAngle(angle * Mathf.Rad2Deg, targetAngle * Mathf.Rad2Deg) * Mathf.Deg2Rad;
            targetAngle = angle + shortest;
        }


		angle = Mathf.SmoothDampAngle(angle, targetAngle, ref velocity, Mathf.Abs(angle - targetAngle)*0.05f);

        transform.localPosition = new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 0);
    }
}