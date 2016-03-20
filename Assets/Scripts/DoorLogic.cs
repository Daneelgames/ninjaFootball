using UnityEngine;
using System.Collections;

public class DoorLogic : MonoBehaviour {

    [SerializeField]
    private Transform destination;
    [SerializeField]
    private AudioSource _audio;

    private Animator canvasAnimator;
    private bool inTrigger = false;
    private bool isInDialog = false;
    private GameObject player;
    private PlayerMovement playerScript;


    // Use this for initialization
    void Start () {
        canvasAnimator = GameObject.Find("Canvas").GetComponent<Animator>();
        player = GameObject.Find("Player");
        playerScript = player.GetComponent<PlayerMovement>() as PlayerMovement;
    }
	
	// Update is called once per frame
	void Update () {

        if (inTrigger && playerScript.playerLives <= 0)
            inTrigger = false;

        if (inTrigger)
        {
            if (!isInDialog && Input.GetButtonDown("Submit") && playerScript.isOnGround)
            {
                isInDialog = true;
                StartCoroutine(Teleport());
            }
        }
    }

    IEnumerator Teleport()
    {
        canvasAnimator.SetTrigger("Dead");
        Physics2D.IgnoreLayerCollision(10, 12, ignore: true);
        Physics2D.IgnoreLayerCollision(10, 11, ignore: true);
        Physics2D.IgnoreLayerCollision(10, 16, ignore: true);
        Physics2D.IgnoreLayerCollision(10, 17, ignore: true);
        yield return new WaitForSeconds(.25F);
        _audio.Play();
        _audio.pitch = Random.Range(0.75f, 1.25f);
        player.transform.position = destination.transform.position;
        isInDialog = false;
        yield return new WaitForSeconds(.75F);
        Physics2D.IgnoreLayerCollision(10, 12, ignore: false);
        Physics2D.IgnoreLayerCollision(10, 11, ignore: false);
        Physics2D.IgnoreLayerCollision(10, 16, ignore: false);
        Physics2D.IgnoreLayerCollision(10, 17, ignore: false);

    }
    
    void OnTriggerEnter2D(Collider2D player)
    {
        if (player.gameObject.tag == "Player")
        {
            inTrigger = true;
        }
    }

    void OnTriggerExit2D(Collider2D player)
    {
        if (player.gameObject.tag == "Player")
        {
            inTrigger = false;
        }
    }
}
