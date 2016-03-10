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
    private GameObject explosion;
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private EnemySplathorsMovementController movementController;

    private GameObject spawner; 
    private GameObject door1;
    private GameObject door2;
    private bool canDestroy = true;
    private bool canSlow = true;

    // Use this for initialization
    void Start () {
        door1 = GameObject.Find("SplathorsDoor_1");
        door1.SetActive(false);
        door2 = GameObject.Find("SplathorsDoor_2"); 
        door2.SetActive(false);
        spawner = GameObject.Find("EnemySpawner Splathhors");
    }
	
	// Update is called once per frame
	void Update () {
        //killed
        if (leftPart == null || rightPart == null)
        {
            if (canDestroy)
            {
                canDestroy = false;
                Instantiate(explosion, transform.position, transform.rotation);
                Instantiate(explosion, new Vector3 (transform.position.x + 5f, transform.position.y, transform.position.z), transform.rotation);
                Instantiate(explosion, new Vector3(transform.position.x - 5f, transform.position.y, transform.position.z), transform.rotation);
                Destroy(spawner);
                Destroy(parent);
            }
        }
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (canSlow && other.tag == "Player")
        {
            _animator.SetTrigger("Attack");
            movementController.SlowDown();
            StartCoroutine(SlowWait());
        }
    }

    IEnumerator SlowWait()
    {
        canSlow = false;
        yield return new WaitForSeconds(2);
        canSlow = true;
    }

    void OnDestroy()
    {
        door1.SetActive(true);
        door2.SetActive(true);
    }
}
