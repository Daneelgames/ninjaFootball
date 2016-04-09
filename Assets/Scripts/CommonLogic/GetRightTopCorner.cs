using UnityEngine;
using System.Collections;

public class GetRightTopCorner : MonoBehaviour {

    [ReadOnly]
    public Vector2 rightCornerPosition;
    
    private GameObject rightCorner;

    void Start()
    {
        rightCorner = gameObject.transform.Find("RightBottomCorner").gameObject;
        rightCornerPosition = rightCorner.transform.position;
    }

}
