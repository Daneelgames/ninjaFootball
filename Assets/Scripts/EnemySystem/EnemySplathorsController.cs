using UnityEngine;
using System.Collections;

public class EnemySplathorsController : MonoBehaviour {

    [SerializeField]
    private GameObject parent;
    [SerializeField]
    private GameObject leftPart;
    [SerializeField]
    private GameObject rightPart;
    [SerializeField]
    private Animator _animator;

    private GameObject door1;
    private GameObject door2;

    // Use this for initialization
    void Start () {
        door1 = GameObject.Find("SplathorsDoor_1");
        door1.SetActive(false);
        door2 = GameObject.Find("SplathorsDoor_2"); 
        door2.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        if (leftPart == null || rightPart == null)
        {
            Destroy(parent);
            door1.SetActive(true);
            door2.SetActive(true);
        }
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            _animator.SetTrigger("Attack");
        }
    }
}
