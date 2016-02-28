using UnityEngine;
using System.Collections;

public class BossZoneController : MonoBehaviour {

    private GameObject leftBound;
    private GameObject rightBound;

    void Start()
    {
        leftBound = transform.Find("LeftBound").gameObject;
        rightBound = transform.Find("RightBound").gameObject;

        leftBound.SetActive(false);
        rightBound.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            leftBound.SetActive(true);
            rightBound.SetActive(true);
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            leftBound.SetActive(false);
            rightBound.SetActive(false);
        }
    }
}
