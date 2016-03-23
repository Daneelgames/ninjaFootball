using UnityEngine;
using System.Collections;

public class DoorNextLevelLogic : MonoBehaviour {

    [SerializeField]
    private int level;
    [SerializeField]
    private AudioClip sound;

    private AudioSource _audio;
    private bool inTrigger = false;
    private bool isInDialog = false;
    private GameObject player;
    private PlayerMovement playerScript;
    private GameManager sceneManager;


    // Use this for initialization
    void Start () {
        _audio = GameObject.Find("SceneManager").GetComponent<AudioSource>();
        sceneManager = GameObject.Find("SceneManager").GetComponent<GameManager>();
        player = GameObject.Find("Player");
        playerScript = player.GetComponent<PlayerMovement>() as PlayerMovement;
    }
	
	// Update is called once per frame
	void Update () {

        if (inTrigger && playerScript.playerLives <= 0)
            inTrigger = false;

        if (inTrigger)
        {
            if (!isInDialog && Input.GetAxisRaw("Vertical") > 0 && playerScript.isOnGround)
            {
                isInDialog = true;
                StartCoroutine(Teleport());
            }
        }
    }

    IEnumerator Teleport()
    {
        Physics2D.IgnoreLayerCollision(10, 12, ignore: true);
        Physics2D.IgnoreLayerCollision(10, 11, ignore: true);
        Physics2D.IgnoreLayerCollision(10, 16, ignore: true);
        Physics2D.IgnoreLayerCollision(10, 17, ignore: true);
        yield return new WaitForSeconds(.1F);
        _audio.PlayOneShot(sound);
        _audio.Play();
        _audio.pitch = Random.Range(0.75f, 1.25f);
        isInDialog = false;

        sceneManager.ChangeScene(level);

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
