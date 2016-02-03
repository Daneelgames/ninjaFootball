using UnityEngine;
using System.Collections;

public class BallCursorController : MonoBehaviour
{

    public float radius = 2;
    public float angle = 0;

    public float targetAngle = 0;


    public float speed = 10;
    float velocity = 0;
    GameObject ball;

    void Start()
    {
        ball = GameObject.Find("Ball");
    }

    // Update is called once per frame 
    void Update()
    {

        if (Input.anyKey)
        {
            targetAngle = Mathf.Atan2(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"));

            var shortest = Mathf.DeltaAngle(angle * Mathf.Rad2Deg, targetAngle * Mathf.Rad2Deg) * Mathf.Deg2Rad;
            targetAngle = angle + shortest;
        }


        angle = Mathf.SmoothDampAngle(angle, targetAngle, ref velocity, 0.01f, 20f);

        //transform.position = new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 0);
        transform.position = new Vector3(ball.transform.position.x * radius, ball.transform.position.y * radius, 0);
    }
}