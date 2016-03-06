using UnityEngine;
using System.Collections;

public class OrginMuderCutScene : MonoBehaviour {

    [SerializeField]
    private AudioClip[] clipList;

    [SerializeField]
    private OrginMuderMovement orginMuder;
    [SerializeField]
    private CircleCollider2D healthCollider;
    [SerializeField]
    private Animator muderAnimator;


    private PlayerMovement playerScript;
    private Animator _animator;

    void Start () {
        healthCollider.enabled = false;
        _animator = GetComponent<Animator>();
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>() as PlayerMovement;

    }

    void OnTriggerEnter2D (Collider2D coll)
    {
        if (coll.tag == "Player")
        {
            GetComponent<Collider2D>().enabled = false;
            playerScript.DialogStart();
            StartCoroutine(CutScene());
        }
    }

    IEnumerator CutScene()
    {
        muderAnimator.SetTrigger("Awake");
        AudioSource.PlayClipAtPoint(clipList[0], transform.position);
        _animator.SetTrigger("Action");
        yield return new WaitForSeconds(3.5f);
        AudioSource.PlayClipAtPoint(clipList[1], transform.position);
        yield return new WaitForSeconds(0.5f);
        orginMuder.inBattle = true;
        playerScript.DialogOver();
        healthCollider.enabled = true;
        Destroy(gameObject);
    }

}
