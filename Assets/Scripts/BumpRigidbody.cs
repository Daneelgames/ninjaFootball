using UnityEngine;
using System.Collections;

public class BumpRigidbody : MonoBehaviour {

    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private AudioSource _audio;

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Player" || coll.tag == "Enemy")
        {
            Rigidbody2D rigid = coll.GetComponent<Rigidbody2D>() as Rigidbody2D;
            rigid.AddForce(Vector2.up * 1000, ForceMode2D.Force);
            _animator.SetTrigger("Action");
            _audio.Play();
            _audio.pitch = Random.Range(.75f, 1.25f);
        }
    }
}
