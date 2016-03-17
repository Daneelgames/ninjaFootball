using UnityEngine;
using System.Collections;

public class RoomBuilder : MonoBehaviour {

    [SerializeField]
    private GameObject[] upPath;
    [SerializeField]
    private GameObject[] upWall;
    [SerializeField]
    private GameObject[] rightPath;
    [SerializeField]
    private GameObject[] rightWall;
    [SerializeField]
    private GameObject[] downPath;
    [SerializeField]
    private GameObject[] downWall;
    [SerializeField]
    private GameObject[] leftPath;
    [SerializeField]
    private GameObject[] leftWall;


    private float roomWidth = 11f;
    private float roomHeigth = 6f;

    private PathDiggerLogic digger;
    private bool canRaycast = false;
    private bool rayCasted = false;

    private bool pathUp = false;
    private bool pathRight = false;
    private bool pathDown = false;
    private bool pathLeft = false;
    
    private bool upCast = false;
    private bool rightCast = false;
    private bool downCast = false;
    private bool leftCast = false;

    private bool uBuilded = false;
    private bool rBuilded = false;
    private bool dBuilded = false;
    private bool lBuilded = false;
    
    private bool roomBuilded = false;

    void Start () {
        digger = GameObject.Find("PathDigger").GetComponent<PathDiggerLogic>();
	}
	
	void Update () {

        if (!roomBuilded)
        {
            if (uBuilded && rBuilded && dBuilded && lBuilded)
            {
                digger.roomBuilded += 1;
                roomBuilded = true;
            } 
        }

        if (!canRaycast && digger.roomsFinished)
            canRaycast = true;

        if (canRaycast && upCast && rightCast && downCast && leftCast) 
        {
            canRaycast = false;
            rayCasted = true;
        }

        if (rayCasted)
            BuildRoom();
	}

    void FixedUpdate()
    {
        if (canRaycast)
        {
            RaycastHit2D hitUp = Physics2D.Raycast(transform.position, Vector2.up, roomHeigth, 1 << 19);
            RaycastHit2D hitRight = Physics2D.Raycast(transform.position, Vector2.right, roomWidth, 1 << 19);
            RaycastHit2D hitDown = Physics2D.Raycast(transform.position, Vector2.down, roomHeigth, 1 << 19);
            RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, Vector2.left, roomWidth, 1 << 19);
            
            if (!upCast)
            {
                if (hitUp.collider != null && hitUp.collider.tag == "RoomBuilder")
                {
                    pathUp = true;
                    upCast = true;
                }
                else
                    upCast = true;
            }

            if (!rightCast)
            {
                if (hitRight.collider != null && hitRight.collider.tag == "RoomBuilder")
                {
                    pathRight = true;
                    rightCast = true;
                }
                else
                    rightCast = true;
            }

            if (!downCast)
            {
                if (hitDown.collider != null && hitDown.collider.tag == "RoomBuilder")
                {
                    pathDown = true;
                    downCast = true;
                }
                else
                    downCast = true;
            }

            if (!leftCast)
            {
                if (hitLeft.collider != null && hitLeft.collider.tag == "RoomBuilder")
                {
                    pathLeft = true;
                    leftCast = true;
                }
                else
                    leftCast = true;
            }
        }
    }

    void BuildRoom()
    {
        if (!uBuilded)
        {
            if (pathUp)
            {
                Instantiate(upPath[Random.Range(0,upPath.Length)], transform.position, transform.rotation);
                uBuilded = true;
            }
            else
            {
                Instantiate(upWall[Random.Range(0, upWall.Length)], transform.position, transform.rotation);
                uBuilded = true;
            }
        }

        if (!rBuilded)
        {
            if (pathRight)
            {
                Instantiate(rightPath[Random.Range(0, rightPath.Length)], transform.position, transform.rotation);
                rBuilded = true;
            }
            else
            {
                Instantiate(rightWall[Random.Range(0, rightWall.Length)], transform.position, transform.rotation);
                rBuilded = true;
            }
        }

        if(!dBuilded)
        {
            if (pathDown)
            {
                Instantiate(downPath[Random.Range(0, downPath.Length)], transform.position, transform.rotation);
                dBuilded = true;
            }
            else
            {
                Instantiate(downWall[Random.Range(0, downWall.Length)], transform.position, transform.rotation);
                dBuilded = true;
            }
        }

        if (!lBuilded)
        {
            if (pathLeft)
            {
                Instantiate(leftPath[Random.Range(0, leftPath.Length)], transform.position, transform.rotation);
                lBuilded = true;
            }
            else
            {
                Instantiate(leftWall[Random.Range(0, leftWall.Length)], transform.position, transform.rotation);
                lBuilded = true;
            }
        }
    }
}
