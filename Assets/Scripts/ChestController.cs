using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChestController : MonoBehaviour {

    [SerializeField]
    private GameObject drop;
    [SerializeField]
    private GameObject weapon;

    private bool inTrigger = false;
    private AudioSource _audio;
    private BoxCollider2D _collider;
    private Animator _animator;

    void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _collider = GetComponent<BoxCollider2D>();
        _audio = GetComponent<AudioSource>() as AudioSource;
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

    void Update () {

        if (inTrigger)
        {
            if (Input.GetAxisRaw("Vertical") > 0)
            {
                inTrigger = false;
                OpenChest();
            }
        }

    }

    public void OpenChest(){
        GameObject lastDrop = Instantiate(drop, transform.position, transform.rotation) as GameObject;
        lastDrop.GetComponent<WeaponDropController>().weapon = weapon;
        _collider.enabled = false;
        _audio.Play();
        _animator.SetTrigger("Open");
    }
}